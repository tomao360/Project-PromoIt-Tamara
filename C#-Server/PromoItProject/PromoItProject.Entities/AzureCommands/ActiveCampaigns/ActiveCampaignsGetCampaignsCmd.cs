using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.ActiveCampaigns
{
    public class ActiveCampaignsGetCampaignsCmd: BaseCommand, ICommand
    {
        public ActiveCampaignsGetCampaignsCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start retrieving all the Active Campaigns by Activist ID (Activist ID - {(string)param[0]}) from DB (Execute function in ActiveCampaignsGetCampaignsCmd class)");
                    // Retrieve all active campaigns by Activist ID
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activeCampaigns.GetActiveCampaignsByActivistID(int.Parse((string)param[0])));

                    Log.LogEvent("All Active Campaigns were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("Activist ID parameter was not found in the Execute function in ActiveCampaignsGetCampaignsCmd class");
                return null;
            }
        }
    }
}
