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
    public class ActiveCampaignsUpdateAddMoneyCmd: BaseCommand, ICommand
    {
        public ActiveCampaignsUpdateAddMoneyCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into an ActiveCampaign object
                    ActiveCampaign activeCampaign1 = System.Text.Json.JsonSerializer.Deserialize<ActiveCampaign>((string)param[1]);

                    // Deserialize the request body into an ActiveCampaign object
                    if (activeCampaign1.ActiveCampID != null && activeCampaign1.MoneyEarned != null && activeCampaign1.TweetsNumber != null)
                    {
                        Log.LogEvent($"Started updating the Active Campaign ('{activeCampaign1.CampaignName}') in the DB (Execute function in ActiveCampaignsUpdateAddMoneyCmd class)");
                        // Update the active campaign in the DB with the money earned and tweets number
                        MainManager.Instance.activeCampaigns.UpdateActiveCampaignAddMoneyInDB(int.Parse((string)param[0]), activeCampaign1.MoneyEarned, activeCampaign1.TweetsNumber);

                        Log.LogEvent($"Active Campaign - '{activeCampaign1.CampaignName}' updated successfully");
                        response = "Active Campaign updated successfully";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Active Campaign ('{activeCampaign1.CampaignName}') in the DB - Execute function in ActiveCampaignsUpdateAddMoneyCmd class");      
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
                Log.LogError("No parameters were received from the client in the Execute function in ActiveCampaignsUpdateAddMoneyCmd class");
                return null;
            }
        }
    }
}
