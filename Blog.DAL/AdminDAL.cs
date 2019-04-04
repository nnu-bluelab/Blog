using Blog.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL
{
    public class AdminDAL //ctrl m o | ctrl m l 
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        public string ConnStr { set; get; }

        public Model.Admin GetModel(string username, string password)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {
                string sql = "select * from admin where username = @UserName and password = @Password";
             
               var m = connection.QueryFirstOrDefault<Blog.Model.Admin>(sql,new { UserName = username, Password = password});
               return m;
            }
        }

        public Admin GetModel(int id)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {

                var m = connection.QueryFirstOrDefault<Model.Admin>(
                           @"SELECT
                            *
                            FROM
                            admin WHERE id = @id; ",

                  new { id = id });
                return m;
            }
        }

        public bool UpdatePwd(string pwd, int id)
        {
            using (var connection = ConnectionFactory.OpenConnection(ConnStr))
            {

                int res = connection.Execute(@"UPDATE admin
                                                       SET password=@password
                                                     WHERE Id=@Id", new { password = pwd, id = id });

                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
