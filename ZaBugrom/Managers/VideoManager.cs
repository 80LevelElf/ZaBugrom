using System;

namespace ZaBugrom.Managers
{
    public static class VideoManager
    {
        public static string GetYoutubeVideoId(string url)
        {
            var start = url.IndexOf("?v=", StringComparison.Ordinal);

            if (start == -1)
            {
                return null;
            }

            const int lengthOfPrefix = 3; /*length of ?v=*/
            int count = url.Length - start - lengthOfPrefix;

            return url.Substring(start + lengthOfPrefix, count);
        }
    }
}