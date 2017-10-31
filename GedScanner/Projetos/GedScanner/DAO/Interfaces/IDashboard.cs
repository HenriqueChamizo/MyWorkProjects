using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface IDashboard
    {
        bool GetDashboard(ref Dashboard dashboard, ref TypesErrors erro);
    }
}
