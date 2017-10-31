using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Ged
{
    public class Lote
    {
        //GLOTE_IND INT IDENTITY,
        //GLOTE_DESCRICAO VARCHAR(100) NOT NULL, 
        //GLOTE_DISPONIVELEM SMALLDATETIME NOT NULL, 
        //GLOTE_DISPONIVELPOR INT NOT NULL,
        //GLOTE_FECHADOEM SMALLDATETIME NULL, 
        //GLOTE_FECHADOPOR INT NULL,
        //GLOTE_ENVIADO BIT NOT NULL, 

        public int GLOTE_IND;
        public string GLOTE_DESCRICAO;
        public DateTime GLOTE_DISPONIVELEM;
        public int GLOTE_DISPONIVELPOR;
        public DateTime GLOTE_FECHADOEM;
        public int GLOTE_FECHADOPOR;
        public bool GLOTE_ENVIADO;

        public int N_ARQUIVOS;
        public int N_ARQUIVOS_NCLASSIC;
    }
}
