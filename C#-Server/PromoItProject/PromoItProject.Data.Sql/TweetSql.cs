using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class TweetSql: BaseDataSql
    {
        public TweetSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        public Tweet GetTheLastUpdatedTweet()
        {
            Tweet tweet = new Tweet();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetTheLastUpdatedTweet", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the values for the properties of the Tweet object from the SQL Stored Procedure
                                tweet.TwitterID = reader.GetInt32(reader.GetOrdinal("TwitterID"));
                                tweet.TwitterUserName = reader.GetString(reader.GetOrdinal("TwitterUserName"));
                                tweet.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                tweet.TweetID = reader.GetString(reader.GetOrdinal("TweetID"));
                                tweet.TweetDate = reader.GetDateTime(reader.GetOrdinal("TweetDate"));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }

            return tweet;
        }

        public void InsertTweetToDB(string twitterUserName, string hashtag, string tweetID, DateTime tweetDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertTweetToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@twitterUserName", twitterUserName);
                        command.Parameters.AddWithValue("@hashtag", hashtag);
                        command.Parameters.AddWithValue("@tweetID", tweetID);
                        command.Parameters.AddWithValue("@tweetDate", tweetDate);

                        //Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
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
