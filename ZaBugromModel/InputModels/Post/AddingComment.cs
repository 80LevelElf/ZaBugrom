using System;

namespace Models.InputModels.Post
{
    public class AddingComment
    {
        public Int64 PostId { get; set; }
        public Int64? ParentCommentId { get; set; }
        public string Source { get; set; }
    }
}
