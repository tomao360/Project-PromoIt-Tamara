using NUnit.Framework;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(5)]
    internal class TestActivists : BaseEntitiesTest
    {
        private string fullName;
        private string email;
        private string address;
        private string phoneNumber;
        private string twitterUserName;
        private int? activeCampaignID = null;


        [Test, Order(1), Category("Activist Test")]
        public void InsertActivistToDB_ValidInputs_ShouldInsertNewActivist()
        {
            // Arrange
            fullName = "Avtivist Test";
            email = "activist@test.com";
            address = "Address Test";
            phoneNumber = "0000000000";

            // Act
            activists.InsertActivistToDB(fullName, email, address, phoneNumber);

            // Assert
            // Verify that the activist was inserted correctly in the database
            Dictionary<int, Activist> activistsDic = activists.GetAllActivistsFromDB();
            Assert.That(activistsDic.Count(), Is.AtLeast(1));
            Assert.That(activistsDic.Values.Any(a => a.FullName == fullName &&
                                              a.Email == email &&
                                              a.Address == address &&
                                              a.PhoneNumber == phoneNumber), $"The social activist with the name:'{fullName}', email: '{email}', address:'{address}', and phone number:'{phoneNumber}' was not inserted into the database.");
        }


        [Test, Order(2), Category("Activist Test")]
        public void GetAllActivistsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, Activist> activistsDic = activists.GetAllActivistsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(activistsDic, "The Dictionary is empty");
            Assert.Greater(activistsDic.Count(), 0);
        }


        [Test, Order(3), Category("Activist Test")]
        public void GetActivistFromDbByEmail_ValidEmail_ShouldReturnActivistObject()
        {
            // Act
            Activist activist = activists.GetActivistFromDbByEmail(email);

            // Assert
            Assert.IsNotNull(activist);
            Assert.AreEqual(email, activist.Email);
        }


        [Test, Order(4), Category("Activist Test")]
        public void UpdateActivistInDB_ValidInput_ShouldUpdateOrganization()
        {
            // Arrange
            Activist activist = activists.GetActivistFromDbByEmail(email);
            Assert.IsNotNull(activist, $"The social activist with the email:{email} does not exist in the database.");
            activistID = activist.ActivistID;
            // Update the social activist data
            fullName = "Avtivist Test Update";
            address = "Address Test Update";
            phoneNumber = "0000000001";

            // Act
            activists.UpdateActivistInDB((int)activistID, fullName, address, phoneNumber);

            // Assert
            // Verify that the social activist was updated successfully
            Dictionary<int, Activist> activistsDic = activists.GetAllActivistsFromDB();
            Assert.That(activistsDic.ContainsKey((int)activistID), "The social activist was not found in the database.");
            Assert.That(activistsDic[(int)activistID].FullName == fullName, $"The social activist name was not updated to '{fullName}'.");
            Assert.That(activistsDic[(int)activistID].Address == address, $"The social activist address was not updated to '{address}'.");
            Assert.That(activistsDic[(int)activistID].PhoneNumber == phoneNumber, $"The social activist phone number was not updated to '{phoneNumber}'.");
        }



        [Test, Order(5), Category("Activist Test")]
        public void InsertActiveCampaignToDB_ValidInputs_ShouldInsertActiveCampaign()
        {
            // Arrange
            twitterUserName = "test";

            // Act
            activeCampaigns.InsertActiveCampaignToDB((int)activistID, (int)campaignID, twitterUserName, hashtag, campaignName);

            // Assert
            // Verify that the active campaign was inserted correctly in the database
            Dictionary<int, ActiveCampaign> activeCampaignsDic = activeCampaigns.GetAllActiveCampaignsFromDB();
            Assert.That(activeCampaignsDic.Count(), Is.AtLeast(1));
            Assert.That(activeCampaignsDic.Values.Any(a => a.ActivistID == activistID &&
                                              a.CampaignID == campaignID &&
                                              a.TwitterUserName == twitterUserName &&
                                              a.Hashtag == hashtag &&
                                              a.CampaignName == campaignName), $"The active campaign with the name:'{campaignName}', hashtag: '{hashtag}', and twitter user name:'{twitterUserName}' was not inserted into the database.");
        }


        [Test, Order(6), Category("Activist Test")]
        public void GetAllActiveCampaignsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, ActiveCampaign> activeCampaignsDic = activeCampaigns.GetAllActiveCampaignsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(activeCampaignsDic, "The Dictionary is empty");
            Assert.Greater(activeCampaignsDic.Count(), 0);
        }


        [Test, Order(7), Category("Activist Test")]
        public void UpdateActiveCampaignAddMoneyInDB_ValidInput_ShouldUpdateActiveCampaignAddMoney()
        {
            // Arrange
            Dictionary<int, ActiveCampaign> activeCampaignsDic = activeCampaigns.GetAllActiveCampaignsFromDB();
            Assert.That(activeCampaignsDic.Count(), Is.AtLeast(1));
            activeCampaignID = activeCampaignsDic.OrderByDescending(a => a.Value.ActiveCampID).FirstOrDefault().Key;
            Assert.IsNotNull(activeCampaignID, $"The active campaign does not exist in the database.");
            // Update the active campaign data
            int moneyEarned = 30;
            int tweetsNumber = 30;

            // Act
            activeCampaigns.UpdateActiveCampaignAddMoneyInDB((int)activeCampaignID, moneyEarned, tweetsNumber);

            // Assert
            // Verify that the active campaign was updated successfully
            activeCampaignsDic = activeCampaigns.GetAllActiveCampaignsFromDB();
            Assert.That(activeCampaignsDic.ContainsKey((int)activeCampaignID), "The social activist was not found in the database.");
            Assert.That(activeCampaignsDic[(int)activeCampaignID].MoneyEarned == moneyEarned, $"The active campaign money earned was not updated to '{moneyEarned}'.");
            Assert.That(activeCampaignsDic[(int)activeCampaignID].TweetsNumber == tweetsNumber, $"The active campaign tweets number was not updated to '{tweetsNumber}'.");
        }


        [Test, Order(8), Category("Activist Test")]
        public void GetAllProductTableByActivistID_ValidActivistID_ShouldReturnNonEmptyList()
        {
            // Act
            List<ActiveCampaignProduct> activeCampaignProductsList = activeCampaigns.GetAllProductTableByActivistID((int)activistID);

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(activeCampaignProductsList, "The List is empty");
            Assert.Greater(activeCampaignProductsList.Count(), 0);
        }


        [Test, Order(9), Category("Activist Test")]
        public void GetActiveCampaignsByActivistID_ValidActivistID_ShouldReturnNonEmptyList()
        {
            // Act
            List<ActiveCampaign> activeCampaignsList = activeCampaigns.GetActiveCampaignsByActivistID((int)activistID);

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(activeCampaignsList, "The List is empty");
            Assert.Greater(activeCampaignsList.Count(), 0);
        }


        [Test, Order(10), Category("Activist Test")]
        public void UpdateDonatedProductsBoughtStatusInDB_ProductID_ShouldUpdateBoughtStatus()
        {
            // Arrange
            List<ProductCampaignORG> productCampaignORGList = donatedProducts.GetAllProductsCampaignsORGByByCompanyID((int)businessID);
            Assert.That(productCampaignORGList.Count(), Is.AtLeast(1));
            ProductCampaignORG donatedProduct = productCampaignORGList.OrderByDescending(p => p.ProductID).FirstOrDefault();
            Assert.IsNotNull(donatedProduct, $"The donated product that the business:{businessID} donated does not exist in the database.");
            productID = donatedProduct.ProductID;

            // Act
            donatedProducts.UpdateDonatedProductsBoughtStatusInDB((int)productID);

            // Assert
            // Verify that the product's bought status was updated correctly in the database
            productCampaignORGList = donatedProducts.GetAllProductsCampaignsORGByByCompanyID((int)businessID);
            Assert.That(productCampaignORGList.Count(), Is.AtLeast(1));
            donatedProduct = productCampaignORGList.OrderByDescending(p => p.ProductID).FirstOrDefault();
            Assert.That(donatedProduct.Bought, Is.EqualTo("YES"), $"The bought status of donated product with ID:{productID} was not updated to YES.");
        }



        [Test, Order(11), Category("Activist Test")]
        public void UpdateActiveCampaignSubtractMoneyInDB_ValidInput_ShouldUpdateActiveCampaignSubtractMoney()
        {
            // Arrange
            // Update the active campaign data
            int moneyEarned = 20;

            // Act
            activeCampaigns.UpdateActiveCampaignSubtractMoneyInDB((int)activeCampaignID, moneyEarned);

            // Assert
            // Verify that the active campaign was updated successfully
            Dictionary<int, ActiveCampaign> activeCampaignsDic = activeCampaigns.GetAllActiveCampaignsFromDB();
            Assert.That(activeCampaignsDic.ContainsKey((int)activeCampaignID), "The social activist was not found in the database.");
            Assert.That(activeCampaignsDic[(int)activeCampaignID].MoneyEarned == 10, $"The active campaign money earned was not updated to '{moneyEarned}'.");
        }

        [Test, Order(12), Category("Activist Test")]
        public void GetAllShipmentsByBusinessID_ValidBusinessID_ShouldReturnNonEmptyList()
        {
            // Act
            List<Shipment> shipmentsList = businessCompanies.GetAllShipmentsByBusinessID((int)businessID);

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(shipmentsList, "The List is empty");
            Assert.Greater(shipmentsList.Count(), 0);
        }


        [Test, Order(13), Category("Activist Test")]
        public void GetAllShipmentsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, Shipment> shipmentsDic = businessCompanies.GetAllShipmentsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(shipmentsDic, "The Dictionary is empty");
            Assert.Greater(shipmentsDic.Count(), 0);
        }


        [Test, Order(14), Category("Activist Test")]
        public void UpdateDonatedProductsShippedStatusInDB_ProductID_ShouldUpdateShippedStatus()
        {
            // Act
            donatedProducts.UpdateDonatedProductsShippedStatusInDB((int)productID);

            // Assert
            // Verify that the product's bought status was updated correctly in the database
            List<ProductCampaignORG> productCampaignORGList = donatedProducts.GetAllProductsCampaignsORGByByCompanyID((int)businessID);
            Assert.That(productCampaignORGList.Count(), Is.AtLeast(1));
            ProductCampaignORG donatedProduct = productCampaignORGList.OrderByDescending(p => p.ProductID).FirstOrDefault();
            Assert.That(donatedProduct.Shipped, Is.EqualTo("YES"), $"The shipped status of donated product with ID:{productID} was not updated to YES.");
        }



        [Test, Order(15), Category("Activist Test")]
        public void GetMostMoneyEarned_ShouldReturnNonEmptyList()
        {
            // Act
            List<ActivistReport> activistReportsList = activists.GetMostMoneyEarned();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(activistReportsList, "The List is empty");
            Assert.Greater(activistReportsList.Count(), 0);
        }


        [Test, Order(16), Category("Activist Test")]
        public void GetMostPromotedCampaigns_ShouldReturnNonEmptyList()
        {
            // Act
            List<ActivistReport> activistReportsList = activists.GetMostPromotedCampaigns();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(activistReportsList, "The List is empty");
            Assert.Greater(activistReportsList.Count(), 0);
        }

        [Test, Order(17), Category("Activist Test")]
        public void GetMostPopularCampaign_ShouldReturnNonEmptyList()
        {
            // Act
            List<OrganizationReport> organizationReportsList = campaigns.GetMostPopularCampaign();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(organizationReportsList, "The List is empty");
            Assert.Greater(organizationReportsList.Count(), 0);
        }


        [Test, Order(18), Category("Activist Test")]
        public void GetMostProfitableCampaign_ShouldReturnNonEmptyList()
        {
            // Act
            List<OrganizationReport> organizationReportsList = campaigns.GetMostProfitableCampaign();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(organizationReportsList, "The List is empty");
            Assert.Greater(organizationReportsList.Count(), 0);
        }
    }
}
