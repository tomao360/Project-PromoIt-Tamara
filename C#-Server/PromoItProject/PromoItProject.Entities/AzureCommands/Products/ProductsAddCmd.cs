using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Products
{
    public class ProductsAddCmd: BaseCommand, ICommand
    {
        public ProductsAddCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a DonatedProduct object
                    DonatedProduct donatedProduct = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>((string)param[1]);

                    // Check if all required fields are present
                    if (donatedProduct.ProductName != "" && donatedProduct.Price != null && donatedProduct.BusinessID != null && donatedProduct.CampaignID != null)
                    {
                        Log.LogEvent($"Start to insert the Donated Product - '{donatedProduct.ProductName}' to DB (Execute function in ProductsAddCmd class)");
                        // Insert the product into the DB
                        MainManager.Instance.donatedProducts.InsertDonatedProductToDB(donatedProduct.ProductName, donatedProduct.Price, donatedProduct.BusinessID, donatedProduct.CampaignID);

                        Log.LogEvent($"Donated Product ('{donatedProduct.ProductName}') inserted successfully into the DB");
                        response = "Donated Product inserted successfully into the DB";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while inserting the Donated Product - '{donatedProduct.ProductName}' into the DB in the Execute function in ProductsAddCmd class");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("A Donated Product object was not received from the client in the Execute function in ProductsAddCmd class");
                return null;
            }
        }
    }
}
