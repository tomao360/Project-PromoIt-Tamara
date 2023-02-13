using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class DonatedProducts: BaseEntity
    {
        public DonatedProducts(Logger log) : base(log) { }

        public void InsertDonatedProductToDB(string productName, decimal price, int businessID, int campaignID)
        {
            try
            {
                Data.Sql.DonatedProductSql donatedProductSql = new Data.Sql.DonatedProductSql(base.Log);
                donatedProductSql.InsertDonatedProductToDB(productName, price, businessID, campaignID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }    
        }

        public void UpdateDonatedProductsBoughtStatusInDB(int productID)
        {
            try
            {
                Data.Sql.DonatedProductSql donatedProductSql = new Data.Sql.DonatedProductSql(base.Log);
                donatedProductSql.UpdateProductBoughtStatus(productID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public void UpdateDonatedProductsShippedStatusInDB(int productID)
        {
            try
            {
                Data.Sql.DonatedProductSql donatedProductSql = new Data.Sql.DonatedProductSql(base.Log);
                donatedProductSql.UpdateProductShippedStatus(productID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }     
        }

        public Dictionary<int, ProductCampaignORG> GetAllProductsCampaignsORGFromDB()
        {
            Dictionary<int, ProductCampaignORG> productCampaignORGDic = null;
            try
            {
                productCampaignORGDic = new Dictionary<int, ProductCampaignORG>();
                Data.Sql.ProductCampaignORGSql productCampaignORGSql = new Data.Sql.ProductCampaignORGSql(base.Log);
                productCampaignORGDic = (Dictionary<int, ProductCampaignORG>)productCampaignORGSql.LoadProductCampaignORGTable();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return productCampaignORGDic;
        }

        public List<ProductCampaignORG> GetAllProductsCampaignsORGByByCompanyID(int businessID)
        {
            List<ProductCampaignORG> productCampaignORGList = null;
            try
            {
                productCampaignORGList = new List<ProductCampaignORG>();
                Data.Sql.ProductCampaignORGSql productCampaignORGSql = new Data.Sql.ProductCampaignORGSql(base.Log);
                productCampaignORGList = productCampaignORGSql.LoadProductCampaignORGTableByCompanyID(businessID);
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
