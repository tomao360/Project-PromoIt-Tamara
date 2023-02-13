using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Messages
{
    public class MessagesGetCmd: BaseCommand, ICommand
    {
        public MessagesGetCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all the Contact Messages from DB (Execute function in MessagesGetCmd class)");
                // Retrieve all messages from DB
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.ContactUsMessages.GetAllMessagesFromDB());

                Log.LogEvent("All the Contact Messages were received from DB");
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
