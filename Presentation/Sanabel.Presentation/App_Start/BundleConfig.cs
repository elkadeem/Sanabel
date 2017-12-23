using System.Web;
using System.Web.Optimization;

namespace Sanabel.Presentation
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

            bundles.Add(new StyleBundle("~/bundles/appplugins").Include(                
                "~/Scripts/pace.min.js",
                "~/Scripts/Chart.min.js",
                "~/Scripts/app.js",
                "~/Scripts/views/main.js"));
            


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/maincss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/appcss").Include(
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/simple-line-icons/css/simple-line-icons.css",
                      "~/fonts/fonts.css",
                      "~/Content/css/style.css"));


        }
    }
}
