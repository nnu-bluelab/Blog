using Blog.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL
{
    public class BlogDAL //ctrl m o | ctrl m l 
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        public string ConnStr { set; get; }

        public List<string> GetTloDate()
        {
            List<string> list;
            string sql = "select LEFT(Convert(Char(10),Blog.CreateTime,120) ,7) as a from blog group by LEFT(Convert(Char(10),Blog.CreateTime,120) ,7) order by  a desc";
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
               list = connection.Query<string>(sql).AsList();
            }
            return list;
        }

        public int Insert(Model.Blog m)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into `blog`(");
            strSql.Append(" title, body, body_md, visitnum, cabh, caname, remark, sort, updatetime  )");
            strSql.Append(" values (");
            strSql.Append(" @title, @body, @body_md, @visitnum, @cabh, @caname, @remark, @sort, @updatetime  )");
            strSql.Append(";select @@IDENTITY");

            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                int i = connection.QuerySingle<int>(strSql.ToString(), m);
                return i;
            }
        }

        public Model.Blog GetFirstOrDefault(int id)
        {
            Model.Blog m=null;
            string sql = @"select * from blog where id=@id";
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                 m = connection.QueryFirstOrDefault<Model.Blog>(sql, new { id = id });
               
            }
            return m;
        }


        public List<Model.Blog> GetList(string fileds, string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            { strWhere = " where " + strWhere; }
            string sql = string.Format("select {0} from `blog` {1} order by {2} limit {3},{4}", fileds, strWhere, orderstr, (PageIndex - 1) * PageSize, PageSize);
            List<Model.Blog> list = new List<Model.Blog>();
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                list = connection.Query<Model.Blog>(sql).AsList();
            }
            return list;
        }

        public int getTotalCount(string v)
        {
            string sql = @"select count(*) from blog";
            if(!string.IsNullOrEmpty(v))
            {
                sql += $" where {v}";
            }
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                int res = connection.ExecuteScalar<int>(sql);
               
                return res;
            }
        }

        public bool Delete(int id)
        {
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                int res = connection.Execute(@"delete from blog where id=@id", new { id=id });
                if(res>0)
                {
                    return true;
                }
                return false;
            }
        }

        public List<Model.Blog> GetList(string cond)
        {
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from blog";
                if(!string.IsNullOrEmpty(cond))
                {
                    sql += $" where {cond}";
                }
                List<Model.Blog> list = connection.Query<Blog.Model.Blog>(sql).AsList();
                return list;
            }
        }

        public Model.Blog GetModel(int id)
        {
            using (var connection =ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from blog where Id = @Id";
             
               var m = connection.QueryFirst<Blog.Model.Blog>(sql,new { id = id});
                return m;
            }
        }

        public bool Update(Model.Blog m)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update `blog` set ");
            strSql.Append("title=@title, body=@body, body_md=@body_md, visitnum=@visitnum, cabh=@cabh, caname=@caname, remark=@remark, sort=@sort, updatetime=@updatetime  ");
            strSql.Append(" where id=@id ");
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                int i = connection.Execute(strSql.ToString(), m);
                return i > 0;
            }
        }

    }
}
