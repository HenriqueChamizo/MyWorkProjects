using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Enuns;
using Model.Ged;
using System.Data.SqlClient;
using System.Data.Common;
using Model;

namespace DAO.Ged
{
    public class ArquivoDAO : IArquivo, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetArquivos(Conta conta, ArquivoTipo arquivoTipo, ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT	a.GEDARQ_IND, 
		                                    a.GEDARQ_DISPONIVELEM, 
		                                    a.GEDARQ_DISPONIVELPOR, 
		                                    a.GEDARQ_ARQUIVO, 
		                                    a.GEDARQ_TAMANHO, 
		                                    a.GEDARQ_EXTENSAO, 
		                                    a.GEDARQ_DESCRICAO, 
		                                    case 
			                                    when a.GEDARQ_ACESSOEM is null
			                                    then ''
			                                    else a.GEDARQ_ACESSOEM
		                                    end as GEDARQ_ACESSOEM, 
		                                    case 
			                                    when a.GEDARQ_ACESSOPOR is null
			                                    then ''
			                                    else a.GEDARQ_ACESSOPOR
		                                    end as GEDARQ_ACESSOPOR, 
		                                    a.GEDARQ_CONTARQUIVOTIPO  
                                    FROM GedArquivo a
                                    inner join ContArqTipo b on b.CATIP_IND = a.GEDARQ_CONTARQUIVOTIPO 
                                    inner join GedArquivoTipo c on c.GEDTIPO_IND = b.CATIP_ARQUIVOTIPO 
                                    inner join PlanContConta d on d.PCONT_IND = b.CATIP_PLANCONTCONTA 
                                    inner join Conta e on e.CONT_IND = d.PCONT_CONTA
                                    where c.GEDTIPO_IND = @GEDTIPO_IND and e.CONT_IND = @CONT_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDTIPO_IND", arquivoTipo.GEDTIPO_IND);
                cmd.Parameters.AddWithValue("@CONT_IND", conta.CONT_IND);

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                ContArqTipo ContaArquivo;
                while (dr.Read())
                {
                    arquivo = new Arquivo();
                    ContaArquivo = new ContArqTipo();

                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_ARQUIVO = (byte[])(dr["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"]);
                    ContaArquivo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"]);
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ContaArquivo;

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

        public bool GetArquivo(ref Arquivo arquivo, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT a.GEDARQ_IND, 
	                                     a.GEDARQ_DISPONIVELEM, 
	                                     a.GEDARQ_DISPONIVELPOR, 
	                                     a.GEDARQ_ARQUIVO, 
	                                     a.GEDARQ_TAMANHO, 
	                                     a.GEDARQ_EXTENSAO, 
	                                     a.GEDARQ_DESCRICAO, 
	                                     case 
			                                 when a.GEDARQ_ACESSOEM is null
			                                 then ''
			                                 else a.GEDARQ_ACESSOEM
		                                 end as GEDARQ_ACESSOEM, 
	                                     case 
			                                 when a.GEDARQ_ACESSOPOR is null
			                                 then ''
			                                 else a.GEDARQ_ACESSOPOR
		                                 end as GEDARQ_ACESSOPOR, 
	                                     a.GEDARQ_CONTARQUIVOTIPO 
                                  FROM GedArquivo a
                                  where a.GEDARQ_IND = @GEDARQ_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ_IND", arquivo.GEDARQ_IND);

                dr = cmd.ExecuteReader();
                
                ContArqTipo ContaArquivo;
                while (dr.Read())
                {
                    ContaArquivo = new ContArqTipo();

                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_ARQUIVO = (byte[])(dr["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"]);
                    ContaArquivo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"]);
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ContaArquivo;
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

        public bool GetArquivoById(ref Arquivo arquivo, ref List<ArquivoTipo> arquivosTipos, ref List<PlanoContas> planos, 
            ref List<Conta> contas, ref TypesErrors erro)
        {
            bool retorno;
            try
            {
                string select = @"SELECT a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_ARQUIVO, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, a.GEDARQ_DESCRICAO, 
	                                       case when a.GEDARQ_ACESSOEM is null then '' else a.GEDARQ_ACESSOEM end as GEDARQ_ACESSOEM, 
	                                       case when a.GEDARQ_ACESSOPOR is null	then ''	else a.GEDARQ_ACESSOPOR	end as GEDARQ_ACESSOPOR, a.GEDARQ_CONTARQUIVOTIPO,  

	                                       b.CATIP_IND, b.CATIP_ARQUIVOTIPO, b.CATIP_PLANCONTCONTA, 

	                                       c.GEDTIPO_IND, c.GEDTIPO_DESCRICAO, c.GEDTIPO_EXPORTA, 

	                                       d.PCONT_IND, d.PCONT_CONTA, d.PCONT_PLANOCONTA, 

	                                       e.CONT_IND, e.CONT_DT_INICIO, e.CONT_DESCRICAO, e.CONT_ACESSO, e.CONT_LOGININSERT, 

	                                       f.PLAN_IND, f.PLAN_DT_INICIO , f.PLAN_DESCRICAO, f.PLAN_CODIGO, f.PLAN_FECHADO, f.PLAN_LOGININSERT 
                                    FROM GedArquivo a
                                    left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
                                    left join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
                                    left join PlanContConta d on b.CATIP_PLANCONTCONTA = d.PCONT_IND
                                    left join Conta e on d.PCONT_CONTA = e.CONT_IND 
                                    left join PlanoConta f on d.PCONT_PLANOCONTA = f.PLAN_IND
                                    where a.GEDARQ_IND = @GEDARQ_IND or 
	                                      (c.GEDTIPO_IND is not null and a.GEDARQ_IND is null)";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ_IND", arquivo.GEDARQ_IND);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();
                
                ArquivoTipo tipo;
                ContArqTipo ContaArquivo;
                while (dr.Read())
                {
                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_ARQUIVO = (byte[])(dr["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    arquivo.GEDARQ_ACESSOEM = Convert.ToDateTime(dr["GEDARQ_ACESSOEM"]);
                    arquivo.GEDARQ_ACESSOPOR = Convert.ToInt32(dr["GEDARQ_ACESSOPOR"]);

                    //b.CATIP_IND, b.CATIP_ARQUIVOTIPO, b.CATIP_PLANCONTCONTA, 
                    if (!(dr["CATIP_IND"] is DBNull))
                    {
                        ContaArquivo = new ContArqTipo();
                        ContaArquivo.CATIP_IND = Convert.ToInt32(dr["GEDARQ_CONTARQUIVOTIPO"]);
                        ContaArquivo.CATIP_ARQUIVOTIPO = new ArquivoTipo();
                        ContaArquivo.CATIP_PLANCONTCONTA = new PlanContConta();
                        //c.GEDTIPO_IND, c.GEDTIPO_DESCRICAO, c.GEDTIPO_EXPORTA, 
                        ContaArquivo.CATIP_ARQUIVOTIPO.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                        ContaArquivo.CATIP_ARQUIVOTIPO.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                        ContaArquivo.CATIP_ARQUIVOTIPO.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);
                        //d.PCONT_IND, d.PCONT_CONTA, d.PCONT_PLANOCONTA, 
                        ContaArquivo.CATIP_PLANCONTCONTA.conta = new Conta();
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas = new PlanoContas();
                        //e.CONT_IND, e.CONT_DT_INICIO, e.CONT_DESCRICAO, e.CONT_ACESSO, e.CONT_LOGININSERT, 
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"]);
                        //f.PLAN_IND, f.PLAN_DT_INICIO , f.PLAN_DESCRICAO, f.PLAN_CODIGO, f.PLAN_FECHADO, f.PLAN_LOGININSERT 
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                        arquivo.GEDARQ_CONTARQUIVOTIPO = ContaArquivo;
                        tipo = ContaArquivo.CATIP_ARQUIVOTIPO;

                        arquivosTipos.Add(tipo);
                    }
                    else 
                    {
                        ContaArquivo = new ContArqTipo();
                        ContaArquivo.CATIP_IND = 0;
                        ContaArquivo.CATIP_ARQUIVOTIPO = new ArquivoTipo();
                        ContaArquivo.CATIP_PLANCONTCONTA = new PlanContConta();
                    }
                    #endregion
                }

                dr.Close();

                select = @"select GEDTIPO_IND, GEDTIPO_DESCRICAO, GEDTIPO_EXPORTA 
                           from GedArquivoTipo";
                
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    tipo = new ArquivoTipo();
                    //c.GEDTIPO_IND, c.GEDTIPO_DESCRICAO, c.GEDTIPO_EXPORTA, 
                    tipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    tipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);
                    
                    arquivosTipos.Add(tipo);
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

        public bool GetArquivoById(ref Arquivo arquivo, ref List<PlanoContas> planosContas, ref List<Conta> contas, ref List<ArquivoTipo> arquivoTipos, 
                                      ref List<Campo> campos, ref List<CampoValor> valores, ref TypesErrors erro)
        {
            bool retorno;
            string select;
            try
            {
                select = @"select a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_ARQUIVO, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, a.GEDARQ_DESCRICAO, 
	                              case when b.CATIP_IND is null then -1 else b.CATIP_IND end as EDIT, ba.GEDTIPO_IND, bb.PCONT_IND, bba.PLAN_IND, bbb.CONT_IND 
                           from GedArquivo a 
                           left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND 
                           left join GedArquivoTipo ba on b.CATIP_ARQUIVOTIPO = ba.GEDTIPO_IND 
                           left join PlanContConta bb on b.CATIP_PLANCONTCONTA = bb.PCONT_IND 
                           left join PlanoConta bba on bb.PCONT_PLANOCONTA = bba.PLAN_IND 
                           left join Conta bbb on bb.PCONT_CONTA = bbb.CONT_IND 
                           where a.GEDARQ_IND = @GEDARQ_IND";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ_IND", arquivo.GEDARQ_IND);

                dr = cmd.ExecuteReader();

                ContArqTipo ContaArquivo;
                while (dr.Read())
                {
                    #region Parametrização
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_ARQUIVO = (byte[])(dr["GEDARQ_ARQUIVO"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);
                    
                    ContaArquivo = new ContArqTipo();
                    ContaArquivo.CATIP_IND = Convert.ToInt32(dr["EDIT"]);
                    if (ContaArquivo.CATIP_IND != -1)
                    {
                        ContaArquivo.CATIP_ARQUIVOTIPO = new ArquivoTipo();
                        ContaArquivo.CATIP_ARQUIVOTIPO.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);

                        ContaArquivo.CATIP_PLANCONTCONTA = new PlanContConta();
                        ContaArquivo.CATIP_PLANCONTCONTA.PCONT_IND = Convert.ToInt32(dr["PCONT_IND"]);

                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas = new PlanoContas();
                        ContaArquivo.CATIP_PLANCONTCONTA.planocontas.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                        ContaArquivo.CATIP_PLANCONTCONTA.conta = new Conta();
                        ContaArquivo.CATIP_PLANCONTCONTA.conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                    }
                    arquivo.GEDARQ_CONTARQUIVOTIPO = ContaArquivo;
                    #endregion
                }
                dr.Close();


                select = @"select PLAN_IND, PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT 
                           from PlanoConta
                           order by PLAN_FECHADO asc, PLAN_CODIGO desc, PLAN_IND desc";
                
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                PlanoContas plano;
                while (dr.Read())
                {
                    #region Parametrização
                    plano = new PlanoContas();
                    plano.PLAN_IND = Convert.ToInt32(dr["PLAN_IND"]);
                    plano.PLAN_CODIGO = Convert.ToString(dr["PLAN_CODIGO"]);
                    plano.PLAN_DESCRICAO = Convert.ToString(dr["PLAN_DESCRICAO"]);
                    plano.PLAN_FECHADO = Convert.ToBoolean(dr["PLAN_FECHADO"]);
                    plano.PLAN_DT_INICIO = Convert.ToDateTime(dr["PLAN_DT_INICIO"]);
                    plano.PLAN_LOGININSERT = Convert.ToInt32(dr["PLAN_LOGININSERT"]);
                    #endregion

                    planosContas.Add(plano);
                }
                dr.Close();

                select = @"select GEDTIPO_IND, GEDTIPO_DESCRICAO, GEDTIPO_EXPORTA, GEDTIPO_DT_INICIO, GEDTIPO_LOGININSERT 
                               from GedArquivoTipo";
                
                cmd = new SqlCommand(select, conn);

                dr = cmd.ExecuteReader();

                ArquivoTipo arquivoTipo;
                while (dr.Read())
                {
                    #region Parametrização
                    arquivoTipo = new ArquivoTipo();
                    arquivoTipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                    arquivoTipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                    arquivoTipo.GEDTIPO_EXPORTA = Convert.ToBoolean(dr["GEDTIPO_EXPORTA"]);
                    arquivoTipo.GEDTIPO_DT_INICIO = Convert.ToDateTime(dr["GEDTIPO_DT_INICIO"]);
                    arquivoTipo.GEDTIPO_LOGININSERT = Convert.ToInt32(dr["GEDTIPO_LOGININSERT"]);
                    #endregion

                    arquivoTipos.Add(arquivoTipo);
                }
                dr.Close();
                

                //SE O CONTAARQUIVOTIPO EXISTE, É PORQUE SE TRATA DE UM 'UPDATE', NÃO UM 'INSERT'
                if (arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_IND != -1)
                {
                    select = @"select a.PCONT_IND, b.PLAN_IND, d.CONTP_IND, d.CONTP_CLASSIFICADOR, d.CONTP_DESCRICAO, 
	                                  c.CONT_IND, c.CONT_DESCRICAO, c.CONT_ACESSO, c.CONT_CONTATIPO, c.CONT_DT_INICIO, c.CONT_LOGININSERT 
                               from PlanContConta a
                               inner join PlanoConta b on a.PCONT_PLANOCONTA = b.PLAN_IND
                               left join Conta c on a.PCONT_CONTA = c.CONT_IND 
                               inner join ContaTipo d on c.CONT_CONTATIPO = d.CONTP_IND 
                               where b.PLAN_IND = @PLAN_IND";
                    
                    cmd = new SqlCommand(select, conn);
                    cmd.Parameters.AddWithValue("@PLAN_IND", arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_PLANCONTCONTA.planocontas.PLAN_IND);

                    dr = cmd.ExecuteReader();

                    Conta conta;
                    ContaTipo contaTipo;
                    while (dr.Read())
                    {
                        #region Parametrização
                        conta = new Conta();
                        conta.CONT_IND = Convert.ToInt32(dr["CONT_IND"]);
                        conta.CONT_ACESSO = Convert.ToString(dr["CONT_ACESSO"]);
                        conta.CONT_DESCRICAO = Convert.ToString(dr["CONT_DESCRICAO"]);
                        conta.CONT_DT_INICIO = Convert.ToDateTime(dr["CONT_DT_INICIO"]);
                        conta.CONT_LOGININSERT = Convert.ToInt32(dr["CONT_LOGININSERT"]);

                        contaTipo = new ContaTipo();
                        contaTipo.CONTP_IND = Convert.ToInt32(dr["CONTP_IND"]);
                        contaTipo.CONTP_CLASSIFICADOR = Convert.ToString(dr["CONTP_CLASSIFICADOR"]);
                        contaTipo.CONTP_DESCRICAO = Convert.ToString(dr["CONTP_DESCRICAO"]);
                        conta.CONT_CONTASTIPOS = contaTipo;
                        #endregion

                        contas.Add(conta);
                    }
                    dr.Close();


                    select = @"select a.CAMP_IND, a.CAMP_DESCRICAO, a.CAMP_ARQUIVOTIPO, a.CAMP_DT_INICIO, a.CAMP_LOGININSERT, a.CAMP_OBRIGATORIO, 
	                                  b.CAPVAL_IND, b.CAPVAL_CAMPO, b.CAPVAL_CONTARQUIVOSTIPO, b.CAPVAL_VALOR, b.CAPVAL_ATUAL, b.CAPVAL_DATA, c.CAPTIP_IND, c.CAPTIP_DESCRICAO 
                               from GedCampo a 
                               left join GedCampoValor b on a.CAMP_IND = b.CAPVAL_CAMPO 
                               inner join GedCampoTipo c on a.CAMP_TIPO = c.CAPTIP_IND 
                               where a.CAMP_ARQUIVOTIPO = @GEDTIPO_IND and 
	                                (b.CAPVAL_CONTARQUIVOSTIPO = @CATIP_IND or 
	                                 b.CAPVAL_CONTARQUIVOSTIPO is null)";
                    
                    cmd = new SqlCommand(select, conn);
                    cmd.Parameters.AddWithValue("@GEDTIPO_IND", arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_IND);
                    cmd.Parameters.AddWithValue("@CATIP_IND", arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_IND);

                    dr = cmd.ExecuteReader();

                    Campo campo;
                    CampoTipo campoTipo;
                    CampoValor campoValor;
                    while (dr.Read())
                    {
                        #region Parametrização
                        campo = new Campo();
                        campo.CAMP_IND = Convert.ToInt32(dr["CAMP_IND"]);
                        campo.CAMP_DESCRICAO = Convert.ToString(dr["CAMP_DESCRICAO"]);
                        campo.CAMP_ARQUIVOTIPO = arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_ARQUIVOTIPO;
                        campo.CAMP_DT_INICIO = Convert.ToDateTime(dr["CAMP_DT_INICIO"]);
                        campo.CAMP_LOGININSERT = Convert.ToInt32(dr["CAMP_LOGININSERT"]);
                        campo.CAMP_OBRIGATORIO = Convert.ToBoolean(dr["CAMP_OBRIGATORIO"]);

                        campoTipo = new CampoTipo();
                        campoTipo.CAPTIP_IND = Convert.ToInt32(dr["CAPTIP_IND"]);
                        campoTipo.CAPTIP_DESCRICAO = Convert.ToString(dr["CAPTIP_DESCRICAO"]);
                        campo.CAMP_TIPO = campoTipo;

                        if (!(dr["CAPVAL_IND"] is DBNull))
                        {
                            campoValor = new CampoValor();
                            campoValor.CAPVAL_IND = Convert.ToInt32(dr["CAPVAL_IND"]);
                            campoValor.CAPVAL_CAMPO = Convert.ToInt32(dr["CAPVAL_CAMPO"]);
                            campoValor.CAPVAL_CONTARQUIVOSTIPO = arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_IND;
                            campoValor.CAPVAL_VALOR = Convert.ToString(dr["CAPVAL_VALOR"]);
                            campoValor.CAPVAL_ATUAL = Convert.ToBoolean(dr["CAPVAL_ATUAL"]);
                            campoValor.CAPVAL_DATA = Convert.ToDateTime(dr["CAPVAL_DATA"]);

                            valores.Add(campoValor);
                        }
                        #endregion

                        campos.Add(campo);
                    }
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

        public ArquivoTipoDetail GetArquivoTipoDetail(int indArquivo, int indTipo, ref TypesErrors erro)
        {
            ArquivoTipoDetail tipoDetail = new ArquivoTipoDetail();
            try
            {
                string select = @"select b.CATIP_IND, c.GEDTIPO_IND, c.GEDTIPO_DESCRICAO, 
	                                     d.CAMP_IND, d.CAMP_DESCRICAO, d.CAMP_DT_INICIO, d.CAMP_LOGININSERT, d.CAMP_OBRIGATORIO, 
	                                     e.CAPTIP_IND, e.CAPTIP_DESCRICAO, 
	                                     f.CAPVAL_IND, f.CAPVAL_DATA, f.CAPVAL_ATUAL, f.CAPVAL_VALOR 
                                  from GedArquivo a 
                                  left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND 
                                  inner join GedArquivoTipo c on b.CATIP_ARQUIVOTIPO = c.GEDTIPO_IND 
                                  left join GedCampo d on c.GEDTIPO_IND = d.CAMP_ARQUIVOTIPO 
                                  inner join GedCampoTipo e on d.CAMP_TIPO = e.CAPTIP_IND 
                                  left join GedCampoValor f on b.CATIP_IND = f.CAPVAL_CONTARQUIVOSTIPO and 
							                                   d.CAMP_IND = f.CAPVAL_CAMPO 
                                  where a.GEDARQ_IND = @GEDARQ_IND and 
                                        c.GEDTIPO_IND = @GEDTIPO_IND ";

                conn.Open();
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@GEDARQ_IND", indArquivo);
                cmd.Parameters.AddWithValue("@GEDTIPO_IND", indTipo);

                dr = cmd.ExecuteReader();
                //DataTable dataTable = er.GetSchemaTable();

                tipoDetail.campos = new List<Campo>();
                tipoDetail.Valores = new List<CampoValor>();
                Campo campo;
                CampoValor valor;
                ArquivoTipo tipo;
                while (dr.Read())
                {
                    #region Parametrização
                    if (tipoDetail.CATIP_IND == null)
                    {
                        tipoDetail.CATIP_IND = new ContArquivoTipo();
                        tipoDetail.CATIP_IND.CATIP_IND = Convert.ToInt32(dr["CATIP_IND"]);

                        tipo = new ArquivoTipo();
                        tipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                        tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);

                        tipoDetail.CATIP_IND.CATIP_ARQUIVOTIPO = tipo;
                    }

                    if (!(dr["CAMP_IND"] is DBNull))
                    {
                        campo = new Campo();
                        campo.CAMP_IND = Convert.ToInt32(dr["CAMP_IND"]);
                        campo.CAMP_DESCRICAO = Convert.ToString(dr["CAMP_DESCRICAO"]);

                        tipo = new ArquivoTipo();
                        tipo.GEDTIPO_IND = Convert.ToInt32(dr["GEDTIPO_IND"]);
                        tipo.GEDTIPO_DESCRICAO = Convert.ToString(dr["GEDTIPO_DESCRICAO"]);
                        campo.CAMP_ARQUIVOTIPO = tipo;

                        campo.CAMP_TIPO = new CampoTipo();
                        campo.CAMP_TIPO.CAPTIP_IND = Convert.ToInt32(dr["CAPTIP_IND"]);
                        campo.CAMP_TIPO.CAPTIP_DESCRICAO = Convert.ToString(dr["CAPTIP_DESCRICAO"]);

                        campo.CAMP_DT_INICIO = Convert.ToDateTime(dr["CAMP_DT_INICIO"]);
                        campo.CAMP_LOGININSERT = Convert.ToInt32(dr["CAMP_LOGININSERT"]);
                        campo.CAMP_OBRIGATORIO = Convert.ToBoolean(dr["CAMP_OBRIGATORIO"]);

                        tipoDetail.campos.Add(campo);
                    }

                    if (!(dr["CAPVAL_IND"] is DBNull))
                    {
                        valor = new CampoValor();
                        valor.CAPVAL_IND = Convert.ToInt32(dr["CAPVAL_IND"]);
                        valor.CAPVAL_VALOR = Convert.ToString(dr["CAPVAL_VALOR"]);
                        valor.CAPVAL_DATA = Convert.ToDateTime(dr["CAPVAL_DATA"]);
                        valor.CAPVAL_CONTARQUIVOSTIPO = tipoDetail.CATIP_IND.CATIP_IND;
                        valor.CAPVAL_CAMPO = Convert.ToInt32(dr["CAMP_IND"]);
                        valor.CAPVAL_ATUAL = Convert.ToBoolean(dr["CAPVAL_ATUAL"]);

                        tipoDetail.Valores.Add(valor);
                    }
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
            if (tipoDetail.CATIP_IND != null &&
                tipoDetail.CATIP_IND.CATIP_IND != 0)
                return tipoDetail;
            else
                return new ArquivoTipoDetail();
        }

        public bool GetArquivosNaoClassificados(ref List<Arquivo> arquivos, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"select a.GEDARQ_IND, 
	                              a.GEDARQ_DISPONIVELEM, 
	                              a.GEDARQ_DISPONIVELPOR, 
	                              a.GEDARQ_TAMANHO, 
	                              a.GEDARQ_EXTENSAO, 
	                              a.GEDARQ_DESCRICAO, 
	                              c.GLOTE_IND, 
	                              c.GLOTE_DESCRICAO, 
	                              c.GLOTE_DISPONIVELEM, 
	                              c.GLOTE_DISPONIVELPOR, 
	                              c.GLOTE_ENVIADO, 
	                              c.GLOTE_FECHADOEM, 
	                              c.GLOTE_FECHADOPOR  
                           from GedArquivo a
                           left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
                           inner join GedLote c on a.GEDARQ_LOTE = c.GLOTE_IND 
                           where b.CATIP_IND is null ";
                cmd = new SqlCommand(select, conn);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                Lote lote;
                while (dr.Read())
                {
                    #region Parametrização
                    arquivo = new Arquivo();
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);

                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);

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

        public bool GetArquivosNaoClassificados(ref List<Arquivo> arquivos, ref Pagination pagination, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"declare @qtde int = @p_qtde, 
                                    @pag int = @p_pag
                            select GEDARQ_IND, GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GLOTE_IND, 
	                                GLOTE_DESCRICAO, GLOTE_DISPONIVELEM, GLOTE_DISPONIVELPOR, GLOTE_ENVIADO, GLOTE_FECHADOEM, GLOTE_FECHADOPOR, N_ARQUIVOS 
                            from (
	                            SELECT ROW_NUMBER() OVER(ORDER BY a.GEDARQ_IND) AS NUMBER, 
		                                a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, a.GEDARQ_DESCRICAO, 
		                                c.GLOTE_IND, c.GLOTE_DESCRICAO, c.GLOTE_DISPONIVELEM, c.GLOTE_DISPONIVELPOR, c.GLOTE_ENVIADO, c.GLOTE_FECHADOEM, c.GLOTE_FECHADOPOR, 
		                                count(*) as N_ARQUIVOS 
	                            from GedArquivo a
	                            left join ContArqTipo b on a.GEDARQ_CONTARQUIVOTIPO = b.CATIP_IND
	                            inner join GedLote c on a.GEDARQ_LOTE = c.GLOTE_IND 
	                            where b.CATIP_IND is null 
	                            group by a.GEDARQ_IND, a.GEDARQ_DISPONIVELEM, a.GEDARQ_DISPONIVELPOR, a.GEDARQ_TAMANHO, a.GEDARQ_EXTENSAO, a.GEDARQ_DESCRICAO, 
	                            c.GLOTE_IND, c.GLOTE_DESCRICAO, c.GLOTE_DISPONIVELEM, c.GLOTE_DISPONIVELPOR, c.GLOTE_ENVIADO, c.GLOTE_FECHADOEM, c.GLOTE_FECHADOPOR) as TBL
                            where NUMBER BETWEEN ((@pag - 1) * @qtde + 1) AND (@pag * @qtde) 
                            order by GEDARQ_IND desc";
                cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@p_qtde", pagination.itens[pagination.item]);
                cmd.Parameters.AddWithValue("@p_pag", pagination.page);

                if (dr != null && !dr.IsClosed)
                    dr.Close();

                dr = cmd.ExecuteReader();

                Arquivo arquivo;
                Lote lote;
                bool passo = false;
                while (dr.Read())
                {
                    #region Parametrização
                    arquivo = new Arquivo();
                    arquivo.GEDARQ_IND = Convert.ToInt32(dr["GEDARQ_IND"]);
                    arquivo.GEDARQ_DISPONIVELEM = Convert.ToDateTime(dr["GEDARQ_DISPONIVELEM"]);
                    arquivo.GEDARQ_DISPONIVELPOR = Convert.ToInt32(dr["GEDARQ_DISPONIVELPOR"]);
                    arquivo.GEDARQ_TAMANHO = Convert.ToInt32(dr["GEDARQ_TAMANHO"]);
                    arquivo.GEDARQ_EXTENSAO = Convert.ToString(dr["GEDARQ_EXTENSAO"]);
                    arquivo.GEDARQ_DESCRICAO = Convert.ToString(dr["GEDARQ_DESCRICAO"]);

                    lote = new Lote();
                    lote.GLOTE_IND = Convert.ToInt32(dr["GLOTE_IND"]);
                    lote.GLOTE_DESCRICAO = Convert.ToString(dr["GLOTE_DESCRICAO"]);
                    lote.GLOTE_DISPONIVELEM = Convert.ToDateTime(dr["GLOTE_DISPONIVELEM"]);
                    lote.GLOTE_DISPONIVELPOR = Convert.ToInt32(dr["GLOTE_DISPONIVELPOR"]);
                    lote.GLOTE_ENVIADO = Convert.ToBoolean(dr["GLOTE_ENVIADO"]);

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

        public bool SetCampoValues(int arquivo, int plano, int conta, int tipo, List<CampoValor> camposValores, DateTime data, int login, ref TypesErrors erro)
        {
            try
            {
                string query = @"declare @GEDARQ_IND int = @arquivo, 
		                                 @PLAN_IND int = @plano, 
		                                 @CONT_INT int = @conta, 
		                                 @GEDTIPO_IND int = @tipo, 
		                                 @DT_INICIO datetime = @data,  
		                                 @LOGININSERT int = @login, 
		                                 @CATIP_IND int, 
		                                 @PCONT_IND int  

                                 select @CATIP_IND = CATIP_IND
                                 from ContArqTipo a
                                 inner join GedArquivo b on a.CATIP_IND = b.GEDARQ_CONTARQUIVOTIPO 
                                 where b.GEDARQ_IND = @GEDARQ_IND
 
                                 if (@CATIP_IND is null)
                                 begin 
	                                 select @PCONT_IND = PCONT_IND 
	                                 from PlanContConta 
	                                 where PCONT_CONTA = @CONT_INT and PCONT_PLANOCONTA = @PLAN_IND 

 	                                 insert into ContArqTipo (CATIP_ARQUIVOTIPO, CATIP_PLANCONTCONTA, CATIP_DT_INICIO, CATIP_LOGININSERT) 
					                                  values (@GEDTIPO_IND, @PCONT_IND, @DT_INICIO, @LOGININSERT)
	                                 set @CATIP_IND = (SELECT IDENT_CURRENT('ContArqTipo'))

	                                 update GedArquivo 
	                                 set GEDARQ_CONTARQUIVOTIPO = @CATIP_IND
	                                 where GEDARQ_IND = @GEDARQ_IND

 	                                 %LISTA%
                                 end
                                 else
                                 begin
	                                 %DELETEANDLISTAS%
                                 end";

                string queryCampos = "";
                int count = camposValores.Count;

                int c = 0;
                foreach (CampoValor campoValor in camposValores)
                {
                    queryCampos = queryCampos +
                    @"insert into GedCampoValor (CAPVAL_DATA, CAPVAL_VALOR, CAPVAL_CAMPO, CAPVAL_CONTARQUIVOSTIPO, CAPVAL_ATUAL)
					                values (@DT_INICIO, @CAPVAL_VALOR" + c + ", @CAPVAL_CAMPO" + c + ", @CATIP_IND, 0) ";
                    c++;
                }

                query = query.Replace("%LISTA%", queryCampos);
                queryCampos = "";

                c = 0;
                foreach (CampoValor campoValor in camposValores)
                {
                    queryCampos = queryCampos +
                    @"delete from GedCampoValor 
	                  where CAPVAL_CONTARQUIVOSTIPO = @CATIP_IND and 
                            CAPVAL_CAMPO = @CAPVAL_CAMPO" + c + @"


                      insert into GedCampoValor (CAPVAL_DATA, CAPVAL_VALOR, CAPVAL_CAMPO, CAPVAL_CONTARQUIVOSTIPO, CAPVAL_ATUAL)
				                    values (@DT_INICIO, @CAPVAL_VALOR" + c + ", @CAPVAL_CAMPO" + c + ", @CATIP_IND, 0) ";
                    c++;
                }

                query = query.Replace("%DELETEANDLISTAS%", queryCampos);

                conn.Open();
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@arquivo", arquivo);
                cmd.Parameters.AddWithValue("@plano", plano);
                cmd.Parameters.AddWithValue("@conta", conta);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@login", login);
                c = 0;
                foreach (CampoValor campoValor in camposValores)
                {
                    cmd.Parameters.AddWithValue("@CAPVAL_VALOR" + c.ToString(), campoValor.CAPVAL_VALOR);
                    cmd.Parameters.AddWithValue("@CAPVAL_CAMPO" + c.ToString(), campoValor.CAPVAL_CAMPO);
                    c++;
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
