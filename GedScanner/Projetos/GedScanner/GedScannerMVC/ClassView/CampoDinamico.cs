using Model.Ged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class CampoDinamico
    {
        public string NameDescricao;
        public string IdDescricao;
        public string ClassDescricao;
        public string PlaceHolderDescricao;
        public string ValueDescricao;

        public string NameObrigacao;
        public string IdObrigacao;
        public string ClassObrigacao;
        public string CheckedObrigacao;

        public string NameTipo;
        public string IdTipo;
        public string ClassTipo;
        public int ValueOptionTipo;
        public List<Model.Ged.CampoTipo> tipos;
    }
}