using BLL;
using BLL.Ged;
using DAO;
using Model;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassBD
{
    public class LoteBD : BD
    {
        public LoteBD()
        {

        }

        public LoteBD(HttpSessionStateBase Session)
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

        public bool GetLotes(ref List<Lote> lotes, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotes(ref lotes, ref erro);
        }

        public bool GetLotes(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLotes(ref lotes, ref pagination, ref erro);
        }

        public bool GetLoteById(ref Lote lote, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetLoteById(ref lote, ref erro);
        }

        public bool GetArquivosByLote(Lote lote, ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetArquivosByLote(lote, ref arquivos, ref erro);
        }

        public bool GetArquivosByLoteClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetArquivosByLoteClassificados(lote, ref arquivos, ref pagination, ref erro);
        }

        public bool GetArquivosByLoteNClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.GetArquivosByLoteNClassificados(lote, ref arquivos, ref pagination, ref erro);
        }

        public bool SetLoteEdit(Lote lote, int login, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.SetLoteEdit(lote, login, ref erro);
        }

        public bool SetLoteInsert(Lote lote, int login, ref TypesErrors erro)
        {
            LoteBLL BLL = new LoteBLL(connect);
            return BLL.SetLoteInsert(lote, login, ref erro);
        }
    }
}