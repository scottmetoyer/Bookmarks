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
        [Column(Name = "BookmarkID", IsPrimaryKey = true)]
        public int BookmarkID { get; set; }

        [Column(Name = "TagID", IsPrimaryKey = true)]
        public int TagID { get; set; }
    }
}
