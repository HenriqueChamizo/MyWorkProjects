using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface IArquivoTipo
    {
        List<ArquivoTipo> GetArquivosTiposFromConta(int indCont, ref TypesErrors erro);
        List<ArquivoTipo> GetArquivosTiposFromContaAndPlano(int indConta, int indPlano, ref TypesErrors erro);
        bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref TypesErrors erro);
        ArquivoTipo GetArquivoTipo(int index, ref TypesErrors erro);
        bool SetArquivoTipoAndCamposInsert(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro);
        bool SetArquivoTipoAndCamposEdit(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro);
        int GetTipoArquivoByArquivo(int indArquivo, ref TypesErrors erro);
    }
}
