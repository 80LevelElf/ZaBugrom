using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class DataBase : LinqToDB.Data.DataConnection
    {
        public DataBase() : base("DefaultConnection")
        {
            
        }

        public ITable<HeaderImageData> HeaderImageTable { get { return GetTable<HeaderImageData>(); } }
        public ITable<MessageData> MessageTable { get { return GetTable<MessageData>(); } }
        public ITable<PostData> PostTable { get { return GetTable<PostData>(); } }
        public ITable<UserData> UserTable { get { return GetTable<UserData>(); } }
        public ITable<CommentData> CommentTable { get { return GetTable<CommentData>(); } }
        public ITable<PostVotingData> PostVotingTable { get { return GetTable<PostVotingData>(); } }
    }
}
