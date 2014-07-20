using System.Web.Optimization;
using WebMatrix.WebData;

namespace ZaBugrom
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //---Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //My scripts
            bundles.Add(new ScriptBundle("~/bundles/creater").Include(
                        "~/Scripts/ZaBugrom/ElementCreater.js"));
            bundles.Add(new ScriptBundle("~/bundles/preparer").Include(
                        "~/Scripts/ZaBugrom/JQueryUIPreparer.js"));
            bundles.Add(new ScriptBundle("~/bundles/mover").Include(
                        "~/Scripts/ZaBugrom/Mover.js"));

            //Pages
            bundles.Add(new ScriptBundle("~/bundles/Shared/_Layout").Include(
                        "~/Scripts/ZaBugrom/Shared/_Layout.js"));
            bundles.Add(new ScriptBundle("~/bundles/Shared/_PostListLayout").Include(
                        "~/Scripts/ZaBugrom/Shared/_PostListLayout.js"));

            bundles.Add(new ScriptBundle("~/bundles/Account/Register").Include(
                        "~/Scripts/ZaBugrom/Account/Register.js"));
            bundles.Add(new ScriptBundle("~/bundles/Account/Profile").Include(
                        "~/Scripts/ZaBugrom/Account/Profile.js"));

            //---CSS
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery-ui.css",
                        "~/Content/themes/base/jquery-ui.structure.css",
                        "~/Content/themes/base/jquery-ui.theme.css"));

            bundles.Add(new StyleBundle("~/Pages/Account/Register/css").Include("~/Content/Pages/Account/Register.css"));
            bundles.Add(new StyleBundle("~/Pages/Account/Profile/css").Include("~/Content/Pages/Account/Profile.css"));
        }
    }
}