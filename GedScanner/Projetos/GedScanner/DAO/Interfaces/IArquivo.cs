using Model;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface IArquivo
    {
        bool GetArquivos(Conta conta, ArquivoTipo arquivoTipo, ref List<Arquivo> arquivos, ref TypesErrors erro);
        bool GetArquivo(ref Arquivo arquivo, ref TypesErrors erro);
        bool GetArquivoById(ref Arquivo arquivo, ref List<ArquivoTipo> arquivosTipos, ref List<PlanoContas> planos,
            ref List<Conta> contas, ref TypesErrors erro);
        bool GetArquivoById(ref Arquivo arquivo, ref List<PlanoContas> planosContas, ref List<Conta> contas, ref List<ArquivoTipo> arquivoTipos,
                                      ref List<Campo> campos, ref List<CampoValor> valores, ref TypesErrors erro);
        ArquivoTipoDetail GetArquivoTipoDetail(int indArquivo, int indTipo, ref TypesErrors erro);
        bool GetArquivosNaoClassificados(ref List<Arquivo> arquivos, ref TypesErrors erro);
        bool SetCampoValues(int arquivo, int plano, int conta, int tipo, List<CampoValor> camposValores, DateTime data, int login, ref TypesErrors erro);
    }
}
