using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.ActiveCampaigns
{
    public class ActiveCampaignsAddCmd: BaseCommand, ICommand
    {
        public ActiveCampaignsAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into an ActiveCampaign object
                    ActiveCampaign activeCampaign = System.Text.Json.JsonSerializer.Deserialize<ActiveCampaign>((string)param[1]);

                    // Check if all required fields are present
                    if (activeCampaign.ActivistID != null && activeCampaign.CampaignID != null && activeCampaign.TwitterUserName != "" && activeCampaign.Hashtag != "" && activeCampaign.CampaignName != "")
                    {
                        Log.LogEvent($"Start to insert the Active Campaign - '{activeCampaign.CampaignName}' to DB (Execute function in ActiveCampaignsAddCmd class)");
                        // Insert the active campaign into the DB
                        MainManager.Instance.activeCampaigns.InsertActiveCampaignToDB(activeCampaign.ActivistID, activeCampaign.CampaignID, activeCampaign.TwitterUserName, activeCampaign.Hashtag, activeCampaign.CampaignName);

                        Log.LogEvent($"Active Campaign ('{activeCampaign.CampaignName}') inserted successfully into the DB");
                        response = "Active Campaign inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Active Campaign - '{activeCampaign.CampaignName}' into the DB in the Execute function in ActiveCampaignsAddCmd class");
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
                Log.LogError("An Active Campaign object was not received from the client in the Execute function in ActiveCampaignsAddCmd class");
                return null;
            }
        }
    }
}
