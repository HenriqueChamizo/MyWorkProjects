using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalDigital
{
    public class Participante
    {
        public int ind;
        private string codPart;
        private string nome;
        private string codPais;
        private string cnpjCpf;
        private string ie;
        private string uf;
        private string cep;
        private string endereco;
        private string numero;
        private string complemento;
        private string bairro;
        private string codMunicipal;
        public string erroUF = "0";
        public string erroCEP = "0";
        public string erroRUA = "0";
        public string erroNUM = "0";

        public string CodPart
        {
            get { return codPart; }
            set { codPart = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public string CnpjCpf
        {
            get { return cnpjCpf; }
            set { cnpjCpf = value; }
        }
        public string Ie
        {
            get { return ie; }
            set { ie = value; }
        }
        public string Uf
        {
            get { return uf; }
            set { uf = value; }
        }
        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }
        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }
        public string CodMunicipal
        {
            get { return codMunicipal; }
            set { codMunicipal = value; }
        }
    }

    public class EnderecoPart
    {
        public int ind;
        private int indPart;
        private string cnpj;
        private string uf;
        private string cep;
        private string rua;

        public int IndPart
        {
            get { return indPart; }
            set { indPart = value; }
        }
        public string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }
        public string Uf
        {
            get { return uf; }
            set { uf = value; }
        }
        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        public string Rua
        {
            get { return rua; }
            set { rua = value; }
        }
    }

    public class Participantes
    {
        EnderecoPart endereco = new EnderecoPart();
        List<EnderecoPart> enderecos = new List<EnderecoPart>();

        public List<EnderecoPart> GetValueParticipante(String[] ArqTxt, List<Participante> participantes)
        {
            int indice = 0;
            string CNPJ;
            string UF;
            string CEP;
            string RUA;
            Participante part = null;
            for (int x = 0; x < ArqTxt.Count(); x++)
            {
                CNPJ = "";
                UF = "";
                CEP = "";
                RUA = "";

                CNPJ = ArqTxt[x].Substring(6, 15);

                if (CNPJ.Substring(14, 1) == "1")
                    CNPJ = CNPJ.Substring(0, 11);
                else
                    CNPJ = CNPJ.Substring(0, 14);

                try { part = participantes.Find(f => f.CnpjCpf == CNPJ); }
                catch { part = null; }

                if (part != null)
                {
                    CEP = ArqTxt[x].Substring(201, 9).Replace(" ", "").Replace("-", "");
                    if (CEP.Count() == 8)
                    {
                        UF = ArqTxt[x].Substring(270, 2);
                        RUA = ArqTxt[x].Substring(111, 60).Replace(" ", "") + " " + ArqTxt[x].Substring(171, 30).Replace(" ", "");
                        indice = indice + 1;
                        endereco.ind = indice;
                        endereco.IndPart = part.ind;
                        endereco.Cnpj = CNPJ;
                        endereco.Cep = CEP;
                        endereco.Uf = UF;
                        endereco.Rua = RUA;

                        enderecos.Add(new EnderecoPart()
                        {
                            ind = endereco.ind,
                            IndPart = endereco.IndPart,
                            Cnpj = endereco.Cnpj,
                            Cep = endereco.Cep,
                            Uf = endereco.Uf,
                            Rua = endereco.Rua
                        });
                    }
                }
            }
            return enderecos;
        }
    }
}
