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
    public class UserSql: BaseDataSql
    {
        public UserSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        // A delegate function that adds the user to dictionary 
        public Dictionary<int, User> AddUserToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of User
            Dictionary<int, User> usersDic = new Dictionary<int, User>();

            // Clear the dictionary before adding new data
            usersDic.Clear();

            while (reader.Read())
            {
                User user = new User();

                // Get the values for the properties of the User object from the SQL query
                user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                user.Email = reader.GetString(reader.GetOrdinal("Email"));

                // Add the User object to the dictionary
                usersDic.Add(user.UserID, user);
            }

            return usersDic;
        }

        public object LoadUsers()
        {
            object retDictionary = null;

            try
            {
                // SQL query to select all columns from the Users table
                string sqlQuery = "select * from Users";
                retDictionary = SqlQuery.RunCommandResult(sqlQuery, AddUserToDictionary);
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



        // A function that inserts a user to the Users table in SQL
        public void InsertUserToDB(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertUserToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@email", email);

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
