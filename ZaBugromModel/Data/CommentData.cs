using System;
using System.Collections.Generic;

namespace Models.Data
{
    public class CommentData : BigData
    {
        public CommentData()
        {
            SubComments = new List<CommentData>();
        }

        public string Source { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        public Int64? ParentCommentId { get; set; }
        public Int64 PostId { get; set; }
        public List<CommentData> SubComments { get; set; }
        public DateTime AddTime { get; set; }
        public int CommentLevel { get; set; }

        public static int MaxCommentLevel
        {
            get { return 7; }
        }
    }
}
