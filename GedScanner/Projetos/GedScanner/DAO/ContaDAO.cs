using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Model;
using Model.Enuns;
using System.Data.Common;

namespace DAO
{
    public class ContaDAO : IConta, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetContas(ref List<Conta> contas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select a.CONT_IND, 
                                         a.CONT_ACESSO, 
                                         a.CONT_DESCRICAO, 
                                         a.CONT_CONTATIPO, 
                                         d.CONTP_DESCRICAO, 
                                         d.CONTP_CLASSIFICADOR 
                                  from Conta a 
                                  inner join ContaTipo d on a.CONT_CONTATIPO = d.CONTP_IND ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                Conta conta;
                ContaTipo tipo;
                while (dr.Read())
                {
                    conta = new Conta();
                    tipo = new ContaTipo();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                    tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    conta.CONT_CONTASTIPOS = tipo;
                    #endregion
                    contas.Add(conta);
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

        public bool GetContas(ref List<Conta> contas, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
		                                    @pag int = @p_pag
                                    select CONT_IND, CONT_ACESSO, CONT_DESCRICAO, CONT_CONTATIPO, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, N_CONTA 
                                    from ( 
	                                    SELECT ROW_NUMBER() OVER(ORDER BY CONVERT(INT, CONT_ACESSO)) AS NUMBER, 
		                                    a.CONT_IND, a.CONT_ACESSO, a.CONT_DESCRICAO, a.CONT_CONTATIPO, d.CONTP_DESCRICAO, d.CONTP_CLASSIFICADOR, 
		                                    (select COUNT(*) from Conta) as N_CONTA
	                                    from Conta a 
	                                    inner join ContaTipo d on a.CONT_CONTATIPO = d.CONTP_IND
                                    ) as TBL
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde)
                                    -- ORDER BY CONT_IND 
                                    order by CONVERT(INT, CONT_ACESSO), CONT_DESCRICAO";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                Conta conta;
                ContaTipo tipo;
                bool passo = false;
                while (dr.Read())
                {
                    conta = new Conta();
                    tipo = new ContaTipo();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                    tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    conta.CONT_CONTASTIPOS = tipo;
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_CONTA"]);
                    passo = true;
                    contas.Add(conta);
                }
                if (!passo)
                    pagination.rows = 0;
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

        public bool GetContasAndAtual(ref PlanoContas planoatual, ref List<Conta> contas, ref TypesErrors erro)
        {
            string select;
            bool retorno;
            try
            {
                select = @"select PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT 
                           from PlanoConta where PLAN_FECHADO = 0";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    planoatual.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    planoatual.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    planoatual.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    planoatual.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    planoatual.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    planoatual.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                }

                dr.Close();

                select = @"select a.CONT_IND, 
	                        a.CONT_DESCRICAO, 
	                        a.CONT_ACESSO, 
	                        a.CONT_CONTATIPO, 
	                        a.CONT_DT_INICIO, 
	                        a.CONT_LOGININSERT, 
	                      --  case when b.PLAN_IND is not null 
			                    --then 1 else 0 end as 'EXIST', 
		                    case when c.PCONT_IND is not null 
			                    then 1 else 0 end as 'EXIST', 
	                        d.CONTP_DESCRICAO,  
	                        d.CONTP_CLASSIFICADOR   
                    from Conta a 
                    left join PlanoConta b on b.PLAN_FECHADO = 0 
                    left join PlanContConta c on exists(select * from PlanContConta t
							                            where a.CONT_IND = c.PCONT_CONTA and 
								                        b.PLAN_IND = c.PCONT_PLANOCONTA and 
								                        c.PCONT_IND = t.PCONT_IND)
                    left join ContaTipo d on a.CONT_CONTATIPO = d.CONTP_IND 
                    order by a.CONT_ACESSO, a.CONT_DESCRICAO";

                cmd = new SqlCommand(select, conn);
                dr = cmd.ExecuteReader();

                Conta conta;
                ContaTipo tipo;
                while (dr.Read())
                {
                    conta = new Conta();
                    tipo = new ContaTipo();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);

                    tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    conta.CONT_CONTASTIPOS = tipo;

                    conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"]);
                    conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"]);
                    conta.condition = Convert.ToBoolean(dr["EXIST"]);

                    #endregion
                    contas.Add(conta);
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

        public bool GetContasByPlano(PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select a.CONT_IND, 
                                         a.CONT_ACESSO, 
                                         a.CONT_DESCRICAO, 
                                         a.CONT_CONTATIPO, 
                                         d.CONTP_DESCRICAO, 
                                         d.CONTP_CLASSIFICADOR 
                                  from Conta a 
                                  inner join PlanContConta b on a.CONT_IND = b.PCONT_CONTA 
                                  inner join PlanoConta c on b.PCONT_PLANOCONTA = c.PLAN_IND 
                                  inner join ContaTipo d on a.CONT_CONTATIPO = d.CONTP_IND 
                                  where c.PLAN_IND = @PLAN_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@PLAN_IND", plano.PLAN_IND);

                dr = cmd.ExecuteReader();

                Conta conta;
                ContaTipo tipo;
                while (dr.Read())
                {
                    conta = new Conta();
                    tipo = new ContaTipo();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                    tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    conta.CONT_CONTASTIPOS = tipo;
                    #endregion
                    contas.Add(conta);
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

        public bool GetContasByPlanoAtual(ref PlanoContas plano, ref List<Conta> contas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select a.CONT_IND, 
	                                     a.CONT_DESCRICAO, 
	                                     a.CONT_ACESSO, 
	                                     a.CONT_CONTATIPO, 
	                                     a.CONT_DT_INICIO, 
	                                     a.CONT_LOGININSERT, 
	                                     d.CONTP_DESCRICAO, 
	                                     d.CONTP_CLASSIFICADOR, 
	                                     c.PLAN_IND, 
	                                     c.PLAN_DESCRICAO    
                                  from Conta a 
                                  left join PlanContConta b on a.CONT_IND = b.PCONT_CONTA 
                                  right join PlanoConta c on b.PCONT_PLANOCONTA = c.PLAN_IND 
                                  left join ContaTipo d on a.CONT_CONTATIPO = d.CONTP_IND 
                                  where c.PLAN_FECHADO = 0 ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                Conta conta;
                ContaTipo tipo;
                bool primeiro = true;
                while (dr.Read())
                {
                    conta = new Conta();
                    tipo = new ContaTipo();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"] is DBNull ? "0" : dr["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"] is DBNull ? "0" : dr["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"] is DBNull ? "0" : dr["CONT_ACESSO"]);
                    conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"] is DBNull ? DateTime.MinValue : dr["CONT_DT_INICIO"]);
                    conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"] is DBNull ? "0" : dr["CONT_LOGININSERT"]);
                    tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"] is DBNull ? "0" : dr["CONT_CONTATIPO"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"] is DBNull ? "0" : dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"] is DBNull ? "0" : dr["CONTP_CLASSIFICADOR"]);
                    conta.CONT_CONTASTIPOS = tipo;

                    if (primeiro)
                    {
                        plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                        plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                        primeiro = false;
                    }
                    #endregion
                    contas.Add(conta);
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

        public bool GetContaById(ref Conta conta, ref PlanoContas plano, ref List<ContaTipo> tipos, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @plano int = (case when exists(select PLAN_IND from PlanoConta where PLAN_FECHADO = 0) 
						                                     then (select PLAN_IND from PlanoConta where PLAN_FECHADO = 0) else 0 end)
                                  select a.CONTP_IND, 
	                                    a.CONTP_DESCRICAO, 
	                                    a.CONTP_CLASSIFICADOR, 
	                                    b.PLAN_IND, 
	                                    b.PLAN_DESCRICAO, 
	                                    b.CONT_IND,
	                                    b.CONT_DESCRICAO, 
	                                    b.CONT_CONTATIPO, 
	                                    b.CONT_ACESSO, 
	                                    b.CONT_DT_INICIO, 
	                                    b.CONT_LOGININSERT, 
	                                    b.CONDITION 
                                    from ContaTipo a
                                    outer apply (
                                    select c.CONT_IND, 
	                                    c.CONT_DESCRICAO, 
	                                    c.CONT_ACESSO, 
	                                    c.CONT_CONTATIPO, 
	                                    c.CONT_DT_INICIO, 
	                                    c.CONT_LOGININSERT, 
	                                    case when @plano != 0 
		                                    then (case when exists(select PCONT_IND 
								                                    from PlanContConta 
								                                    where PCONT_PLANOCONTA = @plano and 
										                                    PCONT_CONTA = CONT_IND)
					                                    then 1 else 0 end)
		                                    else 0 end as CONDITION, 
	                                    d.PLAN_DESCRICAO, d.PLAN_IND 
                                    from Conta c
                                    left join PlanoConta d on d.PLAN_FECHADO = 0 
                                    where c.CONT_IND = @CONT_IND and
		                                    c.CONT_CONTATIPO = a.CONTP_IND 
                                    ) b";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@CONT_IND", conta.CONT_IND);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                ContaTipo tipo;
                bool control = false;
                while (dr.Read())//for (int i = 0; i < dataTable.Rows.Count; i++) //(DataRowCollection row in dataTable.Rows)
                {
                    //DataRow row = dataTable.Rows[i];
                    tipo = new ContaTipo();

                    #region Parametrização
                    tipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                    tipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    tipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);

                    if (!(dr["CONT_IND"] is DBNull) && !control)
                    {
                        control = true;
                        plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"] is DBNull ? "-1" : dr["PLAN_IND"]);
                        plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                        
                        conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                        conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                        conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                        conta.CONT_CONTASTIPOS = tipo;
                        conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"]);
                        conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"]);
                        conta.condition = Convert.ToBoolean(dr["CONDITION"]);
                    }

                    #endregion
                    tipos.Add(tipo);
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

        public bool GetContasTipos(ref List<ContaTipo> contasTipos, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT
                                  FROM ContaTipo ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                ContaTipo contaTipo;
                while (dr.Read())
                {
                    contaTipo = new ContaTipo();

                    #region Parametrização
                    contaTipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                    contaTipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    contaTipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    contaTipo.CONTP_DT_INICIO = Convert.ToDateTime(dr["CONTP_DT_INICIO"]);
                    contaTipo.CONTP_LOGININSERT = Convert.ToInt32(dr["CONTP_LOGININSERT"]);

                    contasTipos.Add(contaTipo);
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

        public bool GetContasTipos(ref List<ContaTipo> contasTipos, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
		                                    @pag int = @p_pag
                                    SELECT CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT, N_TIPO
                                    from ( 
	                                    SELECT ROW_NUMBER() OVER(ORDER BY CONVERT(INT, CONTP_IND)) AS NUMBER, 
		                                    CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT, COUNT(CONTP_IND) as N_TIPO
	                                    FROM ContaTipo 
	                                    group by CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT
                                    ) as TBL
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde)
                                    order by CONTP_DESCRICAO";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                ContaTipo contaTipo;
                bool passo = false;
                while (dr.Read())
                {
                    contaTipo = new ContaTipo();

                    #region Parametrização
                    contaTipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                    contaTipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    contaTipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    contaTipo.CONTP_DT_INICIO = Convert.ToDateTime(dr["CONTP_DT_INICIO"]);
                    contaTipo.CONTP_LOGININSERT = Convert.ToInt32(dr["CONTP_LOGININSERT"]);

                    contasTipos.Add(contaTipo);
                    pagination.rows = Convert.ToInt32(dr["N_TIPO"]);
                    passo = true;
                    #endregion
                }
                if (!passo)
                    pagination.rows = 0;
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

        public bool GetContasTipos(ref List<ContaTipo> tipos, ref PlanoContas plano, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT
                                  FROM ContaTipo ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                ContaTipo contaTipo;
                while (dr.Read())
                {
                    contaTipo = new ContaTipo();

                    #region Parametrização
                    contaTipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                    contaTipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    contaTipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    contaTipo.CONTP_DT_INICIO = Convert.ToDateTime(dr["CONTP_DT_INICIO"]);
                    contaTipo.CONTP_LOGININSERT = Convert.ToInt32(dr["CONTP_LOGININSERT"]);

                    tipos.Add(contaTipo);
                    #endregion
                }
                
                select = @"select PLAN_IND, 
	                               PLAN_DESCRICAO, 
	                               PLAN_CODIGO, 
	                               PLAN_FECHADO, 
	                               PLAN_DT_INICIO, 
	                               PLAN_LOGININSERT  
                            from PlanoConta
                            where PLAN_FECHADO = 0";

                cmd = new SqlCommand(select, conn);

                dr.Close();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
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

        public bool GetContaTipoById(ref ContaTipo contaTipo, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT CONTP_IND, CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT
                                  FROM ContaTipo 
                                  where CONTP_IND = @CONTP_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@CONTP_IND", contaTipo.CONTP_IND);

                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    contaTipo = new ContaTipo();

                    #region Parametrização
                    contaTipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                    contaTipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                    contaTipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                    contaTipo.CONTP_DT_INICIO = Convert.ToDateTime(dr["CONTP_DT_INICIO"]);
                    contaTipo.CONTP_LOGININSERT = Convert.ToInt32(dr["CONTP_LOGININSERT"]);
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

        public bool SetContaEdit(Conta conta, int login, ref TypesErrors erro)
        {
            try
            {
                string update = @"DECLARE @VINCULAR BIT = @PARAM_VINCULAR, 
		                                  @CONT_IND INT = @PARAM_CONTA, 
		                                  @LOGIN_INSERT INT = @PARAM_LOGININSERT, 
		                                  @PLAN_IND INT = CASE WHEN EXISTS(SELECT PLAN_IND FROM PlanoConta WHERE PLAN_FECHADO = 0)
							                                   THEN (SELECT PLAN_IND FROM PlanoConta WHERE PLAN_FECHADO = 0) ELSE 0 END
  
                                  IF (@VINCULAR = 1 AND 
	                                  NOT EXISTS(SELECT * 
			                                     FROM PlanContConta 
			                                     WHERE PCONT_CONTA = @CONT_IND AND 
					                                   PCONT_PLANOCONTA = @PLAN_IND) AND
	                                  @PLAN_IND != 0)
                                  BEGIN
	                                  INSERT INTO PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA, PCONT_DT_INICIO, PCONT_LOGININSERT)
					                                     VALUES (@CONT_IND, @PLAN_IND, @PARAM_DT_INICIO, @LOGIN_INSERT)
                                  END ELSE BEGIN
	                                  IF (EXISTS(SELECT * 
			                                     FROM PlanContConta 
			                                     WHERE PCONT_CONTA = @CONT_IND AND 
					                                   PCONT_PLANOCONTA = @PLAN_IND))
	                                 BEGIN
		                                  DELETE FROM PlanContConta 
		                                  WHERE PCONT_CONTA = @CONT_IND AND 
			                                    PCONT_PLANOCONTA = @PLAN_IND
	                                  END
                                  END
  
                                  UPDATE Conta
                                  SET CONT_DESCRICAO = @PARAM_DESCRICAO, 
	                                  CONT_ACESSO = @PARAM_ACESSO, 
	                                  CONT_CONTATIPO = @PARAM_CONTATIPO, 
	                                  CONT_LOGININSERT = @LOGIN_INSERT
                                  WHERE CONT_IND = @CONT_IND";

                conn.Open();
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@PARAM_DT_INICIO", conta.CONT_DT_INICIO);
                cmd.Parameters.AddWithValue("@PARAM_LOGININSERT", login);
                cmd.Parameters.AddWithValue("@PARAM_VINCULAR", conta.condition);
                cmd.Parameters.AddWithValue("@PARAM_DESCRICAO", conta.CONT_DESCRICAO);
                cmd.Parameters.AddWithValue("@PARAM_ACESSO", conta.CONT_ACESSO);
                cmd.Parameters.AddWithValue("@PARAM_CONTATIPO", conta.CONT_CONTASTIPOS.CONTP_IND);
                cmd.Parameters.AddWithValue("@PARAM_CONTA", conta.CONT_IND);

                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetContaTipoEdit(ContaTipo tipo, ref TypesErrors erro)
        {
            try
            {
                string update = @"UPDATE ContaTipo
                                  SET CONTP_DESCRICAO = @DESCRICAO, 
	                                  CONTP_CLASSIFICADOR = @CLASSIFICACAO, 
	                                  CONTP_DT_INICIO = @CONTP_DT_INICIO, 
	                                  CONTP_LOGININSERT = @CONTP_LOGININSERT 
                                  WHERE CONTP_IND = @CONTP_IND";

                conn.Open();
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@DESCRICAO", tipo.CONTP_DESCRICAO);
                cmd.Parameters.AddWithValue("@CLASSIFICACAO", tipo.CONTP_CLASSIFICADOR);
                cmd.Parameters.AddWithValue("@CONTP_DT_INICIO", tipo.CONTP_DT_INICIO);
                cmd.Parameters.AddWithValue("@CONTP_LOGININSERT", tipo.CONTP_LOGININSERT);
                cmd.Parameters.AddWithValue("@CONTP_IND", tipo.CONTP_IND);

                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetContaInsert(Conta conta, int login, ref TypesErrors erro)
        {
            try
            {
                string insert = @"DECLARE @VINCULAR BIT = @PARAM_VINCULAR, 
		                                    @CONT_IND INT, 
		                                    @LOGIN_INSERT INT = @PARAM_LOGININSERT, 
		                                    @DT_INICIO DATETIME = @PARAM_DT_INICIO, 
		                                    @PLAN_IND INT = CASE WHEN EXISTS(SELECT PLAN_IND FROM PlanoConta WHERE PLAN_FECHADO = 0)
							                                     THEN (SELECT PLAN_IND FROM PlanoConta WHERE PLAN_FECHADO = 0) ELSE 0 END

                                    INSERT INTO Conta (CONT_DESCRICAO, CONT_ACESSO, CONT_CONTATIPO, CONT_LOGININSERT, CONT_DT_INICIO)
	                                           VALUES (@PARAM_DESCRICAO, @PARAM_ACESSO, @PARAM_CONTATIPO, @LOGIN_INSERT, @DT_INICIO)

                                    SET @CONT_IND = (SELECT IDENT_CURRENT('Conta'))

                                    IF (@VINCULAR = 1 AND 
	                                    @PLAN_IND != 0)
                                    BEGIN
	                                    INSERT INTO PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA, PCONT_DT_INICIO, PCONT_LOGININSERT)
					                                       VALUES (@CONT_IND, @PLAN_IND, @DT_INICIO, @LOGIN_INSERT)
                                    END";

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PARAM_DT_INICIO", conta.CONT_DT_INICIO);
                cmd.Parameters.AddWithValue("@PARAM_LOGININSERT", login);
                cmd.Parameters.AddWithValue("@PARAM_VINCULAR", conta.condition);
                cmd.Parameters.AddWithValue("@PARAM_DESCRICAO", conta.CONT_DESCRICAO);
                cmd.Parameters.AddWithValue("@PARAM_ACESSO", conta.CONT_ACESSO);
                cmd.Parameters.AddWithValue("@PARAM_CONTATIPO", conta.CONT_CONTASTIPOS.CONTP_IND);

                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetContaTipoInsert(ContaTipo tipo, ref TypesErrors erro)
        {
            try
            {
                string insert = @"insert into ContaTipo (CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT) 
			                                    values (@CONTP_DESCRICAO,@CONTP_CLASSIFICADOR,@CONTP_DT_INICIO,@CONTP_LOGININSERT) ";

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@CONTP_DESCRICAO", tipo.CONTP_DESCRICAO);
                cmd.Parameters.AddWithValue("@CONTP_CLASSIFICADOR", tipo.CONTP_CLASSIFICADOR);
                cmd.Parameters.AddWithValue("@CONTP_DT_INICIO", tipo.CONTP_DT_INICIO);
                cmd.Parameters.AddWithValue("@CONTP_LOGININSERT", tipo.CONTP_LOGININSERT);

                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetPlanContConta(int indConta, int indPlan, ref TypesErrors erro)
        {
            try
            {
                string insert = @"if (exists(select * from PlanContConta where PCONT_CONTA = @PCONT_CONTA and PCONT_PLANOCONTA = @PCONT_PLANOCONTA))
                                  begin delete from PlanContConta where PCONT_CONTA = @PCONT_CONTA and PCONT_PLANOCONTA = @PCONT_PLANOCONTA end
                                  else
                                  begin insert into PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA) values (@PCONT_CONTA, @PCONT_PLANOCONTA) end";
                insert = insert.Replace("@PCONT_CONTA", indConta.ToString()).Replace("@PCONT_PLANOCONTA", indPlan.ToString());

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetPlanContConta(List<string> valores, ref TypesErrors erro)
        {
            try
            {
                string insert = "";

                foreach (string valor in valores)
                {
                    string[] split = valor.Split(new string[] { " - " }, StringSplitOptions.None);
                    insert += @"if (@exist = 0) 
                                begin 
	                                set @ind = (select PCONT_IND from PlanContConta where PCONT_CONTA = @conta and PCONT_PLANOCONTA = @plano) 
	                                if (@ind != 0) 
		                                delete from PlanContConta where PCONT_IND = @ind 
                                end else  
                                begin 
	                                if (@exist = 1) 
	                                begin 
		                                set @ind = (select PCONT_IND from PlanContConta where PCONT_CONTA = @conta and PCONT_PLANOCONTA = @plano) 
		                                if (@ind = 0) 
			                                insert into PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA)  
				                                values (@conta, @plano) 
	                                end 
                                end 

                                ";
                    insert = insert.Replace("@exist", split[2]).Replace("@conta", split[0]).Replace("@plano", split[1]);
                }


                conn.Open();
                cmd = new SqlCommand(insert, conn);
                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DelPlanContConta(int indConta, int indPlan, ref TypesErrors erro)
        {
            try
            {
                string delete = @"from PlanContConta where PCONT_CONTA = @PCONT_CONTA and PCONT_PLANOCONTA = @PCONT_PLANOCONTA)";
                delete = delete.Replace("@PCONT_CONTA", indConta.ToString()).Replace("@PCONT_PLANOCONTA", indPlan.ToString());

                conn.Open();
                cmd = new SqlCommand(delete, conn);
                int rows = cmd.ExecuteNonQuery();
                //DbDataReader er = cmd.ExecuteReader();
                if (rows > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                erro = TypesErrors.SqlError;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                erro = TypesErrors.GereralFailure;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
