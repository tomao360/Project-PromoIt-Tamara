using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class DonatedProductSql: BaseDataSql
    {
        public DonatedProductSql(Logger log) : base(log) { }


        // Connection string
        public static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A function that inserts a product to the Donated_Products table in SQL
        public void InsertDonatedProductToDB(string productName, decimal price, int businessID, int campaignID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertDonatedProductToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@productName", productName);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@businessID", businessID);
                        command.Parameters.AddWithValue("@campaignID", campaignID);
                        command.Parameters.AddWithValue("@bought", "NO");
                        command.Parameters.AddWithValue("@shipped", "NO");

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



        // A function that updates a product at the Donated_Products table in SQL (Bought status)
        public void UpdateProductBoughtStatus(int productID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateDonatedProductBoughtStatus", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@productID", productID);
                        command.Parameters.AddWithValue("@bought", "YES");

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



        // A function that updates a product at the Donated_Products table in SQL (Shipped status)
        public void UpdateProductShippedStatus(int productID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateDonatedProductShippedStatus", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@productID", productID);
                        command.Parameters.AddWithValue("@shipped", "YES");

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
