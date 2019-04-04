using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Body_md { get; set; }
        public int VisitNum { get; set; }

        public string CaBh { get; set; }
        public string CaName { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }

        private DateTime _updatetime = DateTime.Now;
        public DateTime updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }

    }
}
