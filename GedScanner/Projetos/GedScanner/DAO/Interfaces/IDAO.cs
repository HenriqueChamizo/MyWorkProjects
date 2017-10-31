using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DAO.Interfaces
{
    public interface IDAO
    {
        void SetValuesConnection(Connection connect);
    }
}
