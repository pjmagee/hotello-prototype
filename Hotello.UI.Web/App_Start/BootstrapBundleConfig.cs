﻿using System.Web.Optimization;

namespace Hotello.UI.Web.App_Start
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-1.9.0.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/jquery-ui-1.9.2.custom.min.js",
                "~/Scripts/jquery.rateit.js",
                "~/Scripts/daterangepicker/date*"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/hotello.css",
                "~/Content/rateit.css",
                "~/Content/themes/bootstrap/jquery-ui-1.9.2.custom.css"));

            bundles.Add(new StyleBundle("~/content/css-responsive").Include("~/Content/bootstrap-responsive.css"));
        }
    }
}