using DAO;
using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Enuns;

namespace BLL
{
    public class ContaBLL : IConta
    {
        Connection conn;
        ContaDAO DAO;
        public ContaBLL(Connection connect)
        {
            conn = connect;
            DAO = new ContaDAO();
            DAO.SetValuesConnection(conn);
        }

        public bool GetContaById(ref Conta conta, ref PlanoContas plano, ref List<ContaTipo> tipos, ref TypesErrors erro)
        {
            return DAO.GetContaById(ref conta, ref plano, ref tipos, ref erro);
        }

        public bool GetContas(ref List<Conta> contas, ref TypesErrors erro)
        {
            return DAO.GetContas(ref contas, ref erro);
        }

        public bool GetContas(ref List<Conta> contas, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetContas(ref contas, ref pagination, ref erro);
        }

        public bool GetContasAndAtual(ref PlanoContas planoatual, ref List<Conta> contas, ref TypesErrors erro)
        {
            return DAO.GetContasAndAtual(ref planoatual, ref contas, ref erro);
        }

        public bool GetContasByPlano(PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro)
        {
            return DAO.GetContasByPlano(plano, ref contas, ref erro);
        }

        public bool GetContaTipoById(ref ContaTipo contaTipo, ref TypesErrors erro)
        {
            return DAO.GetContaTipoById(ref contaTipo, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> tipos, ref PlanoContas plano, ref TypesErrors erro)
        {
            return DAO.GetContasTipos(ref tipos, ref plano, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> tipos, ref TypesErrors erro)
        {
            return DAO.GetContasTipos(ref tipos, ref erro);
        }

        public bool GetContasTipos(ref List<ContaTipo> tipos, ref Pagination pagination, ref TypesErrors erro)
        {
            return DAO.GetContasTipos(ref tipos, ref pagination, ref erro);
        }

        public bool GetContasByPlanoAtual(ref PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro)
        {
            return DAO.GetContasByPlanoAtual(ref plano, ref contas, ref erro);
        }

        public bool SetContaEdit(Conta conta, int login, ref TypesErrors erro)
        {
            return DAO.SetContaEdit(conta, login, ref erro);
        }

        public bool SetContaTipoEdit(ContaTipo tipo, ref TypesErrors erro)
        {
            return DAO.SetContaTipoEdit(tipo, ref erro);
        }

        public bool SetContaInsert(Conta conta, int login, ref TypesErrors erro)
        {
            return DAO.SetContaInsert(conta, login, ref erro);
        }

        public bool SetContaTipoInsert(ContaTipo tipo, ref TypesErrors erro)
        {
            return DAO.SetContaTipoInsert(tipo, ref erro);
        }

        public bool SetPlanContConta(int indConta, int indPlan, ref TypesErrors erro)
        {
            return DAO.SetPlanContConta(indConta, indPlan, ref erro);
        }

        public bool SetPlanContConta(List<string> valores, ref TypesErrors erro)
        {
            return DAO.SetPlanContConta(valores, ref erro);
        }

        public bool DelPlanContConta(int indConta, int indPlan, ref TypesErrors erro)
        {
            return DAO.DelPlanContConta(indConta, indPlan, ref erro);
        }
    }
}
