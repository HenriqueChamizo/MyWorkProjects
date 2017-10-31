using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassFrm
{
    public class FrmLoteIndex
    {
        public string indice { get; set; }
        public string descricao { get; set; }
        public string datainicio { get; set; }
        public string datafinal { get; set; }
        public bool finalizar { get; set; }
        public bool enviar { get; set; }
        public string edit { get; set; }
    }
}