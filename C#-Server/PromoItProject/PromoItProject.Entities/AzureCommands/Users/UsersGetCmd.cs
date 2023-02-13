using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Users
{
    public class UsersGetCmd: BaseCommand, ICommand
    {
        public UsersGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all Users from DB (Execute function in UsersGetCmd class)");
                // Retrieve all users  from DB
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.GetAllUsersFromDB());

                Log.LogEvent("All Users were received from DB");
                return json;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }
    }
}
