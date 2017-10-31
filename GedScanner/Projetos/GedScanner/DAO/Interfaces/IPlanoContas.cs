using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface IPlanoContas
    {
        bool GetPlanoContasById(ref PlanoContas planoContas, ref TypesErrors erro);
        PlanoContas GetPlanoContasAtual(ref TypesErrors erro);
        List<PlanoContas> GetPlanoContasFechados(ref TypesErrors erro);
        bool GetPlanoContas(ref List<PlanoContas> planosContas, ref TypesErrors erro);
        bool GetPlanoContas(ref List<PlanoContas> planosContas, ref Pagination pagination, ref TypesErrors erro);
        bool SetPlanoContasEdit(PlanoContas planocontas, int login, ref TypesErrors erro);
        bool SetPlanoContasInsert(PlanoContas planocontas, int login, ref TypesErrors erro);
    }
}
