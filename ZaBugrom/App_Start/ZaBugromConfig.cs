using System;
using System.Configuration;
using System.IO;
using System.Web;
using CommonDAL.SqlDAL;
using WebMatrix.WebData;
using ZaBugrom.Managers;

namespace ZaBugrom
{
    public static class ZaBugromConfig
    {
        public static void Prepare()
        {
            PrepareSimpleMembershipProvider();
            PrepareBlToolKit();
            PrepareHeaderImageManager();
        }

        private static void PrepareSimpleMembershipProvider()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserData", "Id", "Name", true);
        }

        private static void PrepareBlToolKit()
        {
            //Set connection string
            AbstractSqlRepository.SetDataBaseConfig(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private static void PrepareHeaderImageManager()
        {
            HeaderImageManager.Load();
        }
    }
}