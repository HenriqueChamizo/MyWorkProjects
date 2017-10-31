using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace GedScannerMVC.ClassBD
{
    public abstract class BD
    {
        public Connection connect;
        public HttpSessionStateBase session;

        public BD()
        {
            SqlConnectionStringBuilder SqlStringBuilder = new SqlConnectionStringBuilder();

//#if DEBUG
//            SqlStringBuilder.DataSource = "LOCALHOST";
//            SqlStringBuilder.InitialCatalog = "GedControlDataBase";
//            SqlStringBuilder.UserID = "sa";
//            SqlStringBuilder.Password = "1234";
//#else
            SqlStringBuilder.DataSource = "200.98.136.201";
            SqlStringBuilder.InitialCatalog = "GedControlDataBase";
            SqlStringBuilder.UserID = "sa";
            SqlStringBuilder.Password = "@sseC0nt1973";
//#endif

            connect = new Connection(SqlStringBuilder);
        }

        public void SetDataBase()
        {
            LoginsInternos logininterno = session["Usuario"] as LoginsInternos;

            SqlConnectionStringBuilder SqlStringBuilder = new SqlConnectionStringBuilder();

            SqlStringBuilder.DataSource = logininterno.cliente.DATABASE.BASE_SERVER;
            SqlStringBuilder.InitialCatalog = logininterno.cliente.DATABASE.BASE_NOMEBANCO;
            SqlStringBuilder.UserID = logininterno.cliente.DATABASE.BASE_USUARIO;
            SqlStringBuilder.Password = logininterno.cliente.DATABASE.BASE_SENHA;

            connect = new Connection(SqlStringBuilder);
        }
    }
}