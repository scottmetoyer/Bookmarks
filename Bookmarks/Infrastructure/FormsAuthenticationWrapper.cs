using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bookmarks.Domain.Entities;
using System.Security.Principal;

namespace Bookmarks.Infrastructure
{
    public class FormsAuthenticationWrapper : IFormsAuthentication
    {
        #region IFormsAuthentication Members

        public bool Authenticate(string name, string password)
        {
            return Membership.ValidateUser(name, password);
        }

        public void SetAuthCookie(string name, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(name, persistent);
        }

        #endregion
    }
}