using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace AsseFincSharp.Model.NFe
{
    public class impostoModel
    {
        public IcmsModel Icms { get; set; }
        public PISModel Pis { get; set; }
        public COFINSModel Cofins { get; set; }
    }
}
