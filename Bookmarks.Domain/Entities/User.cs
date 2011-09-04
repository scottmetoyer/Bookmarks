using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Bookmarks.Domain.Entities
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int UserID { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public string Token { get; set; }
    }
}
