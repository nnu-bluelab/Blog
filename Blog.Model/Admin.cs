﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Remark { get; set; }

    }
}
