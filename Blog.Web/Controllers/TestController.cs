using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class TestController : Controller
    {
        public DAL.CategoryDAL cadal;
        public DAL.BlogDAL bdal ;
        public TestController(DAL.CategoryDAL category,DAL.BlogDAL blog)
        {
            this.cadal = category;
            this.bdal = blog;
        }

        public IActionResult Index()
        {
            string str = "";

            Random r = new Random();
            List<Model.Category> list = cadal.GetList("");

            for (int i = 0; i < 100; i++)
            {
                Model.Category ca = list[r.Next(0, list.Count)];
                Model.Blog m = new Model.Blog
                {
                    Title = $"第{i}条数据",
                    Body = $"第{i}条数据的内容",
                    CaBh = ca.Bh,
                    CaName = ca.CaName,
                    VisitNum = r.Next(100, 999)
                };

                bdal.Insert(m);
            }
            str = "添加100条数据成功";

            //str = "新增返回的值" + cadal.Insert(new Model.Category() {  CaName="789"}) + "<hr />";
            //bool b = cadal.Delete(7);
            //str += "删除ID=7的数据" + b + "<hr />";
            //Model.Category ca = cadal.GetModel(8);
            //if(ca!=null)
            //{
            //    ca.CaName = "新修改的名字" + DateTime.Now;
            //    bool b2 = cadal.Update(ca);
            //    str += "修改ID=8的实体类结果" + b2 + "<hr />";
            //}

            //List<Model.Category> list = cadal.GetList("");
            //foreach (var item in list)
            //{
            //    str += $"<div>分类ID:{item.Id},分类名称：{item.CaName}</div>";
            //}
            return Content(str);
        }
    }
}