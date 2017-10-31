using DAO;
using DAO.Interfaces;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DashboardBLL : IDashboard
    {
        Connection conn;
        DashboardDAO DAO;
        public DashboardBLL(Connection connect)
        {
            conn = connect;
            DAO = new DashboardDAO();
        }

        public bool GetDashboard(ref Dashboard dashboard, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetDashboard(ref dashboard, ref erro);
        }
    }
}
