using NUnit.Framework;
using PromoItProject.Data.Sql;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(6)]
    internal class TestContactUsMessages: BaseEntitiesTest
    {
        [Test, Order(1), Category("Message Test")]
        public void InsertMessageToDB_ValidInputs_ShouldInsertMessage()
        {
            // Arrange
            string name = "Test Message";
            string userMessage = "This is a test message";

            // Act
            contactUsMessages.InsertMessageToDB(name, emailOrganization, userMessage);

            // Assert
            // Verify that the company was inserted correctly in the database
            Dictionary<int, ContactUsMessage> messagesDic = contactUsMessages.GetAllMessagesFromDB();
            Assert.That(messagesDic.Count(), Is.AtLeast(1));
            Assert.That(messagesDic.Values.Any(m => m.Name == name && m.Email == emailOrganization && m.UserMessage == userMessage), $"The message '{userMessage}' from:'{name}' was not inserted into the database.");
        }


        [Test, Order(2), Category("Message Test")]
        public void GetAllMessagesFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, ContactUsMessage> messagesDic = contactUsMessages.GetAllMessagesFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(messagesDic, "The Dictionary is empty");
            Assert.Greater(messagesDic.Count(), 0);
        }


        [Test, Order(13), Category("Message Test")]
        public void DeleteMessageByID_ValidInput_ShouldDeleteBusiness()
        {
            // Arrange
            Dictionary<int, ContactUsMessage> messagesDic = contactUsMessages.GetAllMessagesFromDB();
            Assert.IsNotNull(messagesDic, "The Dictionary is empty");
            int messageID = messagesDic.OrderByDescending(m => m.Value.MessageID).FirstOrDefault().Key;
            Assert.IsNotNull(campaignID, $"The message with the email:{emailOrganization} does not exist in the database.");

            // Act
            contactUsMessages.DeleteMessageByID(messageID);

            // Assert
            messagesDic = contactUsMessages.GetAllMessagesFromDB();
            Assert.IsFalse(messagesDic.ContainsKey(messageID), $"The message with the ID:'{messageID}' was not deleted from the database.");
        }
    }
}
