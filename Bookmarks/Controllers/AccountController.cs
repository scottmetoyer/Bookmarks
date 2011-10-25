using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarks.Models;
using System.Web.Security;
using Bookmarks.Domain.Concrete;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Entities;
using Bookmarks.Infrastructure;

namespace Bookmarks.Controllers
{
    public class AccountController : Controller
    {
        private IFormsAuthentication _authentication;

        public AccountController(IFormsAuthentication authentication)
        {
            _authentication = authentication;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!_authentication.Authenticate(model.Email, model.Password))
                    ModelState.AddModelError("", "Incorrect username or password");
            }

            if (ModelState.IsValid)
            {
                _authentication.SetAuthCookie(model.Email, false);
                return Redirect(returnUrl ?? Url.Action("List", "Bookmarks"));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Authenticate(string email, string password)
        {
            /*
            var user = _membershipProvider.AccountRepository.Users.FirstOrDefault(
                x => x.Email == HttpUtility.UrlDecode(email) &&
                    x.Password == HttpUtility.UrlDecode(password));

            if (user != null)
            {
                if (user.Token == null)
                {
                    user.Token = Guid.NewGuid().ToString();
                    _membershipProvider.AccountRepository.SaveUser(user);
                }

                return Content(user.Token);
            }
            else
            {
                return Content("unauthorized");
            }
             * */
            return View();
        }
    }
}
