using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class Arquivo
    {
        public string GEDARQ_DESCRICAO;
        public string resourceImg;
        public string exception;
        public string hiddenIndices;

        public List<Model.Ged.ArquivoTipo> tipos;
        public List<Model.PlanoContas> planoscontas;
        public Model.Ged.Arquivo arquivo;
        public List<Model.Conta> contas;

        //List<Model.Ged.ArquivoTipo> arquivosTipos = new List<Model.Ged.ArquivoTipo>();
        //List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
        //List<Model.Ged.CampoValor> valores = new List<Model.Ged.CampoValor>();
    }
}