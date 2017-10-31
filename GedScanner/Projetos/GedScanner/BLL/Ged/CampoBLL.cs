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
    public class CampoBLL : ICampo
    {
        Connection conn;
        CampoDAO DAO;
        public CampoBLL(Connection connect)
        {
            conn = connect;
            DAO = new CampoDAO();
        }

        public List<CampoTipo> GetCampoTipos(ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetCampoTipos(ref erro);
        }

        public List<CampoDetail> GetCamposByArquivoTipo(int ind, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetCamposByArquivoTipo(ind, ref erro);
        }

        public List<CampoValorDetail> GetCamposValoresByArquivo(int arquivo, int arquivoTipo, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetCamposValoresByArquivo(arquivo, arquivoTipo, ref erro);
        }
    }
}
