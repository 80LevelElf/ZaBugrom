using System;

namespace Models.Data
{
    public class CommentVotingData
    {
        public Int64 CommentId { get; set; }
        public int UserId { get; set; }
        public bool IsVoteUp { get; set; }
    }
}