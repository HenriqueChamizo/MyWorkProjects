using DAO.Interfaces;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class LoginsInternosDAO : ILoginsInternos, IDAO 
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT LOGI_IND, 
                                         LOGI_NOME, 
                                         LOGI_CARGO, 
                                         LOGI_EMAIL, 
                                         LOGI_TELEFONE, 
                                         CASE WHEN LOGI_CELULAR_DDD IS NULL THEN '' ELSE LOGI_CELULAR_DDD END AS LOGI_CELULAR_DDD, 
                                         CASE WHEN LOGI_CELULAR IS NULL THEN '' ELSE LOGI_CELULAR END AS LOGI_CELULAR,  
                                         LOGI_SETOR,  
                                         SET_DESCRICAO as LOGI_SETOR_DESCRICAO, 
                                         LOGI_PWD,  
                                         CASE WHEN LOGI_ULTIMO_ACESSO IS NULL THEN '' ELSE LOGI_ULTIMO_ACESSO END AS LOGI_ULTIMO_ACESSO,  
                                         CASE WHEN LOGI_FOTO IS NULL THEN '' ELSE LOGI_FOTO END AS LOGI_FOTO,  
                                         CASE WHEN LOGI_FOTO_DATA IS NULL THEN '' ELSE LOGI_FOTO_DATA END AS LOGI_FOTO_DATA,  
                                         LOGI_ATIVO, 
                                         LOGI_SUPERVISOR ,  
                                         LOGI_HOME_OFFICE,  
                                         LOGI_AUDITOR,  
                                         LOGI_DIRETORIA,  
                                         LOGI_PROGRAMACAO,  
                                         LOGI_TESTE,  
                                         CASE WHEN LOGI_ANIVERSARIO IS NULL THEN '' ELSE LOGI_ANIVERSARIO END AS LOGI_ANIVERSARIO,  
                                         CASE WHEN LOGI_PWD_ENVIADO_EM IS NULL THEN '' ELSE LOGI_PWD_ENVIADO_EM END AS LOGI_PWD_ENVIADO_EM,  
                                         CASE WHEN LOGI_USUARIO_DOMINIO IS NULL THEN '' ELSE LOGI_USUARIO_DOMINIO END AS LOGI_USUARIO_DOMINIO,  
                                         CASE WHEN LOGI_SETOR_SELECAO IS NULL THEN '' ELSE LOGI_SETOR_SELECAO END AS LOGI_SETOR_SELECAO,  
                                         CASE WHEN LOGI_LOGIN_INTERNO_SELECAO IS NULL THEN '' ELSE LOGI_LOGIN_INTERNO_SELECAO END AS LOGI_LOGIN_INTERNO_SELECAO,  
                                         CASE WHEN LOGI_DESLIGAMENTO IS NULL THEN '' ELSE LOGI_DESLIGAMENTO END AS LOGI_DESLIGAMENTO,  
                                         LOGI_PRAZO_ACRESCENTAR,  
                                         LOGI_RECEBER_AVISO_ALTERACAO_PERFIL 
                                  FROM LoginsInternos 
                                  inner join LoginsInternosSetores on SET_REFERENCIA = LOGI_SETOR 
                                  WHERE LOGI_EMAIL = @LOGI_EMAIL AND LOGI_PWD = @LOGI_PWD";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@LOGI_EMAIL", login.LOGI_EMAIL);
                cmd.Parameters.AddWithValue("@LOGI_PWD", login.LOGI_PWD);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    #region Parametrização
                    login.LOGI_IND = Convert.ToInt32(dr["LOGI_IND"]);
                    login.LOGI_NOME = Convert.ToString(dr["LOGI_NOME"]).Replace("\r", "").Replace("\n", "");
                    login.LOGI_CARGO = Convert.ToString(dr["LOGI_CARGO"]);
                    //loginInterno.LOGI_EMAIL = Convert.ToString(dr["LOGI_EMAIL"]);
                    login.LOGI_TELEFONE = Convert.ToString(dr["LOGI_TELEFONE"]);
                    login.LOGI_CELULAR_DDD = Convert.ToString(dr["LOGI_CELULAR_DDD"]);
                    login.LOGI_CELULAR = Convert.ToString(dr["LOGI_CELULAR"]);
                    login.LOGI_SETOR = Convert.ToInt32(dr["LOGI_SETOR"]);
                    login.LOGI_SETOR_DESCRICAO = Convert.ToString(dr["LOGI_SETOR_DESCRICAO"]);
                    //loginInterno.LOGI_PWD = Convert.ToString(dr["LOGI_PWD"]);
                    login.LOGI_ULTIMO_ACESSO = Convert.ToDateTime(dr["LOGI_ULTIMO_ACESSO"]);
                    //loginInterno.LOGI_FOTO = Convert(er["LOGI_FOTO"]);
                    login.LOGI_FOTO_DATA = Convert.ToDateTime(dr["LOGI_FOTO_DATA"]);
                    login.LOGI_ATIVO = Convert.ToBoolean(dr["LOGI_ATIVO"]);
                    login.LOGI_SUPERVISOR = Convert.ToBoolean(dr["LOGI_SUPERVISOR"]);
                    login.LOGI_HOME_OFFICE = Convert.ToBoolean(dr["LOGI_HOME_OFFICE"]);
                    login.LOGI_AUDITOR = Convert.ToBoolean(dr["LOGI_AUDITOR"]);
                    login.LOGI_DIRETORIA = Convert.ToBoolean(dr["LOGI_DIRETORIA"]);
                    login.LOGI_PROGRAMACAO = Convert.ToBoolean(dr["LOGI_PROGRAMACAO"]);
                    login.LOGI_TESTE = Convert.ToBoolean(dr["LOGI_TESTE"]);
                    login.LOGI_ANIVERSARIO = Convert.ToDateTime(dr["LOGI_ANIVERSARIO"]);
                    login.LOGI_PWD_ENVIADO_EM = Convert.ToDateTime(dr["LOGI_PWD_ENVIADO_EM"]);
                    login.LOGI_USUARIO_DOMINIO = Convert.ToString(dr["LOGI_USUARIO_DOMINIO"]);
                    login.LOGI_SETOR_SELECAO = Convert.ToInt32(dr["LOGI_SETOR_SELECAO"]);
                    login.LOGI_LOGIN_INTERNO_SELECAO = Convert.ToInt32(dr["LOGI_LOGIN_INTERNO_SELECAO"]);
                    login.LOGI_DESLIGAMENTO = Convert.ToDateTime(dr["LOGI_DESLIGAMENTO"]);
                    login.LOGI_PRAZO_ACRESCENTAR = Convert.ToInt32(dr["LOGI_PRAZO_ACRESCENTAR"]);
                    login.LOGI_RECEBER_AVISO_ALTERACAO_PERFIL = Convert.ToBoolean(dr["LOGI_RECEBER_AVISO_ALTERACAO_PERFIL"]);
                    #endregion
                }
                if (login.LOGI_IND != 0)
                {
                    dr.Close();

                    select = @"select (case when fis.LOGI_IND is not null and
				                                 fis.LOGI_IND = @log
			                                then 1 else 0 end) as FISCAL,
	                                  (case when con.LOGI_IND is not null and
				                                 con.LOGI_IND = @log
	 		                                then 1 else 0 end) as CONTABIL,
	                                  (case when pes.LOGI_IND is not null and
				                                 pes.LOGI_IND = @log
			                                then 1 else 0 end) as PESSOAL,
	                                  cli.CLI_IND, cli.CLI_NUMERO, cli.CLI_RAZAOSOCIAL, cli.CLI_FANTASIA, cli.CLI_CNPJ  
                               from Clientes cli
                               left join LoginsInternos fis on fis.LOGI_IND = cli.CLI_FISCAL 
                               left join LoginsInternos con on con.LOGI_IND = cli.CLI_CONTABIL 
                               left join LoginsInternos pes on pes.LOGI_IND = cli.CLI_PESSOAL
                               where fis.LOGI_IND = @log OR 
	                                 con.LOGI_IND = @log OR 
	                                 pes.LOGI_IND = @log";

                    cmd = new SqlCommand(select.Replace("@log", login.LOGI_IND.ToString()), conn);

                    dr = cmd.ExecuteReader();

                    Cliente cliente;
                    List<Cliente> clientes = new List<Cliente>();
                    while (dr.Read())
                    {
                        cliente = new Cliente();

                        cliente.CLI_IND = Convert.ToInt32(dr["CLI_IND"]);
                        cliente.CLI_NUMERO = Convert.ToInt32(dr["CLI_NUMERO"]);
                        cliente.CLI_RAZAOSOCIAL = Convert.ToString(dr["CLI_RAZAOSOCIAL"]);
                        cliente.CLI_FANTASIA = Convert.ToString(dr["CLI_FANTASIA"]);
                        cliente.CLI_CNPJ = Convert.ToString(dr["CLI_CNPJ"]);

                        cliente.FISCAL = Convert.ToBoolean(dr["FISCAL"]);
                        cliente.CONTABIL = Convert.ToBoolean(dr["CONTABIL"]);
                        cliente.PESSOAL = Convert.ToBoolean(dr["PESSOAL"]);

                        clientes.Add(cliente);
                    }

                    login.clientes = clientes;
                    retorno = true;
                }
                else
                    retorno = false;
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

        public bool GetExternLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT LOGI_IND, 
                                         LOGI_NOME, 
                                         LOGI_CARGO, 
                                         LOGI_EMAIL, 
                                         LOGI_TELEFONE, 
                                         CASE WHEN LOGI_CELULAR_DDD IS NULL THEN '' ELSE LOGI_CELULAR_DDD END AS LOGI_CELULAR_DDD, 
                                         CASE WHEN LOGI_CELULAR IS NULL THEN '' ELSE LOGI_CELULAR END AS LOGI_CELULAR,  
                                         LOGI_SETOR,  
                                         SET_DESCRICAO as LOGI_SETOR_DESCRICAO, 
                                         LOGI_PWD,  
                                         CASE WHEN LOGI_ULTIMO_ACESSO IS NULL THEN '' ELSE LOGI_ULTIMO_ACESSO END AS LOGI_ULTIMO_ACESSO,  
                                         CASE WHEN LOGI_FOTO IS NULL THEN '' ELSE LOGI_FOTO END AS LOGI_FOTO,  
                                         CASE WHEN LOGI_FOTO_DATA IS NULL THEN '' ELSE LOGI_FOTO_DATA END AS LOGI_FOTO_DATA,  
                                         LOGI_ATIVO, 
                                         LOGI_SUPERVISOR ,  
                                         LOGI_HOME_OFFICE,  
                                         LOGI_AUDITOR,  
                                         LOGI_DIRETORIA,  
                                         LOGI_PROGRAMACAO,  
                                         LOGI_TESTE,  
                                         CASE WHEN LOGI_ANIVERSARIO IS NULL THEN '' ELSE LOGI_ANIVERSARIO END AS LOGI_ANIVERSARIO,  
                                         CASE WHEN LOGI_PWD_ENVIADO_EM IS NULL THEN '' ELSE LOGI_PWD_ENVIADO_EM END AS LOGI_PWD_ENVIADO_EM,  
                                         CASE WHEN LOGI_USUARIO_DOMINIO IS NULL THEN '' ELSE LOGI_USUARIO_DOMINIO END AS LOGI_USUARIO_DOMINIO,  
                                         CASE WHEN LOGI_SETOR_SELECAO IS NULL THEN '' ELSE LOGI_SETOR_SELECAO END AS LOGI_SETOR_SELECAO,  
                                         CASE WHEN LOGI_LOGIN_INTERNO_SELECAO IS NULL THEN '' ELSE LOGI_LOGIN_INTERNO_SELECAO END AS LOGI_LOGIN_INTERNO_SELECAO,  
                                         CASE WHEN LOGI_DESLIGAMENTO IS NULL THEN '' ELSE LOGI_DESLIGAMENTO END AS LOGI_DESLIGAMENTO,  
                                         LOGI_PRAZO_ACRESCENTAR,  
                                         LOGI_RECEBER_AVISO_ALTERACAO_PERFIL 
                                  FROM LoginsInternos 
                                  inner join LoginsInternosSetores on SET_REFERENCIA = LOGI_SETOR 
                                  WHERE LOGI_EMAIL = @LOGI_EMAIL";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@LOGI_EMAIL", login.LOGI_EMAIL);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    #region Parametrização
                    login.LOGI_IND = Convert.ToInt32(dr["LOGI_IND"]);
                    login.LOGI_NOME = Convert.ToString(dr["LOGI_NOME"]).Replace("\r", "").Replace("\n", "");
                    login.LOGI_CARGO = Convert.ToString(dr["LOGI_CARGO"]);
                    //loginInterno.LOGI_EMAIL = Convert.ToString(dr["LOGI_EMAIL"]);
                    login.LOGI_TELEFONE = Convert.ToString(dr["LOGI_TELEFONE"]);
                    login.LOGI_CELULAR_DDD = Convert.ToString(dr["LOGI_CELULAR_DDD"]);
                    login.LOGI_CELULAR = Convert.ToString(dr["LOGI_CELULAR"]);
                    login.LOGI_SETOR = Convert.ToInt32(dr["LOGI_SETOR"]);
                    login.LOGI_SETOR_DESCRICAO = Convert.ToString(dr["LOGI_SETOR_DESCRICAO"]);
                    //loginInterno.LOGI_PWD = Convert.ToString(dr["LOGI_PWD"]);
                    login.LOGI_ULTIMO_ACESSO = Convert.ToDateTime(dr["LOGI_ULTIMO_ACESSO"]);
                    //loginInterno.LOGI_FOTO = Convert(er["LOGI_FOTO"]);
                    login.LOGI_FOTO_DATA = Convert.ToDateTime(dr["LOGI_FOTO_DATA"]);
                    login.LOGI_ATIVO = Convert.ToBoolean(dr["LOGI_ATIVO"]);
                    login.LOGI_SUPERVISOR = Convert.ToBoolean(dr["LOGI_SUPERVISOR"]);
                    login.LOGI_HOME_OFFICE = Convert.ToBoolean(dr["LOGI_HOME_OFFICE"]);
                    login.LOGI_AUDITOR = Convert.ToBoolean(dr["LOGI_AUDITOR"]);
                    login.LOGI_DIRETORIA = Convert.ToBoolean(dr["LOGI_DIRETORIA"]);
                    login.LOGI_PROGRAMACAO = Convert.ToBoolean(dr["LOGI_PROGRAMACAO"]);
                    login.LOGI_TESTE = Convert.ToBoolean(dr["LOGI_TESTE"]);
                    login.LOGI_ANIVERSARIO = Convert.ToDateTime(dr["LOGI_ANIVERSARIO"]);
                    login.LOGI_PWD_ENVIADO_EM = Convert.ToDateTime(dr["LOGI_PWD_ENVIADO_EM"]);
                    login.LOGI_USUARIO_DOMINIO = Convert.ToString(dr["LOGI_USUARIO_DOMINIO"]);
                    login.LOGI_SETOR_SELECAO = Convert.ToInt32(dr["LOGI_SETOR_SELECAO"]);
                    login.LOGI_LOGIN_INTERNO_SELECAO = Convert.ToInt32(dr["LOGI_LOGIN_INTERNO_SELECAO"]);
                    login.LOGI_DESLIGAMENTO = Convert.ToDateTime(dr["LOGI_DESLIGAMENTO"]);
                    login.LOGI_PRAZO_ACRESCENTAR = Convert.ToInt32(dr["LOGI_PRAZO_ACRESCENTAR"]);
                    login.LOGI_RECEBER_AVISO_ALTERACAO_PERFIL = Convert.ToBoolean(dr["LOGI_RECEBER_AVISO_ALTERACAO_PERFIL"]);
                    #endregion
                }
                if (login.LOGI_IND != 0)
                {
                    dr.Close();

                    select = @"select (case when fis.LOGI_IND is not null and
				                                 fis.LOGI_IND = @log
			                                then 1 else 0 end) as FISCAL,
	                                  (case when con.LOGI_IND is not null and
				                                 con.LOGI_IND = @log
	 		                                then 1 else 0 end) as CONTABIL,
	                                  (case when pes.LOGI_IND is not null and
				                                 pes.LOGI_IND = @log
			                                then 1 else 0 end) as PESSOAL,
	                                  cli.CLI_IND, cli.CLI_NUMERO, cli.CLI_RAZAOSOCIAL, cli.CLI_FANTASIA, cli.CLI_CNPJ  
                               from Clientes cli
                               left join LoginsInternos fis on fis.LOGI_IND = cli.CLI_FISCAL 
                               left join LoginsInternos con on con.LOGI_IND = cli.CLI_CONTABIL 
                               left join LoginsInternos pes on pes.LOGI_IND = cli.CLI_PESSOAL
                               where fis.LOGI_IND = @log OR 
	                                 con.LOGI_IND = @log OR 
	                                 pes.LOGI_IND = @log";

                    cmd = new SqlCommand(select.Replace("@log", login.LOGI_IND.ToString()), conn);

                    dr = cmd.ExecuteReader();

                    Cliente cliente;
                    List<Cliente> clientes = new List<Cliente>();
                    while (dr.Read())
                    {
                        cliente = new Cliente();

                        cliente.CLI_IND = Convert.ToInt32(dr["CLI_IND"]);
                        cliente.CLI_NUMERO = Convert.ToInt32(dr["CLI_NUMERO"]);
                        cliente.CLI_RAZAOSOCIAL = Convert.ToString(dr["CLI_RAZAOSOCIAL"]);
                        cliente.CLI_FANTASIA = Convert.ToString(dr["CLI_FANTASIA"]);
                        cliente.CLI_CNPJ = Convert.ToString(dr["CLI_CNPJ"]);

                        cliente.FISCAL = Convert.ToBoolean(dr["FISCAL"]);
                        cliente.CONTABIL = Convert.ToBoolean(dr["CONTABIL"]);
                        cliente.PESSOAL = Convert.ToBoolean(dr["PESSOAL"]);

                        clientes.Add(cliente);
                    }

                    login.clientes = clientes;
                    retorno = true;
                }
                else
                    retorno = false;
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
