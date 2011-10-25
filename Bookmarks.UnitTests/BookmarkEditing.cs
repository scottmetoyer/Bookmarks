using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bookmarks.Controllers;
using Bookmarks.Models;

namespace Bookmarks.UnitTests
{
    [TestFixture]
    public class BookmarkEditing
    {
        [Test]
        public void Can_Edit_Bookmark()
        {
            // Arrange
            BookmarksController controller = new BookmarksController(UnitTestHelpers.MockBookmarkRepository());

            // Act
            var result = controller.Edit(3);

            // Assert
            result.ShouldBeDefaultView();
            ((BookmarkViewModel)result.Model).Bookmark.Name.ShouldEqual("b3");
            ((BookmarkViewModel)result.Model).Tags.Count.ShouldEqual(3);
        }
    }
}
