using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Model.Enuns;
using Model;

namespace DAO.Interfaces
{
    public interface ICliente
    {
        bool GetClientes(ref List<Cliente> clientes, ref TypesErrors erro);
        bool GetCliente(ref Cliente cliente, ref TypesErrors erro);
        bool GetClienteDataBase(Cliente cliente, ref ClienteDatabase database, ref TypesErrors erro);
    }
}
