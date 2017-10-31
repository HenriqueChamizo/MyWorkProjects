using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Ged
{
    public class ArquivoDetail
    {
        public int GEDARQ_IND;
        public DateTime GEDARQ_DISPONIVELEM;
        public int GEDARQ_DISPONIVELPOR;
        public byte[] GEDARQ_ARQUIVO;
        public int GEDARQ_TAMANHO;
        public string GEDARQ_EXTENSAO;
        public string GEDARQ_DESCRICAO;
        public DateTime GEDARQ_ACESSOEM;
        public int GEDARQ_ACESSOPOR;
        public ContArquivoTipo GEDARQ_CONTARQUIVOTIPO;
        public int GEDARQ_LOTE;
        public List<ArquivoTipo> tipos;
        public List<PlanoContas> planos;
        public List<Conta> contas;
        public List<PlanoContaConta> planConts;

        public bool condition;
    }

    public class Arquivo
    {
        public int GEDARQ_IND;
        public DateTime GEDARQ_DISPONIVELEM;
        public int GEDARQ_DISPONIVELPOR;
        public byte[] GEDARQ_ARQUIVO;
        public int GEDARQ_TAMANHO;
        public string GEDARQ_EXTENSAO;
        public string GEDARQ_DESCRICAO;
        public DateTime GEDARQ_ACESSOEM;
        public int GEDARQ_ACESSOPOR;
        public ContArqTipo GEDARQ_CONTARQUIVOTIPO;
        public Lote GEDARQ_LOTE;

        public bool condition;
    }
}
