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
    public class ActivistReportSql: BaseDataSql
    {
        public ActivistReportSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A function that adds social activists to a list by order of money earned
        public List<ActivistReport> MostMoneyEarned()
        {
            // Create a new list of ActivistReport
            List<ActivistReport> mostMoneyEarnedList = new List<ActivistReport>();

            // Clear the list before adding new data
            mostMoneyEarnedList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetActivistsThatEarnedMostMoneyByOrder => Stored Procedure that selects activists and the total money they have earned from their active campaigns
                    using (SqlCommand command = new SqlCommand("GetActivistsThatEarnedMostMoneyByOrder", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivistReport activistReport = new ActivistReport();

                                // Get the values for the properties of the ActivistReport object from the SQL Stored Procedure
                                activistReport.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                                activistReport.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                                activistReport.Email = reader.GetString(reader.GetOrdinal("Email"));
                                activistReport.TotalMoney = reader.GetInt32(reader.GetOrdinal("TotalMoney"));

                                // Add the ActivistReport object to the list
                                mostMoneyEarnedList.Add(activistReport);
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

            return mostMoneyEarnedList;
        }


        // A function that adds social activists to a list by order of most promted campaign
        public List<ActivistReport> MostPromotedCampaigns()
        {
            // Create a new list of ActivistReport
            List<ActivistReport> mostPromotedCampaignsList = new List<ActivistReport>();

            // Clear the list before adding new data
            mostPromotedCampaignsList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetActivistsThatPromotedMostCampaignsByOrder => Stored Procedure to select data from the Social_Activists and Active_Campaigns tables
                    using (SqlCommand command = new SqlCommand("GetActivistsThatPromotedMostCampaignsByOrder", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivistReport activistReport = new ActivistReport();

                                // Get the values for the properties of the ActivistReport object from the SQL Stored Procedure
                                activistReport.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                                activistReport.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                                activistReport.Email = reader.GetString(reader.GetOrdinal("Email"));
                                activistReport.TotalCampaigns = reader.GetInt32(reader.GetOrdinal("TotalCampaigns"));

                                // Add the ActivistReport object to the list
                                mostPromotedCampaignsList.Add(activistReport);
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

            return mostPromotedCampaignsList;
        }

    }
}
