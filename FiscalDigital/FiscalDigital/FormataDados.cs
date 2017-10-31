using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalDigital
{
    class FormataDados
    {
        public string FormataDado(int inteiro)
        {
            if (inteiro == 1)
                return "01";
            else if (inteiro == 2)
                return "02";
            else if (inteiro == 3)
                return "03";
            else if (inteiro == 4)
                return "04";
            else if (inteiro == 5)
                return "05";
            else if (inteiro == 6)
                return "06";
            else if (inteiro == 7)
                return "07";
            else if (inteiro == 8)
                return "08";
            else if (inteiro == 9)
                return "09";
            else
                return inteiro.ToString();
        }
    }
}
