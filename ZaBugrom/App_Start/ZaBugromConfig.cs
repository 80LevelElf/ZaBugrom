using CommonDAL.Managers;
using Models.Data;
using WebMatrix.WebData;
using ZaBugrom.Managers;

namespace ZaBugrom
{
    public static class ZaBugromConfig
    {
        public static void Prepare()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserData", "Id", "Name", true);
            RepositoryManager.Initialize();
            HeaderImageManager.Load();

            FirstInicialization();
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

            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }
    }
}