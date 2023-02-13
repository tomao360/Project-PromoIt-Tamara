using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesGetCountOfDonationsCmd: BaseCommand, ICommand
    {
        public CompaniesGetCountOfDonationsCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving the count of donations from DB (Execute function in CompaniesGetCountOfDonationsCmd class)");
                // Retrieve the count of donations from the DB
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.businessCompanies.GetCountOfDonations());

                Log.LogEvent("The count of donations was received from DB");
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
