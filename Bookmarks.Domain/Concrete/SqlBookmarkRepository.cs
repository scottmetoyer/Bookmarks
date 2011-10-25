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
        private DataContext _context;
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
            _context = new DataContext(connectionString);
            _bookmarkTable = _context.GetTable<Bookmark>();
            _bookmarkTagTable = _context.GetTable<BookmarkTag>();
            _tagTable = _context.GetTable<Tag>();
        }

        public void SaveBookmark(Bookmark bookmark)
        {
            if (bookmark.BookmarkID == 0)
            {
                _bookmarkTable.InsertOnSubmit(bookmark);
                bookmark.CreateDate = DateTime.Now;
            }

            bookmark.Name = bookmark.Name.Trim();
            bookmark.Url = bookmark.Url.Trim();

            if (!(bookmark.Url.StartsWith("http://")))
            {
                bookmark.Url = "http://" + bookmark.Url;
            }
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            // Delete the BookmarkTags for this book
            foreach (var bookmarkTag in _bookmarkTagTable.Where(x => x.BookmarkID == bookmark.BookmarkID))
            {
                _bookmarkTagTable.DeleteOnSubmit(bookmarkTag);
            }

            _bookmarkTable.DeleteOnSubmit(bookmark);
            _context.SubmitChanges();
        }

        public void SaveBookmarkTag(BookmarkTag bookmarkTag)
        {
            _bookmarkTagTable.InsertOnSubmit(bookmarkTag);
        }

        public void DeleteBookmarkTag(BookmarkTag bookmarkTag)
        {
            _bookmarkTagTable.DeleteOnSubmit(bookmarkTag);
            _context.SubmitChanges();
        }

        public void DeleteBookmarkTags(IEnumerable<BookmarkTag> tags)
        {
            _bookmarkTagTable.DeleteAllOnSubmit(tags);
            _context.SubmitChanges();
        }

        public void SaveTag(Tag tag)
        {
            if (tag.TagID == 0)
            {
                _tagTable.InsertOnSubmit(tag);
            }

            tag.Name = tag.Name.Trim();
        }

        public void DeleteTag(Tag tag)
        {
            _tagTable.DeleteOnSubmit(tag);
            _context.SubmitChanges();
        }

        public void Submit()
        {
            _context.SubmitChanges();
        }
    }
}
