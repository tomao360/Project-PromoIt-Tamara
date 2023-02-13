using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Organizations
{
    public class OrganizationsGetCampaignsProductsCmd: BaseCommand, ICommand
    {
        public OrganizationsGetCampaignsProductsCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving the count of Campaigns and Donated Products for Non-Profit Organization from DB (Execute function in OrganizationsGetCampaignsProductsCmd class)");
                // Retrieve count of campaigns and products for organization
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.nonProfitOrganizations.GetCountCampaignAndProductsToOrg());

                Log.LogEvent("All Campaigns and Donated Products for Non-Profit Organization were received from DB");
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
