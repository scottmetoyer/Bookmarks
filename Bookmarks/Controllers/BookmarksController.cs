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

        public int PageSize = 25;

        public BookmarksController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
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

        [Authorize]
        public ViewResult Edit(int bookmarkID, User currentUser)
        {
            var bookmark = _bookmarkRepository.Bookmarks.FirstOrDefault(x => x.BookmarkID == bookmarkID);
            if (bookmark == null)
            {
                bookmark = new Bookmark { BookmarkID = 0, UserID = currentUser.UserId };
            }

            BookmarkViewModel model = new BookmarkViewModel { Bookmark = bookmark };
            model.Tags = (from t in _bookmarkRepository.Tags
                          from bt in _bookmarkRepository.BookmarkTags
                          where bt.TagID == t.TagID &&
                          bt.BookmarkID == bookmark.BookmarkID
                          select t).ToList();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Bookmark bookmark, string tags, User currentUser)
        {
            var model = _bookmarkRepository.Bookmarks.FirstOrDefault(x => x.BookmarkID == bookmark.BookmarkID);
            if (model == null)
            {
                model = new Bookmark { BookmarkID = 0, UserID = currentUser.UserId };
            }

            if (TryUpdateModel(model, "Bookmark"))
            {
                _bookmarkRepository.SaveBookmark(model);
                _bookmarkRepository.Submit();

                // Clear  the old tags for this bookmark
                var oldTags = _bookmarkRepository.BookmarkTags.Where(x => x.BookmarkID == model.BookmarkID);
                _bookmarkRepository.DeleteBookmarkTags(oldTags);

                // Parse out the new tags
                string[] bookmarkTags = tags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                // Save them
                foreach (string s in bookmarkTags)
                {
                    var tag = _bookmarkRepository.Tags.FirstOrDefault(x => x.Name == s.Trim());
                    if (tag == null)
                    {
                        tag = new Tag { Name = s.Trim(), TagID = 0 };
                        _bookmarkRepository.SaveTag(tag);
                        _bookmarkRepository.Submit();
                    }

                    _bookmarkRepository.SaveBookmarkTag(new BookmarkTag { BookmarkID = model.BookmarkID, BookmarkTagID = 0, TagID = tag.TagID });
                }

                _bookmarkRepository.Submit();
            }

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult New(User currentUser)
        {
            var model = new BookmarkViewModel { Bookmark = new Bookmark { BookmarkID = 0, UserID = currentUser.UserId }, Tags = new List<Tag>() };
            return View("Edit", model);
        }
    }
}
