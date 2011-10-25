using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Domain.Abstract
{
    public interface IBookmarkRepository
    {
        IQueryable<Bookmark> Bookmarks { get; }

        IQueryable<BookmarkTag> BookmarkTags { get; }

        IQueryable<Tag> Tags { get; }

        void SaveBookmark(Bookmark bookmark);

        void DeleteBookmark(Bookmark bookmark);

        void SaveBookmarkTag(BookmarkTag bookmarkTag);

        void DeleteBookmarkTag(BookmarkTag bookmarkTag);

        void DeleteBookmarkTags(IEnumerable<BookmarkTag> bookmarkTags);

        void SaveTag(Tag tag);

        void DeleteTag(Tag tag);

        void Submit();
    }
}
