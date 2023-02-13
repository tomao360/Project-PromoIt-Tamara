using Newtonsoft.Json;
using PromoItProject.Data.Sql;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.DTO;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Utilities;

namespace PromoItProject.CommunicationProviders
{
    public class Twitter : BaseCommunicationProvider
    {
        public Twitter(Logger log) : base(log)
        {
            Task.Run(GetTweetsData);
        }

        private bool stopLoop = false;


        private static ConsumerOnlyCredentials appCredentials = new ConsumerOnlyCredentials(Environment.GetEnvironmentVariable("TwitterConsumerKey"), Environment.GetEnvironmentVariable("TwitterConsumerSecret"))
        {
            BearerToken = Environment.GetEnvironmentVariable("TwitterBearerToken")
        };
        private TwitterClient twitterClient = new TwitterClient(appCredentials);

        async void GetTweetsData()
        {
            try
            {
                // Continuously run this loop until the stop is set to true
                while (!stopLoop)
                {
                    // Get all the active campaigns from the database
                    Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                    Dictionary<int, ActiveCampaign> activeCampaignsDic = (Dictionary<int, ActiveCampaign>)activeCampaignSql.LoadActiveCampaigns();

                    // Get the last updated tweet from the database
                    TweetSql tweetSql = new TweetSql(base.Log);
                    Model.Tweet tweet = tweetSql.GetTheLastUpdatedTweet();

                    // Loop through each active campaign
                    foreach (var activeCampaign in activeCampaignsDic)
                    {
                        // Get the tweets for a specific campaign based on the hashtag and user name
                        var tweets = await twitterClient.Execute.RequestAsync(request =>
                        {
                            string hashtag = activeCampaign.Value.Hashtag.Substring(1);
                            // Get the start date for the tweet search
                            string startDate = tweet.TweetDate.AddHours(-2).AddSeconds(1).ToString("yyyy-MM-ddTHH:mm:ssZ");
                            // If the start date is higher than 7 days, then change it to 7 days ago from today
                            if (tweet.TweetDate.AddDays(7) < DateTime.Now)
                            {
                                startDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTHH:mm:ssZ");
                            }

                            if (tweet.TweetID == null)
                            {
                                request.Url = $"https://api.twitter.com/2/tweets/search/recent?query=%23{hashtag} from:{activeCampaign.Value.TwitterUserName}&tweet.fields=created_at";
                            }
                            else
                            {
                                request.Url = $"https://api.twitter.com/2/tweets/search/recent?query=%23{hashtag} from:{activeCampaign.Value.TwitterUserName}&start_time={startDate}&tweet.fields=created_at";
                            }

                            request.HttpMethod = HttpMethod.GET;
                        });

                        var jsonResponse = tweets.Content;
                        var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                        int resultCount = data.meta.result_count;

                        // A list to contain the tweets
                        List<Model.Tweet> tweetList = new List<Model.Tweet>();
                        if (resultCount > 0)
                        {
                            // Update the campaign with the money added
                            activeCampaignSql.UpdateActiveCampaignAddMoneyByID(activeCampaign.Value.ActiveCampID, resultCount, resultCount);

                            // Add the tweets to the list
                            foreach (var tweet1 in data.data)
                            {
                                DateTime tweetDate = tweet1.created_at;
                                tweetDate = tweetDate.AddHours(2);

                                tweetList.Add(new Model.Tweet
                                {
                                    TweetID = tweet1.id,
                                    TweetDate = tweetDate,
                                    TwitterUserName = activeCampaign.Value.TwitterUserName,
                                    Hashtag = activeCampaign.Value.Hashtag,

                                });
                            }

                            // Sort the tweets by the tweet date in ascending order
                            tweetList.Sort((a, b) => DateTime.Compare(a.TweetDate, b.TweetDate));

                            // Insert the tweets into the database
                            foreach (var tweet2 in tweetList)
                            {
                                tweetSql.InsertTweetToDB(tweet2.TwitterUserName, tweet2.Hashtag, tweet2.TweetID, tweet2.TweetDate);
                            }
                        }
                    }

                    // Sleep for 1 hour 
                    Thread.Sleep(1000 * 60 * 60);
                }
            }
            catch (TwitterException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public async Task<object> PostTweet(string userName, string campaignName)
        {
            string consumerKae = Environment.GetEnvironmentVariable("TwitterConsumerKey");
            string consumerSecret = Environment.GetEnvironmentVariable("TwitterConsumerSecret");
            string accessToken = Environment.GetEnvironmentVariable("TwitterAccessToken");
            string accessSecret = Environment.GetEnvironmentVariable("TwitterAccessSecret");

            try
            {
                var userClient = new TwitterClient(consumerKae, consumerSecret, accessToken, accessSecret);

                // Publish a tweet
                var tweet = await userClient.Tweets.PublishTweetAsync($"@{userName} bought a product from the {campaignName} campaign!");
                return tweet;
            }
            catch (TwitterException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

    }
}
