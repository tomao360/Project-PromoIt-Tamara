using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromoItProject.DAL;
using PromoItProject.Data.Sql;
using PromoItProject.Model;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(2)]
    internal class TestNonProfitOrganizations: BaseEntitiesTest
    {        
        private string organizationName;
        private string linkToWebsite;
        private string description;


        [Test, Order(1), Category("Organization Test")]
        public void InsertOrganizationToDB_ValidInputs_ShouldInsertOrganization()
        {
            // Arrange
            organizationName = "Non-Profit Organization Test";
            linkToWebsite = "https://www.test.com";
            description = "A non-profit organization test";

            // Act
            nonProfitOrganizations.InsertOrganizationToDB(organizationName, linkToWebsite, emailOrganization, description);

            // Assert
            // Verify that the organization was inserted correctly in the database
            Dictionary<int, NonProfitOrganization> organizationsDic = nonProfitOrganizations.GetAllOrganizationsFromDB();
            Assert.That(organizationsDic.Count(), Is.AtLeast(1));
            Assert.That(organizationsDic.Values.Any(o => o.OrganizationName == organizationName &&
                                              o.LinkToWebsite == linkToWebsite &&
                                              o.Email == emailOrganization &&
                                              o.Description == description), $"The organization with the name:'{organizationName}', website: '{linkToWebsite}', email:'{emailOrganization}', and description:'{description}' was not inserted into the database.");
        }


        [Test, Order(2), Category("Organization Test")]
        public void GetAllOrganizationsFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, NonProfitOrganization> organizationsDic = nonProfitOrganizations.GetAllOrganizationsFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(organizationsDic, "The Dictionary is empty");
            Assert.Greater(organizationsDic.Count(), 0);
        }


        [Test, Order(3), Category("Organization Test")]
        public void GetOrganizationFromDbByEmail_ShouldReturnOrganizationObject()
        {
            // Act
            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);

            // Assert
            Assert.IsNotNull(nonProfitOrganization);
            Assert.AreEqual(emailOrganization, nonProfitOrganization.Email);
        }


        [Test, Order(4), Category("Organization Test")]
        public void UpdateOrganizationInDB_ValidInput_ShouldUpdateOrganization()
        {
            // Arrange
            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);
            Assert.IsNotNull(nonProfitOrganization, $"The organization with the email:{emailOrganization} does not exist in the database.");
            organizationID = nonProfitOrganization.OrganizationID;
            // Update the organization data
            organizationName = "Non-Profit Organization Update";
            linkToWebsite = "https://www.updatedtest.com";
            description = "Updated test description";

            // Act
            nonProfitOrganizations.UpdateOrganizationInDB((int)organizationID, organizationName, linkToWebsite, description);

            // Assert
            // Verify that the organization was updated successfully
            Dictionary<int, NonProfitOrganization> organizationsDic = nonProfitOrganizations.GetAllOrganizationsFromDB();
            Assert.That(organizationsDic.ContainsKey((int)organizationID), "The organization was not found in the database.");
            Assert.That(organizationsDic[(int)organizationID].OrganizationName == organizationName, $"The organization name was not updated to '{organizationName}'.");
            Assert.That(organizationsDic[(int)organizationID].LinkToWebsite == linkToWebsite, $"The organization website was not updated to '{linkToWebsite}'.");
            Assert.That(organizationsDic[(int)organizationID].Description == description, $"The organization description was not updated to '{description}'.");
        }


       // [Test, Order(5), Category("Organization Test")]
        public void DeleteOrganizationFromDB_ValidInput_ShouldDeleteOrganization()
        {
            // Arrange
            NonProfitOrganization nonProfitOrganization = nonProfitOrganizations.GetOrganizationFromDbByEmail(emailOrganization);
            Assert.IsNotNull(nonProfitOrganization, $"The organization with the ID:{organizationID} does not exist in the database.");

            // Act
            nonProfitOrganizations.DeleteOrganizationFromDB((int)organizationID);

            // Assert
            Dictionary<int, NonProfitOrganization> organizationsDic = nonProfitOrganizations.GetAllOrganizationsFromDB();
            Assert.IsFalse(organizationsDic.ContainsKey((int)organizationID), $"The organization with the ID:'{organizationID}' was not deleted from the database.");
        }

    }
}
