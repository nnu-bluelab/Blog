using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            int? adminid = HttpContext.Session.GetInt32("adminid");
            if (adminid == null)
            {
                return Redirect("/Admin/Login");
            }
            return View();
        }

        public IActionResult Top()
        {
            string adminname = HttpContext.Session.GetString("adminusername");
            ViewBag.adminname = adminname;
            return View();
        }
        public IActionResult Left() { return View(); }
        public IActionResult Welcome() { return View(); }

    }
}