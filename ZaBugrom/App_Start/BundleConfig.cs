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

            //Main layouts
            bundles.Add(new ScriptBundle("~/bundles/Shared/_Layout").Include(
                        "~/Scripts/ZaBugrom/Shared/_Layout.js"));
            bundles.Add(new ScriptBundle("~/bundles/Shared/_PostListLayout").Include(
                        "~/Scripts/ZaBugrom/Shared/_PostListLayout.js"));

            //Profile
            bundles.Add(new ScriptBundle("~/bundles/Account/_ProfileLayout").Include(
                        "~/Scripts/ZaBugrom/Account/Register.js"));
            bundles.Add(new ScriptBundle("~/bundles/Account/Register").Include(
                        "~/Scripts/ZaBugrom/Account/Register.js"));
            bundles.Add(new ScriptBundle("~/bundles/Account/Profile").Include(
                        "~/Scripts/ZaBugrom/Account/Profile.js"));

            //AddPost
            bundles.Add(new ScriptBundle("~/bundles/AddPost/AddSimplePost").Include(
                        "~/Scripts/ZaBugrom/AddPost/AddSimplePost.js"));
            bundles.Add(new ScriptBundle("~/bundles/AddPost/AddVideoPost").Include(
                        "~/Scripts/ZaBugrom/AddPost/AddVideoPost.js"));

            //---CSS
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //JQuiry
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery-ui.css",
                        "~/Content/themes/base/jquery-ui.structure.css",
                        "~/Content/themes/base/jquery-ui.theme.css"));

            //Layouts
            bundles.Add(new StyleBundle("~/Pages/Shared/_ProfileLayout/css").Include("~/Content/Pages/Shared/_ProfileLayout.css"));
            bundles.Add(new StyleBundle("~/Pages/Shared/_PostLayout/css").Include("~/Content/Pages/Shared/_PostLayout.css"));

            //Post
            bundles.Add(new StyleBundle("~/Pages/Account/Register/css").Include("~/Content/Pages/Account/Register.css"));

            //Profile
            bundles.Add(new StyleBundle("~/Pages/Account/Register/css").Include("~/Content/Pages/Account/Register.css"));
            bundles.Add(new StyleBundle("~/Pages/Account/Profile/css").Include("~/Content/Pages/Account/Profile.css"));

            //AddPost
            bundles.Add(new StyleBundle("~/Pages/AddPost/Default/css").Include("~/Content/Pages/AddPost/Default.css"));
            bundles.Add(new StyleBundle("~/Pages/AddPost/AddSimplePost/css").Include("~/Content/Pages/AddPost/AddSimplePost.css"));
            bundles.Add(new StyleBundle("~/Pages/AddPost/AddVideoPost/css").Include("~/Content/Pages/AddPost/AddVideoPost.css"));
        }
    }
}