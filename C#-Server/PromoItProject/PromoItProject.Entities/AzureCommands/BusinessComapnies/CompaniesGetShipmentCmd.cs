using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesGetShipmentCmd: BaseCommand, ICommand
    {
        public CompaniesGetShipmentCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all Shipments from DB (Execute function in CompaniesGetShipmentCmd class)");
                // Retrieve all shipments from the DB
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.businessCompanies.GetAllShipmentsFromDB());

                Log.LogEvent("All Shipments were received from DB");
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
