using DAO.Interfaces;
using Model;
using Model.Enuns;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAO
{
    public class DashboardDAO : IDashboard, IDAO
    {
        SqlConnection conn;
        SqlCommand cmd;
        DbDataReader dr;

        public void SetValuesConnection(Connection connection)
        {
            conn = new SqlConnection(connection.connectionstring);
        }

        public bool GetDashboard(ref Dashboard dashboard, ref TypesErrors erro)
        {
            string select;
            bool retorno = false;
            try
            {
                conn.Open();

                select = @"select COUNT(a.GEDARQ_IND) as 'COUNT'
                            from GedArquivo a
                            left join GedDocumento b on a.GEDARQ_DOCUMENTO = b.GEDDOC_IND 
                            left join ContArqTipo c on b.GEDDOC_CONTARQUIVOTIPO = c.CATIP_IND
                            where c.CATIP_IND is null ";
                cmd = new SqlCommand(select, conn);
                dashboard.arquivos = Convert.ToInt32(cmd.ExecuteScalar());

                select = @"select COUNT(GLOTE_IND) as 'COUNT'
                           from GedLote
                           where GLOTE_FECHADOEM is null ";
                cmd = new SqlCommand(select, conn);
                dashboard.lotesFechados = Convert.ToInt32(cmd.ExecuteScalar());

                select = @"select COUNT(GLOTE_IND) as 'COUNT'
                           from GedLote
                           where GLOTE_ENVIADO = 0 ";
                cmd = new SqlCommand(select, conn);
                dashboard.lotesNEnviados = Convert.ToInt32(cmd.ExecuteScalar());

                select = @"select COUNT(GEDTIPO_IND) as 'COUNT'
                           from GedArquivoTipo
                           where GEDTIPO_EXPORTA = 1";
                cmd = new SqlCommand(select, conn);
                dashboard.tiposArquivosEx = Convert.ToInt32(cmd.ExecuteScalar());

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
                conn.Close();
            }
            return retorno;
        }
    }
}
