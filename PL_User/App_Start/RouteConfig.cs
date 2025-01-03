﻿using System.Web.Mvc;
using System.Web.Routing;

namespace PL_User
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "FormLogin", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChangePassword",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "ViewChange", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "AddUser",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "AddUser", id = UrlParameter.Optional }
            );

            




        }
    }
}
