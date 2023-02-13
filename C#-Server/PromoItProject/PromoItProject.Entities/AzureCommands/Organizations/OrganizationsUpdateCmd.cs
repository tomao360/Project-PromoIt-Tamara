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
    public class OrganizationsUpdateCmd: BaseCommand, ICommand
    {
        public OrganizationsUpdateCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a NonProfitOrganization object
                    NonProfitOrganization nonProfitOrganization1 = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrganization>((string)param[1]);

                    // Check if all required fields are present
                    if (nonProfitOrganization1.OrganizationName != "" && nonProfitOrganization1.LinkToWebsite != "" && nonProfitOrganization1.Description != "")
                    {
                        Log.LogEvent($"Started updating the Non-Profit Organization ('{nonProfitOrganization1.OrganizationName}') in the DB (Execute function in OrganizationsUpdateCmd class)");
                        // Update the organization in the DB
                        MainManager.Instance.nonProfitOrganizations.UpdateOrganizationInDB(int.Parse((string)param[0]), nonProfitOrganization1.OrganizationName, nonProfitOrganization1.LinkToWebsite, nonProfitOrganization1.Description);

                        Log.LogEvent($"Non-Profit Organization - '{nonProfitOrganization1.OrganizationName}' updated successfully");
                        response = "Non-Profit Organization updated successfully";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Non-Profit Organization ('{nonProfitOrganization1.OrganizationName}') in the DB - Execute function in OrganizationsUpdateCmd class");
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
                Log.LogError("No parameters were received from the client in the Execute function in OrganizationsUpdateCmd class");
                return null;
            }
        }
    }
}
