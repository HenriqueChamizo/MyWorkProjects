using DAO.Interfaces;
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
    public class CampoDAO : ICampo, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }
        
        public List<CampoTipo> GetCampoTipos(ref TypesErrors erro)
        {
            List<CampoTipo> tipos = new List<CampoTipo>();
            try
            {
                string select = @"select CAPTIP_IND, CAPTIP_DESCRICAO from GedCampoTipo";

                conn.Open();
                cmd = new SqlCommand(select, conn);

                DbDataReader er = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                CampoTipo tipo;
                while (er.Read())
                {
                    tipo = new CampoTipo();

                    #region Parametrização
                    tipo.CAPTIP_IND = Convert.ToInt32(er["CAPTIP_IND"]);
                    tipo.CAPTIP_DESCRICAO = Convert.ToString(er["CAPTIP_DESCRICAO"]);
                    #endregion
                    tipos.Add(tipo);
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
            if (tipos.Count > 0)
                return tipos;
            else
                return new List<CampoTipo>();
        }
        
        public List<CampoDetail> GetCamposByArquivoTipo(int ind, ref TypesErrors erro)
        {
            List<CampoTipo> tipos = GetCampoTipos(ref erro);
            List<CampoDetail> campos = new List<CampoDetail>();
            try
            {
                string select = @"select a.CAMP_IND, 
	                                     a.CAMP_DESCRICAO, 
	                                     c.CAPTIP_IND, 
		                                 c.CAPTIP_DESCRICAO, 
	                                     a.CAMP_DT_INICIO, 
	                                     a.CAMP_LOGININSERT, 
	                                     a.CAMP_OBRIGATORIO, 
		                                 b.GEDTIPO_IND, 
		                                 b.GEDTIPO_DESCRICAO, 
		                                 b.GEDTIPO_DT_INICIO, 
		                                 b.GEDTIPO_EXPORTA, 
		                                 b.GEDTIPO_LOGININSERT 
                                  from GedCampo a 
                                  inner join GedArquivoTipo b on a.CAMP_ARQUIVOTIPO = b.GEDTIPO_IND 
                                  inner join GedCampoTipo c on a.CAMP_TIPO = c.CAPTIP_IND 
                                  where a.CAMP_ARQUIVOTIPO = @CAMP_ARQUIVOTIPO";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@CAMP_ARQUIVOTIPO", ind);

                DbDataReader er = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                CampoDetail campo;
                CampoTipo tipo;
                Model.Ged.ArquivoTipo arquivoTipo;
                while (er.Read())
                {
                    campo = new CampoDetail();
                    tipo = new CampoTipo();
                    arquivoTipo = new ArquivoTipo();

                    #region Parametrização
                    campo.CAMP_IND = Convert.ToInt32(er["CAMP_IND"]);
                    campo.CAMP_DESCRICAO = Convert.ToString(er["CAMP_DESCRICAO"]);

                    tipo.CAPTIP_IND = Convert.ToInt32(er["CAPTIP_IND"]);
                    tipo.CAPTIP_DESCRICAO = Convert.ToString(er["CAPTIP_DESCRICAO"]);
                    campo.CAMP_TIPO = tipo;

                    campo.CAMP_DT_INICIO = Convert.ToDateTime(er["CAMP_DT_INICIO"]);
                    campo.CAMP_LOGININSERT = Convert.ToInt32(er["CAMP_LOGININSERT"]);
                    campo.CAMP_OBRIGATORIO = Convert.ToBoolean(er["CAMP_OBRIGATORIO"]);
                    campo.tipos = tipos;

                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(er["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(er["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_DT_INICIO = Convert.ToDateTime(er["GEDTIPO_DT_INICIO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(er["GEDTIPO_EXPORTA"]);
                    arquivoTipo.GEDTIPO_LOGININSERT = Convert.ToInt32(er["GEDTIPO_LOGININSERT"]);
                    campo.CAMP_ARQUIVOTIPO = arquivoTipo;
                    #endregion
                    campos.Add(campo);
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
            if (campos.Count > 0)
                return campos;
            else
                return new List<CampoDetail>();
        }

        public List<CampoValorDetail> GetCamposValoresByArquivo(int arquivo, int arquivoTipo, ref TypesErrors erro)
        {
            List<CampoValorDetail> camposValores = new List<CampoValorDetail>();
            try
            {
                string select = @"declare @GEDARQ int = @GEDARQ_IND,
		                                  @GEDTIPO int = @GEDTIPO_IND

                                  select a.CAMP_IND, 
	                                     a.CAMP_DESCRICAO,  
	                                     f.CAPTIP_IND, 
	                                     f.CAPTIP_DESCRICAO, 
	                                     a.CAMP_DT_INICIO, 
	                                     a.CAMP_OBRIGATORIO, 
	                                     b.CAPVAL_IND, 
	                                     b.CAPVAL_VALOR, 
	                                     b.CAPVAL_DATA, 
	                                     d.GEDTIPO_IND as GEDTIPO_IND_ATUAL, 
		                                 d.GEDTIPO_DESCRICAO as GEDTIPO_DESCRICAO_ATUAL, 
		                                 g.GEDTIPO_IGUAIS, 
		                                 g.GEDTIPO_IND, 
		                                 g.GEDTIPO_DESCRICAO 
                                 from GedCampo a 
                                 left join GedCampoValor b on a.CAMP_IND = b.CAPVAL_CAMPO 
                                 left join ContArqTipo c on b.CAPVAL_CONTARQUIVOSTIPO = c.CATIP_IND  
                                 left join GedArquivoTipo d on c.CATIP_ARQUIVOTIPO = d.GEDTIPO_IND 
                                 left join GedArquivo e on c.CATIP_IND = e.GEDARQ_CONTARQUIVOTIPO 
                                 left join GedCampoTipo f on a.CAMP_TIPO = f.CAPTIP_IND 
                                 outer apply(
	                                 select case when ga.GEDTIPO_IND = d.GEDTIPO_IND 
				                                 then 1 else 0 end as GEDTIPO_IGUAIS, ga.GEDTIPO_IND, ga.GEDTIPO_DESCRICAO 
	                                 from GedArquivoTipo ga
	                                 where ga.GEDTIPO_IND = @GEDTIPO
                                 ) g
                                 where ((a.CAMP_ARQUIVOTIPO = d.GEDTIPO_IND and 
		                                    d.GEDTIPO_IND = @GEDTIPO and 
		                                    e.GEDARQ_IND = @GEDARQ) or 
	                                    (a.CAMP_ARQUIVOTIPO = @GEDTIPO and 
 	                                        d.GEDTIPO_IND is null and 
		                                    e.GEDARQ_IND is null)) ";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ_IND", arquivo);
                cmd.Parameters.AddWithValue("@GEDTIPO_IND", arquivoTipo);

                DbDataReader er = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                CampoValorDetail campoValor;
                while (er.Read())
                {
                    campoValor = new CampoValorDetail();

                    #region Parametrização
                    Campo campo = new Campo();
                    campo.CAMP_IND = Convert.ToInt32(er["CAMP_IND"]);
                    campo.CAMP_DESCRICAO = Convert.ToString(er["CAMP_DESCRICAO"]);
                        CampoTipo tipo = new CampoTipo();
                        tipo.CAPTIP_IND = Convert.ToInt32(er["CAPTIP_IND"]);
                        tipo.CAPTIP_DESCRICAO = Convert.ToString(er["CAPTIP_DESCRICAO"]);
                    campo.CAMP_TIPO = tipo;
                    campo.CAMP_DT_INICIO = Convert.ToDateTime(er["CAMP_DT_INICIO"]);
                    campo.CAMP_OBRIGATORIO = Convert.ToBoolean(er["CAMP_OBRIGATORIO"]);
                    
                    campoValor.CAPVAL_CONTARQUIVOSTIPO = new Model.ContArquivoTipo();
                    campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO = new ArquivoTipo();
                    if (Convert.ToBoolean(er["GEDTIPO_IGUAIS"]))
                    {
                        campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_IND = Convert.ToInt32(er["GEDTIPO_IND_ATUAL"]);
                        campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_DESCRICAO = Convert.ToString(er["GEDTIPO_DESCRICAO_ATUAL"]);
                    }
                    else
                    {
                        campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_IND = Convert.ToInt32(er["GEDTIPO_IND"]);
                        campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_DESCRICAO = Convert.ToString(er["GEDTIPO_DESCRICAO"]);
                    }
                    campo.CAMP_ARQUIVOTIPO = campoValor.CAPVAL_CONTARQUIVOSTIPO.CATIP_ARQUIVOTIPO;

                    campoValor = new CampoValorDetail();
                    campoValor.CAPVAL_IND = 0;
                    campoValor.CAPVAL_CAMPO = campo;

                    if (!(er["CAPVAL_IND"] is DBNull))
                    {
                        campoValor.CAPVAL_IND = Convert.ToInt32(er["CAPVAL_IND"]);
                        campoValor.CAPVAL_VALOR = Convert.ToString(er["CAPVAL_VALOR"]);
                        campoValor.CAPVAL_DATA = Convert.ToDateTime(er["CAPVAL_DATA"]);
                    }

                    #endregion
                    camposValores.Add(campoValor);
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
            if (camposValores.Count > 0)
                return camposValores;
            else
                return new List<CampoValorDetail>();
        }
    }
}
