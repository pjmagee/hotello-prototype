using System.Web.Optimization;

namespace Hotello.UI.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            /**
             * Script Bundles
             */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // jQuery UI Base Theme
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // jQuery UI Bootstrap Custom Theme
            bundles.Add(new ScriptBundle("~/bundles/jqueryui-bootstrap").Include(
                        "~/Scripts/jquery-ui-1.9.2.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryrateit").Include(
                        "~/Scripts/jquery.rateit*"));


            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                        "~/Scripts/daterangepicker/date*"));

            /**
             * Style Bundles
             */


            // Bootstrap Framework 
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/daterangepicker").Include(
                        "~/Content/ui.daterangepicker.css"));

            // jQuery UI Custom Bootstrap Theme
            bundles.Add(new StyleBundle("~/Content/themes/bootstrap/css").Include(
                        "~/Content/themes/bootstrap/jquery-ui-{version}.custom.css"));

            // Default jQuery UI Base Theme
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.all.css"));
        }
    }
}