using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        DAL.BlogDAL dal;
        DAL.CategoryDAL cadal;

        public BlogController(DAL.BlogDAL bdal, DAL.CategoryDAL cadal)
        {
            this.dal = bdal;
            this.cadal = cadal;
        }

        public IActionResult Index()
        {
            List<Model.Blog> list = dal.GetList("1=1 order by sort asc,id desc");
            ViewBag.calist = cadal.GetList("");
            return View(list);
        }

        public IActionResult Add(int? id)
        {
            ViewBag.calist = cadal.GetList("");
            Model.Blog m = new Model.Blog();
            if (id != null)
            {
                m = dal.GetModel(id.Value);
            }
            return View(m);
        }


        public IActionResult List(int pageindex, int pagesize ,string key ,string start,string end ,string type)
        {
            List<Model.Blog> list = dal.GetList("*","sort asc,id desc", pagesize, pageindex, GetCond(key, start, end, type));
            ArrayList arr = new ArrayList();
            foreach (var item in list)
            {
                arr.Add(new
                {
                    id = item.Id,
                    title = item.Title,
                    createTime=item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    vistNum=item.VisitNum,
                    caName=item.CaName,
                    sort = item.Sort
                });

            }
            return Json(arr);
        }


        public IActionResult GetTotalCount(string key, string start, string end, string type)
        {
            int tolacount = dal.getTotalCount(GetCond(key,start,end,type));
            return Content(tolacount.ToString());
        }

        private string GetCond(string key, string start, string end, string type)
        {
            string cond = "1 = 1";
            if (!string.IsNullOrEmpty(key))
            {
                key = Tool.GetSafeSQL(key);
                cond += $" and title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(start))
            {
                DateTime d = new DateTime();
                if (DateTime.TryParse(start, out d))
                {
                    cond += $" and createtime >= '{d.ToString("yyyy-MM-dd")}'";
                }
            }

            if (!string.IsNullOrEmpty(end))
            {
                DateTime d = new DateTime();
                if (DateTime.TryParse(end, out d))
                {
                    cond += $" and createtime <= '{d.ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                cond += $" and caBh = '{type}'";
            }
            return cond;
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Add(Model.Blog m)
        {
            Model.Category ca = cadal.GetModelByBh(m.CaBh);
            if (ca != null)
            {
                m.CaName = ca.CaName;
            }
            if (m.Id == 0)
            {
                dal.Insert(m);
            }
            else
            {
                m.updatetime = DateTime.Now;
                dal.Update(m);
            }
            return Redirect("/Admin/Blog/Index");
        }
        [HttpPost]
        public IActionResult Del(int id)
        {
            bool b = dal.Delete(id);
            if (b)
            {
                return Content("删除成功");
            }
            else
            {
                return Content("删除失败,请联系管理员");
            }
        }

    }
}