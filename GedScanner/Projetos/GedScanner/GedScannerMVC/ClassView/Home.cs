using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class Home
    {
        public bool ErroEmail = false;
        public bool ErroSenha = false;
        public bool ErroLogin = false;
        public bool ErroBanco = false;
        public bool panelLogin = false;
        public bool panelSelCliente = false;
        public bool panelInativo = false;

        public string btnValue;

        public List<Cliente> clientes;

        public void SessionOff()
        {
            panelLogin = true;
            btnValue = "Entrar";
        }
    }
}