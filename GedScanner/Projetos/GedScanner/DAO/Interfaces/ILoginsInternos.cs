using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Interfaces
{
    public interface ILoginsInternos
    {
        bool GetLoginInterno(ref LoginsInternos login, ref TypesErrors erro);
    }
}
