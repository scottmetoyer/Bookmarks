using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Domain.Abstract
{
    public interface IAccountRepository
    {
        IQueryable<User> Users { get; }

        void SaveUser(User user);
    }
}
