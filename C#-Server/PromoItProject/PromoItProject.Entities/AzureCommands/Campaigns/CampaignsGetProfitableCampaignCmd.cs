using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsGetProfitableCampaignCmd: BaseCommand, ICommand
    {
        public CampaignsGetProfitableCampaignCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all the Campaigns (by profitability) from DB (Execute function in CampaignsGetProfitableCampaignCmd class)");
                // Retrieve the most profitable campaign
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetMostProfitableCampaign());

                Log.LogEvent("All Campaigns (by profitability) were received from DB");
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
