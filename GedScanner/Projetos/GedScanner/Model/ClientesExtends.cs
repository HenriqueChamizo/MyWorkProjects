using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Cliente
    {
        public bool FISCAL;
        public bool CONTABIL;
        public bool PESSOAL;

        public int CLI_IND; 
        public int CLI_NUMERO;
        public string CLI_RAZAOSOCIAL;
        public string CLI_FANTASIA;
        public string CLI_TELEFONE;
        public string CLI_TELEFONE2;
        public string CLI_ENDERECO;
        public string CLI_ENDERECO_NUMERO;
        public string CLI_ENDERECO_MUNICIPIO;
        public string CLI_ENDERECO_UF;
        public string CLI_CNPJ;
        public string CLI_IE;
        public bool CLI_IE_CASSADO;
        public DateTime CLI_IE_CASSADO_DATA;
        public string CLI_RESPONSAVEL;
        public string CLI_RESPONSAVEL_CPF;
        public string CLI_CONTATO_FISCAL;
        public string CLI_CONTATO_CONTABIL;
        public string CLI_CONTATO_PESSOAL;
        public string CLI_CONTATO_LEGALIZACOES;
        public string CLI_EMAIL;
        public int CLI_FISCAL;
        public int CLI_CONTABIL;
        public int CLI_PESSOAL;
        public int CLI_LEGALIZACOES;
        public string CLI_CNAE;
        public string CLI_CPR;
        public int CLI_PERIODOBALANCOS;
        public int CLI_ATIVO_FISCAL;
        public int CLI_ATIVO_CONTABIL;
        public int CLI_ATIVO_PESSOAL;
        public string CLI_EMAIL_FISCAL;
        public string CLI_EMAIL_CONTABIL;
        public string CLI_EMAIL_PESSOAL;
        public string CLI_EMAIL_LEGAL;
        public DateTime CLI_INICIO;
        public DateTime CLI_DESLIGAMENTO;
        public int CLI_DESLIGAMENTO_TIPO;
        public string CLI_EMAIL_FISCAL_CC;
        public string CLI_EMAIL_CONTABIL_CC;
        public string CLI_EMAIL_PESSOAL_CC;
        public string CLI_EMAIL_PESSOAL_CC2;
        public string CLI_EMAIL_FISCAL_CC2;
        public string CLI_EMAIL_CONTABIL_CC2;
        public string CLI_EMAIL_LEGAL_CC;
        public string CLI_EMAIL_LEGAL_CC2;
        public int CLI_CCM;
        public string CLI_ENDERECO_BAIRRO;
        public string CLI_ENDERECO_CEP;
        public int CLI_AVATAR_INDEX;
        public string CLI_ICONE_IMAGEM;
        public string CLI_ICONE_CSSCLASS;
        public int CLI_PERFIL_ATUAL_CLASSIFICACAO;
        public int CLI_PERFIL_ATUAL_COMERCIO;
        public int CLI_PERFIL_ATUAL_INDUSTRIA;
        public int CLI_PERFIL_ATUAL_SERVICO;
        public int CLI_PERFIL_ATUAL_PROLABORE;
        public int CLI_PERFIL_ATUAL_FOLHA;
        public int CLI_PERFIL_ATUAL_BALANCOS;
        public int CLI_PERFIL_ATUAL_BLOQUEADO;
        public int CLI_PERFIL_ATUAL_SEMMOVIMENTO;
        public int CLI_PERFIL_ATUAL_PORTE;
        public int CLI_PERFIL_ATUAL_TIPO;
        public bool CLI_INATIVO;
        public bool CLI_DEPOSITO;
        public string CLI_CPF;
        public int CLI_MATRIZ;
        public bool CLI_UNI_PROFISSIONAL;
        public string CLI_ENDERECO_COMPLEMENTO;
        public string CLI_CONTATO_FISCAL_EMAIL;
        public string CLI_CONTATO_CONTABIL_EMAIL;
        public string CLI_CONTATO_PESSOAL_EMAIL;
        public string CLI_CONTATO_FISCAL_TELEFONE;
        public string CLI_CONTATO_CONTABIL_TELEFONE;
        public string CLI_CONTATO_PESSOAL_TELEFONE;
        public string CLI_FINANCEIRO_NOME;
        public string CLI_FINANCEIRO_EMAIL;
        public string CLI_FINANCEIRO_TELEFONE;
        public string CLI_ENDERECO_FISCAL;
        public string CLI_ENDERECO_FISCAL_NUMERO;
        public string CLI_ENDERECO_FISCAL_COMPLEMENTO;
        public string CLI_ENDERECO_FISCAL_MUNICIPIO;
        public string CLI_ENDERECO_FISCAL_UF;
        public string CLI_ENDERECO_FISCAL_BAIRRO;
        public string CLI_ENDERECO_FISCAL_CEP;
        public int CLI_TABELA_CNAE;
        public int CLI_TABELA_CNAE2;
        public int CLI_TABELA_CNAE3;
        public string CLI_FISCAL_CELULAR1;
        public string CLI_CONTABIL_CELULAR1;
        public string CLI_PESSOAL_CELULAR1;
        public string CLI_FISCAL_CELULAR1_DDD;
        public string CLI_CONTABIL_CELULAR1_DDD;
        public string CLI_PESSOAL_CELULAR1_DDD;
        public bool CLI_FISCAL_CELULAR1_ACEITA_SMS;
        public bool CLI_CONTABIL_CELULAR1_ACEITA_SMS;
        public bool CLI_PESSOAL_CELULAR1_ACEITA_SMS;
        public string CLI_FINANCEIRO_CELULAR1;
        public string CLI_FINANCEIRO_CELULAR1_DDD;
        public bool CLI_FINANCEIRO_CELULAR1_ACEITA_SMS;
        public int CLI_IMPOSTO_RENDA_OPCAO;
	    public int CLI_DECIMO_TERCEIRO_OPCAO;
        public int _CLI_PERFIL_ATUAL_IMUNE;
        public int CLI_PERFIL_ATUAL_INATIVA;
        public int CLI_PERFIL_ATUAL_DESONERACAO_FOLHA;
        public int CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ;
        public int CLI_PERFIL_ATUAL_DECIMO_TERCEIRO;
        public DateTime CLI_CONSTITUICAO_DATA;
        public bool CLI_TESTE;
        public bool CLI_LOCACAO;
        public DateTime CLI_BLOQUEIO_ACESSOU_EM;
        public DateTime CLI_DATA_ENCERRAMENTO;
        public DateTime CLI_DATA_INICIO_ENCERRAMENTO;
        public bool CLI_PERMITIR_UPLOAD_AVULSO;
        public bool CLI_UNIFICAR_RETENCOES;
        public bool CLI_UNIFICAR_RETENCOES_MATRIZ;
        public string CLI_PORTE;
        public string CLI_TIPO;
        public string CLI_CAMPO_TESTE;

        public ClienteDatabase DATABASE;
    }
    public class ClienteDatabase
    {
        //BASE_IND INT IDENTITY,
        //BASE_DESCRICAO VARCHAR(100) NOT NULL,
        //BASE_NOMEBANCO VARCHAR(100) NOT NULL,
        //BASE_USUARIO VARCHAR(100) NOT NULL,
        //BASE_SENHA VARCHAR(100) NOT NULL,
        //BASE_SERVER VARCHAR(100) NOT NULL,
        //BASE_CLIENTE INT NOT NULL,

        public int BASE_IND;
        public string BASE_DESCRICAO;
        public string BASE_NOMEBANCO;
        public string BASE_USUARIO;
        public string BASE_SENHA;
        public string BASE_SERVER;
        public Cliente BASE_CLIENTE;
    }
}
