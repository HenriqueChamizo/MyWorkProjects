using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class Listas
    {
        public int campo { get; set; }
        public List<Campo> campos { get; set; }
        public int valor { get; set; }
        public List<CampoValor> valores { get; set; }
        public int conta { get; set; }
        public List<Model.Conta> contas { get; set; }
        public bool disabled { get; set; }
    }
}