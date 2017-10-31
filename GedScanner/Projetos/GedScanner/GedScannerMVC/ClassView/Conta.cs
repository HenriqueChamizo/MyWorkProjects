using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GedScannerMVC.ClassView
{
    public class Conta
    {
        public string indice;
        public string acesso;
        public string descricao;
        public string datainicio;
        public bool condition;
        public Model.ContaTipo tipo;
        public SelectListItem[] ddlTipos;

        public HeaderConta header;
        //public TableOfWeb table;
        public TableModel tableModel;
        public bool edit;
        public string Erro;
    }
}