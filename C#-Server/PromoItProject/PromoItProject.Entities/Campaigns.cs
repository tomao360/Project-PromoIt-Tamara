using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class Campaigns: BaseEntity
    {
        public Campaigns(Logger log) : base(log) { }


        List<OrganizationReport> organizationReportsList = new List<OrganizationReport>();

        public Dictionary<int, Campaign> GetAllCampaignsFromDB()
        {
            Dictionary<int, Campaign> campaignsDic = null;  
            try
            {
                campaignsDic = new Dictionary<int, Campaign>();
                Data.Sql.CampaignSql campaignSql = new Data.Sql.CampaignSql(base.Log);
                campaignsDic = (Dictionary<int, Campaign>)campaignSql.LoadCampaigns();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return campaignsDic;
        }

        public List<Campaign> GetAllCampaignsFromDBByORGEmail(string email)
        {
            List<Campaign> campaignsList = null;
            try
            {
                campaignsList = new List<Campaign>();
                Data.Sql.CampaignSql campaignSql = new Data.Sql.CampaignSql(base.Log);
                campaignsList = campaignSql.AddCampaignToList(email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
           
            return campaignsList;
        }

        public void InsertCampaignToDB(int organizationID, string campaignName, string linkToLandingPage, string hashtag)
        {
            try
            {
                Data.Sql.CampaignSql campaignSql = new Data.Sql.CampaignSql(base.Log);
                campaignSql.InsertCampaignToDB(organizationID, campaignName, linkToLandingPage, hashtag);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }

        public void UpdateCampaignInDB(int campaignID, string campaignName, string linkToLandingPage, string hashtag)
        {
            try
            {
                Data.Sql.CampaignSql campaignSql = new Data.Sql.CampaignSql(base.Log);
                campaignSql.UpdateCampaignByID(campaignID, campaignName, linkToLandingPage, hashtag);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void DeleteCampaignByID(int campaignID)
        {
            try
            {
                Data.Sql.CampaignSql campaignSql = new Data.Sql.CampaignSql(base.Log);
                campaignSql.DeleteCampaignByID(campaignID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }            
        }

        public Dictionary<int, OrganizationAndCampaign> GetAllOrganizationsAndCampaignsFromDB()
        {
            Dictionary<int, OrganizationAndCampaign> organizationAndCampaignDic = null;
            try
            {
                organizationAndCampaignDic = new Dictionary<int, OrganizationAndCampaign>();
                Data.Sql.OrganizationAndCampaignSql organizationAndCampaignSql = new Data.Sql.OrganizationAndCampaignSql(base.Log);
                organizationAndCampaignDic = (Dictionary<int, OrganizationAndCampaign>)organizationAndCampaignSql.LoadOrganizationAndCampaignTable();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return organizationAndCampaignDic;
        }

        public List<OrganizationReport> GetMostPopularCampaign()
        {
            try
            {
                Data.Sql.OrganizationReportSql organizationReportSql = new Data.Sql.OrganizationReportSql(base.Log);
                organizationReportsList = organizationReportSql.MostPopularCampaign();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
           
            return organizationReportsList;
        }

        public List<OrganizationReport> GetMostProfitableCampaign()
        {
            try
            {
                Data.Sql.OrganizationReportSql organizationReportSql = new Data.Sql.OrganizationReportSql(base.Log);
                organizationReportsList = organizationReportSql.MostProfitableCampaign();
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
