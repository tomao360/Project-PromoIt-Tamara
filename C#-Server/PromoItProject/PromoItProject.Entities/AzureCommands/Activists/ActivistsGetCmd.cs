using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistsGetCmd: BaseCommand, ICommand
    {
        public ActivistsGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] == null)
            {
                try
                {
                    Log.LogEvent("Start retrieving all the Social Activists from DB (Execute function in ActivistsGetCmd class)");
                    // Retrieve all activists from DB
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activists.GetAllActivistsFromDB());

                    Log.LogEvent("All the Social Activists were received from DB");
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
                    Log.LogEvent($"Start retrieving all the Social Activists by Email (Email - {(string)param[0]}) from DB (Execute function in ActivistsGetCmd class)");
                    // Retrieve activist from DB by email
                    string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activists.GetActivistFromDbByEmail((string)param[0]));

                    Log.LogEvent($"All the Social Activists by Email (Email - {(string)param[0]}) were received from DB");
                    return json;
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }

            Log.LogError("No information was received from the database in the Execute function in ActivistsGetCmd class");
            return null;
        }
    }
}
