using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Bookmarks.Domain.Entities
{
    [Table(Name = "BookmarkTags")]
    public class BookmarkTag
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int BookmarkTagID { get; set; }

        [Column]
        public int BookmarkID { get; set; }

        [Column]
        public int TagID { get; set; }
    }
}
