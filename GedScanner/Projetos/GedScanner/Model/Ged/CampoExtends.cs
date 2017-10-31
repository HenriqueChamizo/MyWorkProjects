using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Ged
{
    public class CampoDetail
    {
        //CAMP_IND INT IDENTITY,
        //CAMP_DESCRICAO VARCHAR(100) NOT NULL,
        //CAMP_ARQUIVOTIPO INT NOT NULL,
        //CAMP_TIPO INT NOT NULL,
        //CAMP_DT_INICIO DATETIME NOT NULL, 
        //CAMP_LOGININSERT INT NOT NULL,
        public int CAMP_IND;
        public string CAMP_DESCRICAO;
        public ArquivoTipo CAMP_ARQUIVOTIPO;
        public CampoTipo CAMP_TIPO;
        public DateTime CAMP_DT_INICIO;
        public int CAMP_LOGININSERT;
        public bool CAMP_OBRIGATORIO;

        public List<CampoTipo> tipos;
    }

    public class Campo
    {
        public int CAMP_IND;
        public string CAMP_DESCRICAO;
        public ArquivoTipo CAMP_ARQUIVOTIPO;
        public CampoTipo CAMP_TIPO;
        public DateTime CAMP_DT_INICIO;
        public int CAMP_LOGININSERT;
        public bool CAMP_OBRIGATORIO;
    }
}
