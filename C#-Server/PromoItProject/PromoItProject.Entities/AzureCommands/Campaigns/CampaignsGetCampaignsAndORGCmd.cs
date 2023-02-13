using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsGetCampaignsAndORGCmd: BaseCommand, ICommand
    {
        public CampaignsGetCampaignsAndORGCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving the Campaigns and their Organizations from DB (Execute function in CampaignsGetCampaignsAndORGCmd class)");
                // Retrieve all campaigns and their organizations from DB
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetAllOrganizationsAndCampaignsFromDB());

                Log.LogEvent("All the Campaigns and their Organizations were received from DB");
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
