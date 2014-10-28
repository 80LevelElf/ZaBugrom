using System;

namespace Models.Data
{
    public class PostVotingData
    {
        public Int64 PostId { get; set; }
        public int UserId { get; set; }
        public bool IsVoteUp { get; set; }
    }
}
