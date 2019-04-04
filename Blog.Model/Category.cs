using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model
{
    public class Category
    {
        //caname = "新分类",bh="222",pbh="0",remark=""
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CaName { get; set; }
        public string Bh { get; set; }
        public string Pbh { get; set; }
        public string Remark { get; set; }


    }
}
