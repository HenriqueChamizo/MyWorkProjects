using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Ged
{
    public class ArquivoTipoDetail
    {
        public ContArquivoTipo CATIP_IND;
        public List<Campo> campos;
        public List<CampoValor> Valores;
    }

    public class ArquivoTipo
    {
        //   GEDTIPO_IND INT IDENTITY,
        //   GEDTIPO_DESCRICAO VARCHAR(100) NOT NULL,
        //   GEDTIPO_EXPORTA BIT NOT NULL,
        //   GEDTIPO_DT_INICIO DATETIME NOT NULL,
        //   GEDTIPO_LOGININSERT INT NOT NULL,
        public int GEDTIPO_IND;
        public string GEDTIPO_DESCRICAO;
        public bool GEDTIPO_EXPORTA;
        public DateTime GEDTIPO_DT_INICIO;
        public int GEDTIPO_LOGININSERT;
        public ArquivoTipo GEDTIPO_PAI;
        public List<ContArqTipo> cArqTipos;
    }
}
