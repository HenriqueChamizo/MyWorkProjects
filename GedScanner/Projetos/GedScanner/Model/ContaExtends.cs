using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ContaDetail
    {
        //   [CONT_IND] [int] IDENTITY(1,1) NOT NULL,
        //   [CONT_DESCRICAO] [varchar](100) NOT NULL,
        //   [CONT_ACESSO] [varchar](7) NOT NULL,
        //   [CONT_CONTATIPO] [int] NOT NULL,
        //   [CONT_DT_INICIO] [datetime] NOT NULL,
        //   [CONT_LOGININSERT] [int] NOT NULL,
        //   [CONT_HISTORICO] [int] NOT NULL,
        //   [CONT_ANTERIOR] [int] NULL,
        public int CONT_IND;
        public string CONT_DESCRICAO;
	    public string CONT_ACESSO;
        public int CONT_LOGININSERT;
        public ContaTipo CONT_CONTASTIPOS;
        public DateTime CONT_DT_INICIO;
    }

    public class Conta
    {
        public int CONT_IND;
        public string CONT_DESCRICAO;
        public string CONT_ACESSO;
        public int CONT_LOGININSERT;
        public ContaTipo CONT_CONTASTIPOS;
        public DateTime CONT_DT_INICIO;
        public List<PlanContConta> pcontas;

        public bool condition;
    }

    public class ContaPlanoConta
    {
        public bool EXISTS;
        public Conta conta;
    }

    public class ContaPlanContAtual
    {
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public List<Conta> contas;
    }

    public class ContaTipo
    {
        //   CONTP_IND INT IDENTITY,
        //   CONTP_DESCRICAO VARCHAR(100),
        //   CONTP_CLASSIFICADOR VARCHAR(15),
        public int CONTP_IND;
        public string CONTP_DESCRICAO;
        public string CONTP_CLASSIFICADOR;
        public DateTime CONTP_DT_INICIO;
        public int CONTP_LOGININSERT;
    }

    public class ContaTipoConta
    {
        public int PLAN_IND;
        public string PLAN_DESCRICAO;
        public Conta conta;
        public List<ContaTipo> tipos;
    }

    public class ContArqTipo
    {
        public int CATIP_IND;
        public PlanContConta CATIP_PLANCONTCONTA;
        public ArquivoTipo CATIP_ARQUIVOTIPO;
    }

    public class ContArquivoTipo
    {
        //   CATIP_IND INT IDENTITY,
        //   CATIP_CONTA INT NOT NULL,
        //   CATIP_ARQUIVOTIPO INT NOT NULL,
        public int CATIP_IND;
        public PlanContContaDetail CATIP_PLANCONTCONTA;
        public ArquivoTipo CATIP_ARQUIVOTIPO;
    }
}
