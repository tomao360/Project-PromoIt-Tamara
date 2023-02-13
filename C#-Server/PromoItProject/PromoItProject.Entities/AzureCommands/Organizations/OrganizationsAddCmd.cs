using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Organizations
{
    public class OrganizationsAddCmd: BaseCommand, ICommand
    {
        public OrganizationsAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a NonProfitOrganization object
                    NonProfitOrganization nonProfitOrganization = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrganization>((string)param[1]);

                    // Check if all required fields are present
                    if (nonProfitOrganization.OrganizationName != "" && nonProfitOrganization.LinkToWebsite != "" && nonProfitOrganization.Email != "" && nonProfitOrganization.Description != "")
                    {
                        Log.LogEvent($"Start to insert the Non-Profit Organization - '{nonProfitOrganization.OrganizationName}' to DB (Execute function in OrganizationsAddCmd class)");
                        // Insert the organization into the DB
                        MainManager.Instance.nonProfitOrganizations.InsertOrganizationToDB(nonProfitOrganization.OrganizationName, nonProfitOrganization.LinkToWebsite, nonProfitOrganization.Email, nonProfitOrganization.Description);

                        Log.LogEvent($"Non-Profit Organization ('{nonProfitOrganization.OrganizationName}') inserted successfully into the DB");
                        response = "Non-Profit Organization inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Non-Profit Organization - '{nonProfitOrganization.OrganizationName}' into the DB in the Execute function in OrganizationsAddCmd class");
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
                Log.LogError("A Non-Profit Organization object was not received from the client in the Execute function in OrganizationsAddCmd class");
                return null;
            }
        }
    }
}
