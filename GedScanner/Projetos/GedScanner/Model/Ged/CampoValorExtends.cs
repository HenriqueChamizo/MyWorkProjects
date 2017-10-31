using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Ged
{
    public class CampoValorDetail
    {
        //   CAPVAL_IND INT IDENTITY,
        //   CAPVAL_CAMPO INT NOT NULL,
        //   CAPVAL_CONTARQUIVOSTIPO INT NOT NULL,
        //   CAPVAL_VALOR VARCHAR(MAX) NOT NULL,
        //   CAPVAL_ATUAL BIT NOT NULL,
        //   CAPVAL_DATA DATETIME NOT NULL,
        public int CAPVAL_IND;
        public Campo CAPVAL_CAMPO;
        public ContArquivoTipo CAPVAL_CONTARQUIVOSTIPO;
        public string CAPVAL_VALOR;
        public bool CAPVAL_ATUAL;
        public DateTime CAPVAL_DATA;
    }

    public class CampoValor
    {
        //   CAPVAL_IND INT IDENTITY,
        //   CAPVAL_CAMPO INT NOT NULL,
        //   CAPVAL_CONTARQUIVOSTIPO INT NOT NULL,
        //   CAPVAL_VALOR VARCHAR(MAX) NOT NULL,
        //   CAPVAL_ATUAL BIT NOT NULL,
        //   CAPVAL_DATA DATETIME NOT NULL,
        public int CAPVAL_IND;
        public int CAPVAL_CAMPO;
        public int CAPVAL_CONTARQUIVOSTIPO;
        public string CAPVAL_VALOR;
        public bool CAPVAL_ATUAL;
        public DateTime CAPVAL_DATA;
    }
}
