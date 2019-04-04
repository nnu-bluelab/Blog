using Blog.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.DAL
{
    public partial class CategoryDAL //ctrl m o | ctrl m l 
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        public string ConnStr { set; get; }

        public int? Insert(Model.Category m)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                int resid = connection.QueryFirstOrDefault<int>("INSERT INTO category(caname,bh,Pbh,remark) values(@CaName,@Bh,@Pbh,@Remark);SELECT @@IDENTITY;",m);
                return resid;
            }
        }

        public dynamic GetTreeJson()
        {
            List<Model.TreeNode_LayUI> list_return = new List<TreeNode_LayUI>();
            List<Model.Category> list = GetList("");
            var top = list.Where(a => a.Pbh == "0");
            foreach (var item in top)
            {
                Model.TreeNode_LayUI node = new TreeNode_LayUI() { id = item.Id, name = item.CaName, spread = false, pid = 0, };
                var sub = list.Where(a => a.Pbh == item.Bh);
                List<Model.TreeNode_LayUI> list_sub = new List<TreeNode_LayUI>();
                foreach (var item2 in sub)
                {
                    Model.TreeNode_LayUI node2 = new TreeNode_LayUI() { id = item2.Id, name = item2.CaName, spread = false, pid = item.Id, };
                    list_sub.Add(node2);
                }
                node.children = list_sub;

                list_return.Add(node);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list_return);
        }

        public bool Delete(int id)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                int res = connection.Execute(@"delete from category where id=@id", new { id=id });
                if(res>0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int CalcCount(string cond)
        {
            string sql = "select count(1) from category";
            if (!string.IsNullOrEmpty(cond))
            {
                sql += $" where {cond}";
            }
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {

                int res = connection.ExecuteScalar<int>(sql);
                return res;
            }
        }

        /// <summary>根据pbh生成下级的bh,自动+1,超过限制则返回文本
        /// 
        /// </summary>
        /// <param name="pbh">父编号</param>
        /// <param name="x">每一级编号的位数</param>
        /// <returns></returns>
        public string GenBH(string pbh, int x)
        {
            string sql = "select right(max(bh)," + x + ") from category where pbh=" + pbh;
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {

                string res = connection.ExecuteScalar<string>(sql);
                if (string.IsNullOrEmpty(res))
                {
                    int a = 1;
                    if (pbh != "0")
                    {
                        return pbh + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }
                else
                {
                    int a = int.Parse(res) + 1;
                    int b = (int)Math.Pow(10, x);
                    if (a >= b)
                    {
                        throw new Exception("编号超过限制!");
                    }
                    if (pbh != "0")
                    {
                        return pbh + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }

            }


        }

        public Category GetModelByBh(string caBh)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from category where bh = @bh";

                var m = connection.QueryFirst<Category>(sql, new { bh = caBh });
                return m;
            }
        }

        public List<Model.Category> GetList(string cond)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from category";
                if(!string.IsNullOrEmpty(cond))
                {
                    sql += $" where {cond}";
                }
                List<Model.Category> list = connection.Query<Category>(sql).AsList();
                return list;
            }
        }

      

        public Model.Category GetModel(int id)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from category where Id = @Id";
             
                var m = connection.QueryFirstOrDefault<Category>(sql,new { id = id});
                return m;
            }
        }

        public bool Update(Model.Category m)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                int res = connection.Execute(@"UPDATE category
                                               SET [CaName] = @CaName
                                                  ,[Bh] = @Bh
                                                  ,[Pbh] = @Pbh
                                                  ,[Remark] = @Remark
                                             WHERE  Id=@Id",m);
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
