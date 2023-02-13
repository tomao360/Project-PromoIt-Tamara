using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Products
{
    public class ProductsGetCmd: BaseCommand, ICommand
    {
        public ProductsGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Donated Products from DB (Execute function in ProductsGetCmd class)");
                    // Retrieve all products from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.donatedProducts.GetAllProductsCampaignsORGFromDB());

                    Log.LogEvent("All the Donated Products were received from DB");
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
                    Log.LogEvent($"Start retrieving all the Donated Products of Business Company by Business Company ID (Business Company ID - {(string)param[0]}) from DB (Execute function in ProductsGetCmd class)");
                    // Retrieve all products from the DB by company ID
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.donatedProducts.GetAllProductsCampaignsORGByByCompanyID(int.Parse((string)param[0])));

                    Log.LogEvent($"All the Donated Products of Business Company by Business Company ID (Business Company ID - {(string)param[0]}) were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in ProductsGetCmd class");
            return null;
        }
    }
}
