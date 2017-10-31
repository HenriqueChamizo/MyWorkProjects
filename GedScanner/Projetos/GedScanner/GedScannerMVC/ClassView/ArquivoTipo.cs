﻿using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class ArquivoTipo
    {
        public string descricao;
        public bool exporta;
        public string mensagem;

        public CampoDinamico campo;
        public List<CampoDinamico> campos;

        //public TableOfWeb table;
        public TableModel tableModel;

        public List<ArquivoTipo> filhos;
    }
}