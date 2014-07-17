using System.Configuration;
using CommonDAL.SqlDAL;
using WebMatrix.WebData;

namespace ZaBugrom.App_Start
{
    public static class ZaBugromConfig
    {
        public static void Prepare()
        {
            PrepareSimpleMembershipProvider();
            PrepareBlToolKit();
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
    }
}