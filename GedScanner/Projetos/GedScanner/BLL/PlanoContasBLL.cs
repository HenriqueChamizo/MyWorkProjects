using DAO;
using DAO.Interfaces;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PlanoContasBLL : IPlanoContas
    {
        Connection conn;
        PlanoContasDAO DAO;
        public PlanoContasBLL(Connection connect)
        {
            conn = connect;
            DAO = new PlanoContasDAO();
        }

        public bool GetPlanoContasById(ref PlanoContas planoContas, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetPlanoContasById(ref planoContas, ref erro);
        }

        public PlanoContas GetPlanoContasAtual(ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetPlanoContasAtual(ref erro);
        }

        public List<PlanoContas> GetPlanoContasFechados(ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetPlanoContasFechados(ref erro);
        }

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetPlanoContas(ref planosContas, ref erro);
        }

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref Pagination pagination, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetPlanoContas(ref planosContas, ref pagination, ref erro);
        }

        public bool SetPlanoContasEdit(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.SetPlanoContasEdit(planocontas, login, ref erro);
        }

        public bool SetPlanoContasInsert(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.SetPlanoContasInsert(planocontas, login, ref erro);
        }
    }
}
