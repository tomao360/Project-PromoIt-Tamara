using NUnit.Framework;
using PromoItProject.DAL;
using PromoItProject.Data.Sql;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture]
    internal class BaseEntitiesTest
    {
        public string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public string emailOrganization;
        public static string campaignName = "";
        public static string hashtag = "";
        public static int? organizationID = null;
        public static int? campaignID = null;
        public static int? businessID = null;
        public static int? productID = null;
        public static int? activistID = null;
        public Users users;
        public NonProfitOrganizations nonProfitOrganizations;
        public Campaigns campaigns;
        public BusinessCompanies businessCompanies;
        public DonatedProducts donatedProducts;
        public Activists activists;
        public ActiveCampaigns activeCampaigns;
        public ContactUsMessages contactUsMessages;

        [OneTimeSetUp]
        public void Init()
        {
            emailOrganization = "organization@test.com";

            // Initializa the entities
            users = MainManager.Instance.users;
            nonProfitOrganizations = MainManager.Instance.nonProfitOrganizations;
            campaigns = MainManager.Instance.campaigns;
            businessCompanies = MainManager.Instance.businessCompanies;
            donatedProducts = MainManager.Instance.donatedProducts;
            activists = MainManager.Instance.activists;
            activeCampaigns = MainManager.Instance.activeCampaigns;
            contactUsMessages = MainManager.Instance.ContactUsMessages;

            // Initialize the connection strings in the Data.Sql, and DAL layers
            SqlQuery.connectionString = connectionString;
            UserSql.connectionString = connectionString;
            NonProfitOrganizationSql.connectionString = connectionString;
            OrganizationReportSql.connectionString = connectionString;
            CampaignSql.connectionString = connectionString;
            BusinessCompanySql.connectionString = connectionString;
            ShipmentSql.connectionString = connectionString;
            BusinessReportSql.connectionString = connectionString;
            DonatedProductSql.connectionString = connectionString;
            ProductCampaignORGSql.connectionString = connectionString;
            ActivistSql.connectionString = connectionString;
            ActiveCampaignSql.connectionString = connectionString;
            ActivistReportSql.connectionString = connectionString;
            ContactUsMessageSql.connectionString = connectionString;
        }

    }
}
