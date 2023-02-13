using PromoItProject.Data.Sql;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class ActiveCampaigns: BaseEntity
    {
        public ActiveCampaigns(Logger log) : base(log) { }

        public Dictionary<int, ActiveCampaign> GetAllActiveCampaignsFromDB()
        {
            Dictionary<int, ActiveCampaign> activeCampaignsDic = null;
            try
            {
                activeCampaignsDic = new Dictionary<int, ActiveCampaign>();
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignsDic = (Dictionary<int, ActiveCampaign>)activeCampaignSql.LoadActiveCampaigns();
                
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }

            return activeCampaignsDic;
        }

        public List<ActiveCampaignProduct> GetAllProductTableByActivistID(int activistID)
        {
            List<ActiveCampaignProduct> activeCampaignProductsList = null;
            try
            {
                activeCampaignProductsList = new List<ActiveCampaignProduct>();
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignProductsList = activeCampaignSql.LoadProductTableByActivistID(activistID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
           
            return activeCampaignProductsList;
        }

        public List<ActiveCampaign> GetActiveCampaignsByActivistID(int activistID)
        {
            List<ActiveCampaign> activeCampaignsList = null;
            try
            {
                activeCampaignsList = new List<ActiveCampaign>();
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignsList = activeCampaignSql.ActiveCampaignsListByActivist(activistID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return activeCampaignsList;
        }

        public void InsertActiveCampaignToDB(int activistID, int campaignID, string twitterUserName, string hashtag, string campaignName)
        {
            try
            {
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignSql.InsertActiveCampaignToDB(activistID, campaignID, twitterUserName, hashtag, campaignName);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void UpdateActiveCampaignAddMoneyInDB(int activeCampID, int moneyEarned, int tweetsNumber)
        {
            try
            {
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignSql.UpdateActiveCampaignAddMoneyByID(activeCampID, moneyEarned, tweetsNumber);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void UpdateActiveCampaignSubtractMoneyInDB(int activeCampID, int moneyEarned)
        {
            try
            {
                Data.Sql.ActiveCampaignSql activeCampaignSql = new Data.Sql.ActiveCampaignSql(base.Log);
                activeCampaignSql.UpdateActiveCampaignSubtractMoneyByID(activeCampID, moneyEarned);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }
    }
}
