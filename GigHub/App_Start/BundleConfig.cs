﻿using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // App scripts
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/services/*.js",
                        "~/Scripts/app/controllers/*.js",
                        "~/Scripts/app/app.js"
                        ));

            // Third party libraries
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/underscore-min.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.min.js"
                        ));

            // Validation on input forms
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-overrides.css",
                      "~/Content/site.css",
                      "~/Content/animate.css"));
        }
    }
}
