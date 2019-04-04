using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        DAL.BlogDAL bdal;
        DAL.CategoryDAL cadal;
        public HomeController(DAL.BlogDAL bdal,DAL.CategoryDAL cadal)
        {
            this.cadal = cadal;
            this.bdal = bdal;
        }

        public IActionResult Index(string key, string Date, string CaBh)
        {
            if(key=="Login"||key=="登陆")
            {
                return Redirect("/Admin/Login");
            }
            ViewBag.calist = cadal.GetIndexLeft_Ca();
            ViewBag.blogmonth = cadal.GetIndexLeft_Month();
            ViewBag.key = key;
            ViewBag.Date = Date;
            ViewBag.Cabh = CaBh;
            ViewBag.blogdal = bdal;
            ViewBag.cadal = cadal;
            return View();
        }

 
    }
}
