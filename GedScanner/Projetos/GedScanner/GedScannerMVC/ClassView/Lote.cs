using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class Lote
    {
        public TypesPages typePage;
        public LoteIndex index;
        public LoteDetail detail;
        public LoteArquivo loteArquivo;

        public string Erro = "";
    }

    public class LoteIndex
    {
        public string indice;
        public string descricao;
        public string datainicio;
        //public string datafinal;
        public bool finalizar;
        //public bool enviar;

        public bool ckbFinalizarVisible;
        //public string ckbFinalizarText;
        public bool ckbFinalizarEnable;

        //public bool ckbEnviarVisible;
        //public string ckbEnviarText;
        //public bool ckbEnviarEnable;

        public bool ckbsLotesVisibles;
        public bool tableFinalizadosVisible;
        public bool edit;
        //public bool disabledAll;

        //public TableOfWeb tableAbertos;
        //public TableOfWeb tableFechados;
        public TableModel tableAbertos;
        public TableModel tableFechados;
    }

    public class LoteDetail
    {
        public string indice;
        public string descricao;
        public string datainicio;
        //public string datafinal;
        public bool finalizar;
        public bool enviar;

        public bool ckbFinalizarVisible;
        //public string ckbFinalizarText;
        public bool ckbFinalizarEnable;

        public bool ckbEnviarVisible;
        //public string ckbEnviarText;
        public bool ckbEnviarEnable;

        public bool ckbsLotesVisibles;
        public bool tableFinalizadosVisible;
        public bool edit;
        public bool disabledAll;
    }

    public class LoteArquivo
    {
        public TableModel tableClass;
        public TableModel tableNClass;
        //public TableOfWeb tableAbertos;
        //public TableOfWeb tableFechados;
    }
}