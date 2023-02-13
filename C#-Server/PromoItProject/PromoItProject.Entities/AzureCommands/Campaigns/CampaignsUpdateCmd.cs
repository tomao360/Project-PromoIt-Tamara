using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsUpdateCmd: BaseCommand, ICommand
    {
        public CampaignsUpdateCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a Campaign object
                    Campaign campaign1 = System.Text.Json.JsonSerializer.Deserialize<Campaign>((string)param[1]);

                    // Check if all required fields are present
                    if (campaign1.CampaignID != null && campaign1.CampaignName != null && campaign1.LinkToLandingPage != null && campaign1.Hashtag != null)
                    {
                        Log.LogEvent($"Started updating the Campaign ('{campaign1.CampaignName}') in the DB (Execute function in CampaignsUpdateCmd class)");
                        // Update the campaign in the DB
                        MainManager.Instance.campaigns.UpdateCampaignInDB(int.Parse((string)param[0]), campaign1.CampaignName, campaign1.LinkToLandingPage, campaign1.Hashtag);

                        Log.LogEvent($"Campaign - '{campaign1.CampaignName}' updated successfully");
                        response = "Campaign updated successfully";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Campaign ('{campaign1.CampaignName}') in the DB - Execute function in CampaignsUpdateCmd class");
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
                Log.LogError("No parameters were received from the client in the Execute function in CampaignsUpdateCmd class");
                return null;
            }
        }
    }
}
