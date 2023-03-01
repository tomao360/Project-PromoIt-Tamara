using NUnit.Framework;
using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Test
{
    [TestFixture, Order(1)]
    internal class TestUsers: BaseEntitiesTest
    {
        [Test, Order(1), Category("User Test")]
        public void InsertUserToDB_ValidEmail_ShouldInsertUser()
        {
            // Act
            users.InsertUserToDB(emailOrganization);

            // Assert
            // Verify that the user was inserted correctly in the database
            Dictionary<int, User> usersDic = users.GetAllUsersFromDB();
            Assert.That(usersDic.Count(), Is.AtLeast(1));
            Assert.That(usersDic.Values.Any(u => u.Email == emailOrganization), $"The user with the email:'{emailOrganization}' was not inserted into the database.");
        }


        [Test, Order(2), Category("User Test")]
        public void GetAllUsersFromDB_ShouldReturnNonEmptyDictionary()
        {
            // Act
            Dictionary<int, User> usersDic = users.GetAllUsersFromDB();

            // Assert
            // Verify that the Dictionary is not null and contains at least one item
            Assert.IsNotNull(usersDic, "The Dictionary is empty");
            Assert.Greater(usersDic.Count(), 0);
        }
    }
    
}
