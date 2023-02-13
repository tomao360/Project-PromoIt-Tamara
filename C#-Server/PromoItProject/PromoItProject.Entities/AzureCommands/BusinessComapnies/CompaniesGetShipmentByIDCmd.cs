using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesGetShipmentByIDCmd: BaseCommand, ICommand
    {
        public CompaniesGetShipmentByIDCmd(Logger log) : base(log) { }


        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start retrieving all the Shipments by Business ID (Business ID - {(string)param[0]}) from DB (Execute function in CompaniesGetShipmentByIDCmd class)");
                    // Retrieve all shipments from the DB by ID
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.businessCompanies.GetAllShipmentsByBusinessID(int.Parse((string)param[0])));

                    Log.LogEvent("All Shipments were received from DB");
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
                Log.LogError("Business ID parameter was not found in the Execute function in CompaniesGetShipmentByIDCmd class");
                return null;
            }
        }
    }
}
