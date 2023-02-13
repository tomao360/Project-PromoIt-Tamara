using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistUpdateCmd: BaseCommand, ICommand
    {
        public ActivistUpdateCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into an Activist object
                    Activist activist1 = System.Text.Json.JsonSerializer.Deserialize<Activist>((string)param[1]);

                    // Check if all required fields are present
                    if (activist1.FullName != "" && activist1.Address != "" && activist1.PhoneNumber != "")
                    {
                        Log.LogEvent($"Started updating the Social Activist ('{activist1.FullName}') in the DB (Execute function in ActivistUpdateCmd class)");
                        // Update the activist in the DB
                        MainManager.Instance.activists.UpdateActivistInDB(int.Parse((string)param[0]), activist1.FullName, activist1.Address, activist1.PhoneNumber);

                        Log.LogEvent($"Social Activist - '{activist1.FullName}' updated successfully");
                        response = "Social activist updated successfully";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Social Activist ('{activist1.FullName}') in the DB - Execute function in ActivistUpdateCmd class");
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
                Log.LogError("No parameters were received from the client in the Execute function in ActivistUpdateCmd class");
                return null;
            }
        }
    }
}
