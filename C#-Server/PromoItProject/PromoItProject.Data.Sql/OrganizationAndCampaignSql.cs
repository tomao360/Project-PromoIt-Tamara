using PromoItProject.DAL;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class OrganizationAndCampaignSql: BaseDataSql
    {
        public OrganizationAndCampaignSql(Logger log) : base(log) { }

        // A delegate function that adds the organizations and campaigns to dictionary 
        public Dictionary<int, OrganizationAndCampaign> AddOrganizationAndCampaignToDictionary(SqlDataReader reader)
        {
            // Create a new dictionary of OrganizationAndCampaign
            Dictionary<int, OrganizationAndCampaign> organizationAndCampaignDic = new Dictionary<int, OrganizationAndCampaign>();

            // Clear the dictionary before adding new data
            organizationAndCampaignDic.Clear();

            while (reader.Read())
            {
                OrganizationAndCampaign organizationAndCampaign = new OrganizationAndCampaign();

                // Get the values for the properties of the OrganizationAndCampaign object from the SQL Stored Procedure
                organizationAndCampaign.OrganizationID = reader.GetInt32(reader.GetOrdinal("OrganizationID"));
                organizationAndCampaign.OrganizationName = reader.GetString(reader.GetOrdinal("OrganizationName"));
                organizationAndCampaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                organizationAndCampaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                organizationAndCampaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                organizationAndCampaign.LinkToLandingPage = reader.GetString(reader.GetOrdinal("LinkToLandingPage"));
                organizationAndCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                organizationAndCampaign.DeleteAnswer = reader.GetString(reader.GetOrdinal("DeleteAnswer"));

                // Add the OrganizationAndCampaign object to the dictionary
                organizationAndCampaignDic.Add(organizationAndCampaign.CampaignID, organizationAndCampaign);
            }

            return organizationAndCampaignDic;
        }

        public object LoadOrganizationAndCampaignTable()
        {
            object retDictionary = null;

            try
            {
                // GetAllCampaignsOfOrganization => Stored Procedure to select all columns from the Campaigns table and OrganizationName, Description from Non_Profit_Organizations table
                string storedProcedure = "GetAllCampaignsOfOrganization";
                retDictionary = SqlQuery.RunCommandResultStoredProcedure(storedProcedure, AddOrganizationAndCampaignToDictionary);
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
    }
}
