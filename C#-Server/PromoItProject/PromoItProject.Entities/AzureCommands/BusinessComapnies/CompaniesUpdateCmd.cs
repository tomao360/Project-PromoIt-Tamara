using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.BusinessComapnies
{
    public class CompaniesUpdateCmd: BaseCommand, ICommand
    {
        public CompaniesUpdateCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a BusinessCompany object
                    BusinessCompany businessCompany1 = System.Text.Json.JsonSerializer.Deserialize<BusinessCompany>((string)param[1]);

                    // Check if the business name is present
                    if (businessCompany1.BusinessName != "")
                    {
                        Log.LogEvent($"Started updating the Business Company ('{businessCompany1.BusinessName}') in the DB (Execute function in CompaniesUpdateCmd class)");
                        // Update the business company in the DB
                        MainManager.Instance.businessCompanies.UpdateBusinessCompanyInDB(int.Parse((string)param[0]), businessCompany1.BusinessName);

                        Log.LogEvent($"Business Company - '{businessCompany1.BusinessName}' updated successfully");
                        response = "Business Company updated successfully";
                        return response;

                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Business Company ('{businessCompany1.BusinessName}') in the DB - Execute function in CompaniesUpdateCmd class");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("No parameters were received from the client in the Execute function in CompaniesUpdateCmd class");
                return null;
            }
        }
    }
}
