using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Category",
                url: "{controller}/{action}/{s}",
                defaults: new { controller = "Home", action = "CategoryResult", s = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MyPage",
                url: "{controller}/{action}/{username}",
                defaults: new { controller = "User", action = "MyPage", username = UrlParameter.Optional }
            );
        }
    }
}
