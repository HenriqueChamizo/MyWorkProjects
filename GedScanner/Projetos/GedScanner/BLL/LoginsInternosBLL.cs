using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Enuns;
using DAO;

namespace BLL
{
    public class LoginsInternosBLL : ILoginsInternos
    {
        Connection conn;
        LoginsInternosDAO DAO;
        public LoginsInternosBLL(Connection connect)
        {
            conn = connect;
            DAO = new LoginsInternosDAO();
        }

        public bool GetLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetLoginInterno(ref login, ref erro);
        }

        public bool GetExternLoginInterno(ref LoginsInternos login, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetExternLoginInterno(ref login, ref erro);
        }
    }
}
