using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiscalDigital
{
    public partial class FrmPrincipal : Form
    {
        List<Participante> participantes = new List<Participante>();
        List<EnderecoPart> enderecos = new List<EnderecoPart>();
        List<NotaFiscal> ntsFiscais = new List<NotaFiscal>();
        List<ItemNotaFiscal> itensNotasFiscais = new List<ItemNotaFiscal>();
        List<RegistroAnalitico> regsAnaliticos = new List<RegistroAnalitico>();
        List<RegistroAnalitico> lstDivergentes = new List<RegistroAnalitico>();
        double Saidas = 0;
        double Entradas = 0;
        double Iva = 0;
        double Pmc = 0;
        double CreditAcumul = 0;
        private string arquivo;
        private string saveArquivo;
        bool selPartEnd = true;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //StreamReader myStream;
            OpenFileDialog ofdBuscar = new OpenFileDialog();
            //List<string> mensagemLinha = new List<string>();

            ofdBuscar.InitialDirectory = "c:\\";
            ofdBuscar.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofdBuscar.FilterIndex = 2;
            ofdBuscar.RestoreDirectory = true;

            if (ofdBuscar.ShowDialog() == DialogResult.OK)
            {
                arquivo = ofdBuscar.FileName; 
                
                if (String.IsNullOrEmpty(arquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        txtArqSped.Text = arquivo;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
            }
        }

        private void btnBuscarClie_Click(object sender, EventArgs e)
        {
            //StreamReader myStream;
            OpenFileDialog ofdBuscar = new OpenFileDialog();
            //List<string> mensagemLinha = new List<string>();

            ofdBuscar.InitialDirectory = "c:\\";
            ofdBuscar.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofdBuscar.FilterIndex = 2;
            ofdBuscar.RestoreDirectory = true;

            if (ofdBuscar.ShowDialog() == DialogResult.OK)
            {
                arquivo = ofdBuscar.FileName;

                if (String.IsNullOrEmpty(arquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        txtArqClientes.Text = arquivo;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(txtCnae.Text);
            }
            catch
            {
                i = 0;
            }
            lstParticipantes.Items.Clear();
            if (String.IsNullOrEmpty(cmbFinalidade.Text))
            {
                MessageBox.Show("A finalidade do arquivo é obrigatória!");
            }
            else if (String.IsNullOrEmpty(txtCnae.Text))
            {
                MessageBox.Show("A CNAE da empresa é obrigatória!");
            }
            else if (i == 0)
            {
                MessageBox.Show("O valor da CNAE é invalido!\r\nDigite um número válido de 7 (sete) campos.");
            }
            else
            {
                AddFinalidade adf = new AddFinalidade();
                String[] split = cmbFinalidade.Text.Split(new String[] { " - " }, StringSplitOptions.None);
                string codFin = split[0];
                Finalidade fin = adf.finalidades().Find(f => f.Codigo == codFin);
                if (fin != null)
                {
                    string ieintima;
                    if (String.IsNullOrEmpty(txtIeIntima.Text))
                        ieintima = "";
                    else
                        ieintima = txtIeIntima.Text;
                    try
                    {
                        String[] LineTxtArqSped = null;
                        String[] LineTxtArqClientes = null;
                        Read read = new Read();
                        if (String.IsNullOrEmpty(txtArqClientes.Text))
                        {
                            MessageBox.Show("O arquivo de clientes/funcionários precisa ser selecionado!");
                        }
                        else
                        {
                            string ivaMediana = txtIvaMediana.Text;
                            if (String.IsNullOrEmpty(ivaMediana))
                                ivaMediana = "0";
                            else
                            {
                                try
                                {
                                    ivaMediana = Convert.ToDouble(ivaMediana).ToString("N2");
                                }
                                catch
                                {
                                    ivaMediana = "0";
                                }
                            }
                            byte[] uni = Encoding.Unicode.GetBytes(System.IO.File.ReadAllLines(txtArqSped.Text).ToString());

                            string Ascii = Encoding.ASCII.GetString(uni);

                            LineTxtArqSped = System.IO.File.ReadAllLines(txtArqSped.Text, ASCIIEncoding.ASCII);
                            LineTxtArqClientes = System.IO.File.ReadAllLines(txtArqClientes.Text, ASCIIEncoding.ASCII);
                            StreamWriter valorArqDigital = new StreamWriter(txtArqDigital.Text, true, Encoding.ASCII);
                            //StreamWriter valorArqClientes = new StreamWriter(txtArqClientes.Text, true, Encoding.ASCII);

                            MessageBox.Show(read.LeArquivo(LineTxtArqSped, LineTxtArqClientes, valorArqDigital, codFin, txtCnae.Text, ieintima, ivaMediana));
                        }

                        btnVerificar.Enabled = true;
                        participantes = read.Participantes();
                        enderecos = read.Enderecos();
                        ntsFiscais = read.NotasFiscais();
                        itensNotasFiscais = read.ItensNotasFiscais();
                        regsAnaliticos = read.RegistroAnalitico();
                        lstDivergentes = read.RegistrosDivergentes();
                        Saidas = read.GetSaidas();
                        Entradas = read.GetEntradas();
                        Iva = read.GetIva();
                        Pmc = read.GetPmc();
                        CreditAcumul = read.GetCreditoAcumulado();
                        foreach (Participante part in participantes)
                        {
                            if (part.erroUF != "0" || part.erroCEP != "0" || part.erroNUM != "0" || part.erroRUA != "0")
                            {
                                lstParticipantes.Items.Add(part.CnpjCpf + " - " + part.Nome);


                                ListViewItem iten;
                                iten = new ListViewItem();
                                iten.Text = part.CodPart;

                                //preenche o listview com itens
                                iten.SubItems.Add(part.Nome);
                                lstVParticipantes.Items.Add(iten);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Finalidade incorreta!");
                }
            }
            if (lstDivergentes.Count == 0)
                divergênciasToolStripMenuItem.Visible = false;
            else
                divergênciasToolStripMenuItem.Visible = true;


            lstVParticipantes.Select();
        }

        private void btnCaminDest_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog sfdSalvar = new FolderBrowserDialog();

                while ((sfdSalvar.ShowDialog() == DialogResult.Cancel))
                {
                    MessageBox.Show("Por favor, selecione um local onde será salvo o arquivo digital.");
                }
                FormataDados fd = new FormataDados();
                saveArquivo = sfdSalvar.SelectedPath;
                string dia = fd.FormataDado(DateTime.Now.Day);
                string mes = fd.FormataDado(DateTime.Now.Month);
                string ano = fd.FormataDado(DateTime.Now.Year);
                string hora = fd.FormataDado(DateTime.Now.Hour);
                string minuto = fd.FormataDado(DateTime.Now.Minute);
                string data = dia + mes + ano + hora + minuto;
                txtArqDigital.Text = saveArquivo + "\\" + data + ".txt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            #region ListView Notas Fiscais
            lstVParticipantes.View = View.Details;
            lstVParticipantes.MultiSelect = true;

            ColumnHeader CodPart = new ColumnHeader();
            CodPart.Text = "Código";
            CodPart.Width = 90;
            CodPart.TextAlign = HorizontalAlignment.Center;
            lstVParticipantes.Columns.Add(CodPart);
            ColumnHeader NomePart = new ColumnHeader();
            NomePart.Text = "Nome";
            NomePart.Width = 300;
            NomePart.TextAlign = HorizontalAlignment.Left;
            lstVParticipantes.Columns.Add(NomePart);
            #endregion

            lstVParticipantes.HideSelection = false;

            AddFinalidade adf = new AddFinalidade();
            foreach (Finalidade f in adf.finalidades())
            {
                cmbFinalidade.Items.Add(f.Codigo + " - " + f.Descricao);
            }
            cmbFinalidade.SelectedIndex = 0;

            btnVerificar.Enabled = false;
            button1.Visible = false;
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            //
            string path2 = path.Remove(path.LastIndexOf("\\"));
            path = path2.Remove(path2.LastIndexOf("\\") + 1);
            path += "log";

            string LineTxt = "";
            if (System.IO.File.Exists(path + "\\Log.txt"))
                LineTxt = System.IO.File.ReadAllText(path + "\\Log.txt");
            else
                LineTxt = "false";

            bool chek = LineTxt.IndexOf("true", 1) > 0 ? true : false;
            if (!chek)
            {
                Inicial inicial = new Inicial();
                inicial.ShowDialog();
            }
            //divergênciasToolStripMenuItem.Visible = true;
        }

        private void tsmResetItem_Click(object sender, EventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            //
            string path2 = path.Remove(path.LastIndexOf("\\"));
            path = path2.Remove(path2.LastIndexOf("\\") + 1);
            path += "log";

            try
            {
                Directory.Delete(path, true);
                MessageBox.Show("Programa resetado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("O diretório não existe ou está corrompido.\r\n\r\n" + ex.Message);
            }


            #region Testar se der problema com arquivos Read-Only
            //// Apagar sub-pastas e ficheiros?
            //if (recursive)
            //{
            //    // Sim... Então vamos a isso
            //    var subfolders = Directory.GetDirectories(path);
            //    foreach (var s in subfolders)
            //    {
            //        DeleteDirectory(s, recursive);
            //    }
            //}

            //// Obtém todos os ficheiros da pasta
            //var files = Directory.GetFiles(path);
            //foreach (var f in files)
            //{
            //    // Obtém os atributos do ficheiro
            //    var attr = File.GetAttributes(f);

            //    // O ficheiro é 'read-only'?
            //    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            //    {
            //        // Sim... Remove o atributo 'read-only' do ficheiro
            //        File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
            //    }

            //    // Apaga o ficheiro
            //    File.Delete(f);
            //}

            //// Ao chegar aqui, todos os ficheiros da pasta
            //// foram apagados... É só apagar a pasta vazia
            //Directory.Delete(path);
            #endregion
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inicial inicial = new Inicial();
            inicial.CheckboxVisible = false;
            inicial.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WebBrowser oWebBrowser = new WebBrowser(); // Instancia o Browser
            //oWebBrowser.Navigate("http://www.consultaenderecos.com.br/busca-endereco/rua+mario+vila+romana+194"); // Acessa a URL do fórum
            //System.Threading.Thread.Sleep(7000);
            //string res = oWebBrowser.Document.GetElementsByTagName("tbody").ToString();
            //oWebBrowser.Document.GetElementsByTagName("class").GetElementsByName("").SetAttribute("value", "SEU VALOR"); // muda o texto do input
            //oWebBrowser.Document.InvokeScript("eval", new object[] { "_mostraTelaAguarde();document.forms['buscaCepForm'].metodo.value='buscarCep'" }); // invoca a função envia() do html

            //WebClient clnt = new WebClient();
            //char s = Convert.ToChar(clnt.DownloadString("http://www.consultaenderecos.com.br/busca-endereco/rua+mario+vila+romana+194"));
            //StreamReader r = new StreamReader(s.ToString());
            //System.Threading.Thread.Sleep(7000);
            //string res = r.ReadToEnd();


            //string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            //string path2 = path.Remove(path.LastIndexOf("\\"));
            //path = path2.Remove(path2.LastIndexOf("\\") + 1);
            //path += "Teste";

            //System.IO.Directory.CreateDirectory(path);
            //using (System.IO.StreamWriter file = File.AppendText(path + "\\teste.txt"))
            //{
            //    string linha = res;

            //    file.Write(linha);
            //    file.WriteLine("");
            //}
            //if (System.IO.File.Exists(path + "\\teste.txt"))
            //    MessageBox.Show("OK");
            //else
            //    MessageBox.Show("Erro");
        }

        private void lstParticipantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstParticipantes.SelectedItem != null)
            {
                string item = lstParticipantes.SelectedItem.ToString(); 
                String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                Participante part = participantes.Find(f => f.CnpjCpf == split[0]);
                string teste = part.ind.ToString() + "|" + part.Nome + "|" + part.CodPart;
                EnderecoPart end = enderecos.Find(f => f.IndPart == part.ind);
                EditaParticipante editPart;
                if (end != null)
                {
                    editPart = new EditaParticipante(this, part, txtArqDigital.Text, end);
                    editPart.frm = this;
                }
                else
                {
                    editPart = new EditaParticipante(this, part, txtArqDigital.Text);
                    editPart.frm = this;
                }
                editPart.ShowDialog();
                if (editPart.close)
                {
                    lstParticipantes.Items.Remove(lstParticipantes.SelectedItem);
                }
            }
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            Notas notas = new Notas(this, ntsFiscais, itensNotasFiscais, participantes, regsAnaliticos, Entradas, 
                                    Saidas, Pmc, Iva, CreditAcumul, lstDivergentes);
            
            notas.Show();
        }

        private void divergênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Divergencias div = new Divergencias(ntsFiscais, regsAnaliticos, lstDivergentes);
            div.Show();
        }

        private void lstVParticipantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVParticipantes.SelectedItems.Count == 1)
            {
                if (selPartEnd)
                {
                    ListView.SelectedListViewItemCollection items = null;
                    ListViewItem item = null;
                    items = lstVParticipantes.SelectedItems;
                    foreach (ListViewItem it in items)
                    {
                        item = it;
                    }
                    string partcip = "";
                    try
                    {
                        partcip = item.SubItems[0].ToString().Replace("ListViewSubItem: {", "").Replace("}", "").Replace(" ", "");
                        //string item = lstParticipantes.SelectedItem.ToString();
                        //String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                        Participante part = participantes.Find(f => f.CodPart == partcip);
                        //string teste = part.ind.ToString() + "|" + part.Nome + "|" + part.CodPart;
                        EnderecoPart end = enderecos.Find(f => f.IndPart == part.ind);
                        EditaParticipante editPart;
                        if (end != null)
                        {
                            editPart = new EditaParticipante(this, part, txtArqDigital.Text, end);
                            editPart.frm = this;
                        }
                        else
                        {
                            editPart = new EditaParticipante(this, part, txtArqDigital.Text);
                            editPart.frm = this;
                        }
                        editPart.ShowDialog();
                        if (editPart.close)
                        {
                            selPartEnd = false;
                            lstVParticipantes.Items.Remove(item);
                        }
                    }
                    catch { }
                }
                else
                    selPartEnd = true;
            }
        }
    }
}
