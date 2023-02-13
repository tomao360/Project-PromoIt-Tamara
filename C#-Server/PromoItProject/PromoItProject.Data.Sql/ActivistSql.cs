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
    public class ActivistSql: BaseDataSql
    {
        public ActivistSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        // A delegate function that adds the social activists to dictionary 
        public Dictionary<int, Activist> AddActivistToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of Activist
            Dictionary<int, Activist> activistsDic = new Dictionary<int, Activist>();

            // Clear the dictionary before adding new data
            activistsDic.Clear();

            while (reader.Read())
            {
                Activist activist = new Activist();

                // Get the values for the properties of the Activist object from the SQL Stored Procedure
                activist.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                activist.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                activist.Email = reader.GetString(reader.GetOrdinal("Email"));
                activist.Address = reader.GetString(reader.GetOrdinal("Address"));
                activist.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                activist.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                // Add the Activist object to the dictionary
                activistsDic.Add(activist.ActivistID, activist);
            }

            return activistsDic;
        }

        public object LoadActivists()
        {
            object retDictionary = null;

            try
            {
                // GetAllSocialActivists => Stored Procedure that: update the DeleteAnswer field to 'Has more records' for activists that have campaigns in the Active_Campaigns table, and selects all columns from the Social_Activists table
                string storedProcedure = "GetAllSocialActivists";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddActivistToDictionary);
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

        // A function that gets a social activist by email
        public object LoadOneActivistObjectByEmail(string email)
        {
            Activist activist = new Activist();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetOneSocialActivistByEmail => Stored Procedure to select all columns from the Social_Activists table by email
                    using (SqlCommand command = new SqlCommand("GetOneSocialActivistByEmail", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add email as a parameter to the query
                        command.Parameters.AddWithValue("@email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the values for the properties of the Activist object from the SQL Stored Procedure
                                activist.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                                activist.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                                activist.Email = reader.GetString(reader.GetOrdinal("Email"));
                                activist.Address = reader.GetString(reader.GetOrdinal("Address"));
                                activist.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                                activist.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));
                            }
                        }
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

            return activist;
        }


        // A function that inserts a social activist to the Social_Activists table in SQL
        public void InsertActivistToDB(string fullName, string email, string address, string phoneNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertSocialActivistToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@fullName", fullName);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@deleteAnswer", "1");

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


        // A function that updates a social activist at the Social_Activists table in SQL 
        public void UpdateActivistByID(int activistID, string fullName, string address, string phoneNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateSocialActivistByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@activistID", activistID);
                        command.Parameters.AddWithValue("@fullName", fullName);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);


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


        // A function that deletes a social activist from the Social_Activists table in SQL
        public void DeleteActivistByID(int activistID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteSocialActivistByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@activistID", activistID);
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
