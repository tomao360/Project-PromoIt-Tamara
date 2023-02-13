using PromoItProject.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoItProject.Model;
using System.Windows.Input;
using System.Data;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class CampaignSql: BaseDataSql
    {
        public CampaignSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the campaigns to dictionary 
        public Dictionary<int, Campaign> AddCampaignToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of Campaign
            Dictionary<int, Campaign> campaignsDic = new Dictionary<int, Campaign>();

            // Clear the dictionary before adding new data
            campaignsDic.Clear();

            while (reader.Read())
            {
                Campaign campaign = new Campaign();

                // Get the values for the properties of the Campaign object from the SQL Stored Procedure
                campaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                campaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                campaign.LinkToLandingPage = reader.GetString(reader.GetOrdinal("LinkToLandingPage"));
                campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                campaign.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                campaign.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                // Add the Campaign object to the dictionary
                campaignsDic.Add(campaign.CampaignID, campaign);
            }

            return campaignsDic;
        }

        public object LoadCampaigns()
        {
            object retDictionary = null;

            try
            {
                // GetAllCampaigns => Stored Procedure that: update the DeleteAnswer field to 'Has more records' for campaigns that have products in the Donated_Products table, and select all columns from the Campaigns table             
                string storedProcedure = "GetAllCampaigns";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddCampaignToDictionary);
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

        // A function that adds campaigns to a list by organization email
        public List<Campaign> AddCampaignToList(string email)
        {
            // Create a new list of Campaign
            List<Campaign> campaignsList = new List<Campaign>();

            // Clear the list before adding new data
            campaignsList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetAllCampaignsByEmail => Stored Procedure that: update the DeleteAnswer field to 'Has more records' for campaigns that have products in the Donated_Products table, and select all columns from the Campaigns table by organization email 
                    using (SqlCommand command = new SqlCommand("GetAllCampaignsByEmail", connection))
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
                                Campaign campaign = new Campaign();

                                // Get the values for the properties of the Campaign object from the SQL Stored Procedure
                                campaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                campaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                campaign.LinkToLandingPage = reader.GetString(reader.GetOrdinal("LinkToLandingPage"));
                                campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                campaign.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                                campaign.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                                // Add the Campaign object to the list
                                campaignsList.Add(campaign);
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

            return campaignsList;
        }


        // A function that inserts a campaign to the Campaigns table in SQL
        public void InsertCampaignToDB(int organizationID, string campaignName, string linkToLandingPage, string hashtag)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertCampaignToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@organizationID", organizationID);
                        command.Parameters.AddWithValue("@campaignName", campaignName);
                        command.Parameters.AddWithValue("@linkToLandingPage", linkToLandingPage);
                        command.Parameters.AddWithValue("@hashtag", hashtag);
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


        // A function that updates a campaign at the Campaigns table in SQL 
        public void UpdateCampaignByID(int campaignID, string campaignName, string linkToLandingPage, string hashtag)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateCampaignByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@campaignID", campaignID);
                        command.Parameters.AddWithValue("@campaignName", campaignName);
                        command.Parameters.AddWithValue("@linkToLandingPage", linkToLandingPage);
                        command.Parameters.AddWithValue("@hashtag", hashtag);

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


        // A function that deletes a campaign from the Campaigns table in SQL
        public void DeleteCampaignByID(int campaignID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteCampaignByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@campaignID", campaignID);
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

