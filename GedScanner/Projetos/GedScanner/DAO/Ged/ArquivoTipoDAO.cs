using DAO.Interfaces;
using Model;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO.Ged
{
    public class ArquivoTipoDAO : IArquivoTipo, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public List<ArquivoTipo> GetArquivosTiposFromConta(int indConut, ref TypesErrors erro)
        {
            List<ArquivoTipo> arquivosTipos = new List<ArquivoTipo>();
            try
            {
                string select = @"select a.GEDTIPO_IND, 
	                                     a.GEDTIPO_DESCRICAO, 
                                         a.GEDTIPO_EXPORTA 
                                  from GedArquivoTipo a
                                  inner join ContArqTipo b on a.GEDTIPO_IND = b.CATIP_ARQUIVOTIPO
                                  inner join PlanContConta c on b.CATIP_PLANCONTCONTA = c.PCONT_IND
                                  inner join PlanoConta d on c.PCONT_PLANOCONTA = d.PLAN_IND 
                                  inner join Conta e on c.PCONT_CONTA = e.CONT_IND
                                  where d.PLAN_DT_FIM is null and e.CONT_IND = " + indConut.ToString();

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                ArquivoTipo arquivoTipo;
                while (dr.Read())
                {
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);

                    #endregion
                    arquivosTipos.Add(arquivoTipo);
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
            if (arquivosTipos.Count > 0)
                return arquivosTipos;
            else
                return new List<ArquivoTipo>();
        }

        public List<ArquivoTipo> GetArquivosTiposFromContaAndPlano(int indConta, int indPlano, ref TypesErrors erro)
        {
            List<ArquivoTipo> arquivosTipos = new List<ArquivoTipo>();
            try
            {
                string select = @"select a.GEDTIPO_IND, 
	                                     a.GEDTIPO_DESCRICAO, 
	                                     a.GEDTIPO_EXPORTA 
                                  from GedArquivoTipo a
                                  inner join ContArqTipo b on a.GEDTIPO_IND = b.CATIP_ARQUIVOTIPO
                                  inner join PlanContConta c on b.CATIP_PLANCONTCONTA = c.PCONT_IND 
                                  where c.PCONT_PLANOCONTA = " + indPlano.ToString() + " and c.PCONT_CONTA = " + indConta.ToString();

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                ArquivoTipo arquivoTipo;
                while (dr.Read())
                {
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);

                    #endregion
                    arquivosTipos.Add(arquivoTipo);
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
            if (arquivosTipos.Count > 0)
                return arquivosTipos;
            else
                return new List<ArquivoTipo>();
        }

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select GEDTIPO_IND, 
	                                       GEDTIPO_DESCRICAO, 
	                                       GEDTIPO_EXPORTA, 
	                                       GEDTIPO_DT_INICIO, 
	                                       GEDTIPO_LOGININSERT, 
	                                       GEDTIPO_PAI 
                                    from GedArquivoTipo
                                    order by GEDTIPO_DT_INICIO desc, 
                                             GEDTIPO_DESCRICAO asc";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                ArquivoTipo arquivoTipo;
                while (dr.Read())
                {
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);
                    arquivoTipo.GEDTIPO_DT_INICIO = Convert.ToDateTime(dr["GEDTIPO_DT_INICIO"]);
                    arquivoTipo.GEDTIPO_LOGININSERT = Convert.ToInt32(dr["GEDTIPO_LOGININSERT"]);
                    arquivoTipo.GEDTIPO_PAI = new ArquivoTipo() { GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_PAI"]) };
                    #endregion
                    arquivosTipos.Add(arquivoTipo);
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

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
                                            @pag int = @p_pag
                                    select a.GEDTIPO_IND, a.GEDTIPO_DESCRICAO, a.GEDTIPO_EXPORTA, a.GEDTIPO_DT_INICIO, a.GEDTIPO_LOGININSERT, a.GEDTIPO_PAI, a.N_COUNT from (
	                                    select ROW_NUMBER() OVER(ORDER BY a.GEDTIPO_IND) AS NUMBER, 
		                                        a.GEDTIPO_IND, a.GEDTIPO_DESCRICAO, a.GEDTIPO_EXPORTA, a.GEDTIPO_DT_INICIO, a.GEDTIPO_LOGININSERT, a.GEDTIPO_PAI, 
		                                        (select count(*) from GedArquivoTipo) as N_COUNT 
	                                    from GedArquivoTipo a 
	                                    group by a.GEDTIPO_IND, a.GEDTIPO_DESCRICAO, a.GEDTIPO_EXPORTA, a.GEDTIPO_DT_INICIO, a.GEDTIPO_LOGININSERT, a.GEDTIPO_PAI) as a
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) 
                                    order by a.GEDTIPO_DT_INICIO desc, 
                                            a.GEDTIPO_DESCRICAO asc";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                ArquivoTipo arquivoTipo;
                bool passo = false;
                while (dr.Read())
                {
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);
                    arquivoTipo.GEDTIPO_DT_INICIO = Convert.ToDateTime(dr["GEDTIPO_DT_INICIO"]);
                    arquivoTipo.GEDTIPO_LOGININSERT = Convert.ToInt32(dr["GEDTIPO_LOGININSERT"]);
                    arquivoTipo.GEDTIPO_PAI = new ArquivoTipo() { GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_PAI"]) };
                    #endregion

                    pagination.rows = Convert.ToInt32(dr["N_COUNT"]);
                    passo = true;
                    arquivosTipos.Add(arquivoTipo);
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

        public ArquivoTipo GetArquivoTipo(int index, ref TypesErrors erro)
        {
            List<ArquivoTipo> arquivosTipos = new List<ArquivoTipo>();
            try
            {
                string select = @"select GEDTIPO_IND, 
	                                     GEDTIPO_DESCRICAO, 
	                                     GEDTIPO_EXPORTA 
                                  from GedArquivoTipo 
                                  where GEDTIPO_IND = " + index.ToString();

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();
                
                ArquivoTipo arquivoTipo;
                while (dr.Read())
                {
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);

                    #endregion
                    arquivosTipos.Add(arquivoTipo);
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
            if (arquivosTipos.Count > 0)
                return arquivosTipos[0];
            else
                return new ArquivoTipo();
        }

        public int GetTipoArquivoByArquivo(int indArquivo, ref TypesErrors erro)
        {
            int indTipoArquivo = -1;
            try
            {
                string select = @"select a.GEDTIPO_IND 
                                  from GedArquivoTipo a
                                  inner join ContArqTipo b on a.GEDTIPO_IND = b.CATIP_ARQUIVOTIPO
                                  inner join GedArquivo c on b.CATIP_IND = c.GEDARQ_CONTARQUIVOTIPO 
                                  where c.GEDARQ_IND = @GEDARQ";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ", indArquivo);

                //DbDataReader er = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                object retorno = cmd.ExecuteScalar();
                if (retorno != null)
                    indTipoArquivo = Convert.ToInt32(retorno);
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
            return indTipoArquivo;
        }

        public bool SetArquivoTipoAndCamposInsert(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            try
            {
                string insert = @"declare @GEDTIPO_IND int
                                  insert into GedArquivoTipo (GEDTIPO_DESCRICAO, GEDTIPO_EXPORTA, GEDTIPO_DT_INICIO, GEDTIPO_LOGININSERT) 
					                                  values(@GEDTIPO_DESCRICAO,@GEDTIPO_EXPORTA,@GEDTIPO_DT_INICIO,@GEDTIPO_LOGININSERT) 
                                  SET @GEDTIPO_IND = (SELECT IDENT_CURRENT('GedArquivoTipo')) ";

                int count = campos.Count;

                for (int c = 0; c < count; c++)
                {
                    insert = insert +
                        @"insert into GedCampo (CAMP_DESCRICAO, CAMP_ARQUIVOTIPO, CAMP_TIPO, CAMP_DT_INICIO, CAMP_LOGININSERT, CAMP_OBRIGATORIO) 
			              values(@CAMP_DESCRICAO_" + c + ",@GEDTIPO_IND,@CAMP_TIPO_" + c + ",@CAMP_DT_INICIO_" + c + @",
                          @CAMP_LOGININSERT_" + c + ",@CAMP_OBRIGATORIO_" + c + ") ";
                }

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@GEDTIPO_DESCRICAO", arquivoTipo.GEDTIPO_DESCRICAO);
                cmd.Parameters.AddWithValue("@GEDTIPO_EXPORTA", arquivoTipo.GEDTIPO_EXPORTA);
                cmd.Parameters.AddWithValue("@GEDTIPO_DT_INICIO", arquivoTipo.GEDTIPO_DT_INICIO);
                cmd.Parameters.AddWithValue("@GEDTIPO_LOGININSERT", arquivoTipo.GEDTIPO_LOGININSERT);
                int atual = 0;
                foreach (Campo campo in campos)
                {
                    cmd.Parameters.AddWithValue("@CAMP_DESCRICAO_" + atual.ToString(), campo.CAMP_DESCRICAO);
                    cmd.Parameters.AddWithValue("@CAMP_TIPO_" + atual.ToString(), campo.CAMP_TIPO.CAPTIP_IND);
                    cmd.Parameters.AddWithValue("@CAMP_DT_INICIO_" + atual.ToString(), campo.CAMP_DT_INICIO);
                    cmd.Parameters.AddWithValue("@CAMP_LOGININSERT_" + atual.ToString(), campo.CAMP_LOGININSERT);
                    cmd.Parameters.AddWithValue("@CAMP_OBRIGATORIO_" + atual.ToString(), campo.CAMP_OBRIGATORIO);
                    atual++;
                }

                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                bool retorno;
                if (rows > 0)
                    retorno = true;
                else
                    retorno = false;
                return retorno;
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

        public bool SetArquivoTipoAndCamposEdit(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            try
            {
                string update = @"update GedArquivoTipo 
                                  set GEDTIPO_DESCRICAO = @GEDTIPO_DESCRICAO, 
	                                  GEDTIPO_EXPORTA = @GEDTIPO_EXPORTA, 
	                                  GEDTIPO_DT_INICIO = @GEDTIPO_DT_INICIO, 
	                                  GEDTIPO_LOGININSERT = @GEDTIPO_LOGININSERT 
                                  where GEDTIPO_IND = @GEDTIPO_IND ";

                int count = campos.Count;

                int c = 0;
                foreach (Campo campo in campos)
                {
                    if (campo.CAMP_IND == 0)
                    {
                        update = update + @"insert into GedCampo (CAMP_DESCRICAO, CAMP_ARQUIVOTIPO, CAMP_TIPO, CAMP_DT_INICIO, CAMP_LOGININSERT, CAMP_OBRIGATORIO) 
			                                    values(@CAMP_DESCRICAO_" + c + ",@GEDTIPO_IND,@CAMP_TIPO_" + c + ",@CAMP_DT_INICIO_" + c + @",
                                                @CAMP_LOGININSERT_" + c + ",@CAMP_OBRIGATORIO_" + c + ") ";   
                    }
                    else
                    {
                        update = update +
                            @"update GedCampo  
                              set CAMP_DESCRICAO = @CAMP_DESCRICAO_" + c + @",  
	                              CAMP_TIPO = @CAMP_TIPO_" + c + @",  
	                              CAMP_DT_INICIO = @CAMP_DT_INICIO_" + c + @",  
	                              CAMP_LOGININSERT = @CAMP_LOGININSERT_" + c + @",  
	                              CAMP_OBRIGATORIO = @CAMP_OBRIGATORIO_" + c + @" 
                              where CAMP_IND = @CAMP_IND_" + c + " ";
                    }
                    c++;
                }

                //for (int c = 0; c < count; c++)
                //{
                //    update = update +
                //        @"update GedCampo  
                //          set CAMP_DESCRICAO = @CAMP_DESCRICAO_" + c + @",  
	               //           CAMP_TIPO = @CAMP_TIPO_" + c + @",  
	               //           CAMP_DT_INICIO = @CAMP_DT_INICIO_" + c + @",  
	               //           CAMP_LOGININSERT = @CAMP_LOGININSERT_" + c + @",  
	               //           CAMP_OBRIGATORIO = @CAMP_OBRIGATORIO_" + c + @" 
                //          where CAMP_IND = @CAMP_IND_" + c + " ";
                //}

                conn.Open();
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@GEDTIPO_IND", arquivoTipo.GEDTIPO_IND);
                cmd.Parameters.AddWithValue("@GEDTIPO_DESCRICAO", arquivoTipo.GEDTIPO_DESCRICAO);
                cmd.Parameters.AddWithValue("@GEDTIPO_EXPORTA", arquivoTipo.GEDTIPO_EXPORTA);
                cmd.Parameters.AddWithValue("@GEDTIPO_DT_INICIO", arquivoTipo.GEDTIPO_DT_INICIO);
                cmd.Parameters.AddWithValue("@GEDTIPO_LOGININSERT", arquivoTipo.GEDTIPO_LOGININSERT);
                int atual = 0;
                foreach (Campo campo in campos)
                {
                    if (campo.CAMP_IND != 0)
                        cmd.Parameters.AddWithValue("@CAMP_IND_" + atual.ToString(), campo.CAMP_IND);
                    cmd.Parameters.AddWithValue("@CAMP_DESCRICAO_" + atual.ToString(), campo.CAMP_DESCRICAO);
                    cmd.Parameters.AddWithValue("@CAMP_TIPO_" + atual.ToString(), campo.CAMP_TIPO.CAPTIP_IND);
                    cmd.Parameters.AddWithValue("@CAMP_DT_INICIO_" + atual.ToString(), campo.CAMP_DT_INICIO);
                    cmd.Parameters.AddWithValue("@CAMP_LOGININSERT_" + atual.ToString(), campo.CAMP_LOGININSERT);
                    cmd.Parameters.AddWithValue("@CAMP_OBRIGATORIO_" + atual.ToString(), campo.CAMP_OBRIGATORIO);
                    atual++;
                }

                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                bool retorno;
                if (rows > 0)
                    retorno = true;
                else
                    retorno = false;
                return retorno;
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
