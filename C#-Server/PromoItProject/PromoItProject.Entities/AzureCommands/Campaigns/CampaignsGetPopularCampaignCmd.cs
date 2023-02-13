using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsGetPopularCampaignCmd: BaseCommand, ICommand
    {
        public CampaignsGetPopularCampaignCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all the Campaigns (by popularity) from DB (Execute function in CampaignsGetPopularCampaignCmd class)");
                // Retrieve the most popular campaign
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetMostPopularCampaign());

                Log.LogEvent("All Campaigns (by popularity) were received from DB");
                return json;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }
    }
}
