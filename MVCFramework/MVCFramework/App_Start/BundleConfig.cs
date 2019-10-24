using System.Web.Optimization;

namespace MVCFramework
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/wwwroot/lib/jquery/dist/*.js",
                    "~/wwwroot/lib/jquery-validation/dist/*.js",
                    "~/wwwroot/js/*.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/wwwroot/lib/jquery-validation-unobtrusive/*.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/wwwroot/lib/bootstrap/dist/js/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/wwwroot/lib/bootstrap/dist/css/*.css",
                   "~/wwwroot/css/*.css"
                   ));
        }
    }
}