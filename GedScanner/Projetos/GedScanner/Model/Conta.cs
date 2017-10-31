using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Conta
    {
        //   CONT_IND INT IDENTITY, 
	    //   CONT_DESCRICAO VARCHAR(100), 
	    //   CONT_ACESSO VARCHAR(7), 
        //   CONT_CONTATIPO INT, 
        public int CONT_IND;
        public string CONT_DESCRICAO;
	    public string CONT_ACESSO;
        public ContaTipo CONT_CONTASTIPOS;
    }

    public class ContaPlanoConta
    {
        //public int PCONT_IND;
        //public int PCONT_CONTA;
        //public int PCONT_PLANOCONTA;
        public bool EXISTS;
        public Conta conta;
    }

    public class ContaPlanContAtual
    {
        public string PLAN_DESCRICAO;

        public List<Conta> contas;
    }
}
