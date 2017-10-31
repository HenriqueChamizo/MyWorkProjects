using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class PlanoContas
    {
        public string indice;
        public string codigo;
        public string descricao;
        public string datainicio;
        public string finalizar;

        public bool ckbFinalizarVisible;
        public string ckbFinalizarText;
        public bool ckbFinalizarEnable;

        public bool ckbsContasVisibles;
        public bool tableFinalizadasVisible;

        public string Erro = "";

        public List<Model.Conta> contas;
        public TableOfWeb tableFinalizadas;
        //public BoxSolid boxFinalizadas;
        public TableModel tableModel;
    }
}