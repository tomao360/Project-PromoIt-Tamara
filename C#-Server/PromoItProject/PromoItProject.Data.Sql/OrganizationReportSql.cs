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
    public class OrganizationReportSql: BaseDataSql
    {
        public OrganizationReportSql(Logger log) : base(log) { }


        // Connection string
        public static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A function that adds campaigns to a list by order of campaign's popularity
        public List<OrganizationReport> MostPopularCampaign()
        {
            // Create a new list of OrganizationReport
            List<OrganizationReport> popularCampaignList = new List<OrganizationReport>();

            // Clear the list before adding new data
            popularCampaignList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetTheMostPopularCampaign => Stored Procedure that selects campaigns and the total activists, tweets, and products they have
                    using (SqlCommand command = new SqlCommand("GetTheMostPopularCampaign", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrganizationReport organizationReport = new OrganizationReport();

                                // Get the values for the properties of the OrganizationReport object from the SQL Stored Procedure
                                organizationReport.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                                organizationReport.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                organizationReport.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                organizationReport.LinkToLandingPage = reader.GetString(reader.GetOrdinal("LinkToLandingPage"));
                                organizationReport.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                organizationReport.TotalActivists = reader.GetInt32(reader.GetOrdinal("TotalActivists"));
                                organizationReport.TotalTweets = reader.GetInt32(reader.GetOrdinal("TotalTweets"));
                                organizationReport.TotalProducts = reader.GetInt32(reader.GetOrdinal("TotalProducts"));

                                // Add the OrganizationReport object to the list
                                popularCampaignList.Add(organizationReport);
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

            return popularCampaignList;
        }



        // A function that adds campaigns to a list by order of campaign's profitability
        public List<OrganizationReport> MostProfitableCampaign()
        {
            // Create a new list of OrganizationReport
            List<OrganizationReport> profitableCampaignList = new List<OrganizationReport>();

            // Clear the list before adding new data
            profitableCampaignList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetTheMostProfitableCampaign => Stored Procedure that selects campaigns and the total money they have earned
                    using (SqlCommand command = new SqlCommand("GetTheMostProfitableCampaign", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrganizationReport organizationReport = new OrganizationReport();

                                // Get the values for the properties of the OrganizationReport object from the SQL Stored Procedure
                                organizationReport.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                                organizationReport.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                organizationReport.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                organizationReport.LinkToLandingPage = reader.GetString(reader.GetOrdinal("LinkToLandingPage"));
                                organizationReport.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                organizationReport.TotalMoney = reader.GetInt32(reader.GetOrdinal("TotalMoney"));

                                // Add the OrganizationReport object to the list
                                profitableCampaignList.Add(organizationReport);
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

            return profitableCampaignList;
        }


        // A function that adds organizations to a list by order of amount campaigns and products
        public List<OrganizationReport> CountCampaignAndProductsToOrg()
        {
            // Create a new list of OrganizationReport
            List<OrganizationReport> countCampaignAndProductsToOrgList = new List<OrganizationReport>();

            // Clear the list before adding new data
            countCampaignAndProductsToOrgList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetCountCampaignAndProductsToOrg => Stored Procedure that selects organizations, total campaigns, and total products they have 
                    using (SqlCommand command = new SqlCommand("GetCountCampaignAndProductsToOrg", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrganizationReport organizationReport = new OrganizationReport();

                                // Get the values for the properties of the OrganizationReport object from the SQL Stored Procedure
                                organizationReport.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                                organizationReport.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                                organizationReport.Email = reader.GetString(reader.GetOrdinal("Email"));
                                organizationReport.LinkToWebsite = reader.GetString(reader.GetOrdinal("LinkToWebsite"));
                                organizationReport.Description = reader.GetString(reader.GetOrdinal("Description"));
                                organizationReport.TotalCampaigns = reader.GetInt32(reader.GetOrdinal("TotalCampaigns"));
                                organizationReport.TotalProducts = reader.GetInt32(reader.GetOrdinal("TotalProducts"));

                                // Add the OrganizationReport object to the list
                                countCampaignAndProductsToOrgList.Add(organizationReport);
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

            return countCampaignAndProductsToOrgList;
        }
       
    }
}
