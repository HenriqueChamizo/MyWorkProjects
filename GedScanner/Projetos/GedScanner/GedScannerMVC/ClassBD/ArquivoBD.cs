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
    public class ArquivoBD : BD
    {
        public ArquivoBD()
        {

        }

        public ArquivoBD(HttpSessionStateBase Session)
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

        public bool GetArquivoById(ref Arquivo arquivo, ref List<ArquivoTipo> arquivosTipos, ref List<PlanoContas> planos,
            ref List<Conta> contas, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.GetArquivoById(ref arquivo, ref arquivosTipos, ref planos, ref contas, ref erro);
        }


        public bool GetArquivoById(ref Arquivo arquivo, ref List<PlanoContas> planosContas, ref List<Conta> contas, ref List<ArquivoTipo> arquivoTipos,
                                      ref List<Campo> campos, ref List<CampoValor> valores, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.GetArquivoById(ref arquivo, ref planosContas, ref contas, ref arquivoTipos, ref campos, ref valores, ref erro);
        }

        public ArquivoTipoDetail GetArquivoTipoDetail(int indArquivo, int indTipo, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.GetArquivoTipoDetail(indArquivo, indTipo, ref erro);
        }

        public List<CampoValorDetail> GetCamposValoresByArquivo(int arquivo, int arquivoTipo, ref TypesErrors erro)
        {
            CampoBLL BLL = new CampoBLL(connect);
            return BLL.GetCamposValoresByArquivo(arquivo, arquivoTipo, ref erro);
        }

        public List<CampoDetail> GetCamposByArquivoTipo(int arquivoTipo, ref TypesErrors erro)
        {
            CampoBLL BLL = new CampoBLL(connect);
            return BLL.GetCamposByArquivoTipo(arquivoTipo, ref erro);
        }

        public int GetTipoArquivoByArquivo(int indArquivo, ref TypesErrors erro)
        {
            ArquivoTipoBLL BLL = new ArquivoTipoBLL(connect);
            return BLL.GetTipoArquivoByArquivo(indArquivo, ref erro);
        }

        public bool SetCampoValues(int arquivo, int plano, int conta, int tipo, List<CampoValor> camposValores, DateTime data, int login, ref TypesErrors erro)
        {
            ArquivoBLL BLL = new ArquivoBLL(connect);
            return BLL.SetCampoValues(arquivo, plano, conta, tipo, camposValores, data, login, ref erro);
        }

        public bool GetContasByPlano(PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContasByPlano(plano, ref contas, ref erro);
        }
    }
}