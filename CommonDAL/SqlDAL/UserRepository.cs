using System;
using System.Linq;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class UserRepository : SqlRepository<UserData>
    {
        public string GetEmail(int id)
        {
            using (var db = new DataBase())
            {
                return db.UserTable.Where(i => i.Id == id).Select(i => i.Email).FirstOrDefault();
            }
        }

        public bool IsEmailExist(string email)
        {
            using (var db = new DataBase())
            {
                return db.UserTable.Count(i => i.Email == email) != 0;
            }
        }
    }
}
