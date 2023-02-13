using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoItProject.Model;
using Utilities;

namespace PromoItProject.Entities
{
    public class NonProfitOrganizations: BaseEntity
    {
        public NonProfitOrganizations(Logger log) : base(log) { }

        public Dictionary<int, NonProfitOrganization> GetAllOrganizationsFromDB()
        {
            Dictionary<int, NonProfitOrganization> organizationsDic = null;
            try
            {
                organizationsDic = new Dictionary<int, NonProfitOrganization>();
                Data.Sql.NonProfitOrganizationSql nonProfitOrganizationSql = new Data.Sql.NonProfitOrganizationSql(base.Log);
                organizationsDic = (Dictionary<int, NonProfitOrganization>)nonProfitOrganizationSql.LoadOrganizations();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return organizationsDic;
        }

        public NonProfitOrganization GetOrganizationFromDbByEmail(string email)
        {
            NonProfitOrganization nonProfitOrganization = null;
            try
            {
                Data.Sql.NonProfitOrganizationSql nonProfitOrganizationSql = new Data.Sql.NonProfitOrganizationSql(base.Log);
                nonProfitOrganization = (NonProfitOrganization)nonProfitOrganizationSql.LoadOneOrganizationObjectByEmail(email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return nonProfitOrganization;
        }

        public void InsertOrganizationToDB(string organizationName, string linkToWebsite, string email, string description)
        {
            try
            {
                Data.Sql.NonProfitOrganizationSql nonProfitOrganizationSql = new Data.Sql.NonProfitOrganizationSql(base.Log);
                nonProfitOrganizationSql.InsertOrganizationToDB(organizationName, linkToWebsite, email, description);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public void UpdateOrganizationInDB(int organizationID, string organizationName, string linkToWebsite, string description)
        {
            try
            {
                Data.Sql.NonProfitOrganizationSql nonProfitOrganizationSql = new Data.Sql.NonProfitOrganizationSql(base.Log);
                nonProfitOrganizationSql.UpdateOrganizationByID(organizationID, organizationName, linkToWebsite, description);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void DeleteOrganizationFromDB(int organizationID)
        {
            try
            {
                Data.Sql.NonProfitOrganizationSql nonProfitOrganizationSql = new Data.Sql.NonProfitOrganizationSql(base.Log);
                nonProfitOrganizationSql.DeleteOrganizationByID(organizationID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public List<OrganizationReport> GetCountCampaignAndProductsToOrg()
        {
            List<OrganizationReport> organizationReportsList = null;
            try
            {
                organizationReportsList = new List<OrganizationReport>();
                Data.Sql.OrganizationReportSql organizationReportSql = new Data.Sql.OrganizationReportSql(base.Log);
                organizationReportsList = organizationReportSql.CountCampaignAndProductsToOrg();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return organizationReportsList;
        }
    }
}
