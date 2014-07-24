namespace ZaBugrom.Managers
{
    public static class ContentPathManager
    {
        private const string ContentFolder = "/Content/";

        public static string ImagesFolder
        {
            get { return ContentFolder + "images/"; }
        }

        public static string UserAvatarFolder
        {
            get { return ImagesFolder + "UserAvatars/"; }
        }
    }
}
