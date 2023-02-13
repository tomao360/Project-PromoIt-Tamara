using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesRemoveCmd: BaseCommand, ICommand
    {
        public CompaniesRemoveCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start deleting Business Company (Business Company ID - {(string)param[0]}) from DB (Execute function in CompaniesRemoveCmd class)");
                    // Delete the business company from the DB by ID
                    MainManager.Instance.businessCompanies.DeleteBusinessCompanyFromDB(int.Parse((string)param[0]));

                    Log.LogEvent($"The Business Company (Business Company ID - {(string)param[0]}) deleted successfully from DB");

                    string response = "Business Company deleted successfully";
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
                Log.LogError("Business Company ID parameter was not found in the Execute function in CompaniesRemoveCmd class");
                return null;
            }
        }
    }
}
