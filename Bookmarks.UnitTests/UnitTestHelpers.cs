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
using System.Globalization;

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

        public static IBookmarkRepository MockBookmarkRepository()
        {
            // Setup a few generic testing objects
            var bookmarks = new List<Bookmark> { 
                new Bookmark { Name = "b1", BookmarkID = 1 },
                new Bookmark { Name = "b2", BookmarkID = 2 },
                new Bookmark { Name = "b3", BookmarkID = 3 },
                new Bookmark { Name = "b4", BookmarkID = 4 },
                new Bookmark { Name = "b5", BookmarkID = 5 }
            };

            var tags = new List<Tag> { 
                new Tag { Name = "t1", TagID = 1 },
                new Tag { Name = "t2", TagID = 2 },
                new Tag { Name = "t3", TagID = 3}
            };

            var bookmarkTags = new List<BookmarkTag> { 
                new BookmarkTag { BookmarkID = 1, TagID = 1},
                new BookmarkTag { BookmarkID = 1, TagID = 2},
                new BookmarkTag { BookmarkID = 2, TagID = 1},
                new BookmarkTag { BookmarkID = 3, TagID = 1},
                new BookmarkTag { BookmarkID = 3, TagID = 2},
                new BookmarkTag { BookmarkID = 3, TagID = 3}
            };

            var mockBookmarkRepository = new Mock<IBookmarkRepository>();
            mockBookmarkRepository.Setup(x => x.Bookmarks).Returns(bookmarks.AsQueryable());
            mockBookmarkRepository.Setup(x => x.Tags).Returns(tags.AsQueryable());
            mockBookmarkRepository.Setup(x => x.BookmarkTags).Returns(bookmarkTags.AsQueryable());

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

        public static T WithIncomingValues<T>(this T controller, FormCollection values)
           where T : Controller
        {
            controller.ControllerContext = new ControllerContext();
            controller.ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.CurrentCulture);
            return controller;
        }
    }
}
