using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarks.Domain.Entities;
using System.Web.Security;

namespace Bookmarks.Infrastructure
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
            {
                throw new InvalidOperationException("Cannot update instances");
            }

            // Return the cart from Session, creating it first if necessary
            // User user = (User)HttpContext.Current.Session[_userSessionKey];
            BookmarksMembershipProvider _provider = (BookmarksMembershipProvider)Membership.Provider;
            User user = _provider.User;

            return user;
        }
    }
}