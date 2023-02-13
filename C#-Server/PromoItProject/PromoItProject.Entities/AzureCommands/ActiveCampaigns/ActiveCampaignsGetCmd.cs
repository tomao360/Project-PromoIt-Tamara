using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.Commands
{
    public class ActiveCampaignsGetCmd: BaseCommand, ICommand
    {
        public ActiveCampaignsGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Active Campaigns from DB (Execute function in ActiveCampaignsGetCmd class)");
                    // Retrieve all active campaigns from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activeCampaigns.GetAllActiveCampaignsFromDB());

                    Log.LogEvent("All the Active Campaigns were received from DB");
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
                    Log.LogEvent($"Start retrieving all the Donated Products by Activist ID (Activist ID - {(string)param[0]}) from DB (Execute function in ActiveCampaignsGetCmd class)");
                    // Retrieve all products by Activist ID
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activeCampaigns.GetAllProductTableByActivistID(int.Parse((string)param[0])));

                    Log.LogEvent($"All the Donated Products by Activist ID (Activist ID - {(string)param[0]}) were received from DB");
                    return json;
                }
                 catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in ActiveCampaignsGetCmd class");
            return null;
        }
    }
}
