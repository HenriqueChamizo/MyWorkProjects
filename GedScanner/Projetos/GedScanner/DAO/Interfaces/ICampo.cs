using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface ICampo
    {
        List<CampoTipo> GetCampoTipos(ref TypesErrors erro);
        List<CampoDetail> GetCamposByArquivoTipo(int ind, ref TypesErrors erro);
        List<CampoValorDetail> GetCamposValoresByArquivo(int arquivo, int arquivoTipo, ref TypesErrors erro);
    }
}
