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
    public class ProductCampaignORGSql: BaseDataSql
    {
        public ProductCampaignORGSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the ProductCampaignORG to dictionary 
        public Dictionary<int, ProductCampaignORG> AddProductCampaignORGTToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of ProductCampaignORG
            Dictionary<int, ProductCampaignORG> productCampaignORGDic = new Dictionary<int, ProductCampaignORG>();

            // Clear the dictionary before adding new data
            productCampaignORGDic.Clear();

            while (reader.Read())
            {
                ProductCampaignORG productCampaignORG = new ProductCampaignORG();

                // Get the values for the properties of the ProductCampaignORG object from the SQL Stored Procedure
                productCampaignORG.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                productCampaignORG.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                productCampaignORG.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                productCampaignORG.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                productCampaignORG.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                productCampaignORG.Bought = reader.GetString(reader.GetOrdinal("Bought"));
                productCampaignORG.Shipped = reader.GetString(reader.GetOrdinal("Shipped"));
                productCampaignORG.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                productCampaignORG.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                productCampaignORG.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                productCampaignORG.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));

                // Add the ProductCampaignORG object to the dictionary
                productCampaignORGDic.Add(productCampaignORG.ProductID, productCampaignORG);
            }

            return productCampaignORGDic;
        }

        public object LoadProductCampaignORGTable()
        {
            object retDictionary = null;

            try
            {
                // GetAllDonatedProductsAndCampaignsAndOrganizations => Stored Procedure to select columns from the Campaigns, Donated_Products, and Non_Profit_Organizations tables
                string storedProcedure = "GetAllDonatedProductsAndCampaignsAndOrganizations";

                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddProductCampaignORGTToDictionary);
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

        // A function that adds ProductCampaignORG to a list by businessID
        public List<ProductCampaignORG> LoadProductCampaignORGTableByCompanyID(int businessID)
        {
            // Create a new list of ProductCampaignORG
            List<ProductCampaignORG> productCampaignORGList = new List<ProductCampaignORG>();

            // Clear the list before adding new data
            productCampaignORGList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllDonatedProductsAndCampaignsAndOrganizationsForBusinessID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@businessID", businessID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCampaignORG productCampaignORG = new ProductCampaignORG();

                                // Get the values for the properties of the ProductCampaignORG object from the SQL Stored Procedure
                                productCampaignORG.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                productCampaignORG.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                                productCampaignORG.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                                productCampaignORG.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                                productCampaignORG.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                productCampaignORG.Bought = reader.GetString(reader.GetOrdinal("Bought"));
                                productCampaignORG.Shipped = reader.GetString(reader.GetOrdinal("Shipped"));
                                productCampaignORG.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                productCampaignORG.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));

                                // Add the ProductCampaignORG object to the list
                                productCampaignORGList.Add(productCampaignORG);
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

            return productCampaignORGList;
        }

    }
}
