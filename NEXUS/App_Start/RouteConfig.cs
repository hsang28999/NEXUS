using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NEXUS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Homepage",
                url: "homepage",
                defaults: new { controller = "Home", action = "Homepage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product",
                url: "product",
                defaults: new { controller = "Home", action = "Product", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "product-detail/{id}",
                defaults: new { controller = "Home", action = "ProductDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pay",
                url: "pay",
                defaults: new { controller = "Home", action = "Pay", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About Us",
                url: "about-us",
                defaults: new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User Profile",
                url: "user-profile",
                defaults: new { controller = "Home", action = "UserProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "History",
                url: "history",
                defaults: new { controller = "Home", action = "History", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Homepage", id = UrlParameter.Optional }
            );

        }
    }
}
