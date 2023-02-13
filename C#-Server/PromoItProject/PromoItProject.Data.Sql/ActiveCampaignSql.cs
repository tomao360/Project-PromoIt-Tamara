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
    public class ActiveCampaignSql: BaseDataSql
    {
        public ActiveCampaignSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the active campaigns to dictionary 
        public Dictionary<int, ActiveCampaign> AddActiveCampaignToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of ActiveCampaign
            Dictionary<int, ActiveCampaign> activeCampaignsDic = new Dictionary<int, ActiveCampaign>();

            // Clear the dictionary before adding new data
            activeCampaignsDic.Clear();

            while (reader.Read())
            {
                ActiveCampaign activeCampaign = new ActiveCampaign();

                // Get the values for the properties of the ActiveCampaign object from the SQL query
                activeCampaign.ActiveCampID = reader.GetInt32(reader.GetOrdinal("ActiveCampID"));
                activeCampaign.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                activeCampaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                activeCampaign.TwitterUserName = reader.GetString(reader.GetOrdinal("TwitterUserName"));
                activeCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                activeCampaign.MoneyEarned = reader.GetInt32(reader.GetOrdinal("MoneyEarned"));
                activeCampaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                activeCampaign.TweetsNumber = reader.GetInt32(reader.GetOrdinal("TweetsNumber"));

                // Add the ActiveCampaign object to the dictionary
                activeCampaignsDic.Add(activeCampaign.ActiveCampID, activeCampaign);
            }

            return activeCampaignsDic;
        }

        public object LoadActiveCampaigns()
        {
            object retDictionary = null;

            try
            {
                // SQL query to select all columns from the Active_Campaigns table
                string sqlQuery = "select * from Active_Campaigns";
                retDictionary = SqlQuery.RunCommandResult(sqlQuery, AddActiveCampaignToDictionary);
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

     
        // A function that adds all the active campaigns to a list (by activistID)
        public List<ActiveCampaign> ActiveCampaignsListByActivist(int activistID)
        {
            // Create a new list of ActiveCampaign
            List<ActiveCampaign> activeCampaignsList = new List<ActiveCampaign>();

            // Clear the list before adding new data
            activeCampaignsList.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetActiveCampaignsForActivist => Stored Procedure to select all columns from the Active_Campaigns table where the ActivistID matches the provided ID
                    using (SqlCommand command = new SqlCommand("GetActiveCampaignsForActivist", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the command
                        command.Parameters.AddWithValue("@activistID", activistID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActiveCampaign activeCampaign = new ActiveCampaign();

                                // Get the values for the properties of the ActiveCampaign object from the SQL Stored Procedure
                                activeCampaign.ActiveCampID = reader.GetInt32(reader.GetOrdinal("ActiveCampID"));
                                activeCampaign.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                                activeCampaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                activeCampaign.TwitterUserName = reader.GetString(reader.GetOrdinal("TwitterUserName"));
                                activeCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                activeCampaign.MoneyEarned = reader.GetInt32(reader.GetOrdinal("MoneyEarned"));
                                activeCampaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                activeCampaign.TweetsNumber = reader.GetInt32(reader.GetOrdinal("TweetsNumber"));

                                // Add the ActiveCampaign object to the list
                                activeCampaignsList.Add(activeCampaign);
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

            return activeCampaignsList;
        }


        // A function that adds all the active campaigns and their products to a list (by activistID)
        public List<ActiveCampaignProduct> LoadProductTableByActivistID(int activistID)
        {
            // Create a new list of ActiveCampaignProduct
            List<ActiveCampaignProduct> activeCampaignProductsList = new List<ActiveCampaignProduct>();

            // Clear the list before adding new data
            activeCampaignProductsList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // GetDonatedProductsAndActiveCampaignsForActivist => Stored Procedure to select the active campaigns and their products by a specific activistID
                    using (SqlCommand command = new SqlCommand("GetDonatedProductsAndActiveCampaignsForActivist", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the command
                        command.Parameters.AddWithValue("@activistID", activistID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActiveCampaignProduct activeCampaignProduct = new ActiveCampaignProduct();

                                // Get the values for the properties of the ActiveCampaignProduct object from the SQL Stored Procedure
                                activeCampaignProduct.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                activeCampaignProduct.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                                activeCampaignProduct.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                                activeCampaignProduct.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                                activeCampaignProduct.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                activeCampaignProduct.Bought = reader.GetString(reader.GetOrdinal("Bought"));
                                activeCampaignProduct.Shipped = reader.GetString(reader.GetOrdinal("Shipped"));
                                activeCampaignProduct.ActiveCampID = reader.GetInt32(reader.GetOrdinal("ActiveCampID"));
                                activeCampaignProduct.ActivistID = reader.GetInt32(reader.GetOrdinal("ActivistID"));
                                activeCampaignProduct.TwitterUserName = reader.GetString(reader.GetOrdinal("TwitterUserName"));
                                activeCampaignProduct.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                                activeCampaignProduct.MoneyEarned = reader.GetInt32(reader.GetOrdinal("MoneyEarned"));
                                activeCampaignProduct.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                activeCampaignProduct.TweetsNumber = reader.GetInt32(reader.GetOrdinal("TweetsNumber"));

                                // Add the ActiveCampaignProduct object to the list
                                activeCampaignProductsList.Add(activeCampaignProduct);
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

            return activeCampaignProductsList;
        }


        // A function that inserts an active campaign to the Active_Campaigns table in SQL
        public void InsertActiveCampaignToDB(int activistID, int campaignID, string twitterUserName, string hashtag, string campaignName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertActiveCampaignToDB", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@activistID", activistID);
                        command.Parameters.AddWithValue("@campaignID", campaignID);
                        command.Parameters.AddWithValue("@twitterUserName", twitterUserName);
                        command.Parameters.AddWithValue("@hashtag", hashtag);
                        command.Parameters.AddWithValue("@moneyEarned", 0);
                        command.Parameters.AddWithValue("@campaignName", campaignName);
                        command.Parameters.AddWithValue("@tweetsNumber", 0);

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


        // A function that updates an active campaign's MoneyEarned column at the Active_Campaigns table in SQL (adds money)
        public void UpdateActiveCampaignAddMoneyByID(int activeCampID, int moneyEarned, int tweetsNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateActiveCampaignAddMoneyByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@activeCampID", activeCampID);
                        command.Parameters.AddWithValue("@moneyEarned", moneyEarned);
                        command.Parameters.AddWithValue("@tweetsNumber", tweetsNumber);

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

        // A function that updates an active campaign's MoneyEarned column at the Active_Campaigns table in SQL (subtract money)
        public void UpdateActiveCampaignSubtractMoneyByID(int activeCampID, int moneyEarned)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateActiveCampaignSubtractMoneyByID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@activeCampID", activeCampID);
                        command.Parameters.AddWithValue("@moneyEarned", moneyEarned);

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
