using BLL;
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
    public class ContaBD : BD
    {
        public ContaBD()
        {

        }

        public ContaBD(HttpSessionStateBase Session)
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

        public bool GetContas(ref List<Conta> contas, ref Pagination pagination, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContas(ref contas, ref pagination, ref erro);
        }

        public bool GetContasAndAtual(ref PlanoContas planoatual, ref List<Conta> contas, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContasAndAtual(ref planoatual, ref contas, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> contasTipos, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContasTipos(ref contasTipos, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> contasTipos, ref Pagination pagination, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContasTipos(ref contasTipos, ref pagination, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> tipos, ref PlanoContas plano, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContasTipos(ref tipos, ref plano, ref erro);
        }

        public bool GetContaById(ref Conta conta, ref PlanoContas plano, ref List<ContaTipo> tipos, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContaById(ref conta, ref plano, ref tipos, ref erro);
        }

        public bool GetContaTipoById(ref Model.ContaTipo contatipo, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.GetContaTipoById(ref contatipo, ref erro);
        }

        public bool SetContaEdit(Conta conta, int login, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.SetContaEdit(conta, login, ref erro);
        }

        public bool SetContaTipoEdit(ContaTipo tipo, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.SetContaTipoEdit(tipo, ref erro);
        }

        public bool SetContaInsert(Conta conta, int login, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.SetContaInsert(conta, login, ref erro);
        }

        public bool SetContaTipoInsert(ContaTipo tipo, ref TypesErrors erro)
        {
            ContaBLL BLL = new ContaBLL(connect);
            return BLL.SetContaTipoInsert(tipo, ref erro);
        }
    }
}