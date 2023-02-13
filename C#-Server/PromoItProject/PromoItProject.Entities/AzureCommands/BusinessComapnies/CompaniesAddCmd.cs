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
    public class CompaniesAddCmd: BaseCommand, ICommand
    {
        public CompaniesAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a BusinessCompany object
                    BusinessCompany businessCompany = System.Text.Json.JsonSerializer.Deserialize<BusinessCompany>((string)param[1]);

                    // Check if all required fields are present
                    if (businessCompany.BusinessName != "" && businessCompany.Email != "")
                    {
                        Log.LogEvent($"Start to insert the Business Company - '{businessCompany.BusinessName}' to DB (Execute function in CompaniesAddCmd class)");
                        // Insert the business company into the DB
                        MainManager.Instance.businessCompanies.InsertBusinessCompanyToDB(businessCompany.BusinessName, businessCompany.Email);

                        Log.LogEvent($"Business Company ('{businessCompany.BusinessName}') inserted successfully into the DB");
                        response = "Business Company inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Business Company - '{businessCompany.BusinessName}' into the DB in the Execute function in CompaniesAddCmd class");
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
                Log.LogError("A Business Company object was not received from the client in the Execute function in CompaniesAddCmd class");
                return null;
            }
        }
    }
}
