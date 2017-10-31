using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using AsseFincSharp.Model.NFe;
namespace AsseFincSharp.Model
{
    public class dadosNFeImportada
    {
        public int idGrupo  { get; set; }
        public int idFornecedor { get; set; }
        public IdeModel Ide { get; set; }
        public EmitModel Emit { get; set; }
        public DestModel Dest { get; set; }
        public List<detModel> Itens { get; set; }
        public ICMSTotModel Total { get; set; }
        public List<Duplicata> Cobranca { get; set; }
        public TranspModel Transp { get; set; }
        public InfAdicModel InfAdic { get; set; }
        public InfProt Prot { get; set; }


        public string nrPedidoCompra { get; set; }

        public string xmlImportado { get; set; }

        public string nomeArquivo { get; set; }
    }

    
}
