using NUnit.Framework;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(7)]
    internal class TestDelete: BaseEntitiesTest
    {
        [Test]
        public void Cleanup()
        {
            businessCompanies.DeleteBusinessCompanyFromDB((int)businessID);
            Dictionary<int, BusinessCompany> businessCompanyDic = businessCompanies.GetAllBusinessCompaniesFromDB();
            Assert.IsFalse(businessCompanyDic.ContainsKey((int)businessID), $"The business comapny with the ID:'{businessID}' was not deleted from the database.");

            activists.DeleteActivistFromDB((int)activistID);
            Dictionary<int, Activist> activistsDic = activists.GetAllActivistsFromDB();
            Assert.IsFalse(activistsDic.ContainsKey((int)activistID), $"The social activist with the ID:'{activistID}' was not deleted from the database.");

            campaigns.DeleteCampaignByID((int)campaignID);
            Dictionary<int, Campaign> campaignsDic = campaigns.GetAllCampaignsFromDB();
            Assert.IsFalse(campaignsDic.ContainsKey((int)campaignID), $"The campaign with the ID:'{campaignID}' was not deleted from the database.");


            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);
            Assert.IsNotNull(nonProfitOrganization, $"The organization with the ID:{organizationID} does not exist in the database.");
            nonProfitOrganizations.DeleteOrganizationFromDB((int)organizationID);
            Dictionary<int, NonProfitOrganization> organizationsDic = nonProfitOrganizations.GetAllOrganizationsFromDB();
            Assert.IsFalse(organizationsDic.ContainsKey((int)organizationID), $"The organization with the ID:'{organizationID}' was not deleted from the database.");
        }
    }
}
