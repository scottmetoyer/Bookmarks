using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Infrastructure
{
    public interface IFormsAuthentication
    {
        bool Authenticate(string name, string password);

        void SetAuthCookie(string name, bool persistent);
    }
}
