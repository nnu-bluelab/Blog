using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        AdminDAL adal;
        public LoginController(AdminDAL adal)
        {
            this.adal = adal;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            Model.Admin m = adal.GetModel(username,Tool.MD5Hash(password));
            if(m==null)
            {
                return Content("<script>alert('账号或密码错误');location.href='/Admin/Login'</script>", "text/html;charset=utf-8");
            }
            else
            {
                HttpContext.Session.SetString("adminusername", m.UserName);
                HttpContext.Session.SetInt32("adminid", m.Id);
                return Redirect("/Admin/Home");
            }

        }

        public IActionResult LoginOut()
        {
            HttpContext.Session.Remove("adminusername");
            HttpContext.Session.Remove("adminusername");
            return Redirect("/Home/Index");
        }
    }
}