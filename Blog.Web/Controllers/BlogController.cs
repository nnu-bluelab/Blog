using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {

        DAL.BlogDAL dal;
        DAL.CategoryDAL cadal;
        public BlogController(DAL.BlogDAL dal, DAL.CategoryDAL cadal)
        {
            this.cadal = cadal;
            this.dal = dal;
        }
        public IActionResult List(int pageindex, int pagesize, string key, string Date, string CaBh)
        {
            List<Model.Blog> list = dal.GetList("*","sort asc,id desc", pagesize, pageindex, GetCond(key, Date, CaBh));
            ArrayList arr = new ArrayList();
            foreach (var item in list)
            {
                arr.Add(new
                {
                    id = item.Id,
                    title = item.Title,
                    createTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    desc = Tool.StringTruncat(Tool.GetNoHTMLString(item.Body),60,"..."),
                    caName = item.CaName,
                });

            }
            return Json(arr);
        }

        /// <summary>
        /// 拼接条件
        /// </summary>
        /// <returns></returns>
        public string GetCond(string key, string month, string cabh)
        {

            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = Tool.GetSafeSQL(key);
                cond += $" and title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(month))
            {
                DateTime d;
                if (DateTime.TryParse(month + "-01", out d))
                {
                    cond += $" and createtime>='{d.ToString("yyyy-MM-dd")}' and createtime<'{d.AddMonths(1).ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(cabh))
            {
                cabh = Tool.GetSafeSQL(cabh);
                cond += $" and cabh='{cabh}'";
            }
            return cond;
        }

        public IActionResult Show(int id)
        {
            Model.Blog m = dal.GetFirstOrDefault(id); 
            if(m==null)
            {
                return Content("扑街仔，某有这文章啊");
            }
            ViewBag.calist = cadal.GetIndexLeft_Ca();
            ViewBag.blogmonth = cadal.GetIndexLeft_Month();
            ViewBag.cadal = cadal;
            ViewBag.blogdal = dal;
            return View(m);
        }

        public IActionResult GetTotalCount(string key, string Date, string CaBh)
        {
 
            int tolacount = dal.getTotalCount(GetCond(key, Date, CaBh));
            return Content(tolacount.ToString());
        }

    }
}
