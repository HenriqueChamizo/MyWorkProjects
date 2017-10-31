using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LoginsInternos
    {
        //   LOGI_IND int IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
        //   LOGI_NOME varchar(120) NOT NULL,
        //   LOGI_CARGO nchar(50) NOT NULL,
        //   LOGI_EMAIL char(60) NOT NULL,
        //   LOGI_TELEFONE nchar(20) NOT NULL,
        //   LOGI_CELULAR_DDD nchar(4) NULL,
        //   LOGI_CELULAR nchar(20) NULL,
        //   LOGI_SETOR tinyint NOT NULL,
        //   LOGI_PWD char(20) NOT NULL,
        //   LOGI_ULTIMO_ACESSO smalldatetime NULL,
        //   LOGI_FOTO varbinary(max) NULL,
        //   LOGI_FOTO_DATA smalldatetime NULL,
        //   LOGI_ATIVO bit NOT NULL,
        //   LOGI_SUPERVISOR bit NOT NULL,
        //   LOGI_HOME_OFFICE bit NOT NULL,
        //   LOGI_AUDITOR bit NOT NULL,
        //   LOGI_DIRETORIA bit NOT NULL,
        //   LOGI_PROGRAMACAO bit NOT NULL,
        //   LOGI_TESTE bit NOT NULL,
        //   LOGI_ANIVERSARIO date NULL,
        //   LOGI_PWD_ENVIADO_EM smalldatetime NULL,
        //   LOGI_USUARIO_DOMINIO nvarchar(50) NULL,
        //   LOGI_SETOR_SELECAO tinyint NULL,
        //   LOGI_LOGIN_INTERNO_SELECAO int NULL,
        //   LOGI_DESLIGAMENTO smalldatetime NULL,
        //   LOGI_PRAZO_ACRESCENTAR smallint NOT NULL,
        //   LOGI_RECEBER_AVISO_ALTERACAO_PERFIL bit NOT NULL,

        public int LOGI_IND;
        public string LOGI_NOME;
        public string LOGI_CARGO;
        public string LOGI_EMAIL;
        public string LOGI_TELEFONE;
        public string LOGI_CELULAR_DDD;
        public string LOGI_CELULAR;
        public int LOGI_SETOR;
        public string LOGI_SETOR_DESCRICAO;
        public string LOGI_PWD;
        public DateTime LOGI_ULTIMO_ACESSO;
        public byte[] LOGI_FOTO;
        public DateTime LOGI_FOTO_DATA;
        public bool LOGI_ATIVO;
        public bool LOGI_SUPERVISOR;
        public bool LOGI_HOME_OFFICE;
        public bool LOGI_AUDITOR;
        public bool LOGI_DIRETORIA;
        public bool LOGI_PROGRAMACAO;
        public bool LOGI_TESTE;
        public DateTime LOGI_ANIVERSARIO;
        public DateTime LOGI_PWD_ENVIADO_EM;
        public string LOGI_USUARIO_DOMINIO;
        public int LOGI_SETOR_SELECAO;
        public int LOGI_LOGIN_INTERNO_SELECAO;
        public DateTime LOGI_DESLIGAMENTO;
        public int LOGI_PRAZO_ACRESCENTAR;
        public bool LOGI_RECEBER_AVISO_ALTERACAO_PERFIL;

        public List<Cliente> clientes;
        public Cliente cliente;
    }
}
