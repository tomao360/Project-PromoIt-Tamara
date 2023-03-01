using PromoItProject.DAL;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class ContactUsMessageSql: BaseDataSql
    {
        public ContactUsMessageSql(Logger log) : base(log) { }


        // Connection string
        public static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the message to dictionary 
        public Dictionary<int, ContactUsMessage> AddMessageToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of ContactUsMessage
            Dictionary<int, ContactUsMessage> messagesDic = new Dictionary<int, ContactUsMessage>();

            // Clear the dictionary before adding new data
            messagesDic.Clear();

            while (reader.Read())
            {
                ContactUsMessage contactUsMessage = new ContactUsMessage();

                // Get the values for the properties of the ContactUsMessage object from the SQL query
                contactUsMessage.MessageID = reader.GetInt32(reader.GetOrdinal("MessageID"));
                contactUsMessage.Name = reader.GetString(reader.GetOrdinal("Name"));
                contactUsMessage.Email = reader.GetString(reader.GetOrdinal("Email"));
                contactUsMessage.UserMessage = reader.GetString(reader.GetOrdinal("UserMessage"));

                // Add the ContactUsMessage object to the dictionary
                messagesDic.Add(contactUsMessage.MessageID, contactUsMessage);
            }

            return messagesDic;
        }

        public object LoadMessages()
        {
            object retDictionary = null;

            try
            {
                // SQL query to select all columns from the Contact_Us table
                string sqlQuery = "select * from Contact_Us";
                retDictionary = SqlQuery.RunCommandResult(sqlQuery, AddMessageToDictionary);
            }
            catch (SqlException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }

            return retDictionary;
        }


        // A function that inserts a message to the Contact_Us table in SQL
        public void InsertMessageToDB(string name, string email, string userMessage)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertContactUsMessageToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@userMessage", userMessage);

                        //Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }



        // A function that deletes a message from the Contact_Us table in SQL
        public void DeleteMessageByID(int messageID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteContactUsMessageByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@messageID", messageID);
                        //Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }
    }
}
