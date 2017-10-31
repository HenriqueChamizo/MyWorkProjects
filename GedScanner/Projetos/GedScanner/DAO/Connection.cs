using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DAO.Interfaces;

namespace DAO
{
    public class Connection
    {
        public string connectionstring;
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlTransaction tran;

        public Connection(string ConnectionString)
        {
            connectionstring = ConnectionString;
        }

        public Connection(string server = "200.98.136.201", string database = "GedControlDataBase", string user = "sa", string password = "@sseC0nt1973")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = server;
            builder.InitialCatalog = database;
            builder.UserID = user;
            builder.Password = password;

            connectionstring = builder.ConnectionString;
        }

        public Connection(SqlConnectionStringBuilder SqlStringBuilder)
        {
            connectionstring = SqlStringBuilder.ConnectionString;
        }

        //public SqlConnection CreateConnection()
        //{
        //    conn = new SqlConnection(connectionstring);
        //    conn.Open();
        //    return conn;
        //}
    }
}