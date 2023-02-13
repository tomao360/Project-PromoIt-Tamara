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
    public class BusinessReportSql: BaseDataSql
    {
        public BusinessReportSql(Logger log) : base(log) { }


        // A function that adds business company to a list by order of products count
        public List<BusinessReport> CountOfDonations()
        {
            // Create a new list of BusinessReport
            List<BusinessReport> countOfDonationsList = new List<BusinessReport>();

            // Clear the list before adding new data
            countOfDonationsList.Clear();

            // Connection string
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetAmountOfDonatedProducts => Stored Procedure to select the number of distinct products donated by each business company
                    using (SqlCommand command = new SqlCommand("GetAmountOfDonatedProducts", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BusinessReport businessReport = new BusinessReport();

                                // Get the values for the properties of the BusinessReport object from the SQL Stored Procedure
                                businessReport.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                                businessReport.BusinessName = reader.GetString(reader.GetOrdinal("BusinessName"));
                                businessReport.Email = reader.GetString(reader.GetOrdinal("Email"));
                                businessReport.TotalProducts = reader.GetInt32(reader.GetOrdinal("TotalProducts"));

                                // Add the BusinessReport object to the list
                                countOfDonationsList.Add(businessReport);
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

            return countOfDonationsList;
        }
    }
}
