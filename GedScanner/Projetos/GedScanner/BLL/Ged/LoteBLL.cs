using DAO;
using DAO.Ged;
using DAO.Interfaces;
using Model;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Ged
{
    public class LoteBLL : ILote
    {
        Connection conn;
        LoteDAO DAO;
        public LoteBLL(Connection connect)
        {
            conn = connect;
            DAO = new LoteDAO();
            DAO.SetValuesConnection(conn);
        }

        public bool GetLotes(ref List<Lote> lotes, ref TypesErrors erro)
        {
            return DAO.GetLotes(ref lotes, ref erro);
        }

        public bool GetLotes(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetLotes(ref lotes, ref pagination, ref erro);
        }

        public bool GetLoteById(ref Lote lote, ref TypesErrors erro)
        {
            return DAO.GetLoteById(ref lote, ref erro);
        }

        public bool GetLotesEmAberto(ref List<Lote> lotes, ref TypesErrors erro)
        {
            return DAO.GetLotesEmAberto(ref lotes, ref erro);
        }

        public bool GetLotesEmAberto(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetLotesEmAberto(ref lotes, ref pagination, ref erro);
        }

        public bool GetLotesNaoEnviados(ref List<Lote> lotes, ref TypesErrors erro)
        {
            return DAO.GetLotesNaoEnviados(ref lotes, ref erro);
        }

        public bool GetLotesNaoEnviados(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetLotesNaoEnviados(ref lotes, ref pagination, ref erro);
        }

        public bool GetArquivosByLote(Lote lote, ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            return DAO.GetArquivosByLote(lote, ref arquivos, ref erro);
        }

        public bool GetArquivosByLoteClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetArquivosByLoteClassificados(lote, ref arquivos, ref pagination, ref erro);
        }

        public bool GetArquivosByLoteNClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetArquivosByLoteNClassificados(lote, ref arquivos, ref pagination, ref erro);
        }

        public bool SetLoteEdit(Lote lote, int login, ref TypesErrors erro)
        {
            return DAO.SetLoteEdit(lote, login, ref erro);
        }
        
        public bool SetLoteInsert(Lote lote, int login, ref TypesErrors erro)
        {
            return DAO.SetLoteInsert(lote, login, ref erro);
        }
    }
}
