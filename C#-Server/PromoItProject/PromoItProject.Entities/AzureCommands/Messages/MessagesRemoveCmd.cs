using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Messages
{
    public class MessagesRemoveCmd: BaseCommand, ICommand
    {
        public MessagesRemoveCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    Log.LogEvent($"Start deleting Contact Message (Contact Message ID - {(string)param[0]}) from DB (Execute function in MessagesRemoveCmd class)");
                    // Delete the message from the DB by ID
                    MainManager.Instance.ContactUsMessages.DeleteMessageByID(int.Parse((string)param[0]));

                    Log.LogEvent($"The Contact Message (Contact Message ID - {(string)param[0]}) deleted successfully from DB");

                    string response = "The Contact Message deleted successfully";
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
                Log.LogError("Contact Message ID parameter was not found in the Execute function in MessagesRemoveCmd class");
                return null;
            }
        }
    }
}
