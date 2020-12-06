﻿using Brief.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brief.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public BriefUser Creator { get; set; }
        public string CreatorName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Comments { get; set; }
    }
}
