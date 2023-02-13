using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesGetCmd: BaseCommand, ICommand
    {
        public CompaniesGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Business Companies from DB (Execute function in CompaniesGetCmd class)");
                    // Retrieve all business campaigns from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.businessCompanies.GetAllBusinessCompaniesFromDB());

                    Log.LogEvent("All the Business Companies were received from DB");
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
                    Log.LogEvent($"Start retrieving all the Business Companies by Email (Email - {(string)param[0]}) from DB (Execute function in CompaniesGetCmd class)");
                    // Retrieve a business company from the DB by email
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.businessCompanies.GetBusinessCompanyFromDbByEmail((string)param[0]));

                    Log.LogEvent($"All the Business Companies by Email (Email - {(string)param[0]}) were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in CompaniesGetCmd class");
            return null;
        }
    }
}
