using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace ZaBugrom.App_Start
{
    public static class ZaBugromConfig
    {
        public static void Prepare()
        {
            PrepareSimpleMembershipProvider();
        }

        private static void PrepareSimpleMembershipProvider()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Name", true);
        }
    }
}