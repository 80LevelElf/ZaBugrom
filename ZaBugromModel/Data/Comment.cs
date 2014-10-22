using System;
using System.Collections.Generic;

namespace Models.Data
{
    public class CommentData : Data
    {
        public CommentData()
        {
            SubComments = new List<CommentData>();
        }

        public string Source { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        public Int64 ParentCommentId { get; set; }
        public List<CommentData> SubComments { get; set; }
    }
}
