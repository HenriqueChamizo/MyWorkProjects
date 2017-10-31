using BLL;
using DAO;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace GedScannerMVC.ClassBD
{
    public class PlanoContasBD : BD
    {
        public PlanoContasBD()
        {

        }

        public PlanoContasBD(HttpSessionStateBase Session)
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

        public bool GetPlanoContasById(ref PlanoContas planoContas, ref TypesErrors erro)
        {
            PlanoContasBLL BLL = new PlanoContasBLL(connect);
            return BLL.GetPlanoContasById(ref planoContas, ref erro);
        }

        //public PlanoContas GetPlanoContasAtual(ref TypesErrors erro)
        //{
        //    PlanoContasBLL BLL = new PlanoContasBLL(connect);
        //    return BLL.GetPlanoContasAtual(ref erro);
        //}

        //public List<PlanoContas> GetPlanoContasFechados(ref TypesErrors erro)
        //{
        //    PlanoContasBLL BLL = new PlanoContasBLL(connect);
        //    return BLL.GetPlanoContasFechados(ref erro);
        //}

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref TypesErrors erro)
        {
            PlanoContasBLL BLL = new PlanoContasBLL(connect);
            return BLL.GetPlanoContas(ref planosContas, ref erro);
        }

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref Pagination pagination, ref TypesErrors erro)
        {
            PlanoContasBLL BLL = new PlanoContasBLL(connect);
            return BLL.GetPlanoContas(ref planosContas, ref pagination, ref erro);
        }

        public bool SetPlanoContasEdit(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            PlanoContasBLL BLL = new PlanoContasBLL(connect);
            return BLL.SetPlanoContasEdit(planocontas, login, ref erro);
        }

        public bool SetPlanoContasInsert(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            PlanoContasBLL BLL = new PlanoContasBLL(connect);
            return BLL.SetPlanoContasInsert(planocontas, login, ref erro);
        }
    }
}