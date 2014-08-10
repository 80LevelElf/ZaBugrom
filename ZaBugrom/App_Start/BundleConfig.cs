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
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            //Utils
            bundles.Add(new ScriptBundle("~/bundles/creater").Include("~/Scripts/Utils/ElementCreater.js"));
            bundles.Add(new ScriptBundle("~/bundles/preparer").Include("~/Scripts/Utils/JQueryUIPreparer.js"));
            bundles.Add(new ScriptBundle("~/bundles/mover").Include("~/Scripts/Utils/Mover.js"));
            bundles.Add(new ScriptBundle("~/bundles/messager").Include("~/Scripts/Utils/Messager.js"));

            //---CSS
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site.css"));

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

            BundleTable.EnableOptimizations = false;
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

            //Normolize prefix
            string prefix;
            switch (bundleType)
            {
                case BundleType.Script:
                    prefix = "~/bundles/";
                    break;
                case BundleType.Style:
                    prefix = "~/Content/";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("bundleType");
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
                    throw new ArgumentException("FullPath don't content contentPath!", "contentPath");
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