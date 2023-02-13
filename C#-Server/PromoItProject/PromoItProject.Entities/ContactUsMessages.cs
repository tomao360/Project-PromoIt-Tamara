using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class ContactUsMessages: BaseEntity
    {
        public ContactUsMessages(Logger log) : base(log) { }

        public Dictionary<int, ContactUsMessage> GetAllMessagesFromDB()
        {
            Dictionary<int, ContactUsMessage> messagesDic = null;   
            try
            {
                messagesDic = new Dictionary<int, ContactUsMessage>();
                Data.Sql.ContactUsMessageSql contactUsMessageSql = new Data.Sql.ContactUsMessageSql(base.Log);
                messagesDic = (Dictionary<int, ContactUsMessage>)contactUsMessageSql.LoadMessages();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return messagesDic;
        }

        public void InsertMessageToDB(string name, string email, string userMessage)
        {
            try
            {
                Data.Sql.ContactUsMessageSql contactUsMessageSql = new Data.Sql.ContactUsMessageSql(base.Log);
                contactUsMessageSql.InsertMessageToDB(name, email, userMessage);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void DeleteMessageByID(int messageID)
        {
            try
            {
                Data.Sql.ContactUsMessageSql contactUsMessageSql = new Data.Sql.ContactUsMessageSql(base.Log);
                contactUsMessageSql.DeleteMessageByID(messageID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }   
        }
    }
}
