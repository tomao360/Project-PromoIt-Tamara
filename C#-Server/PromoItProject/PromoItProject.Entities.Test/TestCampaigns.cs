using NUnit.Framework;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(3)]
    internal class TestCampaigns: BaseEntitiesTest
    {
        private string linkToLandingPage;

        [Test, Order(1), Category("Campaign Test")]
        public void InsertCampaignToDB_ValidInputs_ShouldInsertCampaign()
        {
            // Arrange
            campaignName = "Campaign Test";
            linkToLandingPage = "https://www.test.com";
            hashtag = "#campaigntest";
            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);
            Assert.IsNotNull(nonProfitOrganization, $"The organization with the email:{emailOrganization} does not exist in the database.");


            // Act
            campaigns.InsertCampaignToDB(nonProfitOrganization.OrganizationID, campaignName, linkToLandingPage, hashtag);

            // Assert
            // Verify that the campaign was inserted correctly in the database
            Dictionary<int, Campaign> campaignsDic = campaigns.GetAllCampaignsFromDB();
            Assert.That(campaignsDic.Count(), Is.AtLeast(1));
            Assert.That(campaignsDic.Values.Any(c => c.OrganizationID == nonProfitOrganization.OrganizationID &&
                                              c.CampaignName == campaignName &&
                                              c.LinkToLandingPage == linkToLandingPage &&
                                              c.Hashtag == hashtag), $"The campaign with the name:'{campaignName}', website: '{linkToLandingPage}', and hashtag:'{hashtag}' was not inserted into the database.");
        }



        [Test, Order(2), Category("Campaign Test")]
        public void GetAllCampaignsFromDBByORGEmail_ValidOrganizationEmail_ShouldReturnNonEmptyList()
        {
            // Arrange
            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);
            Assert.IsNotNull(nonProfitOrganization, $"The organization with the email:{emailOrganization} does not exist in the database.");

            // Act
            List<Campaign> campaignsList = campaigns.GetAllCampaignsFromDBByORGEmail(nonProfitOrganization.Email);

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(campaignsList, "The List is empty");
            Assert.Greater(campaignsList.Count(), 0);
        }



        [Test, Order(3), Category("Campaign Test")]
        public void GetAllCampaignsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, Campaign> campaignsDic = campaigns.GetAllCampaignsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(campaignsDic, "The Dictionary is empty");
            Assert.Greater(campaignsDic.Count(), 0);
        }



        [Test, Order(4), Category("Campaign Test")]
        public void GetAllOrganizationsAndCampaignsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, OrganizationAndCampaign> organizationAndCampaignDic = campaigns.GetAllOrganizationsAndCampaignsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(organizationAndCampaignDic, "The Dictionary is empty");
            Assert.Greater(organizationAndCampaignDic.Count(), 0);
        }


        [Test, Order(5), Category("Campaign Test")]
        public void UpdateCampaignInDB_ValidInput_ShouldUpdateOrganization()
        {
            // Arrange
            Dictionary<int, Campaign> campaignsDic = campaigns.GetAllCampaignsFromDB();
            Assert.IsNotNull(campaignsDic, "The Dictionary is empty");
            campaignID = campaignsDic.OrderByDescending(c => c.Value.CampaignID).FirstOrDefault().Key;
            Assert.IsNotNull(campaignID, $"The organization with the email:{emailOrganization} does not exist in the database.");

            // Update the campaign data
            campaignName = "Test Campaign Update";
            linkToLandingPage = "https://www.updatedtest.com";
            hashtag = "#updatedtest";


            // Act
            campaigns.UpdateCampaignInDB((int)campaignID, campaignName, linkToLandingPage, hashtag);

            // Assert
            // Verify that the organization was updated successfully
            campaignsDic = campaigns.GetAllCampaignsFromDB();
            Assert.That(campaignsDic.ContainsKey((int)campaignID), "The campaign was not found in the database.");
            Assert.That(campaignsDic[(int)campaignID].CampaignName == campaignName, $"The campaign name was not updated to '{campaignName}'.");
            Assert.That(campaignsDic[(int)campaignID].LinkToLandingPage == linkToLandingPage, $"The campaign landing page was not updated to '{linkToLandingPage}'.");
            Assert.That(campaignsDic[(int)campaignID].Hashtag == hashtag, $"The campaign hashtg was not updated to '{hashtag}'.");
        }



        //[Test, Order(6), Category("Campaign Test")]
        public void DeleteOrganizationFromDB_ValidInput_ShouldDeleteOrganization()
        {
            // Act
            campaigns.DeleteCampaignByID((int)campaignID);

            // Assert
            Dictionary<int, Campaign> campaignsDic = campaigns.GetAllCampaignsFromDB();
            Assert.IsFalse(campaignsDic.ContainsKey((int)campaignID), $"The campaign with the ID:'{campaignID}' was not deleted from the database.");
        }
    }
}
