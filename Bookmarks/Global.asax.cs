using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bookmarks.Infrastructure;

namespace Bookmarks
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Match empty URL (~/)
            routes.MapRoute(
                null,
                string.Empty,
                new
                {
                    controller = "Bookmarks",
                    action = "List",
                    page = 1
                }
            );

            // Match new
            routes.MapRoute(
               null,
               "New",
               new
               {
                   controller = "Bookmarks",
                   action = "New",
                   page = 1
               }
           );

            // Map numeric pages ~/Page123, ~/Page2
            routes.MapRoute(
                null,
                "Page{page}",
                new
                {
                    controller = "Bookmarks",
                    action = "List"
                },
                new
                {
                    page = @"\d+"
                }
            );

            // Match Admin
            routes.MapRoute(
                null,
                "Bookmarks/{controller}/{action}",
                new
                {
                    controller = "Bookmarks",
                    action = "List",
                    page = 1
                }
            );

            routes.MapRoute(
               null,
               "{controller}/{action}"
           );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            RegisterRoutes(RouteTable.Routes);

            // Register custom model binders
            ModelBinders.Binders.Add(typeof(string[]), new StringSplitModelBinder());
        }
    }
}