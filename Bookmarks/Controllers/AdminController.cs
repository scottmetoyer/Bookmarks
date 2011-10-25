using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarks.Domain.Abstract;
using Bookmarks.Models;
using Bookmarks.Domain.Services;
using Bookmarks.Domain.Entities;
using System.ComponentModel;

namespace Bookmarks.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public int PageSize = 25;
        private IBookmarkRepository _bookmarkRepository;

        public AdminController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public ViewResult Index([DefaultValue(1)]int page)
        {
            var bookmarks = _bookmarkRepository.Bookmarks.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            var bookmarkList = new BookmarksListViewModel();

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
            /*
            var bookmark = _bookmarkRepository.Bookmarks.First(x => x.BookmarkID == bookmarkID);
            var tags = (from tag in _bookmarkRepository.Tags
                        from bookmarkTag in _bookmarkRepository.BookmarkTags
                        where (bookmarkTag.TagID == tag.TagID && bookmarkTag.BookmarkID == bookmarkID)
                        select tag).ToList<Tag>().ToCommaSeparatedString();

            var viewModel = new BookmarkViewModel { Bookmark = bookmark, Tags = tags };

            return View(viewModel);
             * */
            return View();
        }

        public RedirectToRouteResult Delete(int bookmarkID)
        {
            var bookmark = _bookmarkRepository.Bookmarks.First(x => x.BookmarkID == bookmarkID);
            _bookmarkRepository.DeleteBookmark(bookmark);

            TempData["message"] = bookmark.Name + " was deleted";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(BookmarkViewModel model, User user)
        {
            /*
            Bookmark bookmark = model.Bookmark.BookmarkID == 0 ? new Bookmark() : _bookmarkRepository.Bookmarks.First(x => x.BookmarkID == model.Bookmark.BookmarkID);
            TryUpdateModel(bookmark, "Bookmark");

            if (ModelState.IsValid)
            {
                bookmark.UserID = user.UserId;
                _bookmarkRepository.SaveBookmark(bookmark);

                // Clear out the old tags
                _bookmarkRepository.ClearBookmarkTags(bookmark.BookmarkID);

                // Save the tags
                foreach (var tagName in model.Tags.ToTagList())
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

                TempData["message"] = "Your bookmark has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
             * */
            return View();
        }
    }
}
