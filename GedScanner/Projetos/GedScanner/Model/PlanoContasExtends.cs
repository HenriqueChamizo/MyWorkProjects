using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PlanoContasDetail
    {
        //   PLAN_IND INT IDENTITY, 
        //   PLAN_DESCRICAO VARCHAR(100) NOT NULL,
        //   PLAN_CODIGO VARCHAR(10) NOT NULL,
        //   PLAN_FECHADO BIT NOT NULL, 
        //   PLAN_DT_INICIO DATETIME NOT NULL,
        //   PLAN_LOGININSERT INT NOT NULL, 
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public string PLAN_CODIGO;
        public bool PLAN_FECHADO;
        public int PLAN_LOGININSERT;
        public DateTime PLAN_DT_INICIO;
    }

    public class PlanoContas
    {
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public string PLAN_CODIGO;
        public bool PLAN_FECHADO;
        public int PLAN_LOGININSERT;
        public DateTime PLAN_DT_INICIO;
        public List<PlanoContaConta> pcontas;

        public bool condition;
    }

    public class PlanContConta
    {
        public int PCONT_IND;
        public PlanoContas planocontas;
        public Conta conta;
        public List<ContArqTipo> cArqTipos;
    }

    public class PlanoContaConta
    {
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public string PLAN_CODIGO;
        public bool PLAN_FECHADO;
        public int PLAN_LOGININSERT;
        public DateTime PLAN_DT_INICIO;

        public List<Conta> contas;

        public void Contas(List<int> contasInd)
        {
            contas = new List<Conta>();
            Conta conta;
            foreach (int i in contasInd)
            {
                conta = new Conta();
                conta.CONT_IND = i;
                contas.Add(conta);
            }
        }
    }

    public class PlanContContaDetail
    {
        public PlanoContas PCONT_PLANOCONTA;
        public Conta PCONT_CONTA;
    }

    public class PlanoContasDropDown
    {
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public string PLAN_CODIGO;
    }

    public class PlanoContasAll
    {
        public List<PlanoContaConta> plancontContas;

        public List<Conta> contas;
    }
}
