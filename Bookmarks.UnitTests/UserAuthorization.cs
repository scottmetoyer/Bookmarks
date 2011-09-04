using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bookmarks.Controllers;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Concrete;
using Bookmarks.Domain.Entities;
using System.Web.Mvc;
using Bookmarks.Infrastructure;

namespace Bookmarks.UnitTests
{
    [TestFixture]
    public class UserAuthorization
    {
        [Test]
        public void User_Can_Authenticate()
        {
            // Arrange
            IAccountRepository repository = UnitTestHelpers.MockAccountRepository(
                new User { Email = "test@test.com", Password = "test", Token = "12345", UserID = 0 }
                );
            AccountController controller = new AccountController(
                new BookmarksMembershipProvider(repository)
                );

            // Act
            ActionResult result = controller.Authenticate("test@test.com", "test");

            // Assert
            ((ContentResult)result).Content.ShouldEqual("12345");
        }

        [Test]
        public void Invalid_Email_Fails_Authentication()
        {
            // Arrange
            IAccountRepository repository = UnitTestHelpers.MockAccountRepository(
                new User { Email = "test@test.com", Password = "test", Token = "12345", UserID = 0 }
            );
            AccountController controller = new AccountController(
                new BookmarksMembershipProvider(repository)
                );

            // Act
            ActionResult result = controller.Authenticate("wrong@test.com", "test");

            // Assert
            ((ContentResult)result).Content.ShouldEqual("unauthorized");
        }

        [Test]
        public void Invalid_Password_Fails_Authentication()
        {
            // Arrange
            IAccountRepository repository = UnitTestHelpers.MockAccountRepository(
                  new User { Email = "test@test.com", Password = "test", Token = "12345", UserID = 0 }
            );
            AccountController controller = new AccountController(
                new BookmarksMembershipProvider(repository)
                );

            // Act
            ActionResult result = controller.Authenticate("test@test.com", "wrong");

            // Assert
            ((ContentResult)result).Content.ShouldEqual("unauthorized");
        }

        [Test]
        public void Token_Generated_For_First_Time_Login()
        {
            // Arrange
            IAccountRepository repository = UnitTestHelpers.MockAccountRepository(
     new User { Email = "notoken@test.com", Password = "test", UserID = 0 }
 );
            AccountController controller = new AccountController(
                new BookmarksMembershipProvider(repository)
                );

            // Act
            ActionResult result = controller.Authenticate("notoken@test.com", "test");

            // Assert
            ((ContentResult)result).Content.Length.ShouldEqual(36);
        }
    }
}
