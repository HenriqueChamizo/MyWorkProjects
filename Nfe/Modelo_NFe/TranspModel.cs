﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////using System.Threading.Tasks;

namespace AsseFincSharp.Model.NFe
{
    public class TranspModel
    {
        public TranspModel()
        {
            transportadora = new transportadoraModel();
            vol = new volModel();
        }
        public int modFrete { get; set; }
        public transportadoraModel transportadora { get; set; }
        public volModel vol { get; set; }
    }
}