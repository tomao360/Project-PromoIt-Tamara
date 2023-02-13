using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistsGetMostPromotedCampaignsCmd: BaseCommand, ICommand
    {
        public ActivistsGetMostPromotedCampaignsCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all the Social Activists (by the count of promoted campaigns) from DB (Execute function in ActivistsGetMostPromotedCampaignsCmd class)");
                // Retrieve the campaigns that were most promoted by activists
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activists.GetMostPromotedCampaigns());

                Log.LogEvent("All Social Activists (by the count of promoted campaigns) were received from DB");
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
