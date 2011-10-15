using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarks.Domain.Abstract;
using Bookmarks.Models;
using System.ComponentModel;
using Bookmarks.Domain.Entities;
using Bookmarks.Domain.Services;
using System.Web.Security;
using Bookmarks.Infrastructure;

namespace Bookmarks.Controllers
{
    public class BookmarksController : Controller
    {
        private IBookmarkRepository _bookmarkRepository;
        private BookmarksMembershipProvider _membershipProvider;

        public int PageSize = 25;

        public BookmarksController(IBookmarkRepository bookmarkRepository, BookmarksMembershipProvider membershipProvider)
        {
            _bookmarkRepository = bookmarkRepository;
            _membershipProvider = membershipProvider;
        }

        public ViewResult List([DefaultValue(1)]int page)
        {
            var bookmarks = _bookmarkRepository.Bookmarks.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            var bookmarkList = new BookmarksListViewModel { Bookmarks = new List<BookmarkViewModel>() };

            // Build the viewmodel for each bookmark
            foreach (Bookmark bookmark in bookmarks)
            {
                BookmarkViewModel model = new BookmarkViewModel { Bookmark = bookmark };
                model.Tags = (from t in _bookmarkRepository.Tags
                              from bt in _bookmarkRepository.BookmarkTags
                              where bt.TagID == t.TagID &&
                              bt.BookmarkID == bookmark.BookmarkID
                              select t).ToList();

                bookmarkList.Bookmarks.Add(model);
            }

            bookmarkList.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _bookmarkRepository.Bookmarks.Count()
            };

            return View(bookmarkList);
        }

        public string GetTags(int bookmarkID)
        {
            var result = (from tag in _bookmarkRepository.Tags
                          from bookmarkTag in _bookmarkRepository.BookmarkTags
                          where (bookmarkTag.TagID == tag.TagID && bookmarkTag.BookmarkID == bookmarkID)
                          select tag).ToList<Tag>().ToCommaSeparatedString();

            return result;
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult New(string name, string url, string tags, string token)
        {
            // Authorize user
            var user = _membershipProvider.AccountRepository.Users.FirstOrDefault(x => x.Token == token);
            if (user == null)
            {
                return Content("unauthorized");
            }

            if (_membershipProvider.ValidateUser(user.Email, user.Password))
            {
                Bookmark bookmark = _bookmarkRepository.Bookmarks.FirstOrDefault(x => x.Url == Server.UrlDecode(url.Trim()));

                if (bookmark != null)
                {
                    TryUpdateModel(bookmark);
                }
                else
                {
                    bookmark = new Bookmark { BookmarkID = 0, IsPrivate = false, Name = Server.UrlDecode(name), Url = Server.UrlDecode(url), Notes = string.Empty, UserID = 0 };
                }

                if (ModelState.IsValid)
                {
                    _bookmarkRepository.SaveBookmark(bookmark);

                    // Clear out the old tags
                    _bookmarkRepository.ClearBookmarkTags(bookmark.BookmarkID);

                    // Save the tags
                    foreach (var tagName in Server.UrlDecode(tags).ToTagList())
                    {
                        var tag = _bookmarkRepository.Tags.FirstOrDefault(x => x.Name.Trim() == tagName.Trim());

                        if (tag == null)
                        {
                            tag = new Tag { Name = tagName, TagID = 0 };
                        }

                        _bookmarkRepository.SaveTag(tag);
                        _bookmarkRepository.SaveBookmarkTag(
                                new BookmarkTag { BookmarkID = bookmark.BookmarkID, TagID = tag.TagID }
                            );
                    }
                }
            }
            else
            {
                return Content("unauthorized");
            }

            return View();
        }
    }
}
