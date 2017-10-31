using BLL;
using DAO;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace GedScannerMVC.ClassBD
{
    public class HomeBD : BD
    {
        public HomeBD()
        {

        }

        public HomeBD(HttpSessionStateBase Session)
        {
            session = Session;
            SetDataBase();
        }

        public void EventConnectClick(string DataSource, string InitialCatalog, string UserID, string Password)
        {
            SqlConnectionStringBuilder SqlStringBuilder = new SqlConnectionStringBuilder();

            SqlStringBuilder.DataSource = DataSource;
            SqlStringBuilder.InitialCatalog = InitialCatalog;
            SqlStringBuilder.UserID = UserID;
            SqlStringBuilder.Password = Password;

            connect = new Connection(SqlStringBuilder);
            //connect = new Connection();
        }

        public void EventConnectClick(Connection paramConnect)
        {
            connect = paramConnect;
        }

        public bool GetLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            LoginsInternosBLL BLL = new LoginsInternosBLL(connect);
            return BLL.GetLoginInterno(ref login, ref erro);
        }

        public bool GetExternLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            LoginsInternosBLL BLL = new LoginsInternosBLL(connect);
            return BLL.GetExternLoginInterno(ref login, ref erro);
        }

        public bool GetClientes(ref List<Cliente> clientes, ref TypesErrors erro)
        {
            ClienteBLL BLL = new ClienteBLL(connect);
            return BLL.GetClientes(ref clientes, ref erro);
        }

        public bool GetCliente(ref Cliente cliente, ref TypesErrors erro)
        {
            ClienteBLL BLL = new ClienteBLL(connect);
            return BLL.GetCliente(ref cliente, ref erro);
        }

        public bool GetClienteDataBase(Cliente cliente, ref ClienteDatabase database, ref TypesErrors erro)
        {
            ClienteBLL BLL = new ClienteBLL(connect);
            return BLL.GetClienteDataBase(cliente, ref database, ref erro);
        }
    }
}