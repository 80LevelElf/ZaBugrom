using System;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class PostRepository : BigSqlRepository<PostData>
    {
        public override Int64 Insert(PostData instance)
        {
            instance.AddTime = DateTime.Now;
            return base.Insert(instance);
        }
    }
}
