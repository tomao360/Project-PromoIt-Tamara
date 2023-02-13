using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Users
{
    public class UsersAddCmd: BaseCommand, ICommand
    {
        public UsersAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            string response;
            if (param[1] != null)
            {
                try
                {
                    Log.LogEvent($"Start to insert a User to DB (Execute function in UsersAddCmd class)");
                    // Insert the user into the DB
                    MainManager.Instance.users.InsertUserToDB((string)param[1]);

                    Log.LogEvent($"A User inserted successfully into the DB");
                    response = "The user inserted successfully into the DB";
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
                Log.LogError("A User object was not received from the client in the Execute function in UsersAddCmd class");
                return null;
            }
        }
    }
}
