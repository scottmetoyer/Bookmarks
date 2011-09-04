using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace Bookmarks.Domain.Entities
{
    [Table(Name = "Bookmarks")]
    public class Bookmark
    {
        [ScaffoldColumn(false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int BookmarkID { get; set; }

        [Column]
        public int UserID { get; set; }

        [Column]
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Column]
        public string Notes { get; set; }

        [Column]
        [Required(ErrorMessage = "Please enter a URL")]
        public string Url { get; set; }

        [Column]
        public bool IsPrivate { get; set; }
    }
}
