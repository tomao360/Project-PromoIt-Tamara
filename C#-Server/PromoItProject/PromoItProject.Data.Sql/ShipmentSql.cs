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
    public class ShipmentSql: BaseDataSql
    {
        public ShipmentSql(Logger log) : base(log) { }


        // Connection string
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        // A delegate function that adds the Shipment to dictionary 
        public Dictionary<int, Shipment> AddShipmentToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of Shipment
            Dictionary<int, Shipment> shipmentsDic = new Dictionary<int, Shipment>();

            // Clear the dictionary before adding new data
            shipmentsDic.Clear();

            while (reader.Read())
            {
                Shipment shipment = new Shipment();

                // Get the values for the properties of the Shipment object from the SQL Stored Procedure
                shipment.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                shipment.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                shipment.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                shipment.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                shipment.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                shipment.Bought = reader.GetString(reader.GetOrdinal("Bought"));
                shipment.Shipped = reader.GetString(reader.GetOrdinal("Shipped"));
                shipment.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                shipment.Email = reader.GetString(reader.GetOrdinal("Email"));
                shipment.Address = reader.GetString(reader.GetOrdinal("Address"));
                shipment.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                // Add the Shipment object to the dictionary
                shipmentsDic.Add(shipment.ProductID, shipment);
            }

            return shipmentsDic;
        }

        public object LoadShipments()
        {
            object retDictionary = null;

            try
            {
                // GetBusinessCompaniesShipments => Stored Procedure to select columns from the Donated_Products, and Social_Activists tables
                string storedProcedure = "GetBusinessCompaniesShipments";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddShipmentToDictionary);
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


        // A function that adds shipments to a list by business company ID
        public List<Shipment> AddShipmentsToListByBusinessID(int businessID)
        {
            // Create a new list of Shipment
            List<Shipment> shipmentsList = new List<Shipment>();

            // Clear the list before adding new data
            shipmentsList.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("BusinessCompanyShipmentsByBusinessID", connection))
                    {
                        connection.Open();
                        // Set the command type as Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add id as a parameter to the query
                        command.Parameters.AddWithValue("@businessID", businessID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Shipment shipment = new Shipment();

                                // Get the values for the properties of the Shipment object from the SQL Stored Procedure
                                shipment.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                shipment.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                                shipment.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                                shipment.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                                shipment.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                                shipment.Bought = reader.GetString(reader.GetOrdinal("Bought"));
                                shipment.Shipped = reader.GetString(reader.GetOrdinal("Shipped"));
                                shipment.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                                shipment.Email = reader.GetString(reader.GetOrdinal("Email"));
                                shipment.Address = reader.GetString(reader.GetOrdinal("Address"));
                                shipment.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                                // Add the Shipment object to the list
                                shipmentsList.Add(shipment);
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

            return shipmentsList;
        }

    }
}
