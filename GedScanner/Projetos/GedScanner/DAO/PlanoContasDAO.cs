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
    public class PlanoContasDAO : IPlanoContas, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetPlanoContasById(ref PlanoContas planoContas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select PLAN_IND,  
		                                 PLAN_DESCRICAO, 
		                                 PLAN_CODIGO, 
		                                 PLAN_FECHADO, 
		                                 PLAN_DT_INICIO, 
   		                                 PLAN_LOGININSERT, 
	                                     case when exists(select PLAN_IND from PlanoConta where PLAN_FECHADO = 0) 
			                                  then 1 else 0 end as EXIST  
	                             from PlanoConta 
	                             where PLAN_IND = @PLAN_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@PLAN_IND", planoContas.PLAN_IND);

                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    #region Parametrização
                    planoContas.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    planoContas.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    planoContas.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    planoContas.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    planoContas.PLAN_DT_INICIO = Convert.ToDateTime(string.IsNullOrEmpty(dr["PLAN_DT_INICIO"].ToString()) ? DateTime.MinValue : dr["PLAN_DT_INICIO"]);
                    planoContas.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                    planoContas.condition = Convert.ToBoolean(dr["EXIST"]);
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

        public PlanoContas GetPlanoContasAtual(ref TypesErrors erro)
        {
            List<PlanoContas> planosContas = new List<PlanoContas>();
            try
            {
                string select = @"if (exists (select * from PlanoConta where PLAN_FECHADO = 0))
                                    begin
	                                    select PLAN_IND,  
		                                       PLAN_DESCRICAO, 
		                                       PLAN_CODIGO, 
		                                       PLAN_FECHADO, 
		                                       PLAN_DT_INICIO, 
   		                                       PLAN_LOGININSERT, 
		                                       PLAN_HISTORICO, 
		                                       PLAN_ANTERIOR 
	                                    from PlanoConta 
	                                    where PLAN_FECHADO = 0 
                                    end
                                    else begin
	                                    select top 1 0 as PLAN_IND,  
		                                       '' as PLAN_DESCRICAO, 
		                                       PLAN_CODIGO, 
		                                       1  as PLAN_FECHADO, 
		                                       '' as PLAN_DT_INICIO, 
   		                                       0  as PLAN_LOGININSERT, 
		                                       0  as PLAN_HISTORICO, 
		                                       0  as PLAN_ANTERIOR 
	                                    from PlanoConta 
	                                    order by CONVERT(int, PLAN_CODIGO) desc
                                    end";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                PlanoContas plano;
                while (dr.Read())
                {
                    plano = new PlanoContas();

                    #region Parametrização
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(string.IsNullOrEmpty(dr["PLAN_DT_INICIO"].ToString()) ? DateTime.MinValue : dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);

                    #endregion
                    planosContas.Add(plano);
                }
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
            if (planosContas.Count > 0)
                return planosContas[0];
            else
                return new PlanoContas();
        }

        public List<PlanoContas> GetPlanoContasFechados(ref TypesErrors erro)
        {
            List<PlanoContas> planosContas = new List<PlanoContas>();
            try
            {
                string select = @"SET DATEFORMAT DMY   
 
                                create table #PlanoContas(  
                                PLAN_IND INT,   
                                PLAN_CODIGO VARCHAR(10)  
                                );  
 
                                declare @PLAN_IND INT,   
		                                @PLAN_CODIGO VARCHAR(10),   
		                                @LIST varchar(1000) = '',  
		                                @SELECT varchar(max) = 'select PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT,  
			                                 PLAN_HISTORICO, PLAN_ANTERIOR from PlanoConta where PLAN_IND in (@LIST) order by PLAN_DT_INICIO desc'  
   
                                DECLARE PLANOCONTAS_COPIA CURSOR FOR    
	                                select PLAN_IND, PLAN_CODIGO from PlanoConta where PLAN_FECHADO = 1 order by PLAN_DT_INICIO, PLAN_CODIGO desc  
    
                                OPEN PLANOCONTAS_COPIA   
                                FETCH NEXT FROM PLANOCONTAS_COPIA    
                                INTO @PLAN_IND, @PLAN_CODIGO  
    
	                                WHILE @@FETCH_STATUS = 0   
	                                BEGIN    
		                                if not exists(select * from #PlanoContas where PLAN_CODIGO = @PLAN_CODIGO)  
		                                begin  
			                                insert into #PlanoContas (PLAN_IND, PLAN_CODIGO) values (@PLAN_IND, @PLAN_CODIGO)  
			                                set @LIST = @LIST + (CONVERT(VARCHAR(10), @PLAN_IND) + ', ')  
		                                end  
    
		                                FETCH NEXT FROM PLANOCONTAS_COPIA   
		                                INTO @PLAN_IND, @PLAN_CODIGO  
	                                END  
                                CLOSE PLANOCONTAS_COPIA;    
                                DEALLOCATE PLANOCONTAS_COPIA;    
 
                                drop table #PlanoContas  
 
                                set @SELECT = REPLACE(@SELECT, '@LIST', SUBSTRING(@LIST, 1, (LEN(@LIST) - 1))) 
 
                                exec(@SELECT) ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                PlanoContas plano;
                while (dr.Read())
                {
                    plano = new PlanoContas();

                    #region Parametrização
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);

                    #endregion
                    planosContas.Add(plano);
                }
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
            if (planosContas.Count > 0)
                return planosContas;
            else
                return new List<PlanoContas>();
        }

        public PlanoContasAll GetPlanoContasTodos(ref TypesErrors erro)
        {
            PlanoContasAll planoAll = new PlanoContasAll();
            planoAll.plancontContas = new List<PlanoContaConta>();
            try
            {
                string select = @"SELECT A.PLAN_IND, 
	                                   A.PLAN_DESCRICAO, 
	                                   A.PLAN_CODIGO, 
	                                   A.PLAN_FECHADO, 
	                                   A.PLAN_DT_INICIO, 
	                                   A.PLAN_LOGININSERT, 
	                                   C.CONT_IND, 
	                                   C.CONT_DESCRICAO, 
	                                   C.CONT_ACESSO, 
	                                   C.CONT_CONTATIPO, 
	                                   C.CONT_DT_INICIO, 
	                                   C.CONT_LOGININSERT
                                FROM PlanoConta A 
                                LEFT JOIN PlanContConta B ON A.PLAN_IND = B.PCONT_PLANOCONTA 
                                LEFT JOIN Conta C ON B.PCONT_CONTA = C.CONT_IND 
                                WHERE (SELECT MAX(D.PLAN_DT_INICIO) FROM PlanoConta D WHERE D.PLAN_CODIGO = A.PLAN_CODIGO) = A.PLAN_DT_INICIO";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                PlanoContaConta plano = new PlanoContaConta();
                Conta conta;
                while (dr.Read())//for (int i = 0; i < dataTable.Rows.Count; i++) //(DataRowCollection row in dataTable.Rows)
                {
                    plano = new PlanoContaConta();
                    plano.contas = new List<Conta>();

                    #region Parametrização
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);

                    conta = new Conta();

                    conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"] is DBNull ? "0" : dr["CONT_IND"]);
                    if (conta.CONT_IND != 0)
                    {
                        conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"] is DBNull ? "" : dr["CONT_DESCRICAO"]);
                        conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"] is DBNull ? "" : dr["CONT_ACESSO"]);
                        ContaTipo tipo = new ContaTipo();
                        tipo.CONTP_IND = Convert.ToInt32(dr["CONT_CONTATIPO"] is DBNull ? "0" : dr["CONT_CONTATIPO"]);
                        conta.CONT_CONTASTIPOS = tipo;
                        conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"] is DBNull ? DateTime.MinValue : dr["CONT_DT_INICIO"]);
                        conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"] is DBNull ? "0" : dr["CONT_LOGININSERT"]);
                        plano.contas.Add(conta);
                    }
                    #endregion

                    PlanoContaConta find = planoAll.plancontContas.Find(f => f.PLAN_IND == plano.PLAN_IND);
                    if (find != null)
                    {
                        planoAll.plancontContas.Remove(find);
                        find.contas.Add(plano.contas[0]);
                        planoAll.plancontContas.Add(find);
                    }
                    else
                        planoAll.plancontContas.Add(plano);
                }
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


            planoAll.contas = new List<Conta>();
            try
            {
                string select = @"SELECT CONT_IND, 
	                                   CONT_DESCRICAO, 
	                                   CONT_ACESSO, 
	                                   CONT_CONTATIPO, 
	                                   CONT_DT_INICIO, 
	                                   CONT_LOGININSERT
                                FROM Conta ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                DbDataReader er = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                Conta conta;
                while (er.Read())//for (int i = 0; i < dataTable.Rows.Count; i++) //(DataRowCollection row in dataTable.Rows)
                {
                    conta = new Conta();

                    #region Parametrização
                    conta.CONT_IND = Convert.ToInt32(er["CONT_IND"]);
                    conta.CONT_DESCRICAO = Convert.ToString(er["CONT_DESCRICAO"]);
                    conta.CONT_ACESSO = Convert.ToString(er["CONT_ACESSO"]);
                    ContaTipo tipo = new ContaTipo();
                    tipo.CONTP_IND = Convert.ToInt32(er["CONT_CONTATIPO"]);
                    conta.CONT_CONTASTIPOS = tipo;
                    conta.CONT_DT_INICIO = Convert.ToDateTime(er["CONT_DT_INICIO"]);
                    conta.CONT_LOGININSERT = Convert.ToInt32(er["CONT_LOGININSERT"]);

                    planoAll.contas.Add(conta);
                    #endregion
                }
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

            if (planoAll.plancontContas.Count > 0)
                return planoAll;
            else
                return new PlanoContasAll();
        }

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT A.PLAN_IND, 
	                                     A.PLAN_DESCRICAO, 
	                                     A.PLAN_CODIGO, 
	                                     A.PLAN_FECHADO, 
	                                     A.PLAN_DT_INICIO, 
	                                     A.PLAN_LOGININSERT 
                                  FROM PlanoConta A ";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                PlanoContas plano;
                while (dr.Read())
                {
                    plano = new PlanoContas();

                    #region Parametrização
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                    #endregion

                    planosContas.Add(plano);
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

        public bool GetPlanoContas(ref List<PlanoContas> planosContas, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
		                                  @pag int = @p_pag
                                    SELECT PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT, N_PLANOCONTAS 
                                    FROM (
	                                    SELECT ROW_NUMBER() OVER(ORDER BY PLAN_IND) AS NUMBER,
		                                       PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT, 
		                                       (select COUNT(*) from PlanoConta) as N_PLANOCONTAS
	                                    FROM PlanoConta 
	                                    group by PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT
	                                    ) AS TBL
                                    WHERE NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde)
                                    ORDER BY PLAN_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                PlanoContas plano;
                bool passo = false;
                while (dr.Read())
                {
                    plano = new PlanoContas();

                    #region Parametrização
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_PLANOCONTAS"]);
                    passo = true;
                    planosContas.Add(plano);
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

        public bool SetPlanoContasEdit(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            try
            {
                string insert = @"DECLARE @PARAM_FECHADO BIT = @PLAN_FECHADO,
		                                    @PARAM_IND INT = @PLAN_IND

                                    IF ((@PARAM_FECHADO = 0 AND 
	                                    NOT EXISTS(SELECT * FROM PlanoConta 
				                                    WHERE PLAN_IND != @PARAM_IND AND PLAN_FECHADO = 0)) OR @PARAM_FECHADO = 1)
                                    BEGIN 
	                                    UPDATE PlanoConta
	                                    SET PLAN_DESCRICAO = @PLAN_DESCRICAO,
		                                    PLAN_CODIGO = @PLAN_CODIGO, 
		                                    PLAN_FECHADO = @PARAM_FECHADO, 
		                                    PLAN_LOGININSERT = @PLAN_LOGININSERT 
	                                    WHERE PLAN_IND = @PARAM_IND 
                                    END";
                //insert = insert.Replace("@PLAN_IND", planocontas.PLAN_IND.ToString()).Replace("@PLAN_DESCRICAO", planocontas.PLAN_DESCRICAO).Replace("@PLAN_CODIGO", planocontas.PLAN_CODIGO);
                //insert = insert.Replace("@PLAN_FECHADO", Convert.ToByte(planocontas.PLAN_FECHADO).ToString()).Replace("@PLAN_DT_INICIO", planocontas.PLAN_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss"));
                //insert = insert.Replace("@LOGIN", login.ToString());

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PLAN_IND", planocontas.PLAN_IND);
                cmd.Parameters.AddWithValue("@PLAN_DESCRICAO", planocontas.PLAN_DESCRICAO);
                cmd.Parameters.AddWithValue("@PLAN_CODIGO", planocontas.PLAN_CODIGO);
                cmd.Parameters.AddWithValue("@PLAN_FECHADO", planocontas.PLAN_FECHADO);
                cmd.Parameters.AddWithValue("@PLAN_LOGININSERT", login);

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

        public bool SetPlanoContasInsert(PlanoContas planocontas, int login, ref TypesErrors erro)
        {
            try
            {
                string insert = @"SET DATEFORMAT DMY 
                                  INSERT INTO PlanoConta (PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT) 
				                                VALUES (@PLAN_DESCRICAO, @PLAN_CODIGO, 0, @PLAN_DT_INICIO, @LOGIN)";
                //insert = insert.Replace("@PLAN_DESCRICAO", planocontas.PLAN_DESCRICAO).Replace("@PLAN_CODIGO", planocontas.PLAN_CODIGO);
                //insert = insert.Replace("@PLAN_DT_INICIO", planocontas.PLAN_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss")).Replace("@LOGIN", login.ToString());

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PLAN_DESCRICAO", planocontas.PLAN_DESCRICAO);
                cmd.Parameters.AddWithValue("@PLAN_CODIGO", planocontas.PLAN_CODIGO);
                cmd.Parameters.AddWithValue("@PLAN_DT_INICIO", planocontas.PLAN_DT_INICIO);
                cmd.Parameters.AddWithValue("@LOGIN", login);

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
