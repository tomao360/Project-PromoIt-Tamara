using NUnit.Framework;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(4)]
    internal class TestBusinessCompanies: BaseEntitiesTest
    {
        private string businessName;
        private string email = "business@test.com";
        private string productName;
        private decimal price;


        [Test, Order(1), Category("Business Test")]
        public void InsertBusinessCompanyToDB_ValidInputs_ShouldInsertBusiness()
        {
            // Arrange
            businessName = "Business Test";

            // Act
            businessCompanies.InsertBusinessCompanyToDB(businessName, email);

            // Assert
            // Verify that the company was inserted correctly in the database
            Dictionary<int, BusinessCompany> businessCompanyDic = businessCompanies.GetAllBusinessCompaniesFromDB();
            Assert.That(businessCompanyDic.Count(), Is.AtLeast(1));
            Assert.That(businessCompanyDic.Values.Any(b => b.BusinessName == businessName && b.Email == email), $"The business company '{businessName}' with the email:'{email}' was not inserted into the database.");
        }

        [Test, Order(2), Category("Business Test")]
        public void GetAllBusinessCompaniesFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, BusinessCompany> businessCompanyDic = businessCompanies.GetAllBusinessCompaniesFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(businessCompanyDic, "The Dictionary is empty");
            Assert.Greater(businessCompanyDic.Count(), 0);
        }


        [Test, Order(3), Category("Business Test")]
        public void GetBusinessCompanyFromDbByEmail_ValidBusinessEmail_ShouldReturnBusinessObject()
        {
            // Act
            BusinessCompany businessCompany = businessCompanies.GetBusinessCompanyFromDbByEmail(email);

            // Assert
            Assert.IsNotNull(businessCompany);
            Assert.AreEqual(email, businessCompany.Email);
        }



        [Test, Order(4), Category("Business Test")]
        public void UpdateBusinessCompanyInDB_ValidInput_ShouldUpdateBusiness()
        {
            // Arrange
            BusinessCompany businessCompany = businessCompanies.GetBusinessCompanyFromDbByEmail(email);
            Assert.IsNotNull(businessCompany, $"The business company with the email:{email} does not exist in the database.");
            businessID = businessCompany.BusinessID;
            // Update the business data
            businessName = "Business Update";

            // Act
            businessCompanies.UpdateBusinessCompanyInDB((int)businessID, businessName);

            // Assert
            // Verify that the business company was updated successfully
            Dictionary<int, BusinessCompany> businessCompanyDic = businessCompanies.GetAllBusinessCompaniesFromDB();
            Assert.That(businessCompanyDic.ContainsKey((int)businessID), "The business company was not found in the database.");
            Assert.That(businessCompanyDic[(int)businessID].BusinessName == businessName, $"The business comapny name was not updated to '{businessName}'.");
        }


        [Test, Order(5), Category("Business Test")]
        public void InsertDonatedProductToDB_ValidInputs_ShouldInsertProduct()
        {
            // Arrange
            productName = "Product Test";
            price = 100;

            // Act
            donatedProducts.InsertDonatedProductToDB(productName, price, (int)businessID, (int)campaignID);

            // Assert
            // Verify that the product was inserted correctly in the database
            List<ProductCampaignORG> productCampaignORGList = donatedProducts.GetAllProductsCampaignsORGByByCompanyID((int)businessID);
            Assert.That(productCampaignORGList.Count(), Is.AtLeast(1));
            Assert.That(productCampaignORGList.Any(p => p.ProductName == productName && p.Price == price && p.BusinessID == businessID && p.CampaignID == campaignID), $"The donated product '{productName}' with the price:'{price}' was not inserted into the database.");
        }


        [Test, Order(6), Category("Business Test")]
        public void GetAllProductsCampaignsORGFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, ProductCampaignORG> productCampaignORGDic = donatedProducts.GetAllProductsCampaignsORGFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(productCampaignORGDic, "The Dictionary is empty");
            Assert.Greater(productCampaignORGDic.Count(), 0);
        }


        [Test, Order(7), Category("Business Test")]
        public void GetAllProductsCampaignsORGByByCompanyID_ValidBusinessID_ShouldReturnNonEmptyList()
        {
            // Act
            List<ProductCampaignORG> productCampaignORGList = donatedProducts.GetAllProductsCampaignsORGByByCompanyID((int)businessID);

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(productCampaignORGList, "The List is empty");
            Assert.Greater(productCampaignORGList.Count(), 0);
        }


        [Test, Order(8), Category("Business Test")]
        public void GetCountOfDonations_ShouldReturnNonEmptyList()
        {
            // Act
            List<BusinessReport> businessReportsList = businessCompanies.GetCountOfDonations();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(businessReportsList, "The List is empty");
            Assert.Greater(businessReportsList.Count(), 0);
        }


        [Test, Order(9), Category("Business Test")]
        public void GetCountCampaignAndProductsToOrg_ShouldReturnNonEmptyList()
        {
            // Act
            List<OrganizationReport> organizationReportsList = nonProfitOrganizations.GetCountCampaignAndProductsToOrg();

            // Assert
            // Verify that the List is not null and contains at least one item
            Assert.IsNotNull(organizationReportsList, "The List is empty");
            Assert.Greater(organizationReportsList.Count(), 0);
        }


        //[Test, Order(10), Category("Business Test")]
        public void DeleteBusinessCompanyFromDB_ValidInput_ShouldDeleteBusiness()
        {
            // Act
            businessCompanies.DeleteBusinessCompanyFromDB((int)businessID);

            // Assert
            Dictionary<int, BusinessCompany> businessCompanyDic = businessCompanies.GetAllBusinessCompaniesFromDB();
            Assert.IsFalse(businessCompanyDic.ContainsKey((int)businessID), $"The business comapny with the ID:'{businessID}' was not deleted from the database.");
        }
    }
}
