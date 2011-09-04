using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Entities;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bookmarks.UnitTests
{
    public static class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static IAccountRepository MockAccountRepository(params User[] users)
        {
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.Users).Returns(users.AsQueryable());
            return mockAccountRepository.Object;
        }

        public static IBookmarkRepository MockBookmarkRepository(params Bookmark[] bookmarks)
        {
            var mockBookmarkRepository = new Mock<IBookmarkRepository>();
            mockBookmarkRepository.Setup(x => x.Bookmarks).Returns(bookmarks.AsQueryable());
            return mockBookmarkRepository.Object;
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues)
        {
            var actualValues = ((RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);

            foreach (string key in expectedValues.Keys)
            {
                actualValues[key].ShouldEqual(expectedValues[key]);
            }
        }

        public static void ShouldBeDefaultView(this ActionResult actionResult)
        {
            actionResult.ShouldBeView(string.Empty);
        }

        public static void ShouldBeView(this ActionResult actionResult, string viewName)
        {
            Assert.IsInstanceOf<ViewResult>(actionResult);
            ((ViewResult)actionResult).ViewName.ShouldEqual(viewName);
        }
    }
}
