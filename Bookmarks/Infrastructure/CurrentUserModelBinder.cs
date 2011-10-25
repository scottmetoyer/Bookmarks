using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarks.Domain.Entities;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Concrete;
using System.Configuration;

namespace Bookmarks.Infrastructure
{
    public class CurrentUserModelBinder : IModelBinder
    {
        // TODO: Figure out how to use Ninject and an interface here
        private SqlAccountRepository _accountRepository { get; set; }

        public CurrentUserModelBinder()
        {
            _accountRepository = new SqlAccountRepository(
                ConfigurationManager.ConnectionStrings["BookmarksConnectionString"].ConnectionString);
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var user = _accountRepository.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
            return user;
        }
    }
}