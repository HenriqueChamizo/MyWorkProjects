using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;
using DAO.Interfaces;
using Model;
using Model.Enuns;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;

namespace DAO
{
    public class ClienteDAO : ICliente, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetClientes(ref List<Cliente> clientes, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT CLI_IND,CLI_NUMERO,CLI_RAZAOSOCIAL,CLI_FANTASIA,CLI_TELEFONE,CLI_TELEFONE2,CLI_ENDERECO,CLI_ENDERECO_NUMERO,CLI_ENDERECO_MUNICIPIO, 
                                  CLI_ENDERECO_UF, CLI_CNPJ, CLI_IE, CLI_IE_CASSADO, CLI_IE_CASSADO_DATA, CLI_RESPONSAVEL, CLI_RESPONSAVEL_CPF, CLI_CONTATO_FISCAL, 
                                  CLI_CONTATO_CONTABIL, CLI_CONTATO_PESSOAL, CLI_CONTATO_LEGALIZACOES, CLI_EMAIL, CLI_FISCAL, CLI_CONTABIL, CLI_PESSOAL, CLI_LEGALIZACOES, 
                                  CLI_CNAE, CLI_CPR, CLI_PERIODOBALANCOS, CLI_ATIVO_FISCAL, CLI_ATIVO_CONTABIL, CLI_ATIVO_PESSOAL, CLI_EMAIL_FISCAL, CLI_EMAIL_CONTABIL, CLI_EMAIL_PESSOAL, 
                                  CLI_EMAIL_LEGAL, CLI_INICIO, CLI_DESLIGAMENTO, CLI_DESLIGAMENTO_TIPO, CLI_EMAIL_FISCAL_CC, CLI_EMAIL_CONTABIL_CC, CLI_EMAIL_PESSOAL_CC, CLI_EMAIL_PESSOAL_CC2, 
                                  CLI_EMAIL_FISCAL_CC2, CLI_EMAIL_CONTABIL_CC2, CLI_EMAIL_LEGAL_CC, CLI_EMAIL_LEGAL_CC2, CLI_CCM, CLI_ENDERECO_BAIRRO, CLI_ENDERECO_CEP, CLI_AVATAR_INDEX, CLI_ICONE_IMAGEM, 
                                  CLI_ICONE_CSSCLASS, CLI_PERFIL_ATUAL_CLASSIFICACAO, CLI_PERFIL_ATUAL_COMERCIO, CLI_PERFIL_ATUAL_INDUSTRIA, CLI_PERFIL_ATUAL_SERVICO, CLI_PERFIL_ATUAL_PROLABORE, 
                                  CLI_PERFIL_ATUAL_FOLHA, CLI_PERFIL_ATUAL_BALANCOS, CLI_PERFIL_ATUAL_BLOQUEADO, CLI_PERFIL_ATUAL_SEMMOVIMENTO, CLI_PERFIL_ATUAL_PORTE, CLI_PERFIL_ATUAL_TIPO, 
                                  CLI_INATIVO, CLI_DEPOSITO, CLI_CPF, CLI_MATRIZ, CLI_UNI_PROFISSIONAL, CLI_ENDERECO_COMPLEMENTO, CLI_CONTATO_FISCAL_EMAIL, CLI_CONTATO_CONTABIL_EMAIL, CLI_CONTATO_PESSOAL_EMAIL, 
                                  CLI_CONTATO_FISCAL_TELEFONE, CLI_CONTATO_CONTABIL_TELEFONE, CLI_CONTATO_PESSOAL_TELEFONE, CLI_FINANCEIRO_NOME, CLI_FINANCEIRO_EMAIL, CLI_FINANCEIRO_TELEFONE, CLI_ENDERECO_FISCAL, 
                                  CLI_ENDERECO_FISCAL_NUMERO, CLI_ENDERECO_FISCAL_COMPLEMENTO, CLI_ENDERECO_FISCAL_MUNICIPIO, CLI_ENDERECO_FISCAL_UF, CLI_ENDERECO_FISCAL_BAIRRO, CLI_ENDERECO_FISCAL_CEP, 
                                  CLI_TABELA_CNAE, CLI_TABELA_CNAE2, CLI_TABELA_CNAE3, CLI_FISCAL_CELULAR1, CLI_CONTABIL_CELULAR1, CLI_PESSOAL_CELULAR1, CLI_FISCAL_CELULAR1_DDD, CLI_CONTABIL_CELULAR1_DDD, 
                                  CLI_PESSOAL_CELULAR1_DDD, CLI_FISCAL_CELULAR1_ACEITA_SMS, CLI_CONTABIL_CELULAR1_ACEITA_SMS, CLI_PESSOAL_CELULAR1_ACEITA_SMS, CLI_FINANCEIRO_CELULAR1, 
                                  CLI_FINANCEIRO_CELULAR1_DDD, CLI_FINANCEIRO_CELULAR1_ACEITA_SMS, CLI_IMPOSTO_RENDA_OPCAO, CLI_DECIMO_TERCEIRO_OPCAO, _CLI_PERFIL_ATUAL_IMUNE, CLI_PERFIL_ATUAL_INATIVA, 
                                  CLI_PERFIL_ATUAL_DESONERACAO_FOLHA, CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ, CLI_PERFIL_ATUAL_DECIMO_TERCEIRO, CLI_CONSTITUICAO_DATA, CLI_TESTE, 
                                  CLI_LOCACAO, CLI_BLOQUEIO_ACESSOU_EM, CLI_DATA_ENCERRAMENTO, CLI_DATA_INICIO_ENCERRAMENTO, CLI_PERMITIR_UPLOAD_AVULSO, CLI_UNIFICAR_RETENCOES, 
                                  CLI_UNIFICAR_RETENCOES_MATRIZ, CLI_PORTE, CLI_TIPO, CLI_CAMPO_TESTE
                                  FROM Clientes";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                Cliente cliente;
                while (dr.Read())
                {
                    cliente = new Cliente();

                    #region Parametrização
                    cliente.CLI_IND = Convert.ToInt32(dr["CLI_IND"]);
                    cliente.CLI_NUMERO = Convert.ToInt32(dr["CLI_NUMERO"]);
                    cliente.CLI_RAZAOSOCIAL = Convert.ToString(dr["CLI_RAZAOSOCIAL"]);
                    cliente.CLI_FANTASIA = Convert.ToString(dr["CLI_FANTASIA"]);
                    cliente.CLI_CNPJ = Convert.ToString(dr["CLI_CNPJ"]);
                    #region Comments
                    //cliente.CLI_TELEFONE = Convert.ToString(er["CLI_TELEFONE"]);
                    //cliente.CLI_TELEFONE2 = Convert.ToString(er["CLI_TELEFONE2"]);
                    //cliente.CLI_ENDERECO = Convert.ToString(er["CLI_ENDERECO"]);
                    //cliente.CLI_ENDERECO_NUMERO = Convert.ToString(er["CLI_ENDERECO_NUMERO"]);
                    //cliente.CLI_ENDERECO_MUNICIPIO = Convert.ToString(er["CLI_ENDERECO_MUNICIPIO"]);
                    //cliente.CLI_ENDERECO_UF = Convert.ToString(er["CLI_ENDERECO_UF"]);
                    //cliente.CLI_CNPJ = Convert.ToString(er["CLI_CNPJ"]);
                    //cliente.CLI_IE = Convert.ToString(er["CLI_IE"]);
                    //cliente.CLI_IE_CASSADO = Convert.ToBoolean(er["CLI_IE_CASSADO"]);
                    //cliente.CLI_IE_CASSADO_DATA = Convert.ToDateTime(er["CLI_IE_CASSADO_DATA"]);
                    //cliente.CLI_RESPONSAVEL = Convert.ToString(er["CLI_RESPONSAVEL"]);
                    //cliente.CLI_RESPONSAVEL_CPF = Convert.ToString(er["CLI_RESPONSAVEL_CPF"]);
                    //cliente.CLI_CONTATO_FISCAL = Convert.ToString(er["CLI_CONTATO_FISCAL"]);
                    //cliente.CLI_CONTATO_CONTABIL = Convert.ToString(er["CLI_CONTATO_CONTABIL"]);
                    //cliente.CLI_CONTATO_PESSOAL = Convert.ToString(er["CLI_CONTATO_PESSOAL"]);
                    //cliente.CLI_CONTATO_LEGALIZACOES = Convert.ToString(er["CLI_CONTATO_LEGALIZACOES"]);
                    //cliente.CLI_EMAIL = Convert.ToString(er["CLI_EMAIL"]);
                    //cliente.CLI_FISCAL = Convert.ToInt32(er["CLI_FISCAL"]);
                    //cliente.CLI_CONTABIL = Convert.ToInt32(er["CLI_CONTABIL"]);
                    //cliente.CLI_PESSOAL = Convert.ToInt32(er["CLI_PESSOAL"]);
                    //cliente.CLI_LEGALIZACOES = Convert.ToInt32(er["CLI_LEGALIZACOES"]);
                    //cliente.CLI_CNAE = Convert.ToString(er["CLI_CNAE"]);
                    //cliente.CLI_CPR = Convert.ToString(er["CLI_CPR"]);
                    //cliente.CLI_PERIODOBALANCOS = Convert.ToInt32(er["CLI_PERIODOBALANCOS"]);
                    //cliente.CLI_ATIVO_FISCAL = Convert.ToInt32(er["CLI_ATIVO_FISCAL"]);
                    //cliente.CLI_ATIVO_CONTABIL = Convert.ToInt32(er["CLI_ATIVO_CONTABIL"]);
                    //cliente.CLI_ATIVO_PESSOAL = Convert.ToInt32(er["CLI_ATIVO_PESSOAL"]);
                    //cliente.CLI_EMAIL_FISCAL = Convert.ToString(er["CLI_EMAIL_FISCAL"]);
                    //cliente.CLI_EMAIL_CONTABIL = Convert.ToString(er["CLI_EMAIL_CONTABIL"]);
                    //cliente.CLI_EMAIL_PESSOAL = Convert.ToString(er["CLI_EMAIL_PESSOAL"]);
                    //cliente.CLI_EMAIL_LEGAL = Convert.ToString(er["CLI_EMAIL_LEGAL"]);
                    //cliente.CLI_INICIO = Convert.ToDateTime(er["CLI_INICIO"]);
                    //cliente.CLI_DESLIGAMENTO = Convert.ToDateTime(er["CLI_DESLIGAMENTO"]);
                    //cliente.CLI_DESLIGAMENTO_TIPO = Convert.ToInt32(er["CLI_DESLIGAMENTO_TIPO"]);
                    //cliente.CLI_EMAIL_FISCAL_CC = Convert.ToString(er["CLI_EMAIL_FISCAL_CC"]);
                    //cliente.CLI_EMAIL_CONTABIL_CC = Convert.ToString(er["CLI_EMAIL_CONTABIL_CC"]);
                    //cliente.CLI_EMAIL_PESSOAL_CC = Convert.ToString(er["CLI_EMAIL_PESSOAL_CC"]);
                    //cliente.CLI_EMAIL_PESSOAL_CC2 = Convert.ToString(er["CLI_EMAIL_PESSOAL_CC2"]);
                    //cliente.CLI_EMAIL_FISCAL_CC2 = Convert.ToString(er["CLI_EMAIL_FISCAL_CC2"]);
                    //cliente.CLI_EMAIL_CONTABIL_CC2 = Convert.ToString(er["CLI_EMAIL_CONTABIL_CC2"]);
                    //cliente.CLI_EMAIL_LEGAL_CC = Convert.ToString(er["CLI_EMAIL_LEGAL_CC"]);
                    //cliente.CLI_EMAIL_LEGAL_CC2 = Convert.ToString(er["CLI_EMAIL_LEGAL_CC2"]);
                    //cliente.CLI_CCM = Convert.ToInt32(er["CLI_CCM"]);
                    //cliente.CLI_ENDERECO_BAIRRO = Convert.ToString(er["CLI_ENDERECO_BAIRRO"]);
                    //cliente.CLI_ENDERECO_CEP = Convert.ToString(er["CLI_ENDERECO_CEP"]);
                    //cliente.CLI_AVATAR_INDEX = Convert.ToInt32(er["CLI_AVATAR_INDEX"]);
                    //cliente.CLI_ICONE_IMAGEM = Convert.ToString(er["CLI_ICONE_IMAGEM"]);
                    //cliente.CLI_ICONE_CSSCLASS = Convert.ToString(er["CLI_ICONE_CSSCLASS"]);
                    //cliente.CLI_PERFIL_ATUAL_CLASSIFICACAO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_CLASSIFICACAO"]);
                    //cliente.CLI_PERFIL_ATUAL_COMERCIO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_COMERCIO"]);
                    //cliente.CLI_PERFIL_ATUAL_INDUSTRIA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_INDUSTRIA"]);
                    //cliente.CLI_PERFIL_ATUAL_SERVICO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_SERVICO"]);
                    //cliente.CLI_PERFIL_ATUAL_PROLABORE = Convert.ToInt32(er["CLI_PERFIL_ATUAL_PROLABORE"]);
                    //cliente.CLI_PERFIL_ATUAL_FOLHA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_FOLHA"]);
                    //cliente.CLI_PERFIL_ATUAL_BALANCOS = Convert.ToInt32(er["CLI_PERFIL_ATUAL_BALANCOS"]);
                    //cliente.CLI_PERFIL_ATUAL_BLOQUEADO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_BLOQUEADO"]);
                    //cliente.CLI_PERFIL_ATUAL_SEMMOVIMENTO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_SEMMOVIMENTO"]);
                    //cliente.CLI_PERFIL_ATUAL_PORTE = Convert.ToInt32(er["CLI_PERFIL_ATUAL_PORTE"]);
                    //cliente.CLI_PERFIL_ATUAL_TIPO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_TIPO"]);
                    //cliente.CLI_INATIVO = Convert.ToBoolean(er["CLI_INATIVO"]);
                    //cliente.CLI_DEPOSITO = Convert.ToBoolean(er["CLI_DEPOSITO"]);
                    //cliente.CLI_CPF = Convert.ToString(er["CLI_CPF"]);
                    //cliente.CLI_MATRIZ = Convert.ToInt32(er["CLI_MATRIZ"]);
                    //cliente.CLI_UNI_PROFISSIONAL = Convert.ToBoolean(er["CLI_UNI_PROFISSIONAL"]);
                    //cliente.CLI_ENDERECO_COMPLEMENTO = Convert.ToString(er["CLI_ENDERECO_COMPLEMENTO"]);
                    //cliente.CLI_CONTATO_FISCAL_EMAIL = Convert.ToString(er["CLI_CONTATO_FISCAL_EMAIL"]);
                    //cliente.CLI_CONTATO_CONTABIL_EMAIL = Convert.ToString(er["CLI_CONTATO_CONTABIL_EMAIL"]);
                    //cliente.CLI_CONTATO_PESSOAL_EMAIL = Convert.ToString(er["CLI_CONTATO_PESSOAL_EMAIL"]);
                    //cliente.CLI_CONTATO_FISCAL_TELEFONE = Convert.ToString(er["CLI_CONTATO_FISCAL_TELEFONE"]);
                    //cliente.CLI_CONTATO_CONTABIL_TELEFONE = Convert.ToString(er["CLI_CONTATO_CONTABIL_TELEFONE"]);
                    //cliente.CLI_CONTATO_PESSOAL_TELEFONE = Convert.ToString(er["CLI_CONTATO_PESSOAL_TELEFONE"]);
                    //cliente.CLI_FINANCEIRO_NOME = Convert.ToString(er["CLI_FINANCEIRO_NOME"]);
                    //cliente.CLI_FINANCEIRO_EMAIL = Convert.ToString(er["CLI_FINANCEIRO_EMAIL"]);
                    //cliente.CLI_FINANCEIRO_TELEFONE = Convert.ToString(er["CLI_FINANCEIRO_TELEFONE"]);
                    //cliente.CLI_ENDERECO_FISCAL = Convert.ToString(er["CLI_ENDERECO_FISCAL"]);
                    //cliente.CLI_ENDERECO_FISCAL_NUMERO = Convert.ToString(er["CLI_ENDERECO_FISCAL_NUMERO"]);
                    //cliente.CLI_ENDERECO_FISCAL_COMPLEMENTO = Convert.ToString(er["CLI_ENDERECO_FISCAL_COMPLEMENTO"]);
                    //cliente.CLI_ENDERECO_FISCAL_MUNICIPIO = Convert.ToString(er["CLI_ENDERECO_FISCAL_MUNICIPIO"]);
                    //cliente.CLI_ENDERECO_FISCAL_UF = Convert.ToString(er["CLI_ENDERECO_FISCAL_UF"]);
                    //cliente.CLI_ENDERECO_FISCAL_BAIRRO = Convert.ToString(er["CLI_ENDERECO_FISCAL_BAIRRO"]);
                    //cliente.CLI_ENDERECO_FISCAL_CEP = Convert.ToString(er["CLI_ENDERECO_FISCAL_CEP"]);
                    //cliente.CLI_TABELA_CNAE = Convert.ToInt32(er["CLI_TABELA_CNAE"]);
                    //cliente.CLI_TABELA_CNAE2 = Convert.ToInt32(er["CLI_TABELA_CNAE2"]);
                    //cliente.CLI_TABELA_CNAE3 = Convert.ToInt32(er["CLI_TABELA_CNAE3"]);
                    //cliente.CLI_FISCAL_CELULAR1 = Convert.ToString(er["CLI_FISCAL_CELULAR1"]);
                    //cliente.CLI_CONTABIL_CELULAR1 = Convert.ToString(er["CLI_CONTABIL_CELULAR1"]);
                    //cliente.CLI_PESSOAL_CELULAR1 = Convert.ToString(er["CLI_PESSOAL_CELULAR1"]);
                    //cliente.CLI_FISCAL_CELULAR1_DDD = Convert.ToString(er["CLI_FISCAL_CELULAR1_DDD"]);
                    //cliente.CLI_CONTABIL_CELULAR1_DDD = Convert.ToString(er["CLI_CONTABIL_CELULAR1_DDD"]);
                    //cliente.CLI_PESSOAL_CELULAR1_DDD = Convert.ToString(er["CLI_PESSOAL_CELULAR1_DDD"]);
                    //cliente.CLI_FISCAL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_FISCAL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_CONTABIL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_CONTABIL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_PESSOAL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_PESSOAL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1 = Convert.ToString(er["CLI_FINANCEIRO_CELULAR1"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1_DDD = Convert.ToString(er["CLI_FINANCEIRO_CELULAR1_DDD"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_FINANCEIRO_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_IMPOSTO_RENDA_OPCAO = Convert.ToInt32(er["CLI_IMPOSTO_RENDA_OPCAO"]);
                    //cliente.CLI_DECIMO_TERCEIRO_OPCAO = Convert.ToInt32(er["CLI_DECIMO_TERCEIRO_OPCAO"]);
                    //cliente._CLI_PERFIL_ATUAL_IMUNE = Convert.ToInt32(er["_CLI_PERFIL_ATUAL_IMUNE"]);
                    //cliente.CLI_PERFIL_ATUAL_INATIVA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_INATIVA"]);
                    //cliente.CLI_PERFIL_ATUAL_DESONERACAO_FOLHA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_DESONERACAO_FOLHA"]);
                    //cliente.CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ = Convert.ToInt32(er["CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ"]);
                    //cliente.CLI_PERFIL_ATUAL_DECIMO_TERCEIRO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_DECIMO_TERCEIRO"]);
                    //cliente.CLI_CONSTITUICAO_DATA = Convert.ToDateTime(er["CLI_CONSTITUICAO_DATA"]);
                    //cliente.CLI_TESTE = Convert.ToBoolean(er["CLI_TESTE"]);
                    //cliente.CLI_LOCACAO = Convert.ToBoolean(er["CLI_LOCACAO"]);
                    //cliente.CLI_BLOQUEIO_ACESSOU_EM = Convert.ToDateTime(er["CLI_BLOQUEIO_ACESSOU_EM"]);
                    //cliente.CLI_DATA_ENCERRAMENTO = Convert.ToDateTime(er["CLI_DATA_ENCERRAMENTO"]);
                    //cliente.CLI_DATA_INICIO_ENCERRAMENTO = Convert.ToDateTime(er["CLI_DATA_INICIO_ENCERRAMENTO"]);
                    //cliente.CLI_PERMITIR_UPLOAD_AVULSO = Convert.ToBoolean(er["CLI_PERMITIR_UPLOAD_AVULSO"]);
                    //cliente.CLI_UNIFICAR_RETENCOES = Convert.ToBoolean(er["CLI_UNIFICAR_RETENCOES"]);
                    //cliente.CLI_UNIFICAR_RETENCOES_MATRIZ = Convert.ToBoolean(er["CLI_UNIFICAR_RETENCOES_MATRIZ"]);
                    //cliente.CLI_PORTE = Convert.ToString(er["CLI_PORTE"]);
                    //cliente.CLI_TIPO = Convert.ToString(er["CLI_TIPO"]);
                    //cliente.CLI_CAMPO_TESTE = Convert.ToString(er["CLI_CAMPO_TESTE"]);
                    #endregion

                    #endregion
                    clientes.Add(cliente);
                }
                retorno = true;
            }
            catch (SqlException ex)
            {
                retorno = false;
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                retorno = false;
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
            return retorno;
        }

