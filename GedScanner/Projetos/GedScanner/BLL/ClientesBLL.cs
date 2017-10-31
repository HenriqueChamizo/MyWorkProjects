using DAO;
using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Model.Enuns;
using Model;

namespace BLL
{
    public class ClienteBLL : ICliente
    {
        Connection conn;
        ClienteDAO DAO;
        public ClienteBLL(Connection connect)
        {
            conn = connect;
            DAO = new ClienteDAO();
        }

        public bool GetClientes(ref List<Cliente> clientes, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetClientes(ref clientes, ref erro);
        }

        public bool GetCliente(ref Cliente cliente, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetCliente(ref cliente, ref erro);
        }

        public bool GetClienteDataBase(Cliente cliente, ref ClienteDatabase database, ref TypesErrors erro)
        {
            DAO.SetValuesConnection(conn);
            return DAO.GetClienteDataBase(cliente, ref database, ref erro);
        }
    }
}
