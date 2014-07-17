using System.Web.Security;
using WebMatrix.WebData;

namespace ZaBugrom.Managers
{
    public static class MembershipManager
    {
        private static readonly SimpleMembershipProvider SimpleMembershipProvider;

        static MembershipManager()
        {
            SimpleMembershipProvider = (SimpleMembershipProvider)Membership.Provider;
        }

        public static SimpleMembershipProvider MembershipProvider
        {
            get { return SimpleMembershipProvider; }
        }
    }
}