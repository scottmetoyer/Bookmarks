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

        public ViewResult Edit(int bookmarkID)
        {
            var bookmark = _bookmarkRepository.Bookmarks.FirstOrDefault(x => x.BookmarkID == bookmarkID);
            if (bookmark == null)
            {
                bookmark = new Bookmark { BookmarkID = 0 };
            }

            BookmarkViewModel model = new BookmarkViewModel { Bookmark = bookmark };
            model.Tags = (from t in _bookmarkRepository.Tags
                          from bt in _bookmarkRepository.BookmarkTags
                          where bt.TagID == t.TagID &&
                          bt.BookmarkID == bookmark.BookmarkID
                          select t).ToList();

            return View(model);
        }

        [HttpPost]
        public ViewResult Edit(Bookmark bookmark, string tags)
        {
            var model = _bookmarkRepository.Bookmarks.FirstOrDefault(x => x.BookmarkID == bookmark.BookmarkID);
            if (model == null)
            {
                model = new Bookmark { BookmarkID = 0 };
            }

            if (TryUpdateModel(model))
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

            return View();
        }

        public ActionResult New()
        {
            return RedirectToAction("Edit", new { bookmarkID = 0 });
        }
    }
}
