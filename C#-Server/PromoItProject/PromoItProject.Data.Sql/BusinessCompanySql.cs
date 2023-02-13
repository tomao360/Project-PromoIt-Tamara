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
    public class BusinessCompanySql: BaseDataSql
    {
        public BusinessCompanySql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        // A delegate function that adds the business company to dictionary 
        public Dictionary<int, BusinessCompany> AddBusinessCompanyToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of BusinessCompany
            Dictionary<int, BusinessCompany> businessCompanyDic = new Dictionary<int, BusinessCompany>();

            // Clear the dictionary before adding new data
            businessCompanyDic.Clear();

            while (reader.Read())
            {
                BusinessCompany businessCompany = new BusinessCompany();

                // Get the values for the properties of the BusinessCompany object from the SQL Stored Procedure
                businessCompany.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                businessCompany.BusinessName = reader.GetString(reader.GetOrdinal("BusinessName"));
                businessCompany.Email = reader.GetString(reader.GetOrdinal("Email"));
                businessCompany.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                // Add the BusinessCompany object to the dictionary
                businessCompanyDic.Add(businessCompany.BusinessID, businessCompany);
            }

            return businessCompanyDic;
        }

        public object LoadBusinessCompanies()
        {
            object retDictionary = null;

            try
            {
                // GetAllBusinessCompanies => Stored Procedure that: update the DeleteAnswer field to 'Has more records' for business companies that have products in the Donated_Products table, and select all columns from the Business_Companies table
                string storedProcedure = "GetAllBusinessCompanies";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddBusinessCompanyToDictionary);
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

        // A function that gets a business company by email
        public object LoadOneBusinessCompanyObjectByEmail(string email)
        {
            BusinessCompany businessCompany = new BusinessCompany();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetOneBusinessCompanyByEmail => Stored Procedure to select all columns from the Business_Companies table by email
                    using (SqlCommand command = new SqlCommand("GetOneBusinessCompanyByEmail", connection))
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
                                // Get the values for the properties of the BusinessCompany object from the SQL Stored Procedure
                                businessCompany.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                                businessCompany.BusinessName = reader.GetString(reader.GetOrdinal("BusinessName"));
                                businessCompany.Email = reader.GetString(reader.GetOrdinal("Email"));
                                businessCompany.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));
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

            return businessCompany;
        }


        // A function that inserts a business company to the Business_Companies table in SQL
        public void InsertBusinessCompanynToDB(string businessName, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertBusinessCompanyToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@businessName", businessName);                      
                        command.Parameters.AddWithValue("@email", email);
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

        // A function that updates a business company at the Business_Companies table in SQL 
        public void UpdateBusinessCompanyByID(int businessID, string businessName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateBusinessCompanyByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@businessID", businessID);
                        command.Parameters.AddWithValue("@businessName", businessName);

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


        // A function that deletes a business company from the Business_Companies table in SQL
        public void DeleteBusinessCompanyByID(int businessID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteBusinessCompanyByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@businessID", businessID);
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
