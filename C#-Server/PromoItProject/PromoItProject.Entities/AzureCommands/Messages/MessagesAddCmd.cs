using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Messages
{
    public class MessagesAddCmd: BaseCommand, ICommand
    {
        public MessagesAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a ContactUsMessage object
                    ContactUsMessage contactUsMessage = System.Text.Json.JsonSerializer.Deserialize<ContactUsMessage>((string)param[1]);

                    // Check if all required fields are present
                    if (contactUsMessage.Name != "" && contactUsMessage.Email != "" && contactUsMessage.UserMessage != "")
                    {
                        Log.LogEvent($"Start to insert a Contact Message from - '{contactUsMessage.Name}' to DB (Execute function in MessagesAddCmd class)");
                        // Insert the message into the DB
                        MainManager.Instance.ContactUsMessages.InsertMessageToDB(contactUsMessage.Name, contactUsMessage.Email, contactUsMessage.UserMessage);

                        Log.LogEvent($"Contact Message from - '{contactUsMessage.Name}' inserted successfully into the DB");
                        response = "The Contact Message inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Contact Message from - '{contactUsMessage.Name}' into the DB in the Execute function in MessagesAddCmd class");
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
                Log.LogError("A Contact Message object was not received from the client in the Execute function in MessagesAddCmd class");
                return null;
            }
        }
    }
}
