using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoItProject.DAL;
using PromoItProject.Model;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class NonProfitOrganizationSql: BaseDataSql
    {
        public NonProfitOrganizationSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the organizations to dictionary 
        public Dictionary<int, NonProfitOrganization> AddOrganizationToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of NonProfitOrganization
            Dictionary<int, NonProfitOrganization> organizationsDic = new Dictionary<int, NonProfitOrganization>();

            // Clear the dictionary before adding new data
            organizationsDic.Clear();

            while (reader.Read())
            {
                NonProfitOrganization nonProfitOrganization = new NonProfitOrganization();

                // Get the values for the properties of the NonProfitOrganization object from the SQL Stored Procedure
                nonProfitOrganization.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                nonProfitOrganization.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                nonProfitOrganization.Email = reader.GetString(reader.GetOrdinal("Email"));
                nonProfitOrganization.LinkToWebsite = reader.GetString(reader.GetOrdinal("LinkToWebsite"));
                nonProfitOrganization.Description = reader.GetString(reader.GetOrdinal("Description"));
                nonProfitOrganization.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                // Add the NonProfitOrganization object to the dictionary
                organizationsDic.Add(nonProfitOrganization.OrganizationID, nonProfitOrganization);
            }

            return organizationsDic;
        }

        public object LoadOrganizations()
        {
            object retDictionary = null;

            try
            {
                // GetAllOrganizations => Stored Procedure that: update the DeleteAnswer field to 'Has more records' for organizations that have campaigns in the Campaigns table, and select all columns from the Non_Profit_Organizations table
                string storedProcedure = "GetAllOrganizations";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddOrganizationToDictionary);
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


        // A function that gets an organization by email
        public object LoadOneOrganizationObjectByEmail(string email)
        {
            NonProfitOrganization nonProfitOrganization = new NonProfitOrganization();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetOneOrganizationByEmail => Stored Procedure to select all columns from the Non_Profit_Organizations table by email
                    using (SqlCommand command = new SqlCommand("GetOneOrganizationByEmail", connection))
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
                                // Get the values for the properties of the NonProfitOrganization object from the SQL Stored Procedure
                                nonProfitOrganization.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                                nonProfitOrganization.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                                nonProfitOrganization.Email = reader.GetString(reader.GetOrdinal("Email"));
                                nonProfitOrganization.LinkToWebsite = reader.GetString(reader.GetOrdinal("LinkToWebsite"));
                                nonProfitOrganization.Description = reader.GetString(reader.GetOrdinal("Description"));
                                nonProfitOrganization.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));
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

            return nonProfitOrganization;
        }



        // A function that inserts an organization to the Non_Profit_Organizations table in SQL
        public void InsertOrganizationToDB(string organizationName, string linkToWebsite, string email, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertOrganizationToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@organizationName", organizationName);
                        command.Parameters.AddWithValue("@linkToWebsite", linkToWebsite);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@description", description);
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



        // A function that updates an organization at the Non_Profit_Organizations table in SQL 
        public void UpdateOrganizationByID(int organizationID, string organizationName, string linkToWebsite, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateOrganizationByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@organizationID", organizationID);
                        command.Parameters.AddWithValue("@organizationName", organizationName);
                        command.Parameters.AddWithValue("@linkToWebsite", linkToWebsite);
                        command.Parameters.AddWithValue("@description", description);


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



        // A function that deletes an organization from the Non_Profit_Organizations table in SQL
        public void DeleteOrganizationByID(int organizationID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteOrganizationByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@organizationID", organizationID);
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
