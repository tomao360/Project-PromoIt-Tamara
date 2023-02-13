using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistsAddCmd: BaseCommand, ICommand
    {
        public ActivistsAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into an Activist object
                    Activist activist = System.Text.Json.JsonSerializer.Deserialize<Activist>((string)param[1]);

                    // Check if all required fields are present
                    if (activist.FullName != "" && activist.Email != "" && activist.Address != "" && activist.PhoneNumber != "")
                    {
                        Log.LogEvent($"Start to insert the Social Activist - '{activist.FullName}' to DB (Execute function in ActivistsAddCmd class)");
                        // Insert the activist into the DB
                        MainManager.Instance.activists.InsertActivistToDB(activist.FullName, activist.Email, activist.Address, activist.PhoneNumber);

                        Log.LogEvent($"Social Activist ('{activist.FullName}') inserted successfully into the DB");
                        response = "Social Activist inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Social Activist - '{activist.FullName}' into the DB in the Execute function in ActivistsAddCmd class");
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
                Log.LogError("A Social Activist object was not received from the client in the Execute function in ActivistsAddCmd class");
                return null;
            }
        }
    }
}
