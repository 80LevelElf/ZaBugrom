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

            int count = url.Length - start - 3/*length of ?v=*/;

            return url.Substring(start, count);
        }
    }
}