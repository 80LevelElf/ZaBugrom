using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace ZaBugrom
{
    public class BundleConfig
    {
        enum BundleType
        {
            Style,
            Script
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //---Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.zabugrom.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/Scripts/jquery-ui-{version}.js", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.autosize").Include("~/Scripts/jquery.autosize.js"));
            bundles.Add(new ScriptBundle("~/bundles/textext").IncludeDirectory("~/Scripts/textext", "*.js"));

            //Utils
            bundles.Add(new ScriptBundle("~/bundles/creater").Include("~/Scripts/Utils/ElementCreater.js"));
            bundles.Add(new ScriptBundle("~/bundles/preparer").Include("~/Scripts/Utils/JQueryUIPreparer.js"));
            bundles.Add(new ScriptBundle("~/bundles/mover").Include("~/Scripts/Utils/Mover.js"));
            bundles.Add(new ScriptBundle("~/bundles/messager").Include("~/Scripts/Utils/Messager.js"));
            bundles.Add(new ScriptBundle("~/bundles/postManager").Include("~/Scripts/Utils/PostManager.js"));
            bundles.Add(new ScriptBundle("~/bundles/videoManager").Include("~/Scripts/Utils/VideoManager.js"));
            bundles.Add(new ScriptBundle("~/bundles/cssLoad").Include("~/Scripts/Utils/CssLoad.js"));

            //---CSS
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/parseData").Include("~/Content/ParseData.css"));
            bundles.Add(new ScriptBundle("~/Content/textext").IncludeDirectory("~/Content/textext", "*.css"));

            //JQuiry
            bundles.Add(new StyleBundle("~/Content/themes/baseTeame").Include(
                        "~/Content/themes/base/jquery-ui.css",
                        "~/Content/themes/base/jquery-ui.theme.css"));
            
            //Pages scripts bundles
            foreach (var bundle in GetPagesBundles("\\Scripts\\Pages\\", BundleType.Script))
            {
                bundles.Add(bundle);
            }

            //Pages styles bundles
            foreach (var bundle in GetPagesBundles("\\Content\\Pages\\", BundleType.Style))
            {
                bundles.Add(bundle);
            }
        }

        /// <summary>
        /// Return bundles created by files from specified folder
        /// </summary>
        /// <param name="directory">Directory like "/Scripts/Pages/"</param>
        /// <param name="bundleType"></param>
        /// <returns></returns>
        private static IEnumerable<Bundle> GetPagesBundles(string directory, BundleType bundleType)
        {
            //Normolize directory
            if (!directory.EndsWith("\\"))
            {
                directory += "\\";
            }

            var fullPath = HttpContext.Current.Server.MapPath("~" + directory);

            foreach (var fileName in GetFiles(fullPath, directory))
            {
                //back slashes to normal type
                var fileNameWithRightSlash = fileName.Replace("\\", "/");
                var directoryWithRightSlash = directory.Replace("\\", "/");

                Bundle bundle;
                switch (bundleType)
                {
                    case BundleType.Script:
                        bundle = new ScriptBundle("~" + directoryWithRightSlash.Replace("Scripts", "bundles") + RemoveExtension(fileNameWithRightSlash));
                        break;
                    case BundleType.Style:
                        bundle = new StyleBundle("~" + directoryWithRightSlash + RemoveExtension(fileNameWithRightSlash));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("bundleType");
                }

                yield return bundle.Include("~" + directoryWithRightSlash + fileNameWithRightSlash);
            }
        }

        /// <summary>
        /// Return collection of all files(it get files from subfolder too) and return theirs content path like this:
        /// Home/Index.css
        /// </summary>
        private static IEnumerable<string> GetFiles(string fullPath, string contentPath)
        {
            foreach (var filePath in GetFullPathFiles(fullPath))
            {
                var startIndex = filePath.IndexOf(contentPath, StringComparison.Ordinal);

                if (startIndex == -1)
                {
                    throw new ArgumentException("FullPath doen't content contentPath!", "contentPath");
                }

                startIndex += contentPath.Length;
                var count = filePath.Length - startIndex;

                yield return filePath.Substring(startIndex, count);
            }
        }
 
        /// <summary>
        /// Return collection of all files(it get files from subfolder too) and return theirs full path like this:
        /// C:/folder/Home/Index.css
        /// </summary>
        private static IEnumerable<string> GetFullPathFiles(string fullPath)
        {
            //Get all files of this level
            foreach (var file in Directory.GetFiles(fullPath))
            {
                yield return file;
            }

            //Get other level files
            foreach (var currentDirectory in Directory.GetDirectories(fullPath))
            {
                foreach (var file in GetFullPathFiles(currentDirectory))
                {
                    yield return file;
                }
            }
        }

        /// <summary>
        /// ~/folder/file.ext to ~/folder/file
        /// </summary>
        private static string RemoveExtension(string filePath)
        {
            var pointIndex = filePath.LastIndexOf(".", StringComparison.Ordinal);

            if (pointIndex == -1)
            {
                throw new ArgumentException("FilePath must contain extension to remove it!");
            }

            var count = filePath.Length - pointIndex;

            return filePath.Remove(pointIndex, count);
        }
    }
}