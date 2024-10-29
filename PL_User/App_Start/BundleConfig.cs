using System.Data;
using System.Web.Optimization;

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

            bundles.Add(new StyleBundle("~/Permission/css").Include(
                "~/Content/Permission.css"
            ));

            bundles.Add(new StyleBundle("~/ManageUser/css").Include(
                "~/Content/ManageUser.css"
            ));

            bundles.Add(new StyleBundle("~/ChangePass/css").Include(
                "~/Content/ChangePassword.css"
            ));

            bundles.Add(new StyleBundle("~/AdminLte/css").Include(
                "~/Content/plugins/fontawesome-free/css/all.min.css",
                "~/Content/dist/css/adminlte.min.css",
                "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                "~/Content/plugins/toastr/toastr.min.css"
            ));

            bundles.Add(new StyleBundle("~/Datatable/css").Include(
                "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                "~/Content/dist/css/adminlte.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Validate/Js").Include(
                "~/Content/Js/ManageUser.js"
            ));

            bundles.Add(new ScriptBundle("~/ValidatePassword/Js").Include(
                "~/Content/Js/ValidateChange.js"
            ));

            bundles.Add(new ScriptBundle("~/CheckPermission/Js").Include(
                "~/Content/Js/CheckPermissionUser.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/loadApp").Include(
               "~/Scripts/app.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
               "~/Scripts/dataTables.js"
            ));

            bundles.Add(new ScriptBundle("~/AdminLte/js").Include(
                "~/Content/plugins/jquery/jquery.min.js",
                "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/plugins/datatables/jquery.dataTables.min.js",
                "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                "~/Content/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                "~/Content/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                "~/Content/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                "~/Content/plugins/jszip/jszip.min.js",
                "~/Content/plugins/pdfmake/pdfmake.min.js",
                "~/Content/plugins/pdfmake/vfs_fonts.js",
                "~/Content/datatables-buttons/js/buttons.html5.min.js",
                "~/Content/plugins/datatables-buttons/js/buttons.print.min.js",
               "~/Content/plugins/datatables-buttons/js/buttons.colVis.min.js",
                "~/Content/dist/js/adminlte.min.js",
                "~/Content/plugins/dist/js/demo.js"
            ));

        }
    }
}
