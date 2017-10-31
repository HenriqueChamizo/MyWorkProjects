using DAO;
using DAO.Ged;
using DAO.Interfaces;
using Model.Enuns;
using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Ged
{
    public class ArquivoTipoBLL : IArquivoTipo
    {
        Connection conn;
        ArquivoTipoDAO DAO;
        public ArquivoTipoBLL(Connection connect)
        {
            conn = connect;
            DAO = new ArquivoTipoDAO();
        }

        public List<ArquivoTipo> GetArquivosTiposFromConta(int indCont, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosTiposFromConta(indCont, ref erro);
        }

        public List<ArquivoTipo> GetArquivosTiposFromContaAndPlano(int indConta, int indPlano, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosTiposFromContaAndPlano(indConta, indPlano, ref erro);
        }

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosTipos(ref arquivosTipos, ref erro);
        }

        public bool GetArquivosTipos(ref List<ArquivoTipo> arquivosTipos, ref Model.Pagination pagination, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivosTipos(ref arquivosTipos, ref pagination, ref erro);
        }

        public ArquivoTipo GetArquivoTipo(int index, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetArquivoTipo(index, ref erro);
        }

        public bool SetArquivoTipoAndCamposInsert(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.SetArquivoTipoAndCamposInsert(arquivoTipo, campos, ref erro);
        }

        public bool SetArquivoTipoAndCamposEdit(ArquivoTipo arquivoTipo, List<Campo> campos, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.SetArquivoTipoAndCamposEdit(arquivoTipo, campos, ref erro);
        }

        public int GetTipoArquivoByArquivo(int indArquivo, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetTipoArquivoByArquivo(indArquivo, ref erro);
        }
    }
}
