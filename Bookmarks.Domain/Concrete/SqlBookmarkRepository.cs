using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Entities;
using System.Data.Linq;

namespace Bookmarks.Domain.Concrete
{
    public class SqlBookmarkRepository : IBookmarkRepository
    {
        private Table<Bookmark> _bookmarkTable;
        private Table<BookmarkTag> _bookmarkTagTable;
        private Table<Tag> _tagTable;

        public IQueryable<Bookmark> Bookmarks
        {
            get
            {
                return _bookmarkTable;
            }
        }

        public IQueryable<BookmarkTag> BookmarkTags
        {
            get
            {
                return _bookmarkTagTable;
            }
        }

        public IQueryable<Tag> Tags
        {
            get
            {
                return _tagTable;
            }
        }

        public SqlBookmarkRepository(string connectionString)
        {
            DataContext context = new DataContext(connectionString);
            _bookmarkTable = context.GetTable<Bookmark>();
            _bookmarkTagTable = context.GetTable<BookmarkTag>();
            _tagTable = context.GetTable<Tag>();
        }

        public void SaveBookmark(Bookmark bookmark)
        {
            if (bookmark.BookmarkID == 0)
            {
                _bookmarkTable.InsertOnSubmit(bookmark);
            }

            bookmark.Name = bookmark.Name.Trim();
            bookmark.Url = bookmark.Url.Trim();

            if (!(bookmark.Url.StartsWith("http://")))
            {
                bookmark.Url = "http://" + bookmark.Url;
            }

            _bookmarkTable.Context.SubmitChanges();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            // Delete the BookmarkTags for this book
            foreach(var bookmarkTag in _bookmarkTagTable.Where(x => x.BookmarkID == bookmark.BookmarkID))
            {
                _bookmarkTagTable.DeleteOnSubmit(bookmarkTag);
            }

            _bookmarkTable.DeleteOnSubmit(bookmark);
            _bookmarkTable.Context.SubmitChanges();
        }

        public void SaveBookmarkTag(BookmarkTag bookmarkTag)
        {
            _bookmarkTagTable.InsertOnSubmit(bookmarkTag);
            _bookmarkTagTable.Context.SubmitChanges();
        }

        public void DeleteBookmarkTag(BookmarkTag bookmarkTag)
        {
            _bookmarkTagTable.DeleteOnSubmit(bookmarkTag);
            _bookmarkTagTable.Context.SubmitChanges();
        }

        public void ClearBookmarkTags(int bookmarkID)
        {
            foreach (BookmarkTag bookmarkTag in this.BookmarkTags.Where(x => x.BookmarkID == bookmarkID))
            {
                this.DeleteBookmarkTag(bookmarkTag);
            }
        }

        public void SaveTag(Tag tag)
        {
            if (tag.TagID == 0)
            {
                _tagTable.InsertOnSubmit(tag);
            }

            tag.Name = tag.Name.Trim();

            _tagTable.Context.SubmitChanges();
        }

        public void DeleteTag(Tag tag)
        {
            _tagTable.DeleteOnSubmit(tag);
            _tagTable.Context.SubmitChanges();
        }
    }
}
