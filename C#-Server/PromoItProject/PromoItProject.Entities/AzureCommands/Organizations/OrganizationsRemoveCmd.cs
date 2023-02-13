using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Organizations
{
    public class OrganizationsRemoveCmd: BaseCommand, ICommand
    {
        public OrganizationsRemoveCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start deleting the Non-Profit Organization (Non-Profit Organization ID - {(string)param[0]}) from DB (Execute function in OrganizationsRemoveCmd class)");
                    // Delete the organization from the DB by ID
                    MainManager.Instance.nonProfitOrganizations.DeleteOrganizationFromDB(int.Parse((string)param[0]));

                    Log.LogEvent($"The Non-Profit Organization (Non-Profit Organization ID - {(string)param[0]}) deleted successfully from DB");

                    string response = "Non-Profit Organization deleted successfully";
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
                Log.LogError("Non-Profit Organization ID parameter was not found in the Execute function in OrganizationsRemoveCmd class");
                return null;
            }
        }
    }
}
