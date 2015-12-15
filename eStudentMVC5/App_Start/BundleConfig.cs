using System.Web;
using System.Web.Optimization;

namespace eStudentMVC5
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            /*** Custom CSS and JavaScript ***/
            bundles.Add(new StyleBundle("~/Content/matic").Include(
                      "~/Content/bootstrap.css",
                      "~/static/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/static/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/static/dist/css/sb-admin-2.css",
                      "~/static/dist/css/custom.css",
                      "~/static/bower_components/morrisjs/morris.css",
                      "~/static/bower_components/metisMenu/dist/metisMenu.min.css"
                      //"~/static/bower_components/font-awesome/css/font-awesome.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/matic").Include(
                      "~/static/bower_components/jquery/dist/jquery.min.js",
                      "~/static/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/static/bower_components/metisMenu/dist/metisMenu.min.js",
                      "~/static/bower_components/raphael/raphael-min.js",
                      "~/static/bower_components/morrisjs/morris.min.js",
                      "~/static/dist/js/sb-admin-2.js",
                      "~/static/dist/js/custom.js"
                      ));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