        public bool GetCliente(ref Cliente cliente, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT CLI_IND,CLI_NUMERO,CLI_RAZAOSOCIAL,CLI_FANTASIA,CLI_TELEFONE,CLI_TELEFONE2,CLI_ENDERECO,CLI_ENDERECO_NUMERO,CLI_ENDERECO_MUNICIPIO, 
                                  CLI_ENDERECO_UF, CLI_CNPJ, CLI_IE, CLI_IE_CASSADO, CLI_IE_CASSADO_DATA, CLI_RESPONSAVEL, CLI_RESPONSAVEL_CPF, CLI_CONTATO_FISCAL, 
                                  CLI_CONTATO_CONTABIL, CLI_CONTATO_PESSOAL, CLI_CONTATO_LEGALIZACOES, CLI_EMAIL, CLI_FISCAL, CLI_CONTABIL, CLI_PESSOAL, CLI_LEGALIZACOES, 
                                  CLI_CNAE, CLI_CPR, CLI_PERIODOBALANCOS, CLI_ATIVO_FISCAL, CLI_ATIVO_CONTABIL, CLI_ATIVO_PESSOAL, CLI_EMAIL_FISCAL, CLI_EMAIL_CONTABIL, CLI_EMAIL_PESSOAL, 
                                  CLI_EMAIL_LEGAL, CLI_INICIO, CLI_DESLIGAMENTO, CLI_DESLIGAMENTO_TIPO, CLI_EMAIL_FISCAL_CC, CLI_EMAIL_CONTABIL_CC, CLI_EMAIL_PESSOAL_CC, CLI_EMAIL_PESSOAL_CC2, 
                                  CLI_EMAIL_FISCAL_CC2, CLI_EMAIL_CONTABIL_CC2, CLI_EMAIL_LEGAL_CC, CLI_EMAIL_LEGAL_CC2, CLI_CCM, CLI_ENDERECO_BAIRRO, CLI_ENDERECO_CEP, CLI_AVATAR_INDEX, CLI_ICONE_IMAGEM, 
                                  CLI_ICONE_CSSCLASS, CLI_PERFIL_ATUAL_CLASSIFICACAO, CLI_PERFIL_ATUAL_COMERCIO, CLI_PERFIL_ATUAL_INDUSTRIA, CLI_PERFIL_ATUAL_SERVICO, CLI_PERFIL_ATUAL_PROLABORE, 
                                  CLI_PERFIL_ATUAL_FOLHA, CLI_PERFIL_ATUAL_BALANCOS, CLI_PERFIL_ATUAL_BLOQUEADO, CLI_PERFIL_ATUAL_SEMMOVIMENTO, CLI_PERFIL_ATUAL_PORTE, CLI_PERFIL_ATUAL_TIPO, 
                                  CLI_INATIVO, CLI_DEPOSITO, CLI_CPF, CLI_MATRIZ, CLI_UNI_PROFISSIONAL, CLI_ENDERECO_COMPLEMENTO, CLI_CONTATO_FISCAL_EMAIL, CLI_CONTATO_CONTABIL_EMAIL, CLI_CONTATO_PESSOAL_EMAIL, 
                                  CLI_CONTATO_FISCAL_TELEFONE, CLI_CONTATO_CONTABIL_TELEFONE, CLI_CONTATO_PESSOAL_TELEFONE, CLI_FINANCEIRO_NOME, CLI_FINANCEIRO_EMAIL, CLI_FINANCEIRO_TELEFONE, CLI_ENDERECO_FISCAL, 
                                  CLI_ENDERECO_FISCAL_NUMERO, CLI_ENDERECO_FISCAL_COMPLEMENTO, CLI_ENDERECO_FISCAL_MUNICIPIO, CLI_ENDERECO_FISCAL_UF, CLI_ENDERECO_FISCAL_BAIRRO, CLI_ENDERECO_FISCAL_CEP, 
                                  CLI_TABELA_CNAE, CLI_TABELA_CNAE2, CLI_TABELA_CNAE3, CLI_FISCAL_CELULAR1, CLI_CONTABIL_CELULAR1, CLI_PESSOAL_CELULAR1, CLI_FISCAL_CELULAR1_DDD, CLI_CONTABIL_CELULAR1_DDD, 
                                  CLI_PESSOAL_CELULAR1_DDD, CLI_FISCAL_CELULAR1_ACEITA_SMS, CLI_CONTABIL_CELULAR1_ACEITA_SMS, CLI_PESSOAL_CELULAR1_ACEITA_SMS, CLI_FINANCEIRO_CELULAR1, 
                                  CLI_FINANCEIRO_CELULAR1_DDD, CLI_FINANCEIRO_CELULAR1_ACEITA_SMS, CLI_IMPOSTO_RENDA_OPCAO, CLI_DECIMO_TERCEIRO_OPCAO, _CLI_PERFIL_ATUAL_IMUNE, CLI_PERFIL_ATUAL_INATIVA, 
                                  CLI_PERFIL_ATUAL_DESONERACAO_FOLHA, CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ, CLI_PERFIL_ATUAL_DECIMO_TERCEIRO, CLI_CONSTITUICAO_DATA, CLI_TESTE, 
                                  CLI_LOCACAO, CLI_BLOQUEIO_ACESSOU_EM, CLI_DATA_ENCERRAMENTO, CLI_DATA_INICIO_ENCERRAMENTO, CLI_PERMITIR_UPLOAD_AVULSO, CLI_UNIFICAR_RETENCOES, 
                                  CLI_UNIFICAR_RETENCOES_MATRIZ, CLI_PORTE, CLI_TIPO, CLI_CAMPO_TESTE
                                  FROM Clientes where CLI_IND = @CLI_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    #region Parametrização
                    cliente.CLI_IND = Convert.ToInt32(dr["CLI_IND"]);
                    cliente.CLI_NUMERO = Convert.ToInt32(dr["CLI_NUMERO"]);
                    cliente.CLI_RAZAOSOCIAL = Convert.ToString(dr["CLI_RAZAOSOCIAL"]);
                    cliente.CLI_FANTASIA = Convert.ToString(dr["CLI_FANTASIA"]);
                    cliente.CLI_CNPJ = Convert.ToString(dr["CLI_CNPJ"]);
                    #region Comments
                    //cliente.CLI_TELEFONE = Convert.ToString(er["CLI_TELEFONE"]);
                    //cliente.CLI_TELEFONE2 = Convert.ToString(er["CLI_TELEFONE2"]);
                    //cliente.CLI_ENDERECO = Convert.ToString(er["CLI_ENDERECO"]);
                    //cliente.CLI_ENDERECO_NUMERO = Convert.ToString(er["CLI_ENDERECO_NUMERO"]);
                    //cliente.CLI_ENDERECO_MUNICIPIO = Convert.ToString(er["CLI_ENDERECO_MUNICIPIO"]);
                    //cliente.CLI_ENDERECO_UF = Convert.ToString(er["CLI_ENDERECO_UF"]);
                    //cliente.CLI_IE = Convert.ToString(er["CLI_IE"]);
                    //cliente.CLI_IE_CASSADO = Convert.ToBoolean(er["CLI_IE_CASSADO"]);
                    //cliente.CLI_IE_CASSADO_DATA = Convert.ToDateTime(er["CLI_IE_CASSADO_DATA"]);
                    //cliente.CLI_RESPONSAVEL = Convert.ToString(er["CLI_RESPONSAVEL"]);
                    //cliente.CLI_RESPONSAVEL_CPF = Convert.ToString(er["CLI_RESPONSAVEL_CPF"]);
                    //cliente.CLI_CONTATO_FISCAL = Convert.ToString(er["CLI_CONTATO_FISCAL"]);
                    //cliente.CLI_CONTATO_CONTABIL = Convert.ToString(er["CLI_CONTATO_CONTABIL"]);
                    //cliente.CLI_CONTATO_PESSOAL = Convert.ToString(er["CLI_CONTATO_PESSOAL"]);
                    //cliente.CLI_CONTATO_LEGALIZACOES = Convert.ToString(er["CLI_CONTATO_LEGALIZACOES"]);
                    //cliente.CLI_EMAIL = Convert.ToString(er["CLI_EMAIL"]);
                    //cliente.CLI_FISCAL = Convert.ToInt32(er["CLI_FISCAL"]);
                    //cliente.CLI_CONTABIL = Convert.ToInt32(er["CLI_CONTABIL"]);
                    //cliente.CLI_PESSOAL = Convert.ToInt32(er["CLI_PESSOAL"]);
                    //cliente.CLI_LEGALIZACOES = Convert.ToInt32(er["CLI_LEGALIZACOES"]);
                    //cliente.CLI_CNAE = Convert.ToString(er["CLI_CNAE"]);
                    //cliente.CLI_CPR = Convert.ToString(er["CLI_CPR"]);
                    //cliente.CLI_PERIODOBALANCOS = Convert.ToInt32(er["CLI_PERIODOBALANCOS"]);
                    //cliente.CLI_ATIVO_FISCAL = Convert.ToInt32(er["CLI_ATIVO_FISCAL"]);
                    //cliente.CLI_ATIVO_CONTABIL = Convert.ToInt32(er["CLI_ATIVO_CONTABIL"]);
                    //cliente.CLI_ATIVO_PESSOAL = Convert.ToInt32(er["CLI_ATIVO_PESSOAL"]);
                    //cliente.CLI_EMAIL_FISCAL = Convert.ToString(er["CLI_EMAIL_FISCAL"]);
                    //cliente.CLI_EMAIL_CONTABIL = Convert.ToString(er["CLI_EMAIL_CONTABIL"]);
                    //cliente.CLI_EMAIL_PESSOAL = Convert.ToString(er["CLI_EMAIL_PESSOAL"]);
                    //cliente.CLI_EMAIL_LEGAL = Convert.ToString(er["CLI_EMAIL_LEGAL"]);
                    //cliente.CLI_INICIO = Convert.ToDateTime(er["CLI_INICIO"]);
                    //cliente.CLI_DESLIGAMENTO = Convert.ToDateTime(er["CLI_DESLIGAMENTO"]);
                    //cliente.CLI_DESLIGAMENTO_TIPO = Convert.ToInt32(er["CLI_DESLIGAMENTO_TIPO"]);
                    //cliente.CLI_EMAIL_FISCAL_CC = Convert.ToString(er["CLI_EMAIL_FISCAL_CC"]);
                    //cliente.CLI_EMAIL_CONTABIL_CC = Convert.ToString(er["CLI_EMAIL_CONTABIL_CC"]);
                    //cliente.CLI_EMAIL_PESSOAL_CC = Convert.ToString(er["CLI_EMAIL_PESSOAL_CC"]);
                    //cliente.CLI_EMAIL_PESSOAL_CC2 = Convert.ToString(er["CLI_EMAIL_PESSOAL_CC2"]);
                    //cliente.CLI_EMAIL_FISCAL_CC2 = Convert.ToString(er["CLI_EMAIL_FISCAL_CC2"]);
                    //cliente.CLI_EMAIL_CONTABIL_CC2 = Convert.ToString(er["CLI_EMAIL_CONTABIL_CC2"]);
                    //cliente.CLI_EMAIL_LEGAL_CC = Convert.ToString(er["CLI_EMAIL_LEGAL_CC"]);
                    //cliente.CLI_EMAIL_LEGAL_CC2 = Convert.ToString(er["CLI_EMAIL_LEGAL_CC2"]);
                    //cliente.CLI_CCM = Convert.ToInt32(er["CLI_CCM"]);
                    //cliente.CLI_ENDERECO_BAIRRO = Convert.ToString(er["CLI_ENDERECO_BAIRRO"]);
                    //cliente.CLI_ENDERECO_CEP = Convert.ToString(er["CLI_ENDERECO_CEP"]);
                    //cliente.CLI_AVATAR_INDEX = Convert.ToInt32(er["CLI_AVATAR_INDEX"]);
                    //cliente.CLI_ICONE_IMAGEM = Convert.ToString(er["CLI_ICONE_IMAGEM"]);
                    //cliente.CLI_ICONE_CSSCLASS = Convert.ToString(er["CLI_ICONE_CSSCLASS"]);
                    //cliente.CLI_PERFIL_ATUAL_CLASSIFICACAO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_CLASSIFICACAO"]);
                    //cliente.CLI_PERFIL_ATUAL_COMERCIO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_COMERCIO"]);
                    //cliente.CLI_PERFIL_ATUAL_INDUSTRIA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_INDUSTRIA"]);
                    //cliente.CLI_PERFIL_ATUAL_SERVICO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_SERVICO"]);
                    //cliente.CLI_PERFIL_ATUAL_PROLABORE = Convert.ToInt32(er["CLI_PERFIL_ATUAL_PROLABORE"]);
                    //cliente.CLI_PERFIL_ATUAL_FOLHA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_FOLHA"]);
                    //cliente.CLI_PERFIL_ATUAL_BALANCOS = Convert.ToInt32(er["CLI_PERFIL_ATUAL_BALANCOS"]);
                    //cliente.CLI_PERFIL_ATUAL_BLOQUEADO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_BLOQUEADO"]);
                    //cliente.CLI_PERFIL_ATUAL_SEMMOVIMENTO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_SEMMOVIMENTO"]);
                    //cliente.CLI_PERFIL_ATUAL_PORTE = Convert.ToInt32(er["CLI_PERFIL_ATUAL_PORTE"]);
                    //cliente.CLI_PERFIL_ATUAL_TIPO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_TIPO"]);
                    //cliente.CLI_INATIVO = Convert.ToBoolean(er["CLI_INATIVO"]);
                    //cliente.CLI_DEPOSITO = Convert.ToBoolean(er["CLI_DEPOSITO"]);
                    //cliente.CLI_CPF = Convert.ToString(er["CLI_CPF"]);
                    //cliente.CLI_MATRIZ = Convert.ToInt32(er["CLI_MATRIZ"]);
                    //cliente.CLI_UNI_PROFISSIONAL = Convert.ToBoolean(er["CLI_UNI_PROFISSIONAL"]);
                    //cliente.CLI_ENDERECO_COMPLEMENTO = Convert.ToString(er["CLI_ENDERECO_COMPLEMENTO"]);
                    //cliente.CLI_CONTATO_FISCAL_EMAIL = Convert.ToString(er["CLI_CONTATO_FISCAL_EMAIL"]);
                    //cliente.CLI_CONTATO_CONTABIL_EMAIL = Convert.ToString(er["CLI_CONTATO_CONTABIL_EMAIL"]);
                    //cliente.CLI_CONTATO_PESSOAL_EMAIL = Convert.ToString(er["CLI_CONTATO_PESSOAL_EMAIL"]);
                    //cliente.CLI_CONTATO_FISCAL_TELEFONE = Convert.ToString(er["CLI_CONTATO_FISCAL_TELEFONE"]);
                    //cliente.CLI_CONTATO_CONTABIL_TELEFONE = Convert.ToString(er["CLI_CONTATO_CONTABIL_TELEFONE"]);
                    //cliente.CLI_CONTATO_PESSOAL_TELEFONE = Convert.ToString(er["CLI_CONTATO_PESSOAL_TELEFONE"]);
                    //cliente.CLI_FINANCEIRO_NOME = Convert.ToString(er["CLI_FINANCEIRO_NOME"]);
                    //cliente.CLI_FINANCEIRO_EMAIL = Convert.ToString(er["CLI_FINANCEIRO_EMAIL"]);
                    //cliente.CLI_FINANCEIRO_TELEFONE = Convert.ToString(er["CLI_FINANCEIRO_TELEFONE"]);
                    //cliente.CLI_ENDERECO_FISCAL = Convert.ToString(er["CLI_ENDERECO_FISCAL"]);
                    //cliente.CLI_ENDERECO_FISCAL_NUMERO = Convert.ToString(er["CLI_ENDERECO_FISCAL_NUMERO"]);
                    //cliente.CLI_ENDERECO_FISCAL_COMPLEMENTO = Convert.ToString(er["CLI_ENDERECO_FISCAL_COMPLEMENTO"]);
                    //cliente.CLI_ENDERECO_FISCAL_MUNICIPIO = Convert.ToString(er["CLI_ENDERECO_FISCAL_MUNICIPIO"]);
                    //cliente.CLI_ENDERECO_FISCAL_UF = Convert.ToString(er["CLI_ENDERECO_FISCAL_UF"]);
                    //cliente.CLI_ENDERECO_FISCAL_BAIRRO = Convert.ToString(er["CLI_ENDERECO_FISCAL_BAIRRO"]);
                    //cliente.CLI_ENDERECO_FISCAL_CEP = Convert.ToString(er["CLI_ENDERECO_FISCAL_CEP"]);
                    //cliente.CLI_TABELA_CNAE = Convert.ToInt32(er["CLI_TABELA_CNAE"]);
                    //cliente.CLI_TABELA_CNAE2 = Convert.ToInt32(er["CLI_TABELA_CNAE2"]);
                    //cliente.CLI_TABELA_CNAE3 = Convert.ToInt32(er["CLI_TABELA_CNAE3"]);
                    //cliente.CLI_FISCAL_CELULAR1 = Convert.ToString(er["CLI_FISCAL_CELULAR1"]);
                    //cliente.CLI_CONTABIL_CELULAR1 = Convert.ToString(er["CLI_CONTABIL_CELULAR1"]);
                    //cliente.CLI_PESSOAL_CELULAR1 = Convert.ToString(er["CLI_PESSOAL_CELULAR1"]);
                    //cliente.CLI_FISCAL_CELULAR1_DDD = Convert.ToString(er["CLI_FISCAL_CELULAR1_DDD"]);
                    //cliente.CLI_CONTABIL_CELULAR1_DDD = Convert.ToString(er["CLI_CONTABIL_CELULAR1_DDD"]);
                    //cliente.CLI_PESSOAL_CELULAR1_DDD = Convert.ToString(er["CLI_PESSOAL_CELULAR1_DDD"]);
                    //cliente.CLI_FISCAL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_FISCAL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_CONTABIL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_CONTABIL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_PESSOAL_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_PESSOAL_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1 = Convert.ToString(er["CLI_FINANCEIRO_CELULAR1"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1_DDD = Convert.ToString(er["CLI_FINANCEIRO_CELULAR1_DDD"]);
                    //cliente.CLI_FINANCEIRO_CELULAR1_ACEITA_SMS = Convert.ToBoolean(er["CLI_FINANCEIRO_CELULAR1_ACEITA_SMS"]);
                    //cliente.CLI_IMPOSTO_RENDA_OPCAO = Convert.ToInt32(er["CLI_IMPOSTO_RENDA_OPCAO"]);
                    //cliente.CLI_DECIMO_TERCEIRO_OPCAO = Convert.ToInt32(er["CLI_DECIMO_TERCEIRO_OPCAO"]);
                    //cliente._CLI_PERFIL_ATUAL_IMUNE = Convert.ToInt32(er["_CLI_PERFIL_ATUAL_IMUNE"]);
                    //cliente.CLI_PERFIL_ATUAL_INATIVA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_INATIVA"]);
                    //cliente.CLI_PERFIL_ATUAL_DESONERACAO_FOLHA = Convert.ToInt32(er["CLI_PERFIL_ATUAL_DESONERACAO_FOLHA"]);
                    //cliente.CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ = Convert.ToInt32(er["CLI_PERFIL_ATUAL_FORMA_TRIBUTACAO_IRPJ"]);
                    //cliente.CLI_PERFIL_ATUAL_DECIMO_TERCEIRO = Convert.ToInt32(er["CLI_PERFIL_ATUAL_DECIMO_TERCEIRO"]);
                    //cliente.CLI_CONSTITUICAO_DATA = Convert.ToDateTime(er["CLI_CONSTITUICAO_DATA"]);
                    //cliente.CLI_TESTE = Convert.ToBoolean(er["CLI_TESTE"]);
                    //cliente.CLI_LOCACAO = Convert.ToBoolean(er["CLI_LOCACAO"]);
                    //cliente.CLI_BLOQUEIO_ACESSOU_EM = Convert.ToDateTime(er["CLI_BLOQUEIO_ACESSOU_EM"]);
                    //cliente.CLI_DATA_ENCERRAMENTO = Convert.ToDateTime(er["CLI_DATA_ENCERRAMENTO"]);
                    //cliente.CLI_DATA_INICIO_ENCERRAMENTO = Convert.ToDateTime(er["CLI_DATA_INICIO_ENCERRAMENTO"]);
                    //cliente.CLI_PERMITIR_UPLOAD_AVULSO = Convert.ToBoolean(er["CLI_PERMITIR_UPLOAD_AVULSO"]);
                    //cliente.CLI_UNIFICAR_RETENCOES = Convert.ToBoolean(er["CLI_UNIFICAR_RETENCOES"]);
                    //cliente.CLI_UNIFICAR_RETENCOES_MATRIZ = Convert.ToBoolean(er["CLI_UNIFICAR_RETENCOES_MATRIZ"]);
                    //cliente.CLI_PORTE = Convert.ToString(er["CLI_PORTE"]);
                    //cliente.CLI_TIPO = Convert.ToString(er["CLI_TIPO"]);
                    //cliente.CLI_CAMPO_TESTE = Convert.ToString(er["CLI_CAMPO_TESTE"]);
                    #endregion
                    #endregion
                }
                retorno = true;
            }
            catch (SqlException ex)
            {
                retorno = false;
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                retorno = false;
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
            return retorno;
        }

        public bool GetClienteDataBase(Cliente cliente, ref ClienteDatabase database, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT BASE_IND, BASE_DESCRICAO, BASE_NOMEBANCO, BASE_USUARIO, BASE_SENHA, BASE_SERVER 
                                  FROM ClienteDatabase WHERE BASE_CLIENTE = @BASE_CLIENTE";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@BASE_CLIENTE", cliente.CLI_IND);

                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    database = new ClienteDatabase();

                    #region Parametrização
                    database.BASE_IND = Convert.ToInt32(dr["BASE_IND"]);
                    database.BASE_DESCRICAO = Convert.ToString(dr["BASE_DESCRICAO"]);
                    database.BASE_NOMEBANCO = Convert.ToString(dr["BASE_NOMEBANCO"]);
                    database.BASE_USUARIO = Convert.ToString(dr["BASE_USUARIO"]);
                    database.BASE_SENHA = Convert.ToString(dr["BASE_SENHA"]);
                    database.BASE_SERVER = Convert.ToString(dr["BASE_SERVER"]);
                    #endregion
                }
                retorno = true;
            }
            catch (SqlException ex)
            {
                retorno = false;
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                retorno = false;
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
            return retorno;
        }
    }
}
