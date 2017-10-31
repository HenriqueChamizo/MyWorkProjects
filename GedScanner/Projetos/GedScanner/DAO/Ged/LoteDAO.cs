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
    public class LoteDAO : ILote, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetLotes(ref List<Lote> lotes, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select GLOTE_IND, 
	                                    GLOTE_DESCRICAO, 
	                                    GLOTE_DISPONIVELEM, 
	                                    GLOTE_DISPONIVELPOR, 
	                                    GLOTE_FECHADOEM, 
	                                    GLOTE_FECHADOPOR, 
	                                    GLOTE_ENVIADO 
                                from GedLote order by GLOTE_DISPONIVELEM desc, GLOTE_ENVIADO";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                Lote lote;
                while (dr.Read())
                {
                    lote = new Lote();
                    #region Parametrização
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"] is DBNull ? "0" : dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_FECHADOEM = Convert.ToDateTime(dr["GLOTE_FECHADOEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_FECHADOEM"]);
                    lote.GLOTE_FECHADOPOR = Convert.ToInt32(dr["GLOTE_FECHADOPOR"] is DBNull ? "0" : dr["GLOTE_FECHADOPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);

                    #endregion
                    lotes.Add(lote);
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

        public bool GetLotes(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
                                          @pag int = @p_pag
                                    select GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_FECHADOEM, GLOTE_FECHADOPOR, GLOTE_ENVIADO, N_LOTE 
                                    from (
	                                    SELECT ROW_NUMBER() OVER(ORDER BY GLOTE_IND) AS NUMBER, 
	                                    GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_FECHADOEM, GLOTE_FECHADOPOR, GLOTE_ENVIADO, 
	                                    (select COUNT(*) from GedLote) as N_LOTE 
	                                    from GedLote) as TBL 
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) 
                                    order by GLOTE_DISPONIVELEM desc, GLOTE_ENVIADO";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                Lote lote;
                bool passo = false;
                while (dr.Read())
                {
                    lote = new Lote();
                    #region Parametrização
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"] is DBNull ? "0" : dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_FECHADOEM = Convert.ToDateTime(dr["GLOTE_FECHADOEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_FECHADOEM"]);
                    lote.GLOTE_FECHADOPOR = Convert.ToInt32(dr["GLOTE_FECHADOPOR"] is DBNull ? "0" : dr["GLOTE_FECHADOPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_LOTE"]);
                    passo = true;
                    lotes.Add(lote);
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

        public bool GetLoteById(ref Lote lote, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select GLOTE_IND, 
	                                       GLOTE_DESCRICAO, 
	                                       GLOTE_DISPONIVELEM, 
	                                       GLOTE_DISPONIVELPOR, 
	                                       GLOTE_FECHADOEM, 
	                                       GLOTE_FECHADOPOR, 
	                                       GLOTE_ENVIADO 
                                    from GedLote where GLOTE_IND = @GLOTE_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();
                
                while (dr.Read())
                {
                    #region Parametrização
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"] is DBNull ? "0" : dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_FECHADOEM = Convert.ToDateTime(dr["GLOTE_FECHADOEM"] is DBNull ? DateTime.MinValue : dr["GLOTE_FECHADOEM"]);
                    lote.GLOTE_FECHADOPOR = Convert.ToInt32(dr["GLOTE_FECHADOPOR"] is DBNull ? "0" : dr["GLOTE_FECHADOPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);

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

        public bool GetLotesEmAberto(ref List<Lote> lotes, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"select a.GLOTE_IND, 
                                  a.GLOTE_DESCRICAO, 
                                  a.GLOTE_DISPONIVELEM, 
                                  a.GLOTE_DISPONIVELPOR, 
                                  a.GLOTE_ENVIADO, 
	                              COUNT(b.GEDARQ_IND) as 'N_ARQUIVOS', 
	                              (select COUNT(c.GEDARQ_IND) 
	                               from GedArquivo c
		                           left join ContArqTipo d on c.GEDARQ_CONTARQUIVOTIPO = d.CATIP_IND 
		                           where a.GLOTE_IND = c.GEDARQ_LOTE and 
			                             d.CATIP_IND is null) as 'N_ARQUIVOS_NCLASSIC'
                           from GedLote a 
                           left join GedArquivo b on a.GLOTE_IND = b.GEDARQ_LOTE 
                           where GLOTE_FECHADOEM is null 
                           group by a.GLOTE_IND, a.GLOTE_DESCRICAO, a.GLOTE_DISPONIVELEM, a.GLOTE_DISPONIVELPOR, a.GLOTE_ENVIADO ";
                cmd = new SqlCommand(select, conn);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Lote lote;
                while (dr.Read())
                {
                    #region Parametrização
                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);
                    lote.N_ARQUIVOS = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    lote.N_ARQUIVOS_NCLASSIC = Convert.ToInt32(dr["N_ARQUIVOS_NCLASSIC"]);
                    #endregion

                    lotes.Add(lote);
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

        public bool GetLotesEmAberto(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"declare @qtde int = @p_qtde, 
                            		@pag int = @p_pag
                            select GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO, N_ARQUIVOS, N_ARQUIVOS_NCLASSIC, N_LOTES
                            from (
	                            select ROW_NUMBER() OVER(ORDER BY a.GLOTE_IND) AS NUMBER, 
		                               a.GLOTE_IND, a.GLOTE_DESCRICAO, a.GLOTE_DISPONIVELEM, a.GLOTE_DISPONIVELPOR, a.GLOTE_ENVIADO, 
		                               (select COUNT(b.GEDARQ_IND) from GedArquivo b left join ContArqTipo d on b.GEDARQ_CONTARQUIVOTIPO = d.CATIP_IND 
		                                where a.GLOTE_IND = b.GEDARQ_LOTE and d.CATIP_IND is null) as 'N_ARQUIVOS', 
		                               (select COUNT(c.GEDARQ_IND) from GedArquivo c left join ContArqTipo e on c.GEDARQ_CONTARQUIVOTIPO = e.CATIP_IND 
		                                where a.GLOTE_IND = c.GEDARQ_LOTE and e.CATIP_IND is null) as 'N_ARQUIVOS_NCLASSIC', 
		                               (select COUNT(*) from GedLote c where c.GLOTE_FECHADOEM is null) as 'N_LOTES'
	                            from GedLote a  
	                            where a.GLOTE_FECHADOEM is null 
	                            group by GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO) as TBL
                            where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) 
                            order by GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO";
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Lote lote;
                bool passo = false;
                while (dr.Read())
                {
                    #region Parametrização
                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);
                    lote.N_ARQUIVOS = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    lote.N_ARQUIVOS_NCLASSIC = Convert.ToInt32(dr["N_ARQUIVOS_NCLASSIC"]);
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_LOTES"]);
                    passo = true;
                    lotes.Add(lote);
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

        public bool GetLotesNaoEnviados(ref List<Lote> lotes, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"select a.GLOTE_IND, 
                                  a.GLOTE_DESCRICAO, 
                                  a.GLOTE_DISPONIVELEM, 
                                  a.GLOTE_DISPONIVELPOR, 
                                  a.GLOTE_ENVIADO, 
	                              COUNT(b.GEDARQ_IND) as 'N_ARQUIVOS', 
	                              (select COUNT(c.GEDARQ_IND) 
	                               from GedArquivo c
		                           left join ContArqTipo d on c.GEDARQ_CONTARQUIVOTIPO = d.CATIP_IND 
		                           where a.GLOTE_IND = c.GEDARQ_LOTE and 
			                             d.CATIP_IND is null) as 'N_ARQUIVOS_NCLASSIC'
                           from GedLote a 
                           left join GedArquivo b on a.GLOTE_IND = b.GEDARQ_LOTE 
                           where GLOTE_ENVIADO = 0 
                           group by a.GLOTE_IND, a.GLOTE_DESCRICAO, a.GLOTE_DISPONIVELEM, a.GLOTE_DISPONIVELPOR, a.GLOTE_ENVIADO ";
                cmd = new SqlCommand(select, conn);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Lote lote;
                while (dr.Read())
                {
                    #region Parametrização
                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);
                    lote.N_ARQUIVOS = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    lote.N_ARQUIVOS_NCLASSIC = Convert.ToInt32(dr["N_ARQUIVOS_NCLASSIC"]);
                    #endregion

                    lotes.Add(lote);
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

        public bool GetLotesNaoEnviados(ref List<Lote> lotes, ref Pagination pagination, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"declare @qtde int = @p_qtde, 
                            		@pag int = @p_pag
                            select GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO, N_ARQUIVOS, N_ARQUIVOS_NCLASSIC, N_LOTES
                            from (
	                            select ROW_NUMBER() OVER(ORDER BY a.GLOTE_IND) AS NUMBER, 
		                                a.GLOTE_IND, a.GLOTE_DESCRICAO, a.GLOTE_DISPONIVELEM, a.GLOTE_DISPONIVELPOR, a.GLOTE_ENVIADO, 
		                                (select COUNT(b.GEDARQ_IND) from GedArquivo b left join ContArqTipo d on b.GEDARQ_CONTARQUIVOTIPO = d.CATIP_IND 
		                                where a.GLOTE_IND = b.GEDARQ_LOTE and d.CATIP_IND is null) as 'N_ARQUIVOS', 
		                                (select COUNT(c.GEDARQ_IND) from GedArquivo c left join ContArqTipo e on c.GEDARQ_CONTARQUIVOTIPO = e.CATIP_IND 
		                                where a.GLOTE_IND = c.GEDARQ_LOTE and e.CATIP_IND is null) as 'N_ARQUIVOS_NCLASSIC', 
		                                (select COUNT(*) from GedLote c where c.GLOTE_FECHADOEM is null) as 'N_LOTES'
	                            from GedLote a 
	                            where a.GLOTE_ENVIADO = 0 
	                            group by GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO) as TBL
                            where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) 
                            order by GLOTE_IND, GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO";
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Lote lote;
                bool passo = false;
                while (dr.Read())
                {
                    #region Parametrização
                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);
                    lote.N_ARQUIVOS = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    lote.N_ARQUIVOS_NCLASSIC = Convert.ToInt32(dr["N_ARQUIVOS_NCLASSIC"]);
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_LOTES"]);
                    passo = true;
                    lotes.Add(lote);
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

        public bool GetArquivosByLote(Lote lote, ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"select a.GEDARQ_IND, 
	                                   a.GEDARQ_DISPONIVELEM, 
	                                   a.GEDARQ_DISPONIVELPOR, 
	                                   --a.GEDARQ_ARQUIVO, 
	                                   a.GEDARQ_TAMANHO, 
	                                   a.GEDARQ_EXTENSAO, 
	                                   a.GEDARQ_DESCRICAO, 
	                                   a.GEDARQ_ACESSOEM, 
	                                   a.GEDARQ_ACESSOPOR, 
	                                   a.GEDARQ_CONTARQUIVOTIPO, 
	                                   b.CATIP_ARQUIVOTIPO, 
	                                   c.GEDTIPO_DESCRICAO, 
	                                   a.GEDARQ_LOTE 
                                from GedArquivo a 
                                left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
                                left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
                                inner join GedLote d on a.GEDARQ_LOTE = d.GLOTE_IND 
                                where d.GLOTE_IND = @GLOTE_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                ContArqTipo ctipo;
                ArquivoTipo tipo;
                while (dr.Read())
                {
                    arquivo = new Arquivo();
                    ctipo = new ContArqTipo();
                    tipo = new ArquivoTipo();

                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    //arquivo.GEDARQ_ARQUIVO = (byte[])(er["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"] is DBNull ? DateTime.MinValue : dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"] is DBNull ? "0" : dr["GEDARQ_ACESSOPOR"]);

                    ctipo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"] is DBNull ? "0" : dr["GEDARQ_CONTARQUIVOTIPO"]);
                    tipo.GEDTIPO_IND = Convert.ToInt32(dr["CATIP_ARQUIVOTIPO"] is DBNull ? "0" : dr["CATIP_ARQUIVOTIPO"]);
                    tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    ctipo.CATIP_ARQUIVOTIPO = tipo;
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ctipo;

                    lote.GLOTE_IND = Convert.ToInt32(dr["GEDARQ_LOTE"]);
                    arquivo.GEDARQ_LOTE = lote;
                    #endregion
                    arquivos.Add(arquivo);
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

        public bool GetArquivosByLoteClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
                                            @pag int = @p_pag
                                    select GEDARQ_IND, GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, 
	                                       GEDARQ_ACESSOEM, GEDARQ_ACESSOPOR, GEDARQ_CONTARQUIVOTIPO, CATIP_ARQUIVOTIPO, GEDTIPO_DESCRICAO, GEDARQ_LOTE, N_ARQUIVOS 
                                    from (
	                                    select ROW_NUMBER() OVER(ORDER BY a.GEDARQ_IND) AS NUMBER, a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, 
			                                    a.GEDARQ_DESCRICAO, a.GEDARQ_ACESSOEM, a.GEDARQ_ACESSOPOR, a.GEDARQ_CONTARQUIVOTIPO, b.CATIP_ARQUIVOTIPO, c.GEDTIPO_DESCRICAO, a.GEDARQ_LOTE, 
		                                           (select count(*) from GedArquivo a left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND 
			                                        left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
			                                        inner join GedLote d on a.GEDARQ_LOTE = d.GLOTE_IND 
			                                        where d.GLOTE_IND = @GLOTE_IND and a.GEDARQ_CONTARQUIVOTIPO is not null) as N_ARQUIVOS 
	                                    from GedArquivo a 
	                                    left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
	                                    left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
	                                    inner join GedLote d on a.GEDARQ_LOTE = d.GLOTE_IND 
	                                    where d.GLOTE_IND = @GLOTE_IND and a.GEDARQ_CONTARQUIVOTIPO is not null
                                    ) tbl
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) ";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                ContArqTipo ctipo;
                ArquivoTipo tipo;
                bool passo = false;
                while (dr.Read())
                {
                    arquivo = new Arquivo();
                    ctipo = new ContArqTipo();
                    tipo = new ArquivoTipo();

                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    //arquivo.GEDARQ_ARQUIVO = (byte[])(er["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"] is DBNull ? DateTime.MinValue : dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"] is DBNull ? "0" : dr["GEDARQ_ACESSOPOR"]);

                    ctipo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"] is DBNull ? "0" : dr["GEDARQ_CONTARQUIVOTIPO"]);
                    tipo.GEDTIPO_IND = Convert.ToInt32(dr["CATIP_ARQUIVOTIPO"] is DBNull ? "0" : dr["CATIP_ARQUIVOTIPO"]);
                    tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    ctipo.CATIP_ARQUIVOTIPO = tipo;
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ctipo;

                    lote.GLOTE_IND = Convert.ToInt32(dr["GEDARQ_LOTE"]);
                    arquivo.GEDARQ_LOTE = lote;
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    passo = true;
                    arquivos.Add(arquivo);
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

        public bool GetArquivosByLoteNClassificados(Lote lote, ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"declare @qtde int = @p_qtde, 
                                            @pag int = @p_pag
                                    select GEDARQ_IND, GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, 
	                                       GEDARQ_ACESSOEM, GEDARQ_ACESSOPOR, GEDARQ_CONTARQUIVOTIPO, CATIP_ARQUIVOTIPO, GEDTIPO_DESCRICAO, GEDARQ_LOTE, N_ARQUIVOS 
                                    from (
	                                    select ROW_NUMBER() OVER(ORDER BY a.GEDARQ_IND) AS NUMBER, a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, 
			                                    a.GEDARQ_DESCRICAO, a.GEDARQ_ACESSOEM, a.GEDARQ_ACESSOPOR, a.GEDARQ_CONTARQUIVOTIPO, b.CATIP_ARQUIVOTIPO, c.GEDTIPO_DESCRICAO, a.GEDARQ_LOTE, 
		                                           (select count(*) from GedArquivo a left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND 
			                                        left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
			                                        inner join GedLote d on a.GEDARQ_LOTE = d.GLOTE_IND 
			                                        where d.GLOTE_IND = @GLOTE_IND and a.GEDARQ_CONTARQUIVOTIPO is not null) as N_ARQUIVOS 
	                                    from GedArquivo a 
	                                    left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
	                                    left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
	                                    inner join GedLote d on a.GEDARQ_LOTE = d.GLOTE_IND 
	                                    where d.GLOTE_IND = @GLOTE_IND and a.GEDARQ_CONTARQUIVOTIPO is null
                                    ) tbl
                                    where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) ";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                ContArqTipo ctipo;
                ArquivoTipo tipo;
                bool passo = false;
                while (dr.Read())
                {
                    arquivo = new Arquivo();
                    ctipo = new ContArqTipo();
                    tipo = new ArquivoTipo();

                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    //arquivo.GEDARQ_ARQUIVO = (byte[])(er["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"] is DBNull ? DateTime.MinValue : dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"] is DBNull ? "0" : dr["GEDARQ_ACESSOPOR"]);

                    ctipo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"] is DBNull ? "0" : dr["GEDARQ_CONTARQUIVOTIPO"]);
                    tipo.GEDTIPO_IND = Convert.ToInt32(dr["CATIP_ARQUIVOTIPO"] is DBNull ? "0" : dr["CATIP_ARQUIVOTIPO"]);
                    tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    ctipo.CATIP_ARQUIVOTIPO = tipo;
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ctipo;

                    lote.GLOTE_IND = Convert.ToInt32(dr["GEDARQ_LOTE"]);
                    arquivo.GEDARQ_LOTE = lote;
                    #endregion
                    pagination.rows = Convert.ToInt32(dr["N_ARQUIVOS"]);
                    passo = false;
                    arquivos.Add(arquivo);
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

        public bool SetLoteEdit(Lote lote, int login, ref TypesErrors erro)
        {
            try
            {
                conn.Open();
                string update;
                if (lote.GLOTE_FECHADOEM != DateTime.MinValue)
                {
                    update = @"update GedLote  
                            set GLOTE_DESCRICAO = @GLOTE_DESCRICAO,  
	                            GLOTE_FECHADOEM = @GLOTE_FECHADOEM,  
	                            GLOTE_FECHADOPOR = @GLOTE_FECHADOPOR,  
	                            GLOTE_ENVIADO = @GLOTE_ENVIADO 
                            where GLOTE_IND = @GLOTE_IND ";
                    cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@GLOTE_DESCRICAO", lote.GLOTE_DESCRICAO);
                    cmd.Parameters.AddWithValue("@GLOTE_FECHADOEM", lote.GLOTE_FECHADOEM);
                    cmd.Parameters.AddWithValue("@GLOTE_FECHADOPOR", lote.GLOTE_FECHADOPOR);
                    cmd.Parameters.AddWithValue("@GLOTE_ENVIADO", lote.GLOTE_ENVIADO);
                    cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);
                }
                else
                {
                    update = @"update GedLote 
                            set GLOTE_DESCRICAO = @GLOTE_DESCRICAO,  
	                            GLOTE_FECHADOEM = NULL,  
	                            GLOTE_FECHADOPOR = NULL 
                            where GLOTE_IND = @GLOTE_IND ";
                    cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@GLOTE_DESCRICAO", lote.GLOTE_DESCRICAO);
                    cmd.Parameters.AddWithValue("@GLOTE_IND", lote.GLOTE_IND);
                }                

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

        public bool SetLoteInsert(Lote lote, int login, ref TypesErrors erro)
        {
            try
            {
                string insert = @"insert into GedLote (GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO)
			                                  values (@GLOTE_DESCRICAO,@GLOTE_DISPONIVELEM,@GLOTE_DISPONIVELPOR, 0)";
                //insert = insert.Replace("@PLAN_DESCRICAO", planocontas.PLAN_DESCRICAO).Replace("@PLAN_CODIGO", planocontas.PLAN_CODIGO);
                //insert = insert.Replace("@PLAN_DT_INICIO", planocontas.PLAN_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss")).Replace("@LOGIN", login.ToString());

                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@GLOTE_DESCRICAO", lote.GLOTE_DESCRICAO);
                cmd.Parameters.AddWithValue("@GLOTE_DISPONIVELEM", lote.GLOTE_DISPONIVELEM);
                cmd.Parameters.AddWithValue("@GLOTE_DISPONIVELPOR", lote.GLOTE_DISPONIVELPOR);

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
