using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalDigital
{
    public class Finalidade
    {
        private int ind;
        private string codigo;
        private string descricao;

        public int Ind
        {
            get { return ind; }
            set { ind = value; }
        }
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
    }

    public class AddFinalidade
    {
        public List<Finalidade> finalidades()
        {
            List<Finalidade> fins = new List<Finalidade>();

            fins.Add(new Finalidade() { Ind = 1, Codigo = "01", Descricao = "Remessa regular de arquivo" });
            fins.Add(new Finalidade() { Ind = 2, Codigo = "02", Descricao = "Remessa de arquivo requirido por intimação especifica" });
            fins.Add(new Finalidade() { Ind = 3, Codigo = "03", Descricao = "Remessa de arquivo para substituição de arquivo remetido anteriormente" });
            fins.Add(new Finalidade() { Ind = 4, Codigo = "04", Descricao = "Remessa de arquivo com informações complementares" });

            return fins;
        }
    }
}
