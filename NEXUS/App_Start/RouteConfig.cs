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
                name: "Contract",
                url: "admin/contract",
                defaults: new { controller = "Home", action = "Contract", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ContractDetail",
                url: "admin/contract-detail/{id}",
                defaults: new { controller = "Home", action = "ContractDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "User",
                url: "admin/user",
                defaults: new { controller = "Home", action = "User", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserDetail",
                url: "admin/user-detail/{id}",
                defaults: new { controller = "Home", action = "UserDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminProduct",
                url: "admin/product",
                defaults: new { controller = "Home", action = "AdminProduct", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminProductDetail",
                url: "admin/product-detail/{id}",
                defaults: new { controller = "Home", action = "AdminProductDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Shop",
                url: "admin/shop",
                defaults: new { controller = "Home", action = "Shop", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ShopDetail",
                url: "admin/shop-detail/{id}",
                defaults: new { controller = "Home", action = "ShopDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Homepage", id = UrlParameter.Optional }
            );

        }
    }
}
