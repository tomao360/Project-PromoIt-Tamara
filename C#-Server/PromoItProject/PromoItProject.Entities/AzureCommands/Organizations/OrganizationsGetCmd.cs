using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Organizations
{
    public class OrganizationsGetCmd: BaseCommand, ICommand
    {
        public OrganizationsGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Non-Profit Organizations from DB (Execute function in OrganizationsGetCmd class)");
                    // Retrieve all organizations  from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.nonProfitOrganizations.GetAllOrganizationsFromDB());

                    Log.LogEvent("All the Non-Profit Organizations were received from DB");
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
                    Log.LogEvent($"Start retrieving a Non-Profit Organization by Organization Email (Email - {(string)param[0]}) from DB (Execute function in OrganizationsGetCmd class)");
                    // Retrieve an organization from the DB by email
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.nonProfitOrganizations.GetOrganizationFromDbByEmail((string)param[0]));

                    Log.LogEvent($"A Non-Profit Organization by Organization Email (Email - {(string)param[0]}) was received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in OrganizationsGetCmd class");
            return null;
        }
    }
}
