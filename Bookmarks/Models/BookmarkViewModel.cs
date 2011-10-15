using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Models
{
    public class BookmarkViewModel
    {
        public Bookmark Bookmark { get; set; }

        public List<Tag> Tags { get; set; }
    }
}