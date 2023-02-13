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
    public class CampaignsAddCmd: BaseCommand, ICommand
    {
        public CampaignsAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a Campaign object
                    Campaign campaign = System.Text.Json.JsonSerializer.Deserialize<Campaign>((string)param[1]);

                    // Check if all required fields are present
                    if (campaign.OrganizationID != null && campaign.CampaignName != null && campaign.LinkToLandingPage != null && campaign.Hashtag != null)
                    {
                        Log.LogEvent($"Start to insert the Campaign - '{campaign.CampaignName}' to DB (Execute function in CampaignsAddCmd class)");
                        // Insert the campaign into the DB
                        MainManager.Instance.campaigns.InsertCampaignToDB(campaign.OrganizationID, campaign.CampaignName, campaign.LinkToLandingPage, campaign.Hashtag);

                        Log.LogEvent($"Campaign ('{campaign.CampaignName}') inserted successfully into the DB");
                        response = "The Campaign inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Campaign - '{campaign.CampaignName}' into the DB in the Execute function in CampaignsAddCmd class");
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
                Log.LogError("A Campaign object was not received from the client in the Execute function in CampaignsAddCmd class");
                return null;
            }
        }
    }
}
