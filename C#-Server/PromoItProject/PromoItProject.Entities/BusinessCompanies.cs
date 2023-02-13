using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class BusinessCompanies: BaseEntity
    {
        public BusinessCompanies(Logger log) : base(log) { }

        public Dictionary<int, BusinessCompany> GetAllBusinessCompaniesFromDB()
        {
            Dictionary<int, BusinessCompany> businessCompanyDic = null;
            try
            {
                businessCompanyDic = new Dictionary<int, BusinessCompany>();
                Data.Sql.BusinessCompanySql businessCompanySql = new Data.Sql.BusinessCompanySql(base.Log);
                businessCompanyDic = (Dictionary<int, BusinessCompany>)businessCompanySql.LoadBusinessCompanies();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return businessCompanyDic;
        }

        public BusinessCompany GetBusinessCompanyFromDbByEmail(string email)
        {
            BusinessCompany businessCompany = null;
            try
            {
                Data.Sql.BusinessCompanySql businessCompanySql = new Data.Sql.BusinessCompanySql(base.Log);
                businessCompany = (BusinessCompany)businessCompanySql.LoadOneBusinessCompanyObjectByEmail(email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return businessCompany;
        }

        public void InsertBusinessCompanyToDB(string businessName, string email)
        {
            try
            {
                Data.Sql.BusinessCompanySql businessCompanySql = new Data.Sql.BusinessCompanySql(base.Log);
                businessCompanySql.InsertBusinessCompanynToDB(businessName, email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }          
        }

        public void UpdateBusinessCompanyInDB(int businessID, string businessName)
        {
            try
            {
                Data.Sql.BusinessCompanySql businessCompanySql = new Data.Sql.BusinessCompanySql(base.Log);
                businessCompanySql.UpdateBusinessCompanyByID(businessID, businessName);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void DeleteBusinessCompanyFromDB(int businessID)
        {
            try
            {
                Data.Sql.BusinessCompanySql businessCompanySql = new Data.Sql.BusinessCompanySql(base.Log);
                businessCompanySql.DeleteBusinessCompanyByID(businessID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public Dictionary<int, Shipment> GetAllShipmentsFromDB()
        {
            Dictionary<int, Shipment> shipmentsDic = null;
            try
            {
                shipmentsDic = new Dictionary<int, Shipment>();
                Data.Sql.ShipmentSql shipmentSql = new Data.Sql.ShipmentSql(base.Log);
                shipmentsDic = (Dictionary<int, Shipment>)shipmentSql.LoadShipments();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return shipmentsDic;
        }

        public List<Shipment> GetAllShipmentsByBusinessID(int businessID)
        {
            List<Shipment> shipmentsList = null;
            try
            {
                shipmentsList = new List<Shipment>();
                Data.Sql.ShipmentSql shipmentSql = new Data.Sql.ShipmentSql(base.Log);
                shipmentsList = shipmentSql.AddShipmentsToListByBusinessID(businessID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }

            return shipmentsList;
        }

        public List<BusinessReport> GetCountOfDonations()
        {
            List<BusinessReport> businessReportsList = null;
            try
            {
                businessReportsList = new List<BusinessReport>();
                Data.Sql.BusinessReportSql businessReportSql = new Data.Sql.BusinessReportSql(base.Log);
                businessReportsList = businessReportSql.CountOfDonations();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return businessReportsList;
        }
    }
}
