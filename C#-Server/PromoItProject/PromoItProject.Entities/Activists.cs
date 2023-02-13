using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class Activists: BaseEntity
    {
        public Activists(Logger log) : base(log) { }


        List<ActivistReport> activistReportsList = new List<ActivistReport>();

        public Dictionary<int, Activist> GetAllActivistsFromDB()
        {
            Dictionary<int, Activist> activistsDic = null;
            try
            {
                activistsDic = new Dictionary<int, Activist>();
                Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql(base.Log);
                activistsDic = (Dictionary<int, Activist>)activistSql.LoadActivists();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
          
            return activistsDic;
        }

        public Activist GetActivistFromDbByEmail(string email)
        {
            Activist activist = null;
            try
            {
                Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql(base.Log);
                activist = (Activist)activistSql.LoadOneActivistObjectByEmail(email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return activist;
        }

        public void InsertActivistToDB(string fullName, string email, string address, string phoneNumber)
        {
            try
            {
                Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql(base.Log);
                activistSql.InsertActivistToDB(fullName, email, address, phoneNumber);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void UpdateActivistInDB(int activistID, string fullName, string address, string phoneNumber)
        {
            try
            {
                Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql(base.Log);
                activistSql.UpdateActivistByID(activistID, fullName, address, phoneNumber);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }           
        }

        public void DeleteActivistFromDB(int activistID)
        {
            try
            {
                Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql(base.Log);
                activistSql.DeleteActivistByID(activistID);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }            
        }
      
        public List<ActivistReport> GetMostMoneyEarned()
        {
            try
            {
                Data.Sql.ActivistReportSql activistReportSql = new Data.Sql.ActivistReportSql(base.Log);
                activistReportsList = activistReportSql.MostMoneyEarned();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return activistReportsList;
        }

        public List<ActivistReport> GetMostPromotedCampaigns()
        {
            try
            {
                Data.Sql.ActivistReportSql activistReportSql = new Data.Sql.ActivistReportSql(base.Log);
                activistReportsList = activistReportSql.MostPromotedCampaigns();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }

            return activistReportsList;
        }
    }
}
