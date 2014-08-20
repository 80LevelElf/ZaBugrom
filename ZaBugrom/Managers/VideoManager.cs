using System;

namespace ZaBugrom.Managers
{
    public static class VideoManager
    {
        private const string YoutubePrefix = "?v=";

        /// <summary>
        /// Try to identify special Youtube video id from Yuotube url.
        /// For example: http://www.youtube.com/watch?v=i4RPuPKzp0U&list=UUSZ69a-0I1RRdNssyttBFcA -> i4RPuPKzp0U
        /// </summary>
        public static string GetYoutubeVideoId(string url)
        {
            var startIndex = url.IndexOf(YoutubePrefix, StringComparison.Ordinal);

            if (startIndex == -1)
            {
                return null;
            }

            var endIndex = url.IndexOf('&', startIndex + YoutubePrefix.Length);

            if (endIndex == -1)
            {
                endIndex = url.Length;
            }

            int count = endIndex - startIndex - YoutubePrefix.Length;

            return url.Substring(startIndex + YoutubePrefix.Length, count);
        }
    }
}