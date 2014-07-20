using BLToolkit.Data;
using BLToolkit.DataAccess;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public abstract class UserRepository:AbstractSqlRepository<UserData>
    {
        [SprocName("spUserGetEmail")]
        public abstract string GetEmail(int id);

        [SprocName("spUserIsEmailExist"), ScalarSource(ScalarSourceType.ReturnValue)]
        public abstract bool IsEmailExist(string email);
    }
}
