using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalDigital
{
    public class DataGridView
    {
        private int ind;
        private string numDocSaida;
        private string cfop;
        private string valorSai;
        private string valorItem;

        public int Indice
        {
            get { return ind; }
            set { ind = value; }
        }
        public string Cfop
        {
            get { return cfop; }
            set { cfop = value; }
        }
        public string Numero
        {
            get { return numDocSaida; }
            set { numDocSaida = value; }
        }
        public string Saida
        {
            get { return valorSai; }
            set { valorSai = value; }
        }
        public string Item
        {
            get { return valorItem; }
            set { valorItem = value; }
        }
    }
}
