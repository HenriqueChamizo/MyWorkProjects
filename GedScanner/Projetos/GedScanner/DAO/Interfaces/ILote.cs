using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface ILote
    {
        bool GetLotes(ref List<Lote> lotes, ref TypesErrors erro);
        bool GetLoteById(ref Lote lote, ref TypesErrors erro);
        bool GetLotesEmAberto(ref List<Lote> lotes, ref TypesErrors erro);
        bool GetLotesNaoEnviados(ref List<Lote> lotes, ref TypesErrors erro);
        bool GetArquivosByLote(Lote lote, ref List<Arquivo> arquivos, ref TypesErrors erro);
        bool SetLoteEdit(Lote lote, int login, ref TypesErrors erro);
        bool SetLoteInsert(Lote lote, int login, ref TypesErrors erro);
    }
}
