﻿using System.Web.Optimization;

namespace PL_User
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css"
            ));

            bundles.Add(new StyleBundle("~/Datatable/css").Include(
                "~/Content/dataTables.dataTables.css",
                 "~/Content/site.css"
            ));

            bundles.Add(new StyleBundle("~/ManageUser/css").Include(
                "~/Content/ManageUser.css"
            ));

            bundles.Add(new StyleBundle("~/ChangePass/css").Include(
                "~/Content/ChangePassword.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/loadApp").Include(
               "~/Scripts/app.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
               "~/Scripts/dataTables.js"
            ));          
           
        }
    }
}
