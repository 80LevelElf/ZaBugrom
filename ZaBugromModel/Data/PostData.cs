using System;
using System.Collections.Generic;
using LinqToDB.Mapping;
using Models.Data.Enums;

namespace Models.Data
{
    public class PostData : BigData
    {
        public Int64 AuthorId { get; set; }
        public string AuthorName { get; set; }
        public PostType PostType { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public int Rating { get; set; }
        public DateTime AddTime { get; set; }

        [NotColumn]
        public bool IsVote { get; set; }
        [NotColumn]
        public bool IsVoteUp { get; set; }
        [NotColumn]
        public List<TagData> TagList { get; set; }
    }
}
