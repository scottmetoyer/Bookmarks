using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Bookmarks.Domain.Entities
{
    [Table(Name = "Tags")]
    public class Tag
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int TagID { get; set; }

        [Column]
        public string Name { get; set; }
    }
}
