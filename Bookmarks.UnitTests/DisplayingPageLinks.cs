using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using Bookmarks.Models;
using Bookmarks.HtmlHelpers;
using Bookmarks.Domain.Entities;
using Bookmarks.Controllers;

namespace Bookmarks.UnitTests
{
    [TestFixture]
    public class DisplayingPageLinks
    {
        [Test]
        public void Can_Generate_Links_To_Other_Pages()
        {
            // Arrange:
            HtmlHelper html = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrl = i => "Page" + i;

            // Act:
            MvcHtmlString result = html.PageLinks(pagingInfo, pageUrl);

            // Assert:
            result.ToString().ShouldEqual(@"<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>");
        }

        [Test]
        public void Bookmark_Lists_Include_Correct_Page_Numbers()
        {
            // Arrange:
            var mockRepository = UnitTestHelpers.MockBookmarkRepository(
                new Bookmark { Name = "p1" },
                new Bookmark { Name = "p2" },
                new Bookmark { Name = "p3" },
                new Bookmark { Name = "p4" },
                new Bookmark { Name = "p5" }
                );
            var accountRepository = UnitTestHelpers.MockAccountRepository(new User[] { });
            var controller = new BookmarksController(mockRepository, null) { PageSize = 3 };

            // Act:
            var result = controller.List(2);

            // Assert:
            var viewModel = (BookmarksListViewModel)result.ViewData.Model;

            PagingInfo pagingInfo = viewModel.PagingInfo;
            pagingInfo.CurrentPage.ShouldEqual(2);
            pagingInfo.ItemsPerPage.ShouldEqual(3);
            pagingInfo.TotalItems.ShouldEqual(5);
            pagingInfo.TotalPages.ShouldEqual(2);
        }
    }
}
