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
    public class ArquivoBLL : IArquivo
    {
        Connection conn;
        ArquivoDAO DAO;
        public ArquivoBLL(Connection connect)
        {
            conn = connect;
            DAO = new ArquivoDAO();
        }

        public bool GetArquivos(Conta conta, ArquivoTipo arquivoTipo, ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivos(conta, arquivoTipo, ref arquivos, ref erro);
        }

        public bool GetArquivo(ref Arquivo arquivo, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivo(ref arquivo, ref erro);
        }

        public bool GetArquivoById(ref Arquivo arquivo, ref List<ArquivoTipo> arquivosTipos, ref List<PlanoContas> planos,
            ref List<Conta> contas,  ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivoById(ref arquivo, ref arquivosTipos, ref planos, ref contas, ref erro);
        }

        public bool GetArquivoById(ref Arquivo arquivo, ref List<PlanoContas> planosContas, ref List<Conta> contas, ref List<ArquivoTipo> arquivoTipos,
                                      ref List<Campo> campos, ref List<CampoValor> valores, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivoById(ref arquivo, ref planosContas, ref contas, ref arquivoTipos, ref campos, ref valores, ref erro);
        }

        public ArquivoTipoDetail GetArquivoTipoDetail(int indArquivo, int indTipo, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivoTipoDetail(indArquivo, indTipo, ref erro);
        }

        public bool GetArquivosNaoClassificados(ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosNaoClassificados(ref arquivos, ref erro);
        }

        public bool GetArquivosNaoClassificados(ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosNaoClassificados(ref arquivos, ref pagination, ref erro);
        }

        public bool SetCampoValues(int arquivo, int plano, int conta, int tipo, List<CampoValor> camposValores, DateTime data, int login, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.SetCampoValues(arquivo, plano, conta, tipo, camposValores, data, login, ref erro);
        }
    }
}
