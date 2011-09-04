using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Entities;
using Bookmarks.Models;
using Bookmarks.Controllers;

namespace Bookmarks.UnitTests
{
    [TestFixture]
    public class BookmarkBrowsing
    {
        [Test]
        public void Can_View_A_Single_Page_Of_Bookmarks()
        {
            // Arrange
            IBookmarkRepository bookmarkRepository = UnitTestHelpers.MockBookmarkRepository(
                new Bookmark { Name = "b1" },
                new Bookmark { Name = "b2" },
                new Bookmark { Name = "b3" },
                new Bookmark { Name = "b4" },
                new Bookmark { Name = "b5" }
                );
            var bookmarkController = new BookmarksController(bookmarkRepository, null);
            bookmarkController.PageSize = 3;

            // Act
            var result = bookmarkController.List(2);

            // Assert
            var displayedBookmarks = ((BookmarksListViewModel)result.ViewData.Model).Bookmarks;
            displayedBookmarks.Count.ShouldEqual(2);
            displayedBookmarks[0].Name.ShouldEqual("b4");
            displayedBookmarks[1].Name.ShouldEqual("b5");
        }

        [Test]
        public void Can_View_Bookmarks_From_A_Single_Tag()
        {
        }

        [Test]
        public void Can_View_Tags_From_A_Single_Bookmark()
        {

        }

        [Test]
        public void Can_View_Bookmarks_From_A_Single_User()
        {
        }

        [Test]
        public void Can_Remove_Tag_From_A_Bookmark()
        {

        }

        [Test]
        public void Can_Add_Tag_To_A_Bookmark()
        {

        }
    }
}
