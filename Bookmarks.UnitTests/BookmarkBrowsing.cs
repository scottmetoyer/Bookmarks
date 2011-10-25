using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Entities;
using Bookmarks.Models;
using Bookmarks.Controllers;
using Moq;

namespace Bookmarks.UnitTests
{
    [TestFixture]
    public class BookmarkBrowsing
    {
        [Test]
        public void Can_View_A_Single_Page_Of_Bookmarks()
        {
            // Arrange
            var bookmarkController = new BookmarksController(UnitTestHelpers.MockBookmarkRepository());
            bookmarkController.PageSize = 3;

            // Act
            var result = bookmarkController.List(2);

            // Assert
            var displayedBookmarks = ((BookmarksListViewModel)result.ViewData.Model).Bookmarks;
            displayedBookmarks.Count.ShouldEqual(2);
            displayedBookmarks[0].Bookmark.Name.ShouldEqual("b4");
            displayedBookmarks[1].Bookmark.Name.ShouldEqual("b5");
        }

        [Test]
        public void Can_View_Tags_On_Bookmark_List()
        {
            // Arrange
            var bookmarkController = new BookmarksController(UnitTestHelpers.MockBookmarkRepository());
            bookmarkController.PageSize = 3;

            // Act
            var result = bookmarkController.List(1);

            // Assert
            var displayedBookmarks = (BookmarksListViewModel)result.ViewData.Model;
            displayedBookmarks.Bookmarks.Count.ShouldEqual(3);
            displayedBookmarks.Bookmarks[2].Tags.Count.ShouldEqual(3);
            displayedBookmarks.Bookmarks[2].Tags[2].Name.ShouldEqual("t3");
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
