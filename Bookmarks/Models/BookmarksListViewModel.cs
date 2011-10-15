using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Models
{
    public class BookmarksListViewModel
    {
        public List<BookmarkViewModel> Bookmarks { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}