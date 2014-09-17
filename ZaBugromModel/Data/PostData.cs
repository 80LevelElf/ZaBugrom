﻿using System;
using Models.Data.Enums;

namespace Models.Data
{
    public class PostData : BigData
    {
        public Int64 AuthorId { get; set; }
        public int AuthorName { get; set; }
        public PostType PostType { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public int Rating { get; set; }
    }
}
