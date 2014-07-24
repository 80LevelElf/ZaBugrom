using System.Web.Security;
using BLToolkit.Reflection;
using CommonDAL.SqlDAL;
using Models.Data;
using WebMatrix.WebData;

namespace ZaBugrom.Managers
{
    public static class UserManager
    {
        public static SimpleMembershipProvider MembershipProvider { get; private set; }

        public static UserRepository UserRepository { get; private set; }

        static UserManager()
        {
            MembershipProvider = (SimpleMembershipProvider)Membership.Provider;
            UserRepository = TypeAccessor<UserRepository>.CreateInstance();
        }

        public static UserData GetCurrentUser()
        {
            return UserRepository.GetById(WebSecurity.CurrentUserId);
        }
    }
}