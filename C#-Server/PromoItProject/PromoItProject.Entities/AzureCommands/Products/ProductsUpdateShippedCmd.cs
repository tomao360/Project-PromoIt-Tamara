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
    public class ProductsUpdateShippedCmd: BaseCommand, ICommand
    {
        public ProductsUpdateShippedCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    string response;
                    // Deserialize the request body into a DonatedProduct object
                    DonatedProduct donatedProduct2 = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>((string)param[1]);

                    // Check if the product ID is present
                    if (donatedProduct2.ProductID != null)
                    {
                        Log.LogEvent($"Started updating the Donated Product Shipped status ('{donatedProduct2.ProductID}') in the DB (Execute function in ProductsUpdateBoughtCmd class)");
                        // Update the product in the DB
                        MainManager.Instance.donatedProducts.UpdateDonatedProductsShippedStatusInDB(int.Parse((string)param[0]));

                        Log.LogEvent($"Donated Product Shipped status - '{donatedProduct2.ProductID}' updated successfully");
                        response = "Donated Product updated successfully";
                        return response;
                    }
                    else
                    {
                        // Return a failure message if the required fields are not present
                        Log.LogError($"A problem occurred while updating the Donated Product ('{donatedProduct2.ProductID}') in the DB - Execute function in ProductsUpdateShippedCmd class");
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
                Log.LogError("No parameters were received from the client in the Execute function in ProductsUpdateShippedCmd class");
                return null;
            }
        }
    }
}
