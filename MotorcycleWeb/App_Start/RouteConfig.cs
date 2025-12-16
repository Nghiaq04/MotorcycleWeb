using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MotorcycleWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Promotion",
                url: "khuyen-mai",
                defaults: new { controller = "Promotion", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "CheckOut",
                url: "thanh-toan",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "CheckoutSuccess",
                url: "thanh-toan-thanh-cong",
                defaults: new { controller = "ShoppingCart", action = "CheckOutSuccess" }
            );
            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "CategoryProduct",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "detailProducts",
                url: "chi-tiet/{alias}-p{id}",
                defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Products",
                url: "san-pham",
                defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "DetailNew",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Details", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "NewList",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MotorcycleWeb.Controllers" }
            );
        }
    }
}
