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
    public class ArquivoTipoBD : BD
    {
        public ArquivoTipoBD()
        {

        }

        public ArquivoTipoBD(HttpSessionStateBase Session)
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

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetArquivosTipos(ref arquivosTipos, ref erro);
        }

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref Model.Pagination pagination, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetArquivosTipos(ref arquivosTipos, ref pagination, ref erro);
        }

        public ArquivoTipo GetArquivoTipo(int ind, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetArquivoTipo(ind, ref erro);
        }

        public List<CampoTipo> GetCampoTipos(ref TypesErrors erro)
        {
            CampoBLL BLL = new CampoBLL(connect);
            return BLL.GetCampoTipos(ref erro);
        }

        public List<CampoDetail> GetCamposByArquivoTipo(int ind, ref TypesErrors erro)
        {
            CampoBLL BLL = new CampoBLL(connect);
            return BLL.GetCamposByArquivoTipo(ind, ref erro);
        }

        public bool SetArquivoTipoAndCamposInsert(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.SetArquivoTipoAndCamposInsert(arquivoTipo, campos, ref erro);
        }

        public bool SetArquivoTipoAndCamposEdit(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.SetArquivoTipoAndCamposEdit(arquivoTipo, campos, ref erro);
        }
    }
}