using BLL.Ged;
using DAO;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassBD
{
    public class CampoBD : BD
    {
        public CampoBD()
        {

        }

        public CampoBD(HttpSessionStateBase Session)
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

        public List<CampoValorDetail> GetCamposValoresByArquivo(int arquivo, int arquivoTipo, ref TypesErrors erro)
        {
            CampoBLL BLL = new CampoBLL(connect);
            return BLL.GetCamposValoresByArquivo(arquivo, arquivoTipo, ref erro);
        }
    }
}