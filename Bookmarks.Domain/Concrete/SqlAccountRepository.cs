using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Abstract;
using System.Data.Linq;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Domain.Concrete
{
    public class SqlAccountRepository : IAccountRepository
    {
        private Table<User> _usersTable;

        public IQueryable<User> Users { get { return _usersTable; } }

        public SqlAccountRepository(string connectionString)
        {
            _usersTable = (new DataContext(connectionString)).GetTable<User>();
        }

        public void SaveUser(User user)
        {
            if (user.UserID == 0)
            {
                _usersTable.InsertOnSubmit(user);
            }

            _usersTable.Context.SubmitChanges();
        }
    }
}
