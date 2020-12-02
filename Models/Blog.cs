﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brief.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public int CreatorID { get; set; }
        public string Content { get; set; }
    }
}