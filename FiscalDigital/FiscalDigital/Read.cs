using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FiscalDigital
{
    public class Contribuinte
    {
        public string Nome;
        public string CNPJ;
        public string IE;
        public string CodMun;
        public string CodPais = "01058";
        public string UF;
        public string CEP;
        public string Endereco;
        public string NUM;
        public string Complemento;
        public string Bairro;
        public string Fone;
    }

    public class NotaFiscal
    {
        public int ind;
        public bool valida;

        #region C100 - 5315, 5320
        private int tipoOper;
        private string codPart;
        private string valorSai;
        private string percCrdout;
        private string valorCrdout;
        private string dtNotaSaida;
        private string tipoDocSaida;
        private string serieSaida;
        private string numDocSaida;
        private string valorIcms;

        public int TipoOper
        {
            get { return tipoOper; }
            set { tipoOper = value; }
        }
        public string ValorCrdout
        {
            get { return valorCrdout; }
            set { valorCrdout = value; }
        }
        public string PercCrdout
        {
            get { return percCrdout; }
            set { percCrdout = value; }
        }
        public string ValorSai
        {
            get { return valorSai; }
            set { valorSai = value; }
        }
        public string CodPart
        {
            get { return codPart; }
            set { codPart = value; }
        }
        public string DtNotaSaida
        {
            get { return dtNotaSaida; }
            set { dtNotaSaida = value; }
        }
        public string TipoDocSaida
        {
            get { return tipoDocSaida; }
            set { tipoDocSaida = value; }
        }
        public string SerieSaida
        {
            get { return serieSaida; }
            set { serieSaida = value; }
        }
        public string NumDocSaida
        {
            get { return numDocSaida; }
            set { numDocSaida = value; }
        }
        public string ValorIcms
        {
            get { return valorIcms; }
            set { valorIcms = value; }
        }
        #endregion

        #region C190 - 5325
        private string iva;
        private string percentMedio;
        private string creditoEstimado;
        private string creditoGerado;

        public string Iva
        {
            get { return iva; }
            set { iva = value; }
        }
        public string PercentMedio
        {
            get { return percentMedio; }
            set { percentMedio = value; }
        }
        public string CreditoEstimado
        {
            get { return creditoEstimado; }
            set { creditoEstimado = value; }
        }
        public string CreditoGerado
        {
            get { return creditoGerado; }
            set { creditoGerado = value; }
        }
        #endregion

        #region C190 - 5330 Comentado
        //private string valorBaseCalculo;
        //private string icmsDebitado;
        //private string cfop;

        //public string ValorBaseCalculo
        //{
        //    get { return valorBaseCalculo; }
        //    set { valorBaseCalculo = value; }
        //}
        //public string IcmsDebitado
        //{
        //    get { return icmsDebitado; }
        //    set { icmsDebitado = value; }
        //}
        //public string Cfop
        //{
        //    get { return cfop; }
        //    set { cfop = value; }
        //}
        #endregion
    }

    public class ItemNotaFiscal
    {
        public int ind;

        private int indNtFiscal;
        private string valorItem;
        private string valorBaseCalculo;
        private string icmsDebitado;
        private string cfop;
        private string aliquota;

        public int IndNtFiscal
        {
            get { return indNtFiscal; }
            set { indNtFiscal = value; }
        }
        public string ValorItem
        {
            get { return valorItem; }
            set { valorItem = value; }
        }
        public string ValorBaseCalculo
        {
            get { return valorBaseCalculo; }
            set { valorBaseCalculo = value; }
        }
        public string IcmsDebitado
        {
            get { return icmsDebitado; }
            set { icmsDebitado = value; }
        }
        public string Cfop
        {
            get { return cfop; }
            set { cfop = value; }
        }
        public string Aliquota
        {
            get { return aliquota; }
            set { aliquota = value; }
        }
    }

    public class RegistroAnalitico
    {
        public int ind;

        private int indNtFiscal;
        private string valorOperacao;
        private string valorBaseCalculo;
        private string icmsDebitado;
        private string cfop;
        private string aliquota;

        public int IndNtFiscal
        {
            get { return indNtFiscal; }
            set { indNtFiscal = value; }
        }
        public string ValorOperacao
        {
            get { return valorOperacao; }
            set { valorOperacao = value; }
        }
        public string ValorBaseCalculo
        {
            get { return valorBaseCalculo; }
            set { valorBaseCalculo = value; }
        }
        public string IcmsDebitado
        {
            get { return icmsDebitado; }
            set { icmsDebitado = value; }
        }
        public string Cfop
        {
            get { return cfop; }
            set { cfop = value; }
        }
        public string Aliquota
        {
            get { return aliquota; }
            set { aliquota = value; }
        }
    }

    public class Read
    {
        #region Controles
        private int BlocoZero = 0;
        private int BlocoCinco = 0;
        private int BlocoNove = 0;
        private int ArquivoTodo = 0;
        
        private int Column0000 = 0;
        private int Column0001 = 0;
        private int Column0150 = 0;
        private int Column0300 = 0;
        private int Column0990 = 0;

        private int Column5001 = 0;
        private int Column5315 = 0;
        private int Column5320 = 0;
        private int Column5325 = 0;
        private int Column5330 = 0;
        private int Column5335 = 0;
        private int Column5340 = 0;
        private int Column5350 = 0;
        private int Column5990 = 0;

        private int Column9001 = 0;
        private int Column9900 = 0;
        private int Column9990 = 0;
        private int Column9999 = 0;

        private bool controle = false;

        private string Arquivo;
        #endregion

        #region Objetos, Listas e Métodos
        double Saidas = 0;
        double Entradas = 0;
        double Iva = 0;
        double Pmc = 0;
        double CreditoAcumuladoTotal = 0;
        Participante participante = new Participante();
        List<Participante> participantes = new List<Participante>();
        EnderecoPart endereco = new EnderecoPart();
        List<EnderecoPart> enderecos = new List<EnderecoPart>();
        NotaFiscal ntFiscal = new NotaFiscal();
        List<NotaFiscal> ntsFiscais = new List<NotaFiscal>();
        ItemNotaFiscal itemNtFiscal = new ItemNotaFiscal();
        List<ItemNotaFiscal> itensNtFiscal = new List<ItemNotaFiscal>();
        RegistroAnalitico regAnalitico = new RegistroAnalitico();
        List<RegistroAnalitico> regsAnaliticos = new List<RegistroAnalitico>();

        List<ItemNotaFiscal> lstItemNts = new List<ItemNotaFiscal>();
        List<RegistroAnalitico> lstRegAnaNotas = new List<RegistroAnalitico>();

        List<RegistroAnalitico> lstRegAnaDivergentes = new List<RegistroAnalitico>();

        public List<Participante> Participantes()
        {
            return participantes;
        }

        public List<EnderecoPart> Enderecos()
        {
            return enderecos;
        }

        public List<NotaFiscal> NotasFiscais()
        {
            return ntsFiscais;
        }

        public List<ItemNotaFiscal> ItensNotasFiscais()
        {
            return itensNtFiscal;
        }

        public List<RegistroAnalitico> RegistroAnalitico()
        {
            return regsAnaliticos;
        }

        public List<RegistroAnalitico> RegistrosDivergentes()
        {
            return lstRegAnaDivergentes;
        }

        public double GetSaidas()
        {
            return Saidas;
        }

        public double GetEntradas()
        {
            return Entradas;
        }

        public double GetIva()
        {
            return Iva;
        }

        public double GetPmc()
        {
            return Pmc;
        }

        public double GetCreditoAcumulado()
        {
            return CreditoAcumuladoTotal;
        }

        #endregion

        #region LeArquivo antigo (Comentado)
        //public string LeArquivo(String[] ArqTxt, StreamWriter ArqValue, string finalidade, string CNAE, string IeIntima)
        //{
        //    int indNtFiscal = 0;
        //    int indItemNtFiscal = 0;
        //    int indRegAnalitico = 0;
        //    int indParticipante = 0;
        //    #region SAIDAS
        //    double SAIDAS = 0;
        //    double saiVlrCont567 = 0;
        //    double saiVlrCont123 = 0;
        //    #endregion
        //    #region ENTRADAS
        //    double ENTRADAS = 0;
        //    double entVlrCont123 = 0;
        //    double entBsCalc123 = 0;
        //    double entVlrCont567 = 0;
        //    double entBsCalc567 = 0;
        //    #endregion
        //    #region PMC
        //    double PMC = 0;
        //    double vlrImpos123 = 0;
        //    double vlrImpos567 = 0;
        //    #endregion
        //    double IVA = 0;
        //    bool ControleC100 = false;
        //    bool Controle0150 = false;
        //    string uf = "";
        //    string cep = "";
        //    string periodo = "";
        //    int countNotas = 0;
        //    string retorno = "Arquivo criado com sucesso!";
        //    for (int x = 0; x < ArqTxt.Count(); x++)
        //    {
        //        String[] Line = ArqTxt[x].Split(new String[] { "|" }, StringSplitOptions.None);
        //        int lineCount = Line.Count() - 1;
        //        for (int y = 0; y < lineCount; y++)
        //        {
        //            string Field = Line[y].ToString();
        //            #region ABERTURA DO ARQUIVO
        //            //Abertura do arquivo
        //            if (y == 0 && x == 0)
        //            {
        //                Arquivo += "0000|";
        //                Arquivo += "LASIMCA|";
        //                //Arquivo += "LASIMCA|";
        //                //COD_VER
        //                Arquivo += "01|";
        //                //COD_FIN
        //                Arquivo += finalidade + "|";
        //                //PERIODO
        //                periodo = GetPeriodo(Line[4].ToString(), Line[5].ToString());
        //                Arquivo += periodo + "|";
        //                //NOME
        //                Arquivo += Line[6].ToString() + "|";
        //                //CNPJ
        //                Arquivo += Line[7].ToString() + "|";
        //                //IE
        //                Arquivo += Line[10].ToString() + "|";
        //                //CNAE
        //                Arquivo += CNAE + "|";
        //                //COD_MUN
        //                Arquivo += Line[11].ToString() + "|";
        //                //IE_INTIMA
        //                Arquivo += IeIntima;

        //                uf = Line[9].ToString();

        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                BlocoZero++;
        //                Column0000++;
        //                ArquivoTodo++;
        //            }
        //            else if (Line[1].ToString() == "0005")
        //            {
        //                cep = Line[3].ToString();
        //                y = lineCount - 1;
        //            }
        //            #endregion
        //            #region ABERTURA DO BLOCO 0
        //            //abertura do bloco 0
        //            else if (y == 0 && x == 1)
        //            {
        //                Arquivo += "0001|";
        //                //IND_MOV
        //                Arquivo += Line[2].ToString();
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                BlocoZero++;
        //                Column0001++;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region DADOS DO PARTICIPANTE
        //            //dados do participante
        //            else if (Line[1].ToString() == "0150")
        //            {
        //                String endereco = "";
        //                string numero = "";
        //                //END ou COD_MUN ou NUM não forem vazios ou nulos
        //                //if (!String.IsNullOrEmpty(Line[10].ToString()) || 
        //                //    !String.IsNullOrEmpty(Line[08].ToString()) || 
        //                //    !String.IsNullOrEmpty(Line[11].ToString()))
        //                //{
        //                //    if (!String.IsNullOrEmpty(uf) || !String.IsNullOrEmpty(cep))
        //                //    {

        //                bool controleind = false;
        //                //Se já passou uma vez pula para a próxima
        //                if (!Controle0150)
        //                {
        //                    //Lê o arquivo pra procurar dados específicos do bloco 5
        //                    for (int a = 0; a < ArqTxt.Count(); a++)
        //                    {
        //                        String[] Linha = ArqTxt[a].Split(new String[] { "|" }, StringSplitOptions.None);
        //                        int linhaCount = Linha.Count() - 1;
        //                        for (int b = 0; b < linhaCount; b++)
        //                        {
        //                            string Campo = Linha[b].ToString();
        //                            if (Linha[1].ToString() == "0150")
        //                            {
        //                                endereco = Linha[10].ToString();
        //                                String[] split = endereco.Split(new String[] { "," }, StringSplitOptions.None);
        //                                endereco = split[0];
        //                                if (split.Count() > 1)
        //                                    numero = split[1].Replace(" ", "");
        //                                else
        //                                    numero = "000";
        //                                if (String.IsNullOrEmpty(endereco))
        //                                    endereco = "Endereco Invalido";

        //                                if (controleind)
        //                                {
        //                                    indParticipante = indParticipante + 1;
        //                                }
        //                                else
        //                                {
        //                                    indParticipante = 0;
        //                                    controleind = true;
        //                                }
        //                                participante.ind = indParticipante;
        //                                participante.CodPart = Linha[2].ToString();
        //                                participante.Nome = Linha[3].ToString();
        //                                participante.CodPais = Linha[4].ToString();
        //                                participante.CnpjCpf = Linha[5].ToString();
        //                                participante.Ie = Linha[7].ToString();
        //                                participante.Uf = uf;
        //                                participante.Cep = cep;
        //                                participante.Endereco = endereco;
        //                                participante.Numero = numero;
        //                                participante.Complemento = Linha[12].ToString();
        //                                participante.Bairro = Linha[13].ToString();
        //                                participante.CodMunicipal = Linha[08].ToString() == "" ? "3550308" : Linha[08].ToString();

        //                                participantes.Add(new Participante
        //                                {
        //                                    ind = participante.ind,
        //                                    CodPart = participante.CodPart,
        //                                    Nome = participante.Nome,
        //                                    CodPais = participante.CodPais,
        //                                    CnpjCpf = participante.CnpjCpf,
        //                                    Ie = participante.Ie,
        //                                    Uf = participante.Uf,
        //                                    Cep = participante.Cep,
        //                                    Endereco = participante.Endereco,
        //                                    Numero = participante.Numero,
        //                                    Complemento = participante.Complemento,
        //                                    Bairro = participante.Bairro,
        //                                    CodMunicipal = participante.CodMunicipal
        //                                });

        //                                b = linhaCount - 1;
        //                            }
        //                            else if (Linha[1].ToString() == "C100")
        //                            {
        //                                indNtFiscal = indNtFiscal + 1;
        //                                ntFiscal.ind = indNtFiscal;
        //                                ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
        //                                ntFiscal.DtNotaSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                ntFiscal.TipoDocSaida = GetTipoDocumento(Linha[5].ToString()); //ADICIONA NA CLASSE Nota Fiscal
        //                                ntFiscal.SerieSaida = Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                ntFiscal.NumDocSaida = Linha[8].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                try { ntFiscal.ValorSai = (Convert.ToDouble(Linha[12])).ToString("n2").Replace(".", ""); }
        //                                catch { ntFiscal.ValorSai = "00"; }
        //                                ntFiscal.ValorIcms = Linha[22].ToString();
        //                                if (ntFiscal.TipoOper == 1)
        //                                    SAIDAS += Convert.ToDouble(ntFiscal.ValorSai);
        //                                else if (ntFiscal.TipoOper == 0)
        //                                    ENTRADAS += Convert.ToDouble(ntFiscal.ValorSai);

        //                                //ADICIONA NA LISTA DE Notas Fiscais
        //                                ntsFiscais.Add(new NotaFiscal
        //                                {
        //                                    ind = ntFiscal.ind,
        //                                    TipoOper = ntFiscal.TipoOper,
        //                                    DtNotaSaida = ntFiscal.DtNotaSaida,
        //                                    TipoDocSaida = ntFiscal.TipoDocSaida,
        //                                    SerieSaida = ntFiscal.SerieSaida,
        //                                    NumDocSaida = ntFiscal.NumDocSaida,
        //                                    CodPart = ntFiscal.CodPart,
        //                                    ValorSai = ntFiscal.ValorSai,
        //                                    ValorIcms = ntFiscal.ValorIcms
        //                                });

        //                                b = linhaCount - 1;
        //                            }
        //                            else if (Linha[1].ToString() == "C170")
        //                            {
        //                                indItemNtFiscal = indItemNtFiscal + 1;
        //                                NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
        //                                itemNtFiscal.ind = indItemNtFiscal;
        //                                itemNtFiscal.IndNtFiscal = nota.ind;
        //                                itemNtFiscal.ValorItem = Linha[07].ToString();
        //                                itemNtFiscal.Cfop = Convert.ToDouble(Linha[11]).ToString("N2").Replace(",00", "");
        //                                itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[13]).ToString("N2").Replace(".", "");
        //                                itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[15]).ToString("N2").Replace(".", "");

        //                                itensNtFiscal.Add(new ItemNotaFiscal
        //                                {
        //                                    ind = itemNtFiscal.ind,
        //                                    IndNtFiscal = itemNtFiscal.IndNtFiscal,
        //                                    ValorItem = itemNtFiscal.ValorItem,
        //                                    Cfop = itemNtFiscal.Cfop,
        //                                    ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
        //                                    IcmsDebitado = itemNtFiscal.IcmsDebitado
        //                                });

        //                                countNotas++;

        //                                b = linhaCount - 1;
        //                            }
        //                            else if (Linha[1].ToString() == "C190")
        //                            {
        //                                indRegAnalitico = indRegAnalitico + 1;
        //                                NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
        //                                regAnalitico.ind = indRegAnalitico;
        //                                regAnalitico.IndNtFiscal = nota.ind;
        //                                regAnalitico.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");
        //                                regAnalitico.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
        //                                regAnalitico.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");

        //                                regsAnaliticos.Add(new RegistroAnalitico
        //                                {
        //                                    ind = regAnalitico.ind,
        //                                    IndNtFiscal = regAnalitico.IndNtFiscal,
        //                                    Cfop = regAnalitico.Cfop,
        //                                    ValorBaseCalculo = regAnalitico.ValorBaseCalculo,
        //                                    IcmsDebitado = regAnalitico.IcmsDebitado
        //                                });

        //                                countNotas++;

        //                                b = linhaCount - 1;
        //                            }
        //                            else { b = linhaCount - 1; }


        //                            #region Codigo anterior (Precaução)
        //                            //Arquivo += "0150|";
        //                            ////COD_PART
        //                            //Arquivo += Line[2].ToString() + "|";
        //                            ////NOME
        //                            //Arquivo += Line[3].ToString() + "|";
        //                            ////COD_PAIS
        //                            //Arquivo += "0" + Line[4].ToString() + "|";
        //                            ////CNPJ
        //                            //Arquivo += Line[5].ToString() + "|";
        //                            ////IE
        //                            //Arquivo += Line[7].ToString() + "|";
        //                            ////UF
        //                            //Arquivo += uf + "|";
        //                            ////CEP
        //                            //Arquivo += cep + "|";
        //                            ////END
        //                            //Arquivo += Line[10].ToString() == "" ? "Rua Iacanga" + "|" : Line[10].ToString() + "|";
        //                            ////NUM
        //                            //Arquivo += Line[11].ToString() == "" ? "268" + "|" : Line[11].ToString() + "|";
        //                            ////COMPL
        //                            //Arquivo += Line[12].ToString() + "|";
        //                            ////BAIRRO
        //                            //Arquivo += Line[13].ToString() + "|";
        //                            ////COD_MUN
        //                            //Arquivo += Line[08].ToString() == "" ? "3550308" + "|" : Line[08].ToString() + "|";
        //                            ////FONE
        //                            //Arquivo += "";
        //                            //Arquivo += "\r\n";
        //                            //BlocoZero++;
        //                            //Column0150++;
        //                            //ArquivoTodo++;
        //                            #endregion
        //                        }
        //                    }

        //                    foreach (Participante part in participantes)
        //                    {
        //                        NotaFiscal nt = ntsFiscais.Find(f => f.CodPart == part.CodPart);
        //                        if (nt != null)
        //                        {
        //                            Arquivo += "0150|";
        //                            //COD_PART
        //                            Arquivo += part.CodPart + "|";
        //                            //NOME
        //                            Arquivo += part.Nome + "|";
        //                            //COD_PAIS
        //                            Arquivo += (part.CodPais.Count() < 5 ? "0" + part.CodPais : part.CodPais) + "|";
        //                            //CNPJ
        //                            Arquivo += part.CnpjCpf + "|";
        //                            //IE
        //                            Arquivo += part.Ie + "|";
        //                            //UF
        //                            Arquivo += part.Uf + "|";
        //                            //CEP
        //                            Arquivo += part.Cep + "|";
        //                            //END
        //                            Arquivo += part.Endereco + "|";
        //                            //NUM
        //                            Arquivo += part.Numero + "|";
        //                            //COMPL
        //                            Arquivo += part.Complemento + "|";
        //                            //BAIRRO
        //                            Arquivo += part.Bairro + "|";
        //                            //COD_MUN
        //                            Arquivo += part.CodMunicipal + "|";
        //                            //FONE
        //                            Arquivo += "";
        //                            Arquivo += "\r\n";
        //                            BlocoZero++;
        //                            Column0150++;
        //                            ArquivoTodo++;
        //                        }
        //                    }

        //                    #region FOREACH para Entradas e Saidas
        //                    //foreach (NotaFiscal nt in ntsFiscais)
        //                    //{
        //                    //    lstItemNts.Clear();
        //                    //    lstRegAnaNotas.Clear();
        //                    //    foreach (ItemNotaFiscal intf in itensNtFiscal)
        //                    //    {
        //                    //        if (intf.IndNtFiscal == nt.ind)
        //                    //        {
        //                    //            if (ValidaCFOP(intf.Cfop.Replace(".", ""), 1))
        //                    //            {
        //                    //                lstItemNts.Add(new ItemNotaFiscal()
        //                    //                    {
        //                    //                        ind = intf.ind,
        //                    //                        IndNtFiscal = intf.IndNtFiscal,
        //                    //                        Cfop = intf.Cfop,
        //                    //                        IcmsDebitado = intf.IcmsDebitado,
        //                    //                        ValorBaseCalculo = intf.ValorBaseCalculo,
        //                    //                        ValorItem = intf.ValorItem
        //                    //                    });
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //    if (lstItemNts == null)
        //                    //    {
        //                    //        foreach (RegistroAnalitico ra in regsAnaliticos)
        //                    //        {
        //                    //            if (ra.IndNtFiscal == nt.ind)
        //                    //            {
        //                    //                if (ValidaCFOP(ra.Cfop.Replace(".", ""), 1))
        //                    //                {
        //                    //                    lstRegAnaNotas.Add(new RegistroAnalitico()
        //                    //                        {
        //                    //                            ind = ra.ind,
        //                    //                            IndNtFiscal = ra.IndNtFiscal,
        //                    //                            Cfop = ra.Cfop,
        //                    //                            IcmsDebitado = ra.IcmsDebitado,
        //                    //                            ValorBaseCalculo = ra.ValorBaseCalculo
        //                    //                        });
        //                    //                }
        //                    //            }
        //                    //        }
        //                    //        if (lstRegAnaNotas != null)
        //                    //        {
        //                    //            foreach (RegistroAnalitico ra in lstRegAnaNotas)
        //                    //            {
        //                    //                string cfop = Convert.ToDouble(ra.Cfop).ToString("N").Replace(",00", "");

        //                    //                if (VerificaTipoCFOP(cfop)) //Entrada
        //                    //                {
        //                    //                    try { ENTRADAS += Convert.ToDouble(nt.ValorSai); }
        //                    //                    catch { }
        //                    //                }
        //                    //                else //Saida
        //                    //                {
        //                    //                    try { SAIDAS += Convert.ToDouble(nt.ValorSai); }
        //                    //                    catch { }
        //                    //                }
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        foreach (ItemNotaFiscal itemNt in lstItemNts)
        //                    //        {
        //                    //            string cfop = Convert.ToDouble(itemNt.Cfop).ToString("N").Replace(",00", "");

        //                    //            if (VerificaTipoCFOP(cfop)) //Entrada
        //                    //            {
        //                    //                try { ENTRADAS += Convert.ToDouble(itemNt.ValorItem); }
        //                    //                catch { }
        //                    //            }
        //                    //            else //Saida
        //                    //            {
        //                    //                try { SAIDAS += Convert.ToDouble(itemNt.ValorItem); }
        //                    //                catch { }
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //}
        //                    #endregion
        //                    //participantes.Clear();
        //                    lstItemNts.Clear();
        //                    lstRegAnaNotas.Clear();
        //                    regsAnaliticos.Clear();
        //                    ntsFiscais.Clear();
        //                    y = lineCount - 1;
        //                    Controle0150 = true;
        //                }
        //            }
        //            #endregion
        //            #region FECHAMENO DO BLOCO 0
        //            //fechamento do bloco 0
        //            else if (Line[1].ToString() == "0990")
        //            {
        //                #region 0300 Temporário
        //                Arquivo += "0300|";
        //                Arquivo += "0000|01||vazio||||||";
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                BlocoZero++;
        //                Column0300++;
        //                ArquivoTodo++;
        //                #endregion

        //                Arquivo += "0990|";
        //                //LINHAS BLOCO 0
        //                BlocoZero++;
        //                //QTD_LIN_0
        //                Arquivo += BlocoZero.ToString();
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                Column0990++;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region ABERTURA DO BLOCO 5
        //            //abertura do bloco 5
        //            else if (Line[1].ToString() == "C001")
        //            {
        //                Arquivo += "5001|";
        //                //IND_MOV
        //                Arquivo += Line[2].ToString();
        //                Arquivo += "\r\n";
        //                BlocoCinco++;
        //                y = lineCount - 1;
        //                Column5001++;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region CALCULOS MONSTROS E CONFUSOS
        //            else if (Line[1].ToString() == "C100")
        //            {
        //                //Se já passou uma vez pula para a próxima
        //                if (!ControleC100)
        //                {
        //                    indNtFiscal = 0;
        //                    ntsFiscais.Clear();
        //                    indItemNtFiscal = 0;
        //                    itensNtFiscal.Clear();
        //                    //Lê o arquivo pra procurar dados específicos do bloco 5
        //                    for (int a = 0; a < ArqTxt.Count(); a++)
        //                    {
        //                        String[] Linha = ArqTxt[a].Split(new String[] { "|" }, StringSplitOptions.None);
        //                        int linhaCount = Linha.Count() - 1;
        //                        for (int b = 0; b < linhaCount; b++)
        //                        {
        //                            string Campo = Linha[b].ToString();
        //                            if (Linha[1].ToString() == "C100")
        //                            {
        //                                //TIP_DOC INVÁLIDOS
        //                                if (Linha[5].ToString() != "29" ||
        //                                    Linha[5].ToString() != "59" ||
        //                                    Linha[5].ToString() != "60" ||
        //                                    Linha[5].ToString() != "65")
        //                                {
        //                                    indNtFiscal = indNtFiscal + 1;
        //                                    ntFiscal.ind = indNtFiscal;
        //                                    ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
        //                                    ntFiscal.DtNotaSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                    ntFiscal.TipoDocSaida = GetTipoDocumento(Linha[5].ToString()); //ADICIONA NA CLASSE Nota Fiscal
        //                                    ntFiscal.SerieSaida = Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                    ntFiscal.NumDocSaida = Linha[8].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                    ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
        //                                    try
        //                                    {
        //                                        ntFiscal.ValorSai = (Convert.ToDouble(Linha[12])).ToString("n2").Replace(".", ""); //ADICIONA NA CLASSE Nota Fiscal
        //                                    }
        //                                    catch
        //                                    {
        //                                        ntFiscal.ValorSai = "00"; //ADICIONA NA CLASSE Nota Fiscal
        //                                    }
        //                                    ntFiscal.ValorIcms = Linha[22].ToString();

        //                                    //ADICIONA NA LISTA DE Notas Fiscais
        //                                    ntsFiscais.Add(new NotaFiscal
        //                                    {
        //                                        ind = ntFiscal.ind,
        //                                        TipoOper = ntFiscal.TipoOper,
        //                                        DtNotaSaida = ntFiscal.DtNotaSaida,
        //                                        TipoDocSaida = ntFiscal.TipoDocSaida,
        //                                        SerieSaida = ntFiscal.SerieSaida,
        //                                        NumDocSaida = ntFiscal.NumDocSaida,
        //                                        CodPart = ntFiscal.CodPart,
        //                                        ValorSai = ntFiscal.ValorSai,
        //                                        ValorIcms = ntFiscal.ValorIcms
        //                                    }
        //                                                  );
        //                                    b = linhaCount - 1;
        //                                }
        //                            }
        //                            else if (Linha[1].ToString() == "C170")
        //                            {
        //                                indItemNtFiscal = indItemNtFiscal + 1;
        //                                NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
        //                                itemNtFiscal.ind = indNtFiscal;
        //                                itemNtFiscal.IndNtFiscal = nota.ind;
        //                                itemNtFiscal.ValorItem = Linha[07].ToString();
        //                                itemNtFiscal.Cfop = Convert.ToDouble(Linha[11]).ToString("N2").Replace(",00", "");
        //                                itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[13]).ToString("N2").Replace(".", "");
        //                                itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[15]).ToString("N2").Replace(".", "");

        //                                itensNtFiscal.Add(new ItemNotaFiscal
        //                                {
        //                                    ind = itemNtFiscal.ind,
        //                                    IndNtFiscal = itemNtFiscal.IndNtFiscal,
        //                                    ValorItem = itemNtFiscal.ValorItem,
        //                                    Cfop = itemNtFiscal.Cfop,
        //                                    ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
        //                                    IcmsDebitado = itemNtFiscal.IcmsDebitado
        //                                });

        //                                string cfop = Convert.ToDouble(itemNtFiscal.Cfop).ToString("N");

        //                                if (VerificaTipoCFOP(itemNtFiscal.Cfop)) //Entrada
        //                                {
        //                                    try
        //                                    {
        //                                        entVlrCont123 = entVlrCont123 + GetValueForCFOP(regAnalitico.Cfop, 1, itemNtFiscal.ValorItem);
        //                                        entBsCalc123 = entBsCalc123 + GetValueForCFOP(regAnalitico.Cfop, 2, itemNtFiscal.ValorBaseCalculo);
        //                                        saiVlrCont123 = saiVlrCont123 + GetValueForCFOP(regAnalitico.Cfop, 6, itemNtFiscal.ValorItem);

        //                                        vlrImpos123 = vlrImpos123 + GetValueForCFOP(regAnalitico.Cfop, 7, itemNtFiscal.IcmsDebitado);
        //                                    }
        //                                    catch { }
        //                                }
        //                                else //Saida
        //                                {
        //                                    try
        //                                    {
        //                                        entVlrCont567 = entVlrCont567 + GetValueForCFOP(regAnalitico.Cfop, 3, itemNtFiscal.ValorItem);
        //                                        entBsCalc567 = entBsCalc567 + GetValueForCFOP(regAnalitico.Cfop, 4, itemNtFiscal.ValorBaseCalculo);
        //                                        saiVlrCont567 = saiVlrCont567 + GetValueForCFOP(regAnalitico.Cfop, 5, itemNtFiscal.ValorItem);

        //                                        vlrImpos567 = vlrImpos567 + GetValueForCFOP(regAnalitico.Cfop, 8, itemNtFiscal.ValorItem);
        //                                    }
        //                                    catch { }
        //                                }

        //                                countNotas++;

        //                                b = linhaCount - 1;
        //                            }
        //                            #region Código antigo (Precaução)
        //                            else if (Linha[1].ToString() == "C190")
        //                            {
        //                                indRegAnalitico = indRegAnalitico + 1;
        //                                NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
        //                                regAnalitico.ind = indRegAnalitico;
        //                                regAnalitico.IndNtFiscal = nota.ind;
        //                                regAnalitico.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
        //                                regAnalitico.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");
        //                                regAnalitico.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");

        //                                regsAnaliticos.Add(new RegistroAnalitico
        //                                {
        //                                    ind = regAnalitico.ind,
        //                                    IndNtFiscal = regAnalitico.IndNtFiscal,
        //                                    ValorBaseCalculo = regAnalitico.ValorBaseCalculo,
        //                                    IcmsDebitado = regAnalitico.IcmsDebitado,
        //                                    Cfop = regAnalitico.Cfop
        //                                });

        //                                if (VerificaTipoCFOP(regAnalitico.Cfop)) //Entrada
        //                                {
        //                                    try
        //                                    {
        //                                        entVlrCont123 = entVlrCont123 + GetValueForCFOP(regAnalitico.Cfop, 1, nota.ValorSai);
        //                                        entBsCalc123 = entBsCalc123 + GetValueForCFOP(regAnalitico.Cfop, 2, regAnalitico.ValorBaseCalculo);
        //                                        saiVlrCont123 = saiVlrCont123 + GetValueForCFOP(regAnalitico.Cfop, 6, nota.ValorSai);

        //                                        vlrImpos123 = vlrImpos123 + GetValueForCFOP(regAnalitico.Cfop, 7, nota.ValorIcms);
        //                                    }
        //                                    catch { }
        //                                }
        //                                else //Saida
        //                                {
        //                                    try
        //                                    {
        //                                        entVlrCont567 = entVlrCont567 + GetValueForCFOP(regAnalitico.Cfop, 3, nota.ValorSai);
        //                                        entBsCalc567 = entBsCalc567 + GetValueForCFOP(regAnalitico.Cfop, 4, regAnalitico.ValorBaseCalculo);
        //                                        saiVlrCont567 = saiVlrCont567 + GetValueForCFOP(regAnalitico.Cfop, 5, nota.ValorSai);

        //                                        vlrImpos567 = vlrImpos567 + GetValueForCFOP(regAnalitico.Cfop, 8, nota.ValorIcms);
        //                                    }
        //                                    catch { }
        //                                }

        //                                countNotas++;

        //                                b = linhaCount - 1;
        //                            }
        //                            #endregion
        //                            else
        //                            {
        //                                b = linhaCount - 1;
        //                            }
        //                        }
        //                    }

        //                    //SAIDAS = saiVlrCont567 - saiVlrCont123;
        //                    //ENTRADAS = (entVlrCont123 + entBsCalc123) - (entVlrCont567 + entBsCalc567);
        //                    PMC = ((vlrImpos123 - vlrImpos567) / ENTRADAS) * 100;
        //                    IVA = (SAIDAS - ENTRADAS) / ENTRADAS;
        //                    Saidas = SAIDAS;
        //                    Entradas = ENTRADAS;
        //                    Pmc = PMC;
        //                    Iva = IVA;
        //                    double CreditoEstimado = 0;
        //                    double CreditoAcumulado = 0;

        //                    foreach (NotaFiscal nt in ntsFiscais)
        //                    {
        //                        if (GetPeriodoNota(nt.DtNotaSaida, periodo))
        //                        {
        //                            bool campo5315 = true;
        //                            foreach (RegistroAnalitico ra in regsAnaliticos)
        //                            {
        //                                if (ra.IndNtFiscal == nt.ind)
        //                                {
        //                                    string cfop = ra.Cfop.Replace(".", "");
        //                                    if (ValidaCFOP(cfop, 1))
        //                                    {
        //                                        double valorSaida = 0;
        //                                        double icmsValue = 0;
        //                                        CreditoEstimado = 0;
        //                                        CreditoAcumulado = 0;
        //                                        NotaFiscal findDevol = null;

        //                                        try
        //                                        {
        //                                            findDevol = ntsFiscais.Find(f => f.TipoOper == 0 && f.TipoDocSaida == nt.TipoDocSaida &&
        //                                                                                        f.SerieSaida == nt.SerieSaida && f.NumDocSaida == nt.NumDocSaida);
        //                                            if (nt.ValorSai != "0" || !String.IsNullOrEmpty(nt.ValorSai))
        //                                                valorSaida = Convert.ToDouble(nt.ValorSai);
        //                                            if (nt.ValorIcms != "0" || !String.IsNullOrEmpty(nt.ValorIcms))
        //                                                icmsValue = Convert.ToDouble(nt.ValorIcms);
        //                                        }
        //                                        catch //(Exception ex)
        //                                        {
        //                                            //retorno = "Erro no Try: " + ex.Message;
        //                                        }

        //                                        //Verificar se existirá hierarquia certa
        //                                        //Para existir o campo 5315 tem q existir pelo menos um campo 5320 ou 5325. O campo 5330 é obrigatório na presença do 5315
        //                                        if (valorSaida != 0 && icmsValue != 0)
        //                                        {
        //                                            if (campo5315)
        //                                            {
        //                                                if (ValidaCFOP(cfop, 2))
        //                                                {
        //                                                    //valorSaida = valorSaida - (valorSaida * 2);
        //                                                    //CreditoEstimado = ((valorSaida / (1 + IVA)) * PMC) - (((valorSaida / (1 + IVA)) * PMC) * 2);
        //                                                    //CreditoAcumulado = (CreditoEstimado - icmsValue) - ((CreditoEstimado - icmsValue) * 2);
        //                                                    //ra.ValorBaseCalculo = "-" + ra.ValorBaseCalculo;
        //                                                    //ra.IcmsDebitado = "-" + ra.IcmsDebitado;
        //                                                    CreditoEstimado = (valorSaida / (1 + IVA)) * PMC;
        //                                                    CreditoAcumulado = CreditoEstimado - icmsValue;
        //                                                }
        //                                                else
        //                                                {
        //                                                    CreditoEstimado = (valorSaida / (1 + IVA)) * PMC;
        //                                                    CreditoAcumulado = CreditoEstimado - icmsValue;
        //                                                }
        //                                                nt.CreditoEstimado = CreditoEstimado.ToString();
        //                                                nt.CreditoGerado = CreditoAcumulado.ToString();

        //                                                #region 5315
        //                                                Arquivo += "5315|";
        //                                                //DT_EMISSAO
        //                                                Arquivo += nt.DtNotaSaida + "|";
        //                                                //TIP_DOC
        //                                                Arquivo += nt.TipoDocSaida + "|";
        //                                                //SER
        //                                                Arquivo += nt.SerieSaida + "|";
        //                                                //NUM_DOC
        //                                                Arquivo += nt.NumDocSaida + "|";
        //                                                //COD_PART
        //                                                Arquivo += nt.CodPart + "|";
        //                                                //VALOR_SAI
        //                                                Arquivo += valorSaida.ToString("n2").Replace(".", "") + "|";
        //                                                //PERC_CRDOUT
        //                                                Arquivo += nt.PercCrdout + "|";
        //                                                //VALOR_CRDOUT
        //                                                Arquivo += nt.ValorCrdout + "";
        //                                                Arquivo += "\r\n";
        //                                                BlocoCinco++;
        //                                                Column5315++;
        //                                                ArquivoTodo++;
        //                                                #endregion

        //                                                if (ValidaCFOP(cfop, 2))
        //                                                {
        //                                                    #region 5320
        //                                                    Arquivo += "5320|";
        //                                                    //DT_SAI
        //                                                    Arquivo += nt.DtNotaSaida + "|";
        //                                                    //TIP_DOC
        //                                                    Arquivo += nt.TipoDocSaida + "|";
        //                                                    //SER
        //                                                    Arquivo += nt.SerieSaida + "|";
        //                                                    //NUM_DOC
        //                                                    Arquivo += nt.NumDocSaida;
        //                                                    Arquivo += "\r\n";
        //                                                    BlocoCinco++;
        //                                                    Column5320++;
        //                                                    ArquivoTodo++;
        //                                                    #endregion
        //                                                }

        //                                                #region 5325
        //                                                Arquivo += "5325|0000|";
        //                                                Arquivo += IVA.ToString("n4").Replace(".", "") + "|";
        //                                                Arquivo += PMC.ToString("n4").Replace(".", "") + "|";
        //                                                Arquivo += CreditoEstimado.ToString("n2").Replace(".", "") + "|";
        //                                                Arquivo += CreditoAcumulado.ToString("n2").Replace(".", "");
        //                                                Arquivo += "\r\n";
        //                                                BlocoCinco++;
        //                                                Column5325++;
        //                                                ArquivoTodo++;
        //                                                #endregion

        //                                                #region 5330
        //                                                Arquivo += "5330|";
        //                                                //VALOR_BC
        //                                                Arquivo += ra.ValorBaseCalculo + "|";
        //                                                //ICMS_DEB
        //                                                Arquivo += ra.IcmsDebitado;
        //                                                Arquivo += "\r\n";
        //                                                BlocoCinco++;
        //                                                Column5330++;
        //                                                ArquivoTodo++;
        //                                                #endregion
        //                                                campo5315 = false;
        //                                            }
        //                                        }

        //                                        #region Codigo anterior (Precaução)
        //                                        //if (nt.TipoOper == 1)
        //                                        //{
        //                                        //    #region 5315
        //                                        //    Arquivo += "5315|";
        //                                        //    //DT_EMISSAO
        //                                        //    Arquivo += nt.DtNotaSaida + "|";
        //                                        //    //TIP_DOC
        //                                        //    Arquivo += nt.TipoDocSaida + "|";
        //                                        //    //SER
        //                                        //    Arquivo += nt.SerieSaida + "|";
        //                                        //    //NUM_DOC
        //                                        //    Arquivo += nt.NumDocSaida + "|";
        //                                        //    //COD_PART
        //                                        //    Arquivo += nt.CodPart + "|";
        //                                        //    //VALOR_SAI
        //                                        //    Arquivo += nt.ValorSai + "|";
        //                                        //    //PERC_CRDOUT
        //                                        //    Arquivo += nt.PercCrdout + "|";
        //                                        //    //VALOR_CRDOUT
        //                                        //    Arquivo += nt.ValorCrdout + "";
        //                                        //    Arquivo += "\r\n";
        //                                        //    BlocoCinco++;
        //                                        //    Column5315++;
        //                                        //    ArquivoTodo++;
        //                                        //    #endregion

        //                                        //    if (findDevol != null)
        //                                        //    {
        //                                        //        #region 5320
        //                                        //        Arquivo += "5320|";
        //                                        //        //DT_SAI
        //                                        //        Arquivo += nt.DtNotaSaida + "|";
        //                                        //        //TIP_DOC
        //                                        //        Arquivo += nt.TipoDocSaida + "|";
        //                                        //        //SER
        //                                        //        Arquivo += nt.SerieSaida + "|";
        //                                        //        //NUM_DOC
        //                                        //        Arquivo += nt.NumDocSaida;
        //                                        //        Arquivo += "\r\n";
        //                                        //        BlocoCinco++;
        //                                        //        Column5315++;
        //                                        //        ArquivoTodo++;
        //                                        //        #endregion
        //                                        //    }

        //                                        //    if (valorSaidasTotal != 0 &&
        //                                        //        valorEntradasTotal != 0 &&
        //                                        //        aliquotaIcms != 0)
        //                                        //    {
        //                                        //        if (valorSaida != 0 && icmsValue != 0)
        //                                        //        {
        //                                        //            double Iva = (valorSaidasTotal - valorEntradasTotal) / valorEntradasTotal;
        //                                        //            double PmcIcms = aliquotaIcms / countNotas;
        //                                        //            double CreditoEstimado = (valorSaida / (1 + Iva)) * PmcIcms;
        //                                        //            double CreditoAcumulado = CreditoEstimado - icmsValue;

        //                                        //            #region 5325
        //                                        //            Arquivo += "5325|0000|";
        //                                        //            Arquivo += Iva.ToString("n4").Replace(".","") + "|";
        //                                        //            Arquivo += PmcIcms.ToString("n4").Replace(".", "") + "|";
        //                                        //            Arquivo += CreditoEstimado.ToString("n2").Replace(".", "") + "|";
        //                                        //            Arquivo += CreditoAcumulado.ToString("n2").Replace(".", "");
        //                                        //            Arquivo += "\r\n";
        //                                        //            BlocoCinco++;
        //                                        //            Column5315++;
        //                                        //            ArquivoTodo++;
        //                                        //            #endregion
        //                                        //        }
        //                                        //    }

        //                                        //    #region 5330
        //                                        //    Arquivo += "5330|";
        //                                        //    //VALOR_BC
        //                                        //    Arquivo += nt.ValorBaseCalculo + "|";
        //                                        //    //ICMS_DEB
        //                                        //    Arquivo += nt.IcmsDebitado;
        //                                        //    Arquivo += "\r\n";
        //                                        //    BlocoCinco++;
        //                                        //    Column5315++;
        //                                        //    ArquivoTodo++;
        //                                        //    #endregion
        //                                        //}
        //                                        #endregion
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    ControleC100 = true;
        //                }
        //            }
        //            #endregion
        //            #region FECHAMENTO DO BLOCO 5
        //            else if (Line[1].ToString() == "C990")
        //            {
        //                Arquivo += "5990|";
        //                //LINHAS BLOCO 5
        //                BlocoCinco++;
        //                //QTD_LIN_C
        //                Arquivo += BlocoCinco.ToString();
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                Column5990++;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region ABERTURA DO BLOCO 9
        //            else if (Line[1].ToString() == "9001")
        //            {
        //                Arquivo += "9001|";
        //                //IND_MOV
        //                Arquivo += "0";
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                Column9001++;
        //                BlocoNove++;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region CALCULOS MONSTROS DO BLOCO 9
        //            else if (Line[1].ToString() == "9900" && controle == false)
        //            {
        //                #region 9900
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|0000|" + Column0000.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|0001|" + Column0001.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|0150|" + Column0150.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|0300|" + Column0300.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|0990|" + Column0990.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|5001|" + Column5001.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                if (Column5315 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5315|" + Column5315.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5320 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5320|" + Column5320.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5325 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5325|" + Column5325.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5330 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5330|" + Column5330.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5335 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5335|" + Column5335.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5340 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5340|" + Column5340.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                if (Column5350 != 0)
        //                {
        //                    //REG | REG_BLC | QTD_REG_BLC
        //                    Arquivo += "9900|5350|" + Column5350.ToString();
        //                    Arquivo += "\r\n";
        //                    BlocoNove++;
        //                    Column9900++;
        //                    ArquivoTodo++;
        //                }
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|5990|" + Column5990.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|9001|" + Column9001.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9900++;
        //                Column9900++;
        //                Column9900++;
        //                Column9900++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|9900|" + Column9900.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9990++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|9990|" + Column9990.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                Column9999++;
        //                ArquivoTodo++;
        //                //REG | REG_BLC | QTD_REG_BLC
        //                Arquivo += "9900|9999|" + Column9999.ToString();
        //                Arquivo += "\r\n";
        //                BlocoNove++;
        //                ArquivoTodo++;
        //                y = lineCount - 1;
        //                controle = true;
        //                #endregion
        //            }
        //            #endregion
        //            #region FECHAMENTO DO BLOCO 9
        //            else if (Line[1].ToString() == "9990")
        //            {
        //                Arquivo += "9990|";
        //                //LINHAS BLOCO 9
        //                BlocoNove++;
        //                BlocoNove++;
        //                //QTD_LIN_9
        //                Arquivo += BlocoNove.ToString();
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //                ArquivoTodo++;
        //            }
        //            #endregion
        //            #region FECHAMENTO DO ARQUIVO
        //            else if (Line[1].ToString() == "9999")
        //            {
        //                Arquivo += "9999|";
        //                //LINHAS DO ARQUIVO
        //                ArquivoTodo++;
        //                //QTD_LIN
        //                Arquivo += ArquivoTodo.ToString();
        //                Arquivo += "\r\n";
        //                y = lineCount - 1;
        //            }
        //            #endregion
        //            else
        //            {
        //                y = lineCount - 1;
        //            }
        //        }
        //        if (!String.IsNullOrEmpty(Arquivo))
        //        {
        //            try
        //            {
        //                ArqValue.Write(Arquivo);
        //                ArqValue.Flush();
        //            }
        //            catch (Exception ex)
        //            {
        //                ArqValue.Close();
        //                retorno = "Erro: " + ex.Message;
        //            }
        //            Arquivo = "";
        //        }
        //    }
        //    ArqValue.Close();
        //    return retorno;
        //}
        #endregion

        public string LeArquivo(String[] ArqTxtDigital, String[] ArqTxtClientes, StreamWriter ArqValueDigital, 
                                string finalidade, string CNAE, string IeIntima, string IvaMediana)
        {
            Contribuinte contribuinte = new Contribuinte();
            int indNtFiscal = 0;
            int indItemNtFiscal = 0;
            int indRegAnalitico = 0;
            int indParticipante = 0;
            #region SAIDAS
            double SAIDAS = 0;
            //double saiVlrCont567 = 0;
            //double saiVlrCont123 = 0;
            #endregion
            #region ENTRADAS
            double ENTRADAS = 0;
            //double entVlrCont123 = 0;
            //double entBsCalc123 = 0;
            //double entVlrCont567 = 0;
            //double entBsCalc567 = 0;
            #endregion
            #region PMC
            double PMC = 0;
            double vlrImpos123 = 0;
            double vlrImpos567 = 0;
            #endregion
            double IVA = 0;
            bool ControleC100 = false;
            bool Controle0150 = false;
            string uf = "";
            string cep = "";
            string periodo = "";
            int countNotas = 0;
            string retorno = "";
            double IvaMedianaDouble = Convert.ToDouble(IvaMediana);
            for (int x = 0; x < ArqTxtDigital.Count(); x++)
            {
                String[] Line = ArqTxtDigital[x].Split(new String[] { "|" }, StringSplitOptions.None);
                int lineCount = Line.Count() - 1;
                for (int y = 0; y < lineCount; y++)
                {
                    string Field = Line[y].ToString();
                    #region ABERTURA DO ARQUIVO
                    //Abertura do arquivo
                    if (y == 0 && x == 0)
                    {
                        Arquivo += "0000|";
                        Arquivo += "LASIMCA|";
                        //Arquivo += "LASIMCA|";
                        //COD_VER
                        Arquivo += "01|";
                        //COD_FIN
                        Arquivo += finalidade + "|";
                        //PERIODO
                        periodo = GetPeriodo(Line[4].ToString(), Line[5].ToString());
                        Arquivo += periodo + "|";
                        //NOME
                        contribuinte.Nome = Line[6].ToString();
                        Arquivo += Line[6].ToString() + "|";
                        //CNPJ
                        contribuinte.CNPJ = Line[7].ToString();
                        Arquivo += Line[7].ToString() + "|";
                        //IE
                        contribuinte.IE = Line[10].ToString();
                        Arquivo += Line[10].ToString() + "|";
                        //CNAE
                        Arquivo += CNAE + "|";
                        //COD_MUN
                        contribuinte.CodMun = Line[11].ToString();
                        Arquivo += Line[11].ToString() + "|";
                        //IE_INTIMA
                        Arquivo += IeIntima;

                        contribuinte.UF = Line[9].ToString();
                        uf = Line[9].ToString();

                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        BlocoZero++;
                        Column0000++;
                        ArquivoTodo++;
                    }
                    else if (Line[1].ToString() == "0005")
                    {
                        contribuinte.CEP = Line[3].ToString();
                        contribuinte.Endereco = Line[4].ToString();
                        contribuinte.NUM = Line[5].ToString();
                        contribuinte.Complemento = Line[6].ToString();
                        contribuinte.Bairro = Line[7].ToString();
                        contribuinte.Fone = Line[8].ToString();
                        cep = Line[3].ToString();
                        y = lineCount - 1;
                    }
                    #endregion
                    #region ABERTURA DO BLOCO 0
                    //abertura do bloco 0
                    else if (y == 0 && x == 1)
                    {
                        Arquivo += "0001|";
                        //IND_MOV
                        Arquivo += Line[2].ToString();
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        BlocoZero++;
                        Column0001++;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region DADOS DO PARTICIPANTE
                    //dados do participante
                    else if (Line[1].ToString() == "0150")
                    {
                        String endereco = "";
                        string numero = "";

                        bool controleind = false;
                        //Se já passou uma vez pula para a próxima
                        if (!Controle0150)
                        {
                            //Lê o arquivo pra procurar dados específicos do bloco 5
                            for (int a = 0; a < ArqTxtDigital.Count(); a++)
                            {
                                String[] Linha = ArqTxtDigital[a].Split(new String[] { "|" }, StringSplitOptions.None);
                                int linhaCount = Linha.Count() - 1;
                                for (int b = 0; b < linhaCount; b++)
                                {
                                    string Campo = Linha[b].ToString();
                                    if (Linha[1].ToString() == "0150")
                                    {
                                        #region 0150
                                        endereco = Linha[10].ToString();
                                        String[] split = endereco.Split(new String[] { "," }, StringSplitOptions.None);
                                        endereco = split[0];
                                        if (split.Count() > 1)
                                            numero = split[1].Replace(" ", "");
                                        else
                                            numero = "";
                                        if (String.IsNullOrEmpty(endereco))
                                            endereco = "";

                                        if (controleind)
                                        {
                                            indParticipante = indParticipante + 1;
                                        }
                                        else
                                        {
                                            indParticipante = 0;
                                            controleind = true;
                                        }
                                        participante.ind = indParticipante;
                                        participante.CodPart = Linha[2].ToString();
                                        participante.Nome = Linha[3].ToString();
                                        participante.CodPais = Linha[4].ToString();
                                        participante.CnpjCpf = Linha[5].ToString();
                                        participante.Ie = Linha[7].ToString();
                                        participante.Uf = uf;
                                        participante.Cep = cep;
                                        participante.Endereco = endereco;
                                        participante.Numero = numero;
                                        participante.Complemento = Linha[12].ToString();
                                        participante.Bairro = Linha[13].ToString();
                                        participante.CodMunicipal = Linha[08].ToString() == "" ? "3550308" : Linha[08].ToString();

                                        participantes.Add(new Participante
                                        {
                                            ind = participante.ind,
                                            CodPart = participante.CodPart,
                                            Nome = participante.Nome,
                                            CodPais = participante.CodPais,
                                            CnpjCpf = participante.CnpjCpf,
                                            Ie = participante.Ie,
                                            Uf = participante.Uf,
                                            Cep = participante.Cep,
                                            Endereco = participante.Endereco,
                                            Numero = participante.Numero,
                                            Complemento = participante.Complemento,
                                            Bairro = participante.Bairro,
                                            CodMunicipal = participante.CodMunicipal
                                        });

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C100")
                                    {
                                        #region C100
                                        indNtFiscal = indNtFiscal + 1;
                                        ntFiscal.ind = indNtFiscal;
                                        ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.DtNotaSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.TipoDocSaida = GetTipoDocumento(Linha[5].ToString()); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.SerieSaida = Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.NumDocSaida = Linha[8].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        try { ntFiscal.ValorSai = (Convert.ToDouble(Linha[12])).ToString("n2").Replace(".", ""); }
                                        catch { ntFiscal.ValorSai = "00"; }
                                        ntFiscal.ValorIcms = Linha[22].ToString();

                                        if (GetPeriodoNota(ntFiscal.DtNotaSaida, periodo))
                                            ntFiscal.valida = true;
                                        else
                                            ntFiscal.valida = false;
                                        //ADICIONA NA LISTA DE Notas Fiscais
                                        ntsFiscais.Add(new NotaFiscal
                                        {
                                            ind = ntFiscal.ind,
                                            TipoOper = ntFiscal.TipoOper,
                                            DtNotaSaida = ntFiscal.DtNotaSaida,
                                            TipoDocSaida = ntFiscal.TipoDocSaida,
                                            SerieSaida = ntFiscal.SerieSaida,
                                            NumDocSaida = ntFiscal.NumDocSaida,
                                            CodPart = ntFiscal.CodPart,
                                            ValorSai = ntFiscal.ValorSai,
                                            ValorIcms = ntFiscal.ValorIcms,
                                            valida = ntFiscal.valida
                                        });

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C170")
                                    {
                                        #region C170
                                        indItemNtFiscal = indItemNtFiscal + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        itemNtFiscal.ind = indItemNtFiscal;
                                        itemNtFiscal.IndNtFiscal = nota.ind;
                                        itemNtFiscal.ValorItem = (Convert.ToDouble(Linha[07]) - Convert.ToDouble(Linha[8])).ToString("N2");
                                        itemNtFiscal.Cfop = Convert.ToDouble(Linha[11]).ToString("N2").Replace(",00", "");
                                        itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[13]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[15]).ToString("N2").Replace(".", "");


                                        itensNtFiscal.Add(new ItemNotaFiscal
                                        {
                                            ind = itemNtFiscal.ind,
                                            IndNtFiscal = itemNtFiscal.IndNtFiscal,
                                            ValorItem = itemNtFiscal.ValorItem,
                                            Cfop = itemNtFiscal.Cfop,
                                            ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
                                            IcmsDebitado = itemNtFiscal.IcmsDebitado
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C190")
                                    {
                                        #region C190
                                        indRegAnalitico = indRegAnalitico + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        regAnalitico.ind = indRegAnalitico;
                                        regAnalitico.IndNtFiscal = nota.ind;
                                        regAnalitico.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");
                                        regAnalitico.ValorOperacao = Convert.ToDouble(Linha[5]).ToString("N2").Replace(".", "");
                                        regAnalitico.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
                                        regAnalitico.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");

                                        regsAnaliticos.Add(new RegistroAnalitico
                                        {
                                            ind = regAnalitico.ind,
                                            IndNtFiscal = regAnalitico.IndNtFiscal,
                                            Cfop = regAnalitico.Cfop,
                                            ValorOperacao = regAnalitico.ValorOperacao,
                                            ValorBaseCalculo = regAnalitico.ValorBaseCalculo,
                                            IcmsDebitado = regAnalitico.IcmsDebitado
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C500")
                                    {
                                        #region C500
                                        indNtFiscal = indNtFiscal + 1;
                                        ntFiscal.ind = indNtFiscal;
                                        ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.DtNotaSaida = Linha[11].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.TipoDocSaida = "07"; //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.SerieSaida = Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.NumDocSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        try
                                        {
                                            ntFiscal.ValorSai = Convert.ToDouble(Linha[13]).ToString("n2").Replace(".", ""); //ADICIONA NA CLASSE Nota Fiscal
                                        }
                                        catch
                                        {
                                            ntFiscal.ValorSai = "00"; //ADICIONA NA CLASSE Nota Fiscal
                                        }
                                        ntFiscal.ValorIcms = Linha[20].ToString();

                                        if (GetPeriodoNota(ntFiscal.DtNotaSaida, periodo))
                                            ntFiscal.valida = true;
                                        else
                                            ntFiscal.valida = false;


                                        //ADICIONA NA LISTA DE Notas Fiscais
                                        ntsFiscais.Add(new NotaFiscal
                                        {
                                            ind = ntFiscal.ind,
                                            TipoOper = ntFiscal.TipoOper,
                                            DtNotaSaida = ntFiscal.DtNotaSaida,
                                            TipoDocSaida = ntFiscal.TipoDocSaida,
                                            SerieSaida = ntFiscal.SerieSaida,
                                            NumDocSaida = ntFiscal.NumDocSaida,
                                            CodPart = ntFiscal.CodPart,
                                            ValorSai = ntFiscal.ValorSai,
                                            ValorIcms = ntFiscal.ValorIcms,
                                            valida = ntFiscal.valida
                                        });

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C590")
                                    {
                                        #region C590
                                        indItemNtFiscal = indItemNtFiscal + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        itemNtFiscal.ind = indItemNtFiscal;
                                        itemNtFiscal.IndNtFiscal = nota.ind;
                                        itemNtFiscal.ValorItem = Convert.ToDouble(Linha[05]).ToString("N2");
                                        itemNtFiscal.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");
                                        itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");

                                        itensNtFiscal.Add(new ItemNotaFiscal
                                        {
                                            ind = itemNtFiscal.ind,
                                            IndNtFiscal = itemNtFiscal.IndNtFiscal,
                                            ValorItem = itemNtFiscal.ValorItem,
                                            Cfop = itemNtFiscal.Cfop,
                                            ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
                                            IcmsDebitado = itemNtFiscal.IcmsDebitado
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else { b = linhaCount - 1; }
                                }
                            }

                            Participantes parts = new FiscalDigital.Participantes();
                            enderecos = parts.GetValueParticipante(ArqTxtClientes, participantes);
                            //int controlUF = 0;
                            //int controlCEP = 0;
                            //int controlRUA = 0;
                            //int controlNUM = 0;

                            foreach (Participante part in participantes)
                            {
                                NotaFiscal nt = ntsFiscais.Find(f => f.CodPart == part.CodPart);
                                EnderecoPart end = enderecos.Find(f => f.IndPart == part.ind && f.Cnpj == part.CnpjCpf);
                                if (nt != null)
                                {
                                    if (end != null)
                                    {
                                        #region Se endereço existir no arquivo
                                        Arquivo += "0150|";
                                        //COD_PART
                                        Arquivo += part.CodPart + "|";
                                        //NOME
                                        Arquivo += part.Nome + "|";
                                        //COD_PAIS
                                        Arquivo += (part.CodPais.Count() < 5 ? "0" + part.CodPais : part.CodPais) + "|";
                                        //CNPJ
                                        Arquivo += part.CnpjCpf + "|";
                                        //IE
                                        Arquivo += part.Ie + "|";

                                        string cnpjCpf = part.CnpjCpf;
                                        //UF
                                        if (String.IsNullOrEmpty(end.Uf))
                                        {
                                            //controlUF = controlUF + 1;
                                            Arquivo += "UF" + cnpjCpf.ToString() + "|";
                                            part.erroUF = cnpjCpf;
                                        }
                                        else
                                            Arquivo += end.Uf + "|";

                                        //CEP
                                        if (String.IsNullOrEmpty(end.Cep))
                                        {
                                            Arquivo += "CEP" + cnpjCpf.ToString() + "|";
                                            part.erroCEP = cnpjCpf;
                                        }
                                        else
                                            Arquivo += end.Cep + "|";

                                        //END
                                        if (String.IsNullOrEmpty(part.Endereco))
                                        {
                                            if (String.IsNullOrEmpty(end.Rua))
                                            {
                                                Arquivo += "RUA" + cnpjCpf.ToString() + "|";
                                                part.erroRUA = cnpjCpf;
                                            }
                                            else
                                                Arquivo += end.Rua + "|";
                                        }
                                        else
                                            Arquivo += part.Endereco + "|";

                                        //NUM
                                        if (String.IsNullOrEmpty(part.Numero))
                                        {
                                            Arquivo += "NUM" + cnpjCpf.ToString() + "|";
                                            part.erroNUM = cnpjCpf;
                                        }
                                        else
                                            Arquivo += part.Numero + "|";

                                        //COMPL
                                        Arquivo += part.Complemento + "|";
                                        //BAIRRO
                                        Arquivo += part.Bairro + "|";
                                        //COD_MUN
                                        Arquivo += part.CodMunicipal + "|";
                                        //FONE
                                        Arquivo += "";
                                        Arquivo += "\r\n";
                                        BlocoZero++;
                                        Column0150++;
                                        ArquivoTodo++;
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Se endereço não existir no arquivo
                                        Arquivo += "0150|";
                                        //COD_PART
                                        Arquivo += part.CodPart + "|";
                                        //NOME
                                        Arquivo += part.Nome + "|";
                                        //COD_PAIS
                                        Arquivo += (part.CodPais.Count() < 5 ? "0" + part.CodPais : part.CodPais) + "|";
                                        //CNPJ
                                        Arquivo += part.CnpjCpf + "|";
                                        //IE
                                        Arquivo += part.Ie + "|";

                                        string cnpjCpf = part.CnpjCpf;
                                        //UF
                                        Arquivo += "UF" + cnpjCpf.ToString() + "|";
                                        part.erroUF = cnpjCpf;
                                        //CEP
                                        Arquivo += "CEP" + cnpjCpf.ToString() + "|";
                                        part.erroCEP = cnpjCpf;
                                        
                                        //END
                                        if (String.IsNullOrEmpty(part.Endereco))
                                        {
                                            Arquivo += "RUA" + cnpjCpf.ToString() + "|";
                                            part.erroRUA = cnpjCpf;
                                        }
                                        else
                                            Arquivo += part.Endereco + "|";
                                        
                                        //NUM
                                        if (String.IsNullOrEmpty(part.Numero))
                                        {
                                            Arquivo += "NUM" + cnpjCpf.ToString() + "|";
                                            part.erroNUM = cnpjCpf;
                                        }
                                        else
                                            Arquivo += part.Numero + "|";

                                        //COMPL
                                        Arquivo += part.Complemento + "|";
                                        //BAIRRO
                                        Arquivo += part.Bairro + "|";
                                        //COD_MUN
                                        Arquivo += part.CodMunicipal + "|";
                                        //FONE
                                        Arquivo += "";
                                        Arquivo += "\r\n";
                                        BlocoZero++;
                                        Column0150++;
                                        ArquivoTodo++;
                                        #endregion
                                    }
                                }
                            }

                            #region Dados contribuinte como participante
                            Arquivo += "0150|";
                            //COD_PART
                            Arquivo += "000001" + "|";
                            //NOME
                            Arquivo += contribuinte.Nome + "|";
                            //COD_PAIS
                            Arquivo += (contribuinte.CodPais.Count() < 5 ? "0" + contribuinte.CodPais : contribuinte.CodPais) + "|";
                            //CNPJ
                            Arquivo += contribuinte.CNPJ + "|";
                            //IE
                            Arquivo += contribuinte.IE + "|";

                            //UF
                            Arquivo += contribuinte.UF + "|";

                            //CEP
                            Arquivo += contribuinte.CEP + "|";

                            //END
                            Arquivo += contribuinte.Endereco + "|";

                            //NUM
                            Arquivo += contribuinte.NUM + "|";

                            //COMPL
                            Arquivo += contribuinte.Complemento + "|";
                            //BAIRRO
                            Arquivo += contribuinte.Bairro + "|";
                            //COD_MUN
                            Arquivo += contribuinte.CodMun + "|";
                            //FONE
                            Arquivo += contribuinte.Fone;
                            Arquivo += "\r\n";
                            BlocoZero++;
                            Column0150++;
                            ArquivoTodo++;
                            #endregion

                            double vlrForCfop = 0;

                            #region FOREACH para Entradas e Saidas 
                            foreach (NotaFiscal nt in ntsFiscais)
                            {
                                if (nt.valida)
                                {
                                    lstItemNts.Clear();
                                    lstRegAnaNotas.Clear();
                                    foreach (ItemNotaFiscal intf in itensNtFiscal)
                                    {
                                        if (intf.IndNtFiscal == nt.ind)
                                        {
                                            if (ValidaCFOP(intf.Cfop.Replace(".", ""), 1))
                                            {
                                                lstItemNts.Add(new ItemNotaFiscal()
                                                    {
                                                        ind = intf.ind,
                                                        IndNtFiscal = intf.IndNtFiscal,
                                                        Cfop = intf.Cfop,
                                                        IcmsDebitado = intf.IcmsDebitado,
                                                        ValorBaseCalculo = intf.ValorBaseCalculo,
                                                        ValorItem = intf.ValorItem,
                                                        Aliquota = intf.Aliquota
                                                    });
                                            }
                                        }
                                    }
                                    if (lstItemNts.Count() != 0)
                                    {
                                        foreach (ItemNotaFiscal itemNt in lstItemNts)
                                        {
                                            string cfop = Convert.ToDouble(itemNt.Cfop).ToString("N").Replace(",00", "");

                                            if (VerificaTipoCFOP(cfop)) //Entrada
                                            {
                                                try
                                                {
                                                    ENTRADAS += Convert.ToDouble(itemNt.ValorItem);
                                                    vlrForCfop = GetValueForCFOP(cfop.Replace(".", ""), 7, itemNt.IcmsDebitado);
                                                    if (vlrForCfop != 0)
                                                        vlrImpos123 = vlrImpos123 + vlrForCfop;
                                                }
                                                catch { }
                                            }
                                            else //Saida
                                            {
                                                try
                                                {
                                                    SAIDAS += Convert.ToDouble(itemNt.ValorItem);
                                                    vlrForCfop = GetValueForCFOP(cfop.Replace(".", ""), 8, itemNt.IcmsDebitado);
                                                    if (vlrForCfop != 0)
                                                        vlrImpos567 = vlrImpos567 + vlrForCfop;
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                    foreach (RegistroAnalitico ra in regsAnaliticos)
                                    {
                                        if (ra.IndNtFiscal == nt.ind)
                                        {
                                            ItemNotaFiscal item = lstItemNts.Find(f => f.IndNtFiscal == ra.IndNtFiscal);
                                            if (item == null)
                                            {
                                                if (ValidaCFOP(ra.Cfop.Replace(".", ""), 1))
                                                {
                                                    lstRegAnaNotas.Add(new RegistroAnalitico()
                                                        {
                                                            ind = ra.ind,
                                                            IndNtFiscal = ra.IndNtFiscal,
                                                            Cfop = ra.Cfop,
                                                            IcmsDebitado = ra.IcmsDebitado,
                                                            ValorBaseCalculo = ra.ValorBaseCalculo,
                                                            ValorOperacao = ra.ValorOperacao,
                                                            Aliquota = ra.Aliquota
                                                        });
                                                }
                                            }
                                        }
                                    }
                                    if (lstRegAnaNotas.Count() != 0)
                                    {
                                        foreach (RegistroAnalitico ra in lstRegAnaNotas)
                                        {
                                            string cfop = Convert.ToDouble(ra.Cfop).ToString("N").Replace(",00", "");

                                            if (!VerificaTipoCFOP(cfop)) //Saidas
                                            {
                                                try
                                                {
                                                    SAIDAS += Convert.ToDouble(ra.ValorOperacao);
                                                    vlrForCfop = GetValueForCFOP(cfop.Replace(".", ""), 8, ra.IcmsDebitado);
                                                    if (vlrForCfop != 0)
                                                        vlrImpos567 = vlrImpos567 + vlrForCfop;
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    lstItemNts.Clear();
                                    lstRegAnaNotas.Clear();
                                    lstItemNts = itensNtFiscal.FindAll(f => f.IndNtFiscal == nt.ind);
                                    if (lstItemNts.Count() >= 1)
                                    {
                                        foreach (ItemNotaFiscal item in lstItemNts)
                                        {
                                            ItemNotaFiscal itemLst = itensNtFiscal.Find(f => f.ind == item.ind);
                                            itensNtFiscal.Remove(itemLst);
                                        }
                                    }
                                    else
                                    {
                                        lstRegAnaNotas = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);
                                        if (lstRegAnaNotas.Count() >= 1)
                                        {
                                            foreach (RegistroAnalitico ra in lstRegAnaNotas)
                                            {
                                                RegistroAnalitico raLst = regsAnaliticos.Find(f => f.ind == ra.ind);
                                                regsAnaliticos.Remove(raLst);
                                            }
                                        }
                                    }
                                }
                            }
                            List<NotaFiscal> notasInvalidas = ntsFiscais.FindAll(f => !f.valida);
                            if (notasInvalidas.Count >= 1)
                            {
                                foreach (NotaFiscal nt in notasInvalidas)
                                {
                                    NotaFiscal ntLst = ntsFiscais.Find(f => f.ind == nt.ind);
                                    ntsFiscais.Remove(ntLst);
                                }
                            }
                            #endregion
                            //participantes.Clear();
                            lstItemNts.Clear();
                            lstRegAnaNotas.Clear();
                            regsAnaliticos.Clear();
                            ntsFiscais.Clear();
                            y = lineCount - 1;
                            Controle0150 = true;
                        }
                    }
                    #endregion
                    #region FECHAMENO DO BLOCO 0
                    //fechamento do bloco 0
                    else if (Line[1].ToString() == "0990")
                    {
                        #region 0300 Temporário
                        //Inciso I - Operações interestaduais com alíquota 7%
                        //Quando a Aliquota do item for de 7% e for insterestadual usar o '0001'
                        Arquivo += "0300|";
                        Arquivo += "0001|01||vazio||||||";
                        Arquivo += "\r\n";
                        BlocoZero++;
                        Column0300++;
                        ArquivoTodo++;

                        //Inciso I - Operações interestaduais com alíquota 12%
                        //Quando a Aliquota do item for de 12% e for insterestadual usar o '0002'
                        Arquivo += "0300|";
                        Arquivo += "0002|02||vazio||||||";
                        Arquivo += "\r\n";
                        BlocoZero++;
                        Column0300++;
                        ArquivoTodo++;

                        //Inciso III - Saídas sem pagamento de imposto - Diferimento
                        //Quando a aliquota for de 0% usar o '0010'
                        Arquivo += "0300|";
                        Arquivo += "0010|10||vazio||||||";
                        Arquivo += "\r\n";
                        BlocoZero++;
                        Column0300++;
                        ArquivoTodo++;

                        y = lineCount - 1;
                        #endregion

                        Arquivo += "0990|";
                        //LINHAS BLOCO 0
                        BlocoZero++;
                        //QTD_LIN_0
                        Arquivo += BlocoZero.ToString();
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        Column0990++;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region ABERTURA DO BLOCO 5
                    //abertura do bloco 5
                    else if (Line[1].ToString() == "C001")
                    {
                        Arquivo += "5001|";
                        //IND_MOV
                        Arquivo += Line[2].ToString();
                        Arquivo += "\r\n";
                        BlocoCinco++;
                        y = lineCount - 1;
                        Column5001++;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region CALCULOS MONSTROS E CONFUSOS
                    else if (Line[1].ToString() == "C100")
                    {
                        //Se já passou uma vez pula para a próxima
                        if (!ControleC100)
                        {
                            indNtFiscal = 0;
                            ntsFiscais.Clear();
                            indItemNtFiscal = 0;
                            itensNtFiscal.Clear();
                            indRegAnalitico = 0;
                            regsAnaliticos.Clear();
                            //Lê o arquivo pra procurar dados específicos do bloco 5
                            for (int a = 0; a < ArqTxtDigital.Count(); a++)
                            {
                                String[] Linha = ArqTxtDigital[a].Split(new String[] { "|" }, StringSplitOptions.None);
                                int linhaCount = Linha.Count() - 1;
                                for (int b = 0; b < linhaCount; b++)
                                {
                                    string Campo = Linha[b].ToString();

                                    #region Código C100, C170 e C190
                                    if (Linha[1].ToString() == "C100")
                                    {
                                        #region C100
                                        //TIP_DOC INVÁLIDOS
                                        if (Linha[5].ToString() != "29" ||
                                            Linha[5].ToString() != "59" ||
                                            Linha[5].ToString() != "60" ||
                                            Linha[5].ToString() != "65")
                                        {
                                            indNtFiscal = indNtFiscal + 1;
                                            ntFiscal.ind = indNtFiscal;
                                            ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
                                            ntFiscal.DtNotaSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                            ntFiscal.TipoDocSaida = GetTipoDocumento(Linha[5].ToString()); //ADICIONA NA CLASSE Nota Fiscal
                                            ntFiscal.SerieSaida = Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                            ntFiscal.NumDocSaida = Linha[8].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                            ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                            try
                                            {
                                                ntFiscal.ValorSai = Convert.ToDouble(Linha[12]).ToString("n2").Replace(".", ""); //ADICIONA NA CLASSE Nota Fiscal
                                            }
                                            catch
                                            {
                                                ntFiscal.ValorSai = "00"; //ADICIONA NA CLASSE Nota Fiscal
                                            }
                                            ntFiscal.ValorIcms = Linha[22].ToString();

                                            if (GetPeriodoNota(ntFiscal.DtNotaSaida, periodo))
                                                ntFiscal.valida = true;
                                            else
                                                ntFiscal.valida = false;


                                            //ADICIONA NA LISTA DE Notas Fiscais
                                            ntsFiscais.Add(new NotaFiscal
                                            {
                                                ind = ntFiscal.ind,
                                                TipoOper = ntFiscal.TipoOper,
                                                DtNotaSaida = ntFiscal.DtNotaSaida,
                                                TipoDocSaida = ntFiscal.TipoDocSaida,
                                                SerieSaida = ntFiscal.SerieSaida,
                                                NumDocSaida = ntFiscal.NumDocSaida,
                                                CodPart = ntFiscal.CodPart,
                                                ValorSai = ntFiscal.ValorSai,
                                                ValorIcms = ntFiscal.ValorIcms,
                                                valida = ntFiscal.valida
                                            });

                                            b = linhaCount - 1;
                                        }
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C170")
                                    {
                                        #region C170
                                        indItemNtFiscal = indItemNtFiscal + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        itemNtFiscal.ind = indItemNtFiscal;
                                        itemNtFiscal.IndNtFiscal = nota.ind;
                                        itemNtFiscal.ValorItem = (Convert.ToDouble(Linha[07]) - Convert.ToDouble(Linha[8])).ToString("N2");
                                        itemNtFiscal.Cfop = Convert.ToDouble(Linha[11]).ToString("N2").Replace(",00", "");
                                        itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[13]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[15]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.Aliquota = Convert.ToDouble(Linha[14]).ToString("N6").Replace(".", "");

                                        itensNtFiscal.Add(new ItemNotaFiscal
                                        {
                                            ind = itemNtFiscal.ind,
                                            IndNtFiscal = itemNtFiscal.IndNtFiscal,
                                            ValorItem = itemNtFiscal.ValorItem,
                                            Cfop = itemNtFiscal.Cfop,
                                            ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
                                            IcmsDebitado = itemNtFiscal.IcmsDebitado,
                                            Aliquota = itemNtFiscal.Aliquota
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C190")
                                    {
                                        #region C190
                                        indRegAnalitico = indRegAnalitico + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        regAnalitico.ind = indRegAnalitico;
                                        regAnalitico.IndNtFiscal = nota.ind;
                                        regAnalitico.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
                                        regAnalitico.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");
                                        regAnalitico.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");
                                        regAnalitico.ValorOperacao = Convert.ToDouble(Linha[5]).ToString("N2").Replace(",00","");
                                        regAnalitico.Aliquota = Convert.ToDouble(Linha[4]).ToString("N6").Replace(".", "");

                                        regsAnaliticos.Add(new RegistroAnalitico
                                        {
                                            ind = regAnalitico.ind,
                                            IndNtFiscal = regAnalitico.IndNtFiscal,
                                            ValorBaseCalculo = regAnalitico.ValorBaseCalculo,
                                            IcmsDebitado = regAnalitico.IcmsDebitado,
                                            Cfop = regAnalitico.Cfop,
                                            ValorOperacao = regAnalitico.ValorOperacao,
                                            Aliquota = regAnalitico.Aliquota
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C500")
                                    {
                                        #region C500
                                        indNtFiscal = indNtFiscal + 1;
                                        ntFiscal.ind = indNtFiscal;
                                        ntFiscal.TipoOper = Convert.ToInt32(Linha[2]); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.DtNotaSaida = Linha[11].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.TipoDocSaida = "07"; //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.SerieSaida = String.IsNullOrEmpty(Linha[7].ToString()) ? "1" : Linha[7].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.NumDocSaida = Linha[10].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        ntFiscal.CodPart = Linha[4].ToString(); //ADICIONA NA CLASSE Nota Fiscal
                                        try
                                        {
                                            ntFiscal.ValorSai = Convert.ToDouble(Linha[13]).ToString("n2").Replace(".", ""); //ADICIONA NA CLASSE Nota Fiscal
                                        }
                                        catch
                                        {
                                            ntFiscal.ValorSai = "00"; //ADICIONA NA CLASSE Nota Fiscal
                                        }
                                        ntFiscal.ValorIcms = Linha[20].ToString();

                                        if (GetPeriodoNota(ntFiscal.DtNotaSaida, periodo))
                                            ntFiscal.valida = true;
                                        else
                                            ntFiscal.valida = false;


                                        //ADICIONA NA LISTA DE Notas Fiscais
                                        ntsFiscais.Add(new NotaFiscal
                                        {
                                            ind = ntFiscal.ind,
                                            TipoOper = ntFiscal.TipoOper,
                                            DtNotaSaida = ntFiscal.DtNotaSaida,
                                            TipoDocSaida = ntFiscal.TipoDocSaida,
                                            SerieSaida = ntFiscal.SerieSaida,
                                            NumDocSaida = ntFiscal.NumDocSaida,
                                            CodPart = ntFiscal.CodPart,
                                            ValorSai = ntFiscal.ValorSai,
                                            ValorIcms = ntFiscal.ValorIcms,
                                            valida = ntFiscal.valida
                                        });

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    else if (Linha[1].ToString() == "C590")
                                    {
                                        #region C590
                                        indItemNtFiscal = indItemNtFiscal + 1;
                                        NotaFiscal nota = ntsFiscais.Find(f => f.ind == indNtFiscal);
                                        itemNtFiscal.ind = indItemNtFiscal;
                                        itemNtFiscal.IndNtFiscal = nota.ind;
                                        itemNtFiscal.ValorItem = Convert.ToDouble(Linha[05]).ToString("N2");
                                        itemNtFiscal.Cfop = Convert.ToDouble(Linha[3]).ToString("N2").Replace(",00", "");
                                        itemNtFiscal.ValorBaseCalculo = Convert.ToDouble(Linha[6]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.IcmsDebitado = Convert.ToDouble(Linha[7]).ToString("N2").Replace(".", "");
                                        itemNtFiscal.Aliquota = Convert.ToDouble(Linha[4]).ToString("N6").Replace(".", "");

                                        itensNtFiscal.Add(new ItemNotaFiscal
                                        {
                                            ind = itemNtFiscal.ind,
                                            IndNtFiscal = itemNtFiscal.IndNtFiscal,
                                            ValorItem = itemNtFiscal.ValorItem,
                                            Cfop = itemNtFiscal.Cfop,
                                            ValorBaseCalculo = itemNtFiscal.ValorBaseCalculo,
                                            IcmsDebitado = itemNtFiscal.IcmsDebitado,
                                            Aliquota = itemNtFiscal.Aliquota
                                        });

                                        countNotas++;

                                        b = linhaCount - 1;
                                        #endregion
                                    }
                                    #endregion
                                    else
                                        b = linhaCount - 1;
                                }
                            }
                            foreach (NotaFiscal nt in ntsFiscais)
                            {
                                if (!nt.valida)
                                {
                                    lstItemNts.Clear();
                                    lstRegAnaNotas.Clear();
                                    lstItemNts = itensNtFiscal.FindAll(f => f.IndNtFiscal == nt.ind);
                                    if (lstItemNts.Count() >= 1)
                                    {
                                        foreach (ItemNotaFiscal item in lstItemNts)
                                        {
                                            ItemNotaFiscal itemLst = itensNtFiscal.Find(f => f.ind == item.ind);
                                            itensNtFiscal.Remove(itemLst);
                                        }
                                    }
                                    else
                                    {
                                        lstRegAnaNotas = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);
                                        if (lstRegAnaNotas.Count() >= 1)
                                        {
                                            foreach (RegistroAnalitico ra in lstRegAnaNotas)
                                            {
                                                RegistroAnalitico raLst = regsAnaliticos.Find(f => f.ind == ra.ind);
                                                regsAnaliticos.Remove(raLst);
                                            }
                                        }
                                    }
                                }
                            }
                            List<NotaFiscal> notasInvalidas = ntsFiscais.FindAll(f => f.valida == false);
                            if (notasInvalidas.Count >= 1)
                            {
                                foreach (NotaFiscal nt in notasInvalidas)
                                {
                                    NotaFiscal ntLst = ntsFiscais.Find(f => f.ind == nt.ind);
                                    ntsFiscais.Remove(ntLst);
                                }
                            }

                            PMC = Convert.ToDouble((((vlrImpos123 - vlrImpos567) / ENTRADAS) * 100).ToString("N4"));
                            IVA = Convert.ToDouble(((SAIDAS - ENTRADAS) / ENTRADAS).ToString("N4"));
                            if (IVA < IvaMedianaDouble)
                                Iva = IVA = IvaMedianaDouble;
                            else
                                Iva = IVA;

                            Saidas = SAIDAS;
                            Entradas = ENTRADAS;
                            Pmc = PMC;
                            //double CreditoEstimado = 0;
                            double CreditoAcumulado = 0;
                            double CustoEstimado = 0;
                            double ImpostoCreditado = 0;

                            foreach (NotaFiscal nt in ntsFiscais)
                            {
                                if (GetPeriodoNota(nt.DtNotaSaida, periodo))
                                {
                                    double valorSaida = 0;
                                    double icmsDebitado = 0;
                                    double valorBaseCalculo = 0;
                                    bool campo5315 = false;
                                    bool cfopValid = false;
                                    bool aliquota01 = false;
                                    bool aliquota02 = false;
                                    bool aliquota10 = false;
                                    bool aliquotaControle = true;
                                    //bool cfop1403 = false;

                                    lstItemNts.Clear();
                                    lstRegAnaNotas.Clear();

                                    lstRegAnaNotas = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);

                                    if (lstRegAnaNotas.Count() >= 1)
                                    {
                                        valorSaida = 0;
                                        icmsDebitado = 0;
                                        //CreditoEstimado = 0;
                                        CreditoAcumulado = 0;
                                        CustoEstimado = 0;
                                        ImpostoCreditado = 0;
                                        foreach (RegistroAnalitico ra in lstRegAnaNotas)
                                        {
                                            //if (!VerificaTipoCFOP(ra.Cfop))
                                            //{
                                                try
                                                {
                                                    string cfop = ra.Cfop.Replace(".", "");
                                                    valorSaida = valorSaida + Convert.ToDouble(ra.ValorOperacao);
                                                    icmsDebitado = icmsDebitado + Convert.ToDouble(ra.IcmsDebitado);
                                                    valorBaseCalculo = valorBaseCalculo + Convert.ToDouble(ra.ValorBaseCalculo);

                                                    double ali = Convert.ToDouble(ra.Aliquota);
                                                    double impDebi = Convert.ToDouble(ra.IcmsDebitado);
                                                    double vlrOpe = Convert.ToDouble(ra.ValorOperacao);
                                                    double impCalc = (vlrOpe * (ali / 100));

                                                    bool ok = false;
                                                    //Imposto Lido = Imposto gerado
                                                    if ((impCalc.ToString("N2").Replace(".", "") == ra.IcmsDebitado))
                                                        ok = false;
                                                    else if ((impCalc >= impDebi - 1) && (impCalc <= impDebi + 1))
                                                        ok = false;
                                                    else
                                                        ok = true;
                                                    #region Codigo Anterior (Comentado)
                                                    //else
                                                    //{
                                                    //    double iAli = 0;
                                                    //    double impCalcAux = impCalc;
                                                    //    bool sair = false;
                                                    //    #region Imposto com diferença em até 1 pra menos
                                                    //    do
                                                    //    {
                                                    //        impCalcAux = impCalcAux - 0.01;
                                                    //        if (impCalcAux.ToString("N2").Replace(".", "") == ra.IcmsDebitado)
                                                    //            ok = prox = false;
                                                    //        else
                                                    //        {
                                                    //            ok = prox = true;
                                                    //            iAli = iAli + 0.01;
                                                    //            if (iAli >= 1)
                                                    //                sair = true;
                                                    //        }
                                                    //    } while ((ok && prox) && !sair);
                                                    //    #endregion

                                                    //    if (ok && prox)
                                                    //    {
                                                    //        iAli = 0;
                                                    //        sair = false;
                                                    //        impCalcAux = impCalc;
                                                    //        #region Imposto com diferença em até 1 pra mais
                                                    //        do
                                                    //        {
                                                    //            impCalcAux = impCalcAux + 0.01;
                                                    //            if (impCalcAux.ToString("N2").Replace(".", "") == ra.IcmsDebitado)
                                                    //                ok = prox = false;
                                                    //            else
                                                    //            {
                                                    //                ok = prox = true;
                                                    //                iAli = iAli + 0.01;
                                                    //                if (iAli >= 1)
                                                    //                    sair = true;
                                                    //            }
                                                    //        } while ((ok && prox) && !sair);
                                                    //        #endregion
                                                    //    }
                                                    //}
                                                    #endregion

                                                    //Se tiver difereça coloca na lista
                                                    if (ok)
                                                    {
                                                        lstRegAnaDivergentes.Add(new RegistroAnalitico()
                                                        {
                                                            ind = ra.ind,
                                                            IndNtFiscal = ra.IndNtFiscal,
                                                            ValorBaseCalculo = ra.ValorBaseCalculo,
                                                            ValorOperacao = ra.ValorOperacao,
                                                            Aliquota = ra.Aliquota,
                                                            Cfop = ra.Cfop,
                                                            IcmsDebitado = ra.IcmsDebitado
                                                        });
                                                    }

                                                    if (aliquotaControle)
                                                    {
                                                        if (ali == 7)
                                                            aliquota01 = true;
                                                        else if (ali == 12)
                                                            aliquota02 = true;
                                                        else
                                                            aliquota10 = true;

                                                        if (aliquota01 || aliquota02 || aliquota10)
                                                            aliquotaControle = false;
                                                    }
                                                }
                                                catch { }

                                                if (ValidaCFOP(ra.Cfop.Replace(".", ""), 1))
                                                    cfopValid = campo5315 = true;
                                            //}
                                        }

                                        CustoEstimado = Convert.ToDouble((CustoEstimado + (valorSaida / (1 + IVA))).ToString("N2"));
                                        ImpostoCreditado = CustoEstimado * (PMC / 100);
                                        if (cfopValid)// && (cfop1403 || icmsDebitado != 0))
                                        {
                                            CreditoAcumulado = ImpostoCreditado - icmsDebitado;
                                            CreditoAcumuladoTotal = CreditoAcumuladoTotal + CreditoAcumulado;
                                            if (CreditoAcumulado > 0)
                                                campo5315 = true;
                                            else
                                                campo5315 = false;
                                        }
                                        else
                                        {
                                            ImpostoCreditado = 0;
                                            CreditoAcumulado = 0;
                                        }

                                        #region Escreve dados nota
                                        nt.CreditoEstimado = ImpostoCreditado.ToString("N2").Replace(".", "");
                                        nt.CreditoGerado = CreditoAcumulado.ToString("N2").Replace(".", "");

                                        #region 5315
                                        Arquivo += "5315|";
                                        //DT_EMISSAO
                                        Arquivo += nt.DtNotaSaida + "|";
                                        //TIP_DOC
                                        Arquivo += nt.TipoDocSaida + "|";
                                        //SER
                                        Arquivo += nt.SerieSaida + "|";
                                        //NUM_DOC
                                        Arquivo += nt.NumDocSaida + "|";
                                        //COD_PART
                                        Arquivo += nt.CodPart + "|";
                                        //VALOR_SAI
                                        Arquivo += Convert.ToDouble(nt.ValorSai).ToString("N2").Replace(".", "") + "|";
                                        //PERC_CRDOUT
                                        Arquivo += nt.PercCrdout + "|";
                                        //VALOR_CRDOUT
                                        Arquivo += nt.ValorCrdout + "";
                                        Arquivo += "\r\n";
                                        BlocoCinco++;
                                        Column5315++;
                                        ArquivoTodo++;
                                        #endregion

                                        if (campo5315)
                                        {
                                            #region Devolução (Comentado)
                                            //if (ValidaCFOP(cfop, 2))
                                            //{
                                            //    #region 5320
                                            //    Arquivo += "5320|";
                                            //    //DT_SAI
                                            //    Arquivo += nt.DtNotaSaida + "|";
                                            //    //TIP_DOC
                                            //    Arquivo += nt.TipoDocSaida + "|";
                                            //    //SER
                                            //    Arquivo += nt.SerieSaida + "|";
                                            //    //NUM_DOC
                                            //    Arquivo += nt.NumDocSaida;
                                            //    Arquivo += "\r\n";
                                            //    BlocoCinco++;
                                            //    Column5320++;
                                            //    ArquivoTodo++;
                                            //    #endregion
                                            //}
                                            #endregion

                                            #region 5325
                                            if (aliquota01)//aliquota de 7%
                                                Arquivo += "5325|0001|";
                                            if (aliquota02)//aliquota de 12%
                                                Arquivo += "5325|0002|";
                                            if (aliquota10)//diferimento
                                                Arquivo += "5325|0010|";
                                            Arquivo += IVA.ToString("n4").Replace(".", "") + "|";
                                            Arquivo += PMC.ToString("n4").Replace(".", "") + "|";
                                            Arquivo += nt.CreditoEstimado + "|";
                                            Arquivo += nt.CreditoGerado;
                                            Arquivo += "\r\n";
                                            BlocoCinco++;
                                            Column5325++;
                                            ArquivoTodo++;
                                            #endregion

                                            if (aliquota01 || aliquota02 || aliquota10)
                                            {
                                                #region 5330
                                                Arquivo += "5330|";
                                                //VALOR_BC
                                                Arquivo += valorBaseCalculo.ToString("N2").Replace(".", "") + "|";
                                                //ICMS_DEB
                                                Arquivo += icmsDebitado.ToString("N2").Replace(".", "");
                                                Arquivo += "\r\n";
                                                BlocoCinco++;
                                                Column5330++;
                                                ArquivoTodo++;
                                                #endregion
                                            }
                                            campo5315 = false;
                                            cfopValid = false;
                                        }
                                        else
                                        {
                                            #region 5350
                                            Arquivo += "5350|";
                                            //VALOR_BC
                                            Arquivo += valorBaseCalculo.ToString("N2").Replace(".", "") + "|";
                                            //ICMS_DEB
                                            Arquivo += icmsDebitado.ToString("N2").Replace(".", "") + "\r\n";
                                            BlocoCinco++;
                                            Column5350++;
                                            ArquivoTodo++;
                                            #endregion
                                        }

                                        #endregion
                                        Arquivo = EscreveArquivo(Arquivo, ArqValueDigital);
                                    }
                                    #region Código anterior(comentado)
                                    //lstItemNts = itensNtFiscal.FindAll(f => f.IndNtFiscal == nt.ind);
                                    //if (lstItemNts.Count() >= 1)
                                    //{
                                    //    valorSaida = 0;
                                    //    icmsDebitado = 0;
                                    //    CreditoEstimado = 0;
                                    //    CreditoAcumulado = 0;
                                    //    foreach (ItemNotaFiscal item in lstItemNts)
                                    //    {
                                    //        if (!VerificaTipoCFOP(item.Cfop))
                                    //        {
                                    //            try
                                    //            {
                                    //                string cfop = item.Cfop.Replace(".", "");
                                    //                valorSaida = valorSaida + Convert.ToDouble(item.ValorItem);
                                    //                icmsDebitado = icmsDebitado + Convert.ToDouble(item.IcmsDebitado);
                                    //                valorBaseCalculo = valorBaseCalculo + Convert.ToDouble(item.ValorBaseCalculo);

                                    //                //if (item.Cfop == "1.403")
                                    //                //    cfop1403 = true;
                                    //                if (ValidaCFOP(cfop, 1))
                                    //                    campo5315 = true;
                                    //            }
                                    //            catch { }

                                    //            cfopValid = true;
                                    //        }
                                    //    }

                                    //    if (campo5315)
                                    //    {
                                    //        CreditoEstimado = Convert.ToDouble((CreditoEstimado + (valorSaida / (1 + IVA))).ToString("N2"));
                                    //        if (cfopValid)// && (cfop1403 || icmsDebitado != 0))
                                    //        {
                                    //            CreditoAcumulado = CreditoEstimado - icmsDebitado;
                                    //            CreditoAcumuladoTotal = CreditoAcumuladoTotal + CreditoAcumulado;
                                    //        }
                                    //        else
                                    //        {
                                    //            CreditoEstimado = 0;
                                    //            CreditoAcumulado = 0;
                                    //        }

                                    //        #region Escreve dados nota

                                    //        //Verificar se existirá hierarquia certa
                                    //        //Para existir o campo 5315 tem q existir pelo menos um campo 5320 ou 5325. O campo 5330 é obrigatório na presença do 5315

                                    //        nt.CreditoEstimado = CreditoEstimado.ToString();
                                    //        nt.CreditoGerado = CreditoAcumulado.ToString();

                                    //        #region 5315
                                    //        Arquivo += "5315|";
                                    //        //DT_EMISSAO
                                    //        Arquivo += nt.DtNotaSaida + "|";
                                    //        //TIP_DOC
                                    //        Arquivo += nt.TipoDocSaida + "|";
                                    //        //SER
                                    //        Arquivo += nt.SerieSaida + "|";
                                    //        //NUM_DOC
                                    //        Arquivo += nt.NumDocSaida + "|";
                                    //        //COD_PART
                                    //        Arquivo += nt.CodPart + "|";
                                    //        //VALOR_SAI
                                    //        Arquivo += Convert.ToDouble(nt.ValorSai).ToString("N2").Replace(".", "") + "|";
                                    //        //PERC_CRDOUT
                                    //        Arquivo += nt.PercCrdout + "|";
                                    //        //VALOR_CRDOUT
                                    //        Arquivo += nt.ValorCrdout + "";
                                    //        Arquivo += "\r\n";
                                    //        BlocoCinco++;
                                    //        Column5315++;
                                    //        ArquivoTodo++;
                                    //        #endregion

                                    //        #region Devolução (Comentado)
                                    //        //if (ValidaCFOP(cfop, 2))
                                    //        //{
                                    //        //    #region 5320
                                    //        //    Arquivo += "5320|";
                                    //        //    //DT_SAI
                                    //        //    Arquivo += nt.DtNotaSaida + "|";
                                    //        //    //TIP_DOC
                                    //        //    Arquivo += nt.TipoDocSaida + "|";
                                    //        //    //SER
                                    //        //    Arquivo += nt.SerieSaida + "|";
                                    //        //    //NUM_DOC
                                    //        //    Arquivo += nt.NumDocSaida;
                                    //        //    Arquivo += "\r\n";
                                    //        //    BlocoCinco++;
                                    //        //    Column5320++;
                                    //        //    ArquivoTodo++;
                                    //        //    #endregion
                                    //        //}
                                    //        #endregion

                                    //        #region 5325
                                    //        Arquivo += "5325|0000|";
                                    //        Arquivo += IVA.ToString("n4").Replace(".", "") + "|";
                                    //        Arquivo += PMC.ToString("n4").Replace(".", "") + "|";
                                    //        Arquivo += CreditoEstimado.ToString("n2").Replace(".", "") + "|";
                                    //        Arquivo += CreditoAcumulado.ToString("n2").Replace(".", "");
                                    //        Arquivo += "\r\n";
                                    //        BlocoCinco++;
                                    //        Column5325++;
                                    //        ArquivoTodo++;
                                    //        #endregion

                                    //        #region 5330
                                    //        Arquivo += "5330|";
                                    //        //VALOR_BC
                                    //        Arquivo += valorBaseCalculo.ToString("N2").Replace(".","") + "|";
                                    //        //ICMS_DEB
                                    //        Arquivo += icmsDebitado.ToString("N2").Replace(".", "");
                                    //        Arquivo += "\r\n";
                                    //        BlocoCinco++;
                                    //        Column5330++;
                                    //        ArquivoTodo++;
                                    //        #endregion
                                    //        campo5315 = false;
                                    //        cfopValid = false;
                                    //        #endregion
                                    //    }
                                    //    Arquivo = EscreveArquivo(Arquivo, ArqValueDigital);
                                    //    i = 0;
                                    //}
                                    //#region Registro Analitico Nota (Comentado)
                                    //else
                                    //{
                                    //    lstRegAnaNotas = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);
                                    //    if (lstRegAnaNotas.Count() >= 1)
                                    //    {
                                    //        valorSaida = 0;
                                    //        icmsDebitado = 0;
                                    //        CreditoEstimado = 0;
                                    //        CreditoAcumulado = 0;
                                    //        foreach (RegistroAnalitico ra in lstRegAnaNotas)
                                    //        {
                                    //            i++;
                                    //            string cfop = ra.Cfop.Replace(".", "");
                                    //            if (VerificaTipoCFOP(ra.Cfop))
                                    //            {
                                    //                try
                                    //                {
                                    //                    if (!String.IsNullOrEmpty(ra.ValorOperacao) && !String.IsNullOrEmpty(ra.IcmsDebitado))
                                    //                    {
                                    //                        valorSaida = Convert.ToDouble(ra.ValorOperacao);
                                    //                        icmsDebitado = icmsDebitado + Convert.ToDouble(ra.IcmsDebitado);
                                    //                    }
                                    //                }
                                    //                catch { }

                                    //                cfopValid = true;
                                    //            }

                                    //            if (i == lstRegAnaNotas.Count())
                                    //            {
                                    //                CreditoEstimado = Convert.ToDouble((CreditoEstimado + (valorSaida / (1 + IVA))).ToString("N2"));
                                    //                if (campo5315)
                                    //                {
                                    //                    //if (ValidaCFOP(cfop, 1))
                                    //                    //{
                                    //                    double icmsDebit = Convert.ToDouble(nt.ValorIcms);
                                    //                    if (cfopValid && (ra.Cfop == "1.403" || icmsDebitado != 0))
                                    //                    {
                                    //                        CreditoAcumulado = CreditoEstimado - icmsDebit;
                                    //                        CreditoAcumuladoTotal = CreditoAcumuladoTotal + CreditoAcumulado;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        CreditoEstimado = 0;
                                    //                        CreditoAcumulado = 0;
                                    //                    }

                                    //                    #region Escreve dados nota

                                    //                    nt.CreditoEstimado = CreditoEstimado.ToString();
                                    //                    nt.CreditoGerado = CreditoAcumulado.ToString();

                                    //                    #region 5315
                                    //                    Arquivo += "5315|";
                                    //                    //DT_EMISSAO
                                    //                    Arquivo += nt.DtNotaSaida + "|";
                                    //                    //TIP_DOC
                                    //                    Arquivo += nt.TipoDocSaida + "|";
                                    //                    //SER
                                    //                    Arquivo += nt.SerieSaida + "|";
                                    //                    //NUM_DOC
                                    //                    Arquivo += nt.NumDocSaida + "|";
                                    //                    //COD_PART
                                    //                    Arquivo += nt.CodPart + "|";
                                    //                    //VALOR_SAI
                                    //                    Arquivo += valorSaida.ToString("n2").Replace(".", "") + "|";
                                    //                    //PERC_CRDOUT
                                    //                    Arquivo += nt.PercCrdout + "|";
                                    //                    //VALOR_CRDOUT
                                    //                    Arquivo += nt.ValorCrdout + "";
                                    //                    Arquivo += "\r\n";
                                    //                    BlocoCinco++;
                                    //                    Column5315++;
                                    //                    ArquivoTodo++;
                                    //                    #endregion

                                    //                    #region Devolução (Comentado)
                                    //                    //if (ValidaCFOP(cfop, 2))
                                    //                    //{
                                    //                    //    #region 5320
                                    //                    //    Arquivo += "5320|";
                                    //                    //    //DT_SAI
                                    //                    //    Arquivo += nt.DtNotaSaida + "|";
                                    //                    //    //TIP_DOC
                                    //                    //    Arquivo += nt.TipoDocSaida + "|";
                                    //                    //    //SER
                                    //                    //    Arquivo += nt.SerieSaida + "|";
                                    //                    //    //NUM_DOC
                                    //                    //    Arquivo += nt.NumDocSaida;
                                    //                    //    Arquivo += "\r\n";
                                    //                    //    BlocoCinco++;
                                    //                    //    Column5320++;
                                    //                    //    ArquivoTodo++;
                                    //                    //    #endregion
                                    //                    //}
                                    //                    #endregion

                                    //                    #region 5325
                                    //                    Arquivo += "5325|0000|";
                                    //                    Arquivo += IVA.ToString("n4").Replace(".", "") + "|";
                                    //                    Arquivo += PMC.ToString("n4").Replace(".", "") + "|";
                                    //                    Arquivo += CreditoEstimado.ToString("n2").Replace(".", "") + "|";
                                    //                    Arquivo += CreditoAcumulado.ToString("n2").Replace(".", "");
                                    //                    Arquivo += "\r\n";
                                    //                    BlocoCinco++;
                                    //                    Column5325++;
                                    //                    ArquivoTodo++;
                                    //                    #endregion

                                    //                    #region 5330
                                    //                    Arquivo += "5330|";
                                    //                    //VALOR_BC
                                    //                    Arquivo += ra.ValorBaseCalculo + "|";
                                    //                    //ICMS_DEB
                                    //                    Arquivo += ra.IcmsDebitado;
                                    //                    Arquivo += "\r\n";
                                    //                    BlocoCinco++;
                                    //                    Column5330++;
                                    //                    ArquivoTodo++;
                                    //                    #endregion

                                    //                    campo5315 = false;
                                    //                    cfopValid = false;
                                    //                    //}
                                    //                    #endregion
                                    //                }
                                    //                Arquivo = EscreveArquivo(Arquivo, ArqValueDigital);
                                    //                i = 0;
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    #endregion
                                }
                            }
                            y = lineCount - 1;
                            ControleC100 = true;
                        }
                    }
                    #endregion
                    #region FECHAMENTO DO BLOCO 5
                    else if (Line[1].ToString() == "C990")
                    {
                        Arquivo += "5990|";
                        //LINHAS BLOCO 5
                        BlocoCinco++;
                        //QTD_LIN_C
                        Arquivo += BlocoCinco.ToString();
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        Column5990++;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region ABERTURA DO BLOCO 9
                    else if (Line[1].ToString() == "9001")
                    {
                        Arquivo += "9001|";
                        //IND_MOV
                        Arquivo += "0";
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        Column9001++;
                        BlocoNove++;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region CALCULOS MONSTROS DO BLOCO 9
                    else if (Line[1].ToString() == "9900" && controle == false)
                    {
                        #region 9900
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|0000|" + Column0000.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|0001|" + Column0001.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|0150|" + Column0150.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|0300|" + Column0300.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|0990|" + Column0990.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|5001|" + Column5001.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        if (Column5315 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5315|" + Column5315.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5320 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5320|" + Column5320.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5325 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5325|" + Column5325.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5330 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5330|" + Column5330.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5335 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5335|" + Column5335.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5340 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5340|" + Column5340.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        if (Column5350 != 0)
                        {
                            //REG | REG_BLC | QTD_REG_BLC
                            Arquivo += "9900|5350|" + Column5350.ToString();
                            Arquivo += "\r\n";
                            BlocoNove++;
                            Column9900++;
                            ArquivoTodo++;
                        }
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|5990|" + Column5990.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|9001|" + Column9001.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9900++;
                        Column9900++;
                        Column9900++;
                        Column9900++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|9900|" + Column9900.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9990++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|9990|" + Column9990.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        Column9999++;
                        ArquivoTodo++;
                        //REG | REG_BLC | QTD_REG_BLC
                        Arquivo += "9900|9999|" + Column9999.ToString();
                        Arquivo += "\r\n";
                        BlocoNove++;
                        ArquivoTodo++;
                        y = lineCount - 1;
                        controle = true;
                        #endregion
                    }
                    #endregion
                    #region FECHAMENTO DO BLOCO 9
                    else if (Line[1].ToString() == "9990")
                    {
                        Arquivo += "9990|";
                        //LINHAS BLOCO 9
                        BlocoNove++;
                        BlocoNove++;
                        //QTD_LIN_9
                        Arquivo += BlocoNove.ToString();
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                        ArquivoTodo++;
                    }
                    #endregion
                    #region FECHAMENTO DO ARQUIVO
                    else if (Line[1].ToString() == "9999")
                    {
                        Arquivo += "9999|";
                        //LINHAS DO ARQUIVO
                        ArquivoTodo++;
                        //QTD_LIN
                        Arquivo += ArquivoTodo.ToString();
                        Arquivo += "\r\n";
                        y = lineCount - 1;
                    }
                    #endregion
                    else
                    {
                        y = lineCount - 1;
                    }
                }
                if (!String.IsNullOrEmpty(Arquivo))
                {
                    try
                    {
                        ArqValueDigital.Write(Arquivo);
                        ArqValueDigital.Flush();
                    }
                    catch (Exception ex)
                    {
                        ArqValueDigital.Close();
                        retorno = "Erro: " + ex.Message;
                    }
                    Arquivo = "";
                }
            }
            ArqValueDigital.Close();
            if (lstRegAnaDivergentes.Count == 0)
                retorno = "Arquivo criado com sucesso!";
            else
                retorno = "Arquivo criado com sucesso!\r\nCom divergências de ICMS!";
            return retorno;
        }

        #region Metodos auxiliares
        public string GetDate(string date)
        {
            DateTime data;
            DateTime.TryParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out data);
            string retorno = data.ToString();
            return retorno;
        }

        public string GetPeriodo(string inicio, string final)
        {
            //DateTime datainicio = Convert.ToDateTime(inicio.ToString("dd/MM/yyyy"));
            DateTime datainicio;
            DateTime datafinal;
            DateTime.TryParseExact(inicio, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datainicio);
            DateTime.TryParseExact(final, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datafinal);
            string periodo = "|";
            if (datainicio.Month == datafinal.Month && datainicio.Year == datafinal.Year)
            {
                string mes = "";
                if (datainicio.Month == 1)
                {
                    mes = "01";
                }
                else if (datainicio.Month == 2)
                {
                    mes = "02";
                }
                else if (datainicio.Month == 3)
                {
                    mes = "03";
                }
                else if (datainicio.Month == 4)
                {
                    mes = "04";
                }
                else if (datainicio.Month == 5)
                {
                    mes = "05";
                }
                else if (datainicio.Month == 6)
                {
                    mes = "06";
                }
                else if (datainicio.Month == 7)
                {
                    mes = "07";
                }
                else if (datainicio.Month == 8)
                {
                    mes = "08";
                }
                else if (datainicio.Month == 9)
                {
                    mes = "09";
                }
                else if (datainicio.Month == 10)
                {
                    mes = "10";
                }
                else if (datainicio.Month == 11)
                {
                    mes = "11";
                }
                else if (datainicio.Month == 12)
                {
                    mes = "12";
                }
                else
                {
                    mes = "";
                }

                periodo = mes.ToString() + datainicio.Year.ToString();
                return periodo;
            }
            else
            {
                return periodo;
            }
        }

        public bool GetPeriodoNota(string dtNota, string periodo)
        {
            if (!String.IsNullOrEmpty(dtNota))
            {
                string mesNota = dtNota.Substring(2, 2);
                string mesPeriodo = periodo.Substring(0, 2);
                if (mesNota == mesPeriodo)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public string GetTipoDocumento(string tipDoc)
        {
            if (tipDoc == "01")
                return "01";
            else if (tipDoc == "1B" || tipDoc == "1b")
                return "02";
            else if (tipDoc == "02")
                return "03";
            else if (tipDoc == "2D" || tipDoc == "2d")
                return "04";
            else if (tipDoc == "2E" || tipDoc == "2e")
                return "05";
            else if (tipDoc == "04")
                return "06";
            else if (tipDoc == "06")
                return "07";
            else if (tipDoc == "07")
                return "08";
            else if (tipDoc == "08")
                return "09";
            else if (tipDoc == "8B" || tipDoc == "8b")
                return "10";
            else if (tipDoc == "09")
                return "11";
            else if (tipDoc == "10")
                return "12";
            else if (tipDoc == "11")
                return "13";
            else if (tipDoc == "13")
                return "14";
            else if (tipDoc == "14")
                return "15";
            else if (tipDoc == "15")
                return "16";
            else if (tipDoc == "16")
                return "17";
            else if (tipDoc == "17")
                return "18";
            else if (tipDoc == "18")
                return "19";
            else if (tipDoc == "19")
                return "20";
            else if (tipDoc == "20")
                return "20";
            else if (tipDoc == "21")
                return "21";
            else if (tipDoc == "22")
                return "22";
            else if (tipDoc == "23")
                return "23";
            else if (tipDoc == "24")
                return "24";
            else if (tipDoc == "25")
                return "25";
            else if (tipDoc == "26")
                return "26";
            else if (tipDoc == "27")
                return "27";
            else if (tipDoc == "28")
                return "28";
            else if (tipDoc == "29")
                return "29";
            else if (tipDoc == "30")
                return "30";
            else if (tipDoc == "55")
                return "31";
            else if (tipDoc == "57")
                return "32";
            else
                return "02";
        }

        public bool VerificaTipoCFOP(string CFOP)
        {
            String[] split = CFOP.Split(new String[] { "." }, StringSplitOptions.None);
            string control = split[0].ToString();
            if (control == "1" || control == "2" || control == "3") //Entrada
                return true;
            else //Saida //if (control == "5" || control == "6" || control == "7")
                return false;
        }

        public bool ValidaCFOP(string CFOP, int tipo)
        {
            //Tipo: Verifica se é valido para a apuração
            if (tipo == 1)
            {
                #region If Grandão
                if (CFOP != null && CFOP != "")
                {
                    if (CFOP == "1101" || CFOP == "1102" || CFOP == "1116" || CFOP == "1117" || CFOP == "1118" || CFOP == "1120" || CFOP == "1121" || CFOP == "1122" ||
                        CFOP == "1126" || CFOP == "1917" || CFOP == "1151" || CFOP == "1152" || CFOP == "1153" || CFOP == "1154" || CFOP == "1351" || CFOP == "1352" ||
                        CFOP == "1353" || CFOP == "1354" || CFOP == "1355" || CFOP == "1356" || CFOP == "1360" || CFOP == "1931" || CFOP == "1932" || CFOP == "1401" ||
                        CFOP == "1403" || CFOP == "1651" || CFOP == "1652" || CFOP == "1408" || CFOP == "1409" || CFOP == "1658" || CFOP == "1659" || CFOP == "1910" ||
                        CFOP == "1124" || CFOP == "1125" || CFOP == "2101" || CFOP == "2102" || CFOP == "2116" || CFOP == "2117" || CFOP == "2118" || CFOP == "2120" ||
                        CFOP == "2121" || CFOP == "2122" || CFOP == "2126" || CFOP == "2917" || CFOP == "2151" || CFOP == "2152" || CFOP == "2153" || CFOP == "2154" ||
                        CFOP == "2351" || CFOP == "2352" || CFOP == "2353" || CFOP == "2354" || CFOP == "2355" || CFOP == "2356" || CFOP == "2931" || CFOP == "2932" ||
                        CFOP == "2401" || CFOP == "2403" || CFOP == "2651" || CFOP == "2652" || CFOP == "2408" || CFOP == "2409" || CFOP == "2658" || CFOP == "2659" ||
                        CFOP == "2910" || CFOP == "2124" || CFOP == "2125" || CFOP == "3101" || CFOP == "3102" || CFOP == "3126" || CFOP == "3127" || CFOP == "3651" ||
                        CFOP == "3652" || CFOP == "3353" || CFOP == "3354" || CFOP == "3355" || CFOP == "3356" || CFOP == "3930" || CFOP == "1251" || CFOP == "1252" ||
                        CFOP == "1253" || CFOP == "1254" || CFOP == "1255" || CFOP == "1256" || CFOP == "1257" || CFOP == "1301" || CFOP == "1302" || CFOP == "1303" ||
                        CFOP == "1304" || CFOP == "1305" || CFOP == "1306" || CFOP == "1653" || CFOP == "2251" || CFOP == "2252" || CFOP == "2253" || CFOP == "2254" ||
                        CFOP == "2255" || CFOP == "2256" || CFOP == "2257" || CFOP == "2301" || CFOP == "2302" || CFOP == "2303" || CFOP == "2304" || CFOP == "2305" ||
                        CFOP == "2306" || CFOP == "2653" || CFOP == "3251" || CFOP == "3301" || CFOP == "3653" || CFOP == "5201" || CFOP == "5202" || CFOP == "5205" ||
                        CFOP == "5206" || CFOP == "5207" || CFOP == "5208" || CFOP == "5209" || CFOP == "5210" || CFOP == "5918" || CFOP == "5410" || CFOP == "5411" ||
                        CFOP == "5660" || CFOP == "5661" || CFOP == "6201" || CFOP == "6202" || CFOP == "6205" || CFOP == "6206" || CFOP == "6207" || CFOP == "6208" ||
                        CFOP == "6209" || CFOP == "6210" || CFOP == "6918" || CFOP == "6410" || CFOP == "6411" || CFOP == "6660" || CFOP == "6661" || CFOP == "7201" ||
                        CFOP == "7202" || CFOP == "7205" || CFOP == "7206" || CFOP == "7207" || CFOP == "7210" || CFOP == "7211" || CFOP == "7930" || CFOP == "5662" ||
                        CFOP == "6662" || CFOP == "5101" || CFOP == "5102" || CFOP == "5103" || CFOP == "5104" || CFOP == "5105" || CFOP == "5106" || CFOP == "5109" ||
                        CFOP == "5110" || CFOP == "5115" || CFOP == "5116" || CFOP == "5117" || CFOP == "5118" || CFOP == "5119" || CFOP == "5120" || CFOP == "5122" ||
                        CFOP == "5123" || CFOP == "5917" || CFOP == "5151" || CFOP == "5152" || CFOP == "5153" || CFOP == "5155" || CFOP == "5156" || CFOP == "5251" ||
                        CFOP == "5252" || CFOP == "5253" || CFOP == "5254" || CFOP == "5255" || CFOP == "5256" || CFOP == "5257" || CFOP == "5258" || CFOP == "5301" ||
                        CFOP == "5302" || CFOP == "5303" || CFOP == "5304" || CFOP == "5305" || CFOP == "5306" || CFOP == "5307" || CFOP == "5351" || CFOP == "5352" ||
                        CFOP == "5353" || CFOP == "5354" || CFOP == "5355" || CFOP == "5356" || CFOP == "5357" || CFOP == "5359" || CFOP == "5360" || CFOP == "5401" ||
                        CFOP == "5402" || CFOP == "5403" || CFOP == "5405" || CFOP == "5651" || CFOP == "5652" || CFOP == "5653" || CFOP == "5654" || CFOP == "5655" ||
                        CFOP == "5656" || CFOP == "5408" || CFOP == "5409" || CFOP == "5658" || CFOP == "5659" || CFOP == "5501" || CFOP == "5502" || CFOP == "5928" ||
                        CFOP == "5910" || CFOP == "5124" || CFOP == "5125" || CFOP == "6101" || CFOP == "6102" || CFOP == "6103" || CFOP == "6104" || CFOP == "6105" ||
                        CFOP == "6106" || CFOP == "6107" || CFOP == "6108" || CFOP == "6109" || CFOP == "6110" || CFOP == "6115" || CFOP == "6116" || CFOP == "6117" ||
                        CFOP == "6118" || CFOP == "6119" || CFOP == "6120" || CFOP == "6122" || CFOP == "6123" || CFOP == "6917" || CFOP == "6151" || CFOP == "6152" ||
                        CFOP == "6153" || CFOP == "6155" || CFOP == "6156" || CFOP == "6251" || CFOP == "6252" || CFOP == "6253" || CFOP == "6254" || CFOP == "6255" ||
                        CFOP == "6256" || CFOP == "6257" || CFOP == "6258" || CFOP == "6301" || CFOP == "6302" || CFOP == "6303" || CFOP == "6304" || CFOP == "6305" ||
                        CFOP == "6306" || CFOP == "6307" || CFOP == "6351" || CFOP == "6352" || CFOP == "6353" || CFOP == "6354" || CFOP == "6355" || CFOP == "6356" ||
                        CFOP == "6357" || CFOP == "6359" || CFOP == "6401" || CFOP == "6402" || CFOP == "6403" || CFOP == "6404" || CFOP == "6651" || CFOP == "6652" ||
                        CFOP == "6653" || CFOP == "6654" || CFOP == "6655" || CFOP == "6656" || CFOP == "6408" || CFOP == "6409" || CFOP == "6658" || CFOP == "6659" ||
                        CFOP == "6501" || CFOP == "6502" || CFOP == "6910" || CFOP == "6124" || CFOP == "6125" || CFOP == "7101" || CFOP == "7102" || CFOP == "7105" ||
                        CFOP == "7106" || CFOP == "7127" || CFOP == "7651" || CFOP == "7654" || CFOP == "7251" || CFOP == "7301" || CFOP == "7358" || CFOP == "1201" ||
                        CFOP == "1202" || CFOP == "1203" || CFOP == "1204" || CFOP == "1205" || CFOP == "1206" || CFOP == "1207" || CFOP == "1208" || CFOP == "1209" ||
                        CFOP == "1918" || CFOP == "1410" || CFOP == "1411" || CFOP == "1660" || CFOP == "1661" || CFOP == "1662" || CFOP == "1503" || CFOP == "1504" ||
                        CFOP == "2201" || CFOP == "2202" || CFOP == "2203" || CFOP == "2204" || CFOP == "2205" || CFOP == "2206" || CFOP == "2207" || CFOP == "2208" ||
                        CFOP == "2209" || CFOP == "2918" || CFOP == "2410" || CFOP == "2411" || CFOP == "2660" || CFOP == "2661" || CFOP == "2662" || CFOP == "2503" ||
                        CFOP == "2504" || CFOP == "3201" || CFOP == "3202" || CFOP == "3205" || CFOP == "3206" || CFOP == "3207" || CFOP == "3211" || CFOP == "1101" ||
                        CFOP == "1102" || CFOP == "1116" || CFOP == "1117" || CFOP == "1118" || CFOP == "1120" || CFOP == "1121" || CFOP == "1122" || CFOP == "1126" ||
                        CFOP == "1917" || CFOP == "1151" || CFOP == "1152" || CFOP == "1153" || CFOP == "1154" || CFOP == "1251" || CFOP == "1252" || CFOP == "1253" ||
                        CFOP == "1254" || CFOP == "1255" || CFOP == "1256" || CFOP == "1257" || CFOP == "1301" || CFOP == "1302" || CFOP == "1303" || CFOP == "1304" ||
                        CFOP == "1305" || CFOP == "1306" || CFOP == "1351" || CFOP == "1352" || CFOP == "1353" || CFOP == "1354" || CFOP == "1355" || CFOP == "1356" ||
                        CFOP == "1360" || CFOP == "1931" || CFOP == "1932" || CFOP == "1401" || CFOP == "1403" || CFOP == "1651" || CFOP == "1652" || CFOP == "1408" ||
                        CFOP == "1409" || CFOP == "1658" || CFOP == "1659" || CFOP == "1653" || CFOP == "1910" || CFOP == "1124" || CFOP == "1125" || CFOP == "2101" ||
                        CFOP == "2102" || CFOP == "2116" || CFOP == "2117" || CFOP == "2118" || CFOP == "2120" || CFOP == "2121" || CFOP == "2122" || CFOP == "2126" ||
                        CFOP == "2917" || CFOP == "2151" || CFOP == "2152" || CFOP == "2153" || CFOP == "2154" || CFOP == "2251" || CFOP == "2252" || CFOP == "2253" ||
                        CFOP == "2254" || CFOP == "2255" || CFOP == "2256" || CFOP == "2257" || CFOP == "2301" || CFOP == "2302" || CFOP == "2303" || CFOP == "2304" ||
                        CFOP == "2305" || CFOP == "2306" || CFOP == "2351" || CFOP == "2352" || CFOP == "2353" || CFOP == "2354" || CFOP == "2355" || CFOP == "2356" ||
                        CFOP == "2931" || CFOP == "2932" || CFOP == "2401" || CFOP == "2403" || CFOP == "2651" || CFOP == "2652" || CFOP == "2408" || CFOP == "2409" ||
                        CFOP == "2658" || CFOP == "2659" || CFOP == "2653" || CFOP == "2910" || CFOP == "2124" || CFOP == "2125" || CFOP == "3101" || CFOP == "3102" ||
                        CFOP == "3126" || CFOP == "3127" || CFOP == "3651" || CFOP == "3652" || CFOP == "3251" || CFOP == "3301" || CFOP == "3351" || CFOP == "3352" ||
                        CFOP == "3353" || CFOP == "3354" || CFOP == "3355" || CFOP == "3356" || CFOP == "3653" || CFOP == "3930" || CFOP == "5201" || CFOP == "5202" ||
                        CFOP == "5205" || CFOP == "5206" || CFOP == "5207" || CFOP == "5208" || CFOP == "5209" || CFOP == "5210" || CFOP == "5918" || CFOP == "6206" ||
                        CFOP == "5410" || CFOP == "5411" || CFOP == "5660" || CFOP == "5661" || CFOP == "5662" || CFOP == "6201" || CFOP == "6202" || CFOP == "6205" ||
                        CFOP == "6207" || CFOP == "6208" || CFOP == "6209" || CFOP == "6210" || CFOP == "6918" || CFOP == "6410" || CFOP == "6411" || CFOP == "6660" ||
                        CFOP == "6661" || CFOP == "6662" || CFOP == "7201" || CFOP == "7202" || CFOP == "7205" || CFOP == "7206" || CFOP == "7207" || CFOP == "7210" ||
                        CFOP == "7211" || CFOP == "7930")
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
                #endregion
            }
            //Tipo: verifica se é nota de devolução
            else if (tipo == 2)
            {
                #region If Pequenino
                if (CFOP != null && CFOP != "")
                {
                    if (CFOP == "2202" || CFOP == "1202" || CFOP == "1411" || CFOP == "1410" || CFOP == "2411" || CFOP == "2410" || CFOP == "5200" || CFOP == "5201" ||
                        CFOP == "5202" || CFOP == "5203" || CFOP == "5204" || CFOP == "5205" || CFOP == "5206" || CFOP == "5207" || CFOP == "5208" || CFOP == "5209" ||
                        CFOP == "5210" || CFOP == "5410" || CFOP == "5411" || CFOP == "5412" || CFOP == "5413" || CFOP == "5503" || CFOP == "5553" || CFOP == "5555" ||
                        CFOP == "5556" || CFOP == "5660" || CFOP == "5661" || CFOP == "5662" || CFOP == "1201" || CFOP == "1203" || CFOP == "1204" || CFOP == "1208" ||
                        CFOP == "1209" || CFOP == "1410" || CFOP == "2201" || CFOP == "1503" || CFOP == "1504" || CFOP == "1505" || CFOP == "1506" || CFOP == "1553" ||
                        CFOP == "1660" || CFOP == "1661" || CFOP == "1662" || CFOP == "1918" || CFOP == "1919" || CFOP == "2203" || CFOP == "2204" || CFOP == "2208" ||
                        CFOP == "2209" || CFOP == "2506" || CFOP == "2553" || CFOP == "2503" || CFOP == "2504" || CFOP == "2505" || CFOP == "2660" || CFOP == "2661" ||
                        CFOP == "2662" || CFOP == "2918" || CFOP == "2919" || CFOP == "3201" || CFOP == "3202" || CFOP == "3211" || CFOP == "3503" || CFOP == "3553" || 
                        CFOP == "5201" || CFOP == "5202" || CFOP == "5208" || CFOP == "5209" || CFOP == "5210" || CFOP == "5410" || CFOP == "5411" || CFOP == "5412" || 
                        CFOP == "5413" || CFOP == "5503" || CFOP == "5553" || CFOP == "5555" || CFOP == "5556" || CFOP == "5660" || CFOP == "5661" || CFOP == "5662" || 
                        CFOP == "5918" || CFOP == "5919" || CFOP == "5921" || CFOP == "6201" || CFOP == "6202" || CFOP == "6208" || CFOP == "6209" || CFOP == "6210" || 
                        CFOP == "6410" || CFOP == "6411" || CFOP == "6412" || CFOP == "6413" || CFOP == "6503" || CFOP == "6553" || CFOP == "6555" || CFOP == "6556" || 
                        CFOP == "6660" || CFOP == "6661" || CFOP == "6662" || CFOP == "6918" || CFOP == "6919" || CFOP == "6921" || CFOP == "7201" || CFOP == "7202" || 
                        CFOP == "7210" || CFOP == "7211" || CFOP == "7553" || CFOP == "7556")
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
                #endregion
            }
            else
            {
                return false;
            }
        }

        public double GetValueForCFOP(string CFOP, int tipo, string vlr)
        {
            CFOP = CFOP.Replace(".", "");
            if (tipo == 1)
            {
                #region Entrada valor Contabil 123 (Entrada)
                if (CFOP == "1101" || CFOP == "1102" || CFOP == "1116" || CFOP == "1117" || CFOP == "1118" || CFOP == "1120" || CFOP == "1121" || CFOP == "1122" ||
                    CFOP == "1126" || CFOP == "1917" || CFOP == "1151" || CFOP == "1152" || CFOP == "1153" || CFOP == "1154" || CFOP == "1351" || CFOP == "1352" ||
                    CFOP == "1353" || CFOP == "1354" || CFOP == "1355" || CFOP == "1356" || CFOP == "1360" || CFOP == "1931" || CFOP == "1932" || CFOP == "1401" ||
                    CFOP == "1403" || CFOP == "1651" || CFOP == "1652" || CFOP == "1408" || CFOP == "1409" || CFOP == "1658" || CFOP == "1659" || CFOP == "1910" ||
                    CFOP == "1124" || CFOP == "1125" || CFOP == "2101" || CFOP == "2102" || CFOP == "2116" || CFOP == "2117" || CFOP == "2118" || CFOP == "2120" ||
                    CFOP == "2121" || CFOP == "2122" || CFOP == "2126" || CFOP == "2917" || CFOP == "2151" || CFOP == "2152" || CFOP == "2153" || CFOP == "2154" ||
                    CFOP == "2351" || CFOP == "2352" || CFOP == "2353" || CFOP == "2354" || CFOP == "2355" || CFOP == "2356" || CFOP == "2931" || CFOP == "2932" ||
                    CFOP == "2401" || CFOP == "2403" || CFOP == "2651" || CFOP == "2652" || CFOP == "2408" || CFOP == "2409" || CFOP == "2658" || CFOP == "2659" ||
                    CFOP == "2910" || CFOP == "2124" || CFOP == "2125" || CFOP == "3101" || CFOP == "3102" || CFOP == "3126" || CFOP == "3127" || CFOP == "3651" ||
                    CFOP == "3652" || CFOP == "3353" || CFOP == "3354" || CFOP == "3355" || CFOP == "3356" || CFOP == "3930")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 2)
            {
                #region Entrada base de Calculo 123 (Entrada)
                if (CFOP == "1251" || CFOP == "1252" || CFOP == "1253" || CFOP == "1254" || CFOP == "1255" || CFOP == "1256" || CFOP == "1257" || CFOP == "1301" ||
                    CFOP == "1302" || CFOP == "1303" || CFOP == "1304" || CFOP == "1305" || CFOP == "1306" || CFOP == "1653" || CFOP == "2251" || CFOP == "2252" ||
                    CFOP == "2253" || CFOP == "2254" || CFOP == "2255" || CFOP == "2256" || CFOP == "2257" || CFOP == "2301" || CFOP == "2302" || CFOP == "2303" ||
                    CFOP == "2304" || CFOP == "2305" || CFOP == "2306" || CFOP == "2653" || CFOP == "3251" || CFOP == "3301" || CFOP == "3653")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 3)
            {
                #region Entrada valor Contabil 567 (Saida)
                if (CFOP == "5201" || CFOP == "5202" || CFOP == "5205" || CFOP == "5206" || CFOP == "5207" || CFOP == "5208" || CFOP == "5209" || CFOP == "5210" ||
                    CFOP == "5918" || CFOP == "5410" || CFOP == "5411" || CFOP == "5660" || CFOP == "5661" || CFOP == "6201" || CFOP == "6202" || CFOP == "6205" ||
                    CFOP == "6206" || CFOP == "6207" || CFOP == "6208" || CFOP == "6209" || CFOP == "6210" || CFOP == "6918" || CFOP == "6410" || CFOP == "6411" ||
                    CFOP == "6660" || CFOP == "6661" || CFOP == "7201" || CFOP == "7202" || CFOP == "7205" || CFOP == "7206" || CFOP == "7207" || CFOP == "7210" ||
                    CFOP == "7211" || CFOP == "7930")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 4)
            {
                #region Entrada base de Calculo 567 (Saida)
                if (CFOP == "5662" || CFOP == "6662")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 5)
            {
                #region Saida valor Contabil 567 (Saida)
    if (CFOP == "5101" || CFOP == "5102" || CFOP == "5103" || CFOP == "5104" || CFOP == "5105" || CFOP == "5106" ||
        CFOP == "5109" || CFOP == "5110" || CFOP == "5115" || CFOP == "5116" || CFOP == "5117" || CFOP == "5118" || CFOP == "5119" || CFOP == "5120" || CFOP == "5122" ||
        CFOP == "5123" || CFOP == "5917" || CFOP == "5151" || CFOP == "5152" || CFOP == "5153" || CFOP == "5155" || CFOP == "5156" || CFOP == "5251" || CFOP == "5252" ||
        CFOP == "5253" || CFOP == "5254" || CFOP == "5255" || CFOP == "5256" || CFOP == "5257" || CFOP == "5258" || CFOP == "5301" || CFOP == "5302" || CFOP == "5303" ||
        CFOP == "5304" || CFOP == "5305" || CFOP == "5306" || CFOP == "5307" || CFOP == "5351" || CFOP == "5352" || CFOP == "5353" || CFOP == "5354" || CFOP == "5355" ||
        CFOP == "5356" || CFOP == "5357" || CFOP == "5359" || CFOP == "5360" || CFOP == "5401" || CFOP == "5402" || CFOP == "5403" || CFOP == "5405" || CFOP == "5651" ||
        CFOP == "5652" || CFOP == "5653" || CFOP == "5654" || CFOP == "5655" || CFOP == "5656" || CFOP == "5408" || CFOP == "5409" || CFOP == "5658" || CFOP == "5659" ||
        CFOP == "5501" || CFOP == "5502" || CFOP == "5928" || CFOP == "5910" || CFOP == "5124" || CFOP == "5125" || CFOP == "6101" || CFOP == "6102" || CFOP == "6103" ||
        CFOP == "6104" || CFOP == "6105" || CFOP == "6106" || CFOP == "6107" || CFOP == "6108" || CFOP == "6109" || CFOP == "6110" || CFOP == "6115" || CFOP == "6116" ||
        CFOP == "6117" || CFOP == "6118" || CFOP == "6119" || CFOP == "6120" || CFOP == "6122" || CFOP == "6123" || CFOP == "6917" || CFOP == "6151" || CFOP == "6152" ||
        CFOP == "6153" || CFOP == "6155" || CFOP == "6156" || CFOP == "6251" || CFOP == "6252" || CFOP == "6253" || CFOP == "6254" || CFOP == "6255" || CFOP == "6256" ||
        CFOP == "6257" || CFOP == "6258" || CFOP == "6301" || CFOP == "6302" || CFOP == "6303" || CFOP == "6304" || CFOP == "6305" || CFOP == "6306" || CFOP == "6307" ||
        CFOP == "6351" || CFOP == "6352" || CFOP == "6353" || CFOP == "6354" || CFOP == "6355" || CFOP == "6356" || CFOP == "6357" || CFOP == "6359" || CFOP == "6401" ||
        CFOP == "6402" || CFOP == "6403" || CFOP == "6404" || CFOP == "6651" || CFOP == "6652" || CFOP == "6653" || CFOP == "6654" || CFOP == "6655" || CFOP == "6656" ||
        CFOP == "6408" || CFOP == "6409" || CFOP == "6658" || CFOP == "6659" || CFOP == "6501" || CFOP == "6502" || CFOP == "6910" || CFOP == "6124" || CFOP == "6125" ||
        CFOP == "7101" || CFOP == "7102" || CFOP == "7105" || CFOP == "7106" || CFOP == "7127" || CFOP == "7651" || CFOP == "7654" || CFOP == "7251" || CFOP == "7301" ||
        CFOP == "7358")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 6)
            {
                #region Saida valor Contabil 123 (Saida)
    if (CFOP == "1201" || CFOP == "1202" || CFOP == "1203" || CFOP == "1204" || CFOP == "1205" || CFOP == "1206" || CFOP == "1207" || CFOP == "1208" || CFOP == "1209" ||
        CFOP == "1918" || CFOP == "1410" || CFOP == "1411" || CFOP == "1660" || CFOP == "1661" || CFOP == "1662" || CFOP == "1503" || CFOP == "1504" || CFOP == "2201" ||
        CFOP == "2202" || CFOP == "2203" || CFOP == "2204" || CFOP == "2205" || CFOP == "2206" || CFOP == "2207" || CFOP == "2208" || CFOP == "2209" || CFOP == "2918" ||
        CFOP == "2410" || CFOP == "2411" || CFOP == "2660" || CFOP == "2661" || CFOP == "2662" || CFOP == "2503" || CFOP == "2504" || CFOP == "3201" || CFOP == "3202" ||
        CFOP == "3205" || CFOP == "3206" || CFOP == "3207" || CFOP == "3211")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 7)
            {
                #region PMC valor do Imposto 123 (ICMS)
    if (CFOP == "1101" || CFOP == "1102" || CFOP == "1116" || CFOP == "1117" || CFOP == "1118" || CFOP == "1120" ||
        CFOP == "1121" || CFOP == "1122" || CFOP == "1126" || CFOP == "1917" || CFOP == "1151" || CFOP == "1152" || CFOP == "1153" || CFOP == "1154" || CFOP == "1251" ||
        CFOP == "1252" || CFOP == "1253" || CFOP == "1254" || CFOP == "1255" || CFOP == "1256" || CFOP == "1257" || CFOP == "1301" || CFOP == "1302" || CFOP == "1303" ||
        CFOP == "1304" || CFOP == "1305" || CFOP == "1306" || CFOP == "1351" || CFOP == "1352" || CFOP == "1353" || CFOP == "1354" || CFOP == "1355" || CFOP == "1356" ||
        CFOP == "1360" || CFOP == "1931" || CFOP == "1932" || CFOP == "1401" || CFOP == "1403" || CFOP == "1651" || CFOP == "1652" || CFOP == "1408" || CFOP == "1409" ||
        CFOP == "1658" || CFOP == "1659" || CFOP == "1653" || CFOP == "1910" || CFOP == "1124" || CFOP == "1125" || CFOP == "2101" || CFOP == "2102" || CFOP == "2116" ||
        CFOP == "2117" || CFOP == "2118" || CFOP == "2120" || CFOP == "2121" || CFOP == "2122" || CFOP == "2126" || CFOP == "2917" || CFOP == "2151" || CFOP == "2152" ||
        CFOP == "2153" || CFOP == "2154" || CFOP == "2251" || CFOP == "2252" || CFOP == "2253" || CFOP == "2254" || CFOP == "2255" || CFOP == "2256" || CFOP == "2257" ||
        CFOP == "2301" || CFOP == "2302" || CFOP == "2303" || CFOP == "2304" || CFOP == "2305" || CFOP == "2306" || CFOP == "2351" || CFOP == "2352" || CFOP == "2353" ||
        CFOP == "2354" || CFOP == "2355" || CFOP == "2356" || CFOP == "2931" || CFOP == "2932" || CFOP == "2401" || CFOP == "2403" || CFOP == "2651" || CFOP == "2652" ||
        CFOP == "2408" || CFOP == "2409" || CFOP == "2658" || CFOP == "2659" || CFOP == "2653" || CFOP == "2910" || CFOP == "2124" || CFOP == "2125" || CFOP == "3101" ||
        CFOP == "3102" || CFOP == "3126" || CFOP == "3127" || CFOP == "3651" || CFOP == "3652" || CFOP == "3251" || CFOP == "3301" || CFOP == "3351" || CFOP == "3352" ||
        CFOP == "3353" || CFOP == "3354" || CFOP == "3355" || CFOP == "3356" || CFOP == "3653" || CFOP == "3930")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else if (tipo == 8)
            {
                #region PMC valor do Imposto 567 (ICMS)
    if (CFOP == "5201" || CFOP == "5202" || CFOP == "5205" || CFOP == "5206" || CFOP == "5207" || CFOP == "5208" || CFOP == "5209" || CFOP == "5210" || CFOP == "5918" ||
        CFOP == "5410" || CFOP == "5411" || CFOP == "5660" || CFOP == "5661" || CFOP == "5662" || CFOP == "6201" || CFOP == "6202" || CFOP == "6205" || CFOP == "6206" ||
        CFOP == "6207" || CFOP == "6208" || CFOP == "6209" || CFOP == "6210" || CFOP == "6918" || CFOP == "6410" || CFOP == "6411" || CFOP == "6660" || CFOP == "6661" ||
        CFOP == "6662" || CFOP == "7201" || CFOP == "7202" || CFOP == "7205" || CFOP == "7206" || CFOP == "7207" || CFOP == "7210" || CFOP == "7211" || CFOP == "7930")
                {
                    return Convert.ToDouble(vlr);
                }
                else
                    return 0;
                #endregion
            }
            else
                return 0;
        }

        public string EscreveArquivo(string Arquivo, StreamWriter ArqValueDigital)
        {
            if (!String.IsNullOrEmpty(Arquivo))
            {
                try
                {
                    ArqValueDigital.Write(Arquivo);
                    ArqValueDigital.Flush();
                    Arquivo = "";
                    return Arquivo;
                }
                catch
                {
                    return ""; 
                }
            }
            else
                return ""; 
        }
        #endregion
    }
}
