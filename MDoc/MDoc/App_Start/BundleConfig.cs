using System.Web;
using System.Web.Optimization;

namespace MDoc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            var corejs = new[]
            {
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-confirmation.min.js",
                "~/Scripts/respond.js",
                "~/Scripts/kendo.modernizr.custom.js",
                "~/Scripts/select2.js",
                "~/Scripts/custom/select2.js",
                "~/Scripts/freeow/jquery.freeow.min.js",
                "~/Scripts/custom.js",
                "~/Scripts/application/common.js"
            };

            var coreCss = new[]
            {
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/select2/select2.css",
                "~/Content/select2-bootstrap.css",
                "~/Content/freeow.css",
                "~/Content/custom.css",
                "~/Content/loading.css",
                "~/Content/Site.css"
            };

            bundles.Add(new ScriptBundle("~/bundles/coreJs").Include(corejs));
            bundles.Add(new StyleBundle("~/Content/coreCss").Include(coreCss));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryVal").Include(
                "~/Scripts/jquery.validate.*",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/logon").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/logon.css"));

            bundles.Add(new ScriptBundle("~/application/school").Include("~/Scripts/application/school.js"));
            bundles.Add(new ScriptBundle("~/application/customer").Include("~/Scripts/application/customer.js"));
            bundles.Add(new ScriptBundle("~/application/document").Include("~/Scripts/application/document.js"));
            bundles.Add(new ScriptBundle("~/application/user").Include("~/Scripts/application/user.js"));
            bundles.Add(new ScriptBundle("~/application/setting").Include("~/Scripts/application/setting.js"));
            bundles.Add(new ScriptBundle("~/application/notice").Include("~/Scripts/application/notice.js"));
            bundles.Add(new ScriptBundle("~/application/plugin").Include("~/Scripts/plugins/clock/clock-1.1.0.min.js"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
