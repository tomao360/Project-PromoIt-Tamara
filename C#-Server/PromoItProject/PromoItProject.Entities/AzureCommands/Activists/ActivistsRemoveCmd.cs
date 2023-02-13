using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistsRemoveCmd: BaseCommand, ICommand
    {
        public ActivistsRemoveCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start deleting Social Activist (Social Activist ID - {(string)param[0]}) from DB (Execute function in ActivistsRemoveCmd class)");
                    // Delete the activist from the DB
                    MainManager.Instance.activists.DeleteActivistFromDB(int.Parse((string)param[0]));

                    Log.LogEvent($"The Social Activist (Social Activist ID - {(string)param[0]}) deleted successfully from DB");

                    string response = "Social activist deleted successfully";
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
                Log.LogError("Social Activist ID parameter was not found in the Execute function in ActivistsRemoveCmd class");
                return null;
            }
        }
    }
}
