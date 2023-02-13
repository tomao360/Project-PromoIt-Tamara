using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoItProject.CommunicationProviders;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Twitter
{
    public class TwitterAddCmd: BaseCommand, ICommand
    {
        public TwitterAddCmd(Logger log) : base(log) { }
        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    
                    var tweet = MainManager.Instance.twitter.PostTweet((string)param[0], (string)param[1]);
                    Log.LogEvent($"A tweet posted in Twitter");

                    if (tweet != null)
                    {
                        return tweet;
                    }
                    else
                    {
                        Log.LogError($"A problem occurred while posting a tweet in Twitter");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("A Tweet object was not received from the client in the Execute function in TwitterAddCmd class");
                return null;
            }
        }
    }
}
