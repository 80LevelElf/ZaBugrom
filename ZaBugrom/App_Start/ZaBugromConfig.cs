using System.Configuration;
using CommonDAL.SqlDAL;
using Models.Data;
using WebMatrix.WebData;
using ZaBugrom.Managers;

namespace ZaBugrom
{
    public static class ZaBugromConfig
    {
        public static void Prepare()
        {
            PrepareSimpleMembershipProvider();
            PrepareHeaderImageManager();

            FirstInicialization();
        }

        private static void PrepareSimpleMembershipProvider()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserData", "Id", "Name", true);
        }

        private static void PrepareHeaderImageManager()
        {
            HeaderImageManager.Load();
        }

        private static void FirstInicialization()
        {
            //Get first header image, if there is no such images
            if (HeaderImageManager.CurrentHeaderImage == null)
            {
                RepositoryManager.HeaderImageRepository.Insert(new HeaderImageData
                {
                    FileName = "1.jpg",
                    ShiftByTop = 30,
                    Title = "Токио"
                });

                HeaderImageManager.Load();
            }
        }
    }
}