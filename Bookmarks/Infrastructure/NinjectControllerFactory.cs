using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using Ninject.Modules;
using Bookmarks.Domain.Abstract;
using Bookmarks.Domain.Concrete;
using System.Configuration;
using System.Web.Security;

namespace Bookmarks.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel = new StandardKernel(new BookmarksServices());

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            return (IController)_kernel.Get(controllerType);
        }
    }

    // Configures how abstract service types are mapped to concrete implementations
    public class BookmarksServices : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookmarkRepository>()
                .To<SqlBookmarkRepository>()
                .WithConstructorArgument(
                    "connectionString",
                    ConfigurationManager.ConnectionStrings["BookmarksConnectionString"].ConnectionString
                    );

            Bind<IAccountRepository>()
               .To<SqlAccountRepository>()
               .WithConstructorArgument(
                   "connectionString",
                   ConfigurationManager.ConnectionStrings["BookmarksConnectionString"].ConnectionString
                   );

            Bind<IFormsAuthentication>().To<FormsAuthenticationWrapper>();
        }
    }
}