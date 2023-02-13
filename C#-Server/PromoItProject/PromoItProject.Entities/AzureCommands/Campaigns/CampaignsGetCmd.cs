using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsGetCmd: BaseCommand, ICommand
    {
        public CampaignsGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Campaigns from DB (Execute function in CampaignsGetCmd class)");
                    // Retrieve all campaigns from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetAllCampaignsFromDB());

                    Log.LogEvent("All the Campaigns were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start retrieving all the Campaigns by Organization Email (Email - {(string)param[0]}) from DB (Execute function in CampaignsGetCmd class)");
                    // Retrieve campaigns from DB by organization email
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetAllCampaignsFromDBByORGEmail((string)param[0]));

                    Log.LogEvent($"All the Campaigns by Organization Email (Email - {(string)param[0]}) were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in CampaignsGetCmd class");
            return null;
        }
    }
}
