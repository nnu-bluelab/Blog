using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;

namespace Blog.DAL
{
    public class ConnectionFactory
    {
        public static DbConnection OpenConnection(string connStr)
        {
            var connection = new MySqlConnection(connStr);
            connection.Open();
            return connection;
        }
    }
}
