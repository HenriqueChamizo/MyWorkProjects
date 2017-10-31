using BLL;
using BLL.Ged;
using DAO;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassBD
{
    public class DashboardBD : BD
    {
        public DashboardBD()
        {

        }

        public DashboardBD(HttpSessionStateBase Session)
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

        public bool GetDashboard(ref Dashboard dashboard, ref TypesErrors erro)
        {
            DashboardBLL BLL = new DashboardBLL(connect);
            return BLL.GetDashboard(ref dashboard, ref erro);
        }

        public bool GetArquivosNaoClassificados(ref List<Model.Ged.Arquivo> arquivos, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.GetArquivosNaoClassificados(ref arquivos, ref erro);
        }

        public bool GetArquivosNaoClassificados(ref List<Model.Ged.Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.GetArquivosNaoClassificados(ref arquivos, ref pagination, ref erro);
        }

        public bool GetLotesEmAberto(ref List<Model.Ged.Lote> lotes, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotesEmAberto(ref lotes, ref erro);
        }

        public bool GetLotesEmAberto(ref List<Model.Ged.Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotesEmAberto(ref lotes, ref pagination, ref erro);
        }

        public bool GetLotesNaoEnviados(ref List<Model.Ged.Lote> lotes, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotesNaoEnviados(ref lotes, ref erro);
        }

        public bool GetLotesNaoEnviados(ref List<Model.Ged.Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotesNaoEnviados(ref lotes, ref pagination, ref erro);
        }

        public bool GetArquivosTipos(ref List<Model.Ged.ArquivoTipo> arquivosTipos, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetArquivosTipos(ref arquivosTipos, ref erro);
        }

        public bool GetArquivosTipos(ref List<Model.Ged.ArquivoTipo> arquivosTipos, ref Pagination pagination, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetArquivosTipos(ref arquivosTipos, ref pagination, ref erro);
        }
    }
}