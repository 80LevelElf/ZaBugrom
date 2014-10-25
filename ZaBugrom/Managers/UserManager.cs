using System.Web.Security;
using Models.Data;
using WebMatrix.WebData;

namespace ZaBugrom.Managers
{
    public static class UserManager
    {
        public static SimpleMembershipProvider MembershipProvider { get; private set; }

        static UserManager()
        {
            MembershipProvider = (SimpleMembershipProvider)Membership.Provider;
        }

        public static UserData GetCurrentUser()
        {
            return RepositoryManager.UserRepository.GetById(WebSecurity.CurrentUserId);
        }

        public static int UserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        public static string UserName
        {
            get { return WebSecurity.CurrentUserName; }
        }
    }
}