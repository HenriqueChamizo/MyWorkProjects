using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface IConta
    {
        bool GetContas(ref List<Conta> contas, ref TypesErrors erro);
        bool GetContasAndAtual(ref PlanoContas planoatual, ref List<Conta> contas, ref TypesErrors erro);
        bool GetContasByPlano(PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro);
        bool GetContasByPlanoAtual(ref PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro);
        bool GetContaById(ref Conta conta, ref PlanoContas plano, ref List<ContaTipo> tipos, ref TypesErrors erro);
        bool GetContasTipos(ref List<ContaTipo> contasTipos, ref TypesErrors erro);
        bool GetContasTipos(ref List<ContaTipo> tipos, ref PlanoContas plano, ref TypesErrors erro);

        //List<ContaPlanoConta> GetContasFromTipo(int indTipo, int indPlan, ref TypesErrors erro);
        bool GetContaTipoById(ref ContaTipo contaTipo, ref TypesErrors erro);

        bool SetContaEdit(Conta conta, int login, ref TypesErrors erro);
        bool SetContaTipoEdit(ContaTipo tipo, ref TypesErrors erro);
        bool SetContaInsert(Conta conta, int login, ref TypesErrors erro);
        bool SetContaTipoInsert(ContaTipo tipo, ref TypesErrors erro);
        bool SetPlanContConta(int indConta, int indPlan, ref TypesErrors erro);
        bool SetPlanContConta(List<string> valores, ref TypesErrors erro);
        bool DelPlanContConta(int indConta, int indPlan, ref TypesErrors erro);
    }
}
