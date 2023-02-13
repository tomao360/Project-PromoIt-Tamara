using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Campaigns
{
    public class CampaignsRemoveCmd: BaseCommand, ICommand
    {
        public CampaignsRemoveCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start deleting Campaign (Campaign ID - {(string)param[0]}) from DB (Execute function in CampaignsRemoveCmd class)");
                    // Delete the campaign from the DB by ID
                    MainManager.Instance.campaigns.DeleteCampaignByID(int.Parse((string)param[0]));

                    Log.LogEvent($"The Campaign (Campaign ID - {(string)param[0]}) deleted successfully from DB");

                    string response = "The Campaign deleted successfully";
                    return response;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("Campaign ID parameter was not found in the Execute function in CampaignsRemoveCmd class");
                return null;
            }
        }
    }
}
