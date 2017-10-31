using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportFormat
{
    public partial class Form1 : Form
    {
        private string arquivo;
        private string saveArquivo;
        public List<String> Lista;
        public Form FormInicial = null;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Form form)
        {
            InitializeComponent();
            FormInicial = form;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            txtDestination.Clear();
            OpenFileDialog source = new OpenFileDialog();
            source.InitialDirectory = "c:\\";
            source.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            source.FilterIndex = 2;
            source.RestoreDirectory = true;

            if (source.ShowDialog() == DialogResult.OK)
            {
                progressBar1.Visible = true;
                arquivo = source.FileName;

                if (String.IsNullOrEmpty(arquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        //teste

                        Lista = new List<String>();
                        txtSource.Text = arquivo;
                        List<String> linhas = ScriptFromSource(arquivo);
                        progressBar1.Maximum = linhas.Count();
                        progressBar1.Step = 1;
                        progressBar1.Value = 0;
                        foreach (string l in linhas)
                        {
                            //progressBar1.Step++;
                            progressBar1.Value++;
                            Lista.Add(l);
                            txtDestination.Text = txtDestination.Text + l;
                        }
                        MessageBox.Show("Script gerado com sucesso!");
                        progressBar1.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
            }
        }

        private List<String> ScriptFromSource(string source)
        {
            //bool result = false;
            //string returnIn = "";
            List<String> Linhas = new List<String>();
            List<String> LinhasAux = new List<String>();
            try
            {
                if (String.IsNullOrEmpty(txtSource.Text))
                    throw new Exception("Insira um Arquivo Fonte");
                else
                {
                    String[] LinesTxt = null;
                    LinesTxt = System.IO.File.ReadAllLines(source, ASCIIEncoding.ASCII);
                    //System.IO.StreamWriter arqDestination = new System.IO.StreamWriter(txtDestination.Text, true, Encoding.ASCII);
                    try
                    {
                        Linhas.Add("DELETE FROM [dbo].[Classificacoes] " + "\r\n");
                        Linhas.Add("INSERT INTO [dbo].[Classificacoes] ([CLA_PLANO],[CLA_GRUPO],[CLA_SUBGRUPO1],[CLA_SUBGRUPO2],[CLA_DESCRICAO],[CLA_TIPO],[CLA_CONTA_CONTABIL],[CLA_CONTAP],[CLA_CONTRA_APRESENTACAO],[CLA_GRAFICO_ANALISE_CRESCIMENTO]) VALUES (1 ,0 ,0 ,NULL ,'RECEITAS' ,1 ,'' ,NULL ,0 ,1)" + "\r\n");
                        Linhas.Add("INSERT INTO [dbo].[Classificacoes] ([CLA_PLANO],[CLA_GRUPO],[CLA_SUBGRUPO1],[CLA_SUBGRUPO2],[CLA_DESCRICAO],[CLA_TIPO],[CLA_CONTA_CONTABIL],[CLA_CONTAP],[CLA_CONTRA_APRESENTACAO],[CLA_GRAFICO_ANALISE_CRESCIMENTO]) VALUES (1 ,0 ,0 ,NULL ,'DESPESAS' ,2 ,'' ,NULL ,0 ,1)" + "\r\n");

                        //Tipo Receita(1) ou Despesa(2)
                        int rd = 0;

                        for (int x = 0; x < LinesTxt.Count(); x++)
                        {
                            string linha = LinesTxt[x].ToString();
                            if (!String.IsNullOrEmpty(linha))
                            {
                                while (linha.Count() < 150)
                                    linha = linha + " ";
                                string acesso;
                                string classificador;
                                string nome;
                                string lcto;
                                string cor;
                                string red;
                                string custo;
                                string moeda;
                                string conc;
                                string contas;
                                //string dmbl;
                                //string seq;
                                //string dipj;
                                acesso = (linha.Substring(1, 8)).Trim();
                                classificador = (linha.Substring(9, 30)).Trim();
                                nome = (linha.Substring(39, 35)).Trim();
                                lcto = (linha.Substring(74, 5)).Trim();
                                cor = (linha.Substring(79, 5)).Trim();
                                red = (linha.Substring(84, 5)).Trim();
                                custo = (linha.Substring(89, 7)).Trim();
                                moeda = (linha.Substring(96, 9)).Trim();
                                conc = (linha.Substring(105, 5)).Trim();
                                contas = (linha.Substring(110, 6)).Trim();

                                if (nome.ToUpper() == "RECEITAS")
                                    rd = 1;
                                if (nome.ToUpper() == "DESPESAS")
                                    rd = 2;

                                if (nome.ToUpper() != "RECEITAS" && nome.ToUpper() != "DESPESAS")
                                {
                                    linha = acesso + "%//%" + classificador + "%//%" + nome + "%//%" + lcto + "%//%" + cor + "%//%" + red + "%//%" + custo + "%//%" + moeda + "%//%" + conc + "%//%" + contas + "%//%" + rd;
                                    LinhasAux.Add(linha + "\r\n");
                                }
                            }
                        }

                        LinhasAux = GetListInserts(LinhasAux);

                        foreach (string l in LinhasAux)
                        {
                            Linhas.Add(l);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (FieldAccessException ex)
            {
                MessageBox.Show("ERRO no arquivo: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
            //if (result)
            //    MessageBox.Show("Script gerado com sucesso!");

            return Linhas;
        }

        private List<String> GetListInserts(List<String> lista)
        {
            string beginInsert = "INSERT INTO [dbo].[Classificacoes] ([CLA_PLANO],[CLA_GRUPO],[CLA_SUBGRUPO1],[CLA_DESCRICAO],[CLA_TIPO]) VALUES (";
            string endInsert = ")\r\n";
            string strAux = "";
            int grupo = 0;
            int subgrupo = 0;
            //int tipo = 0;
            List<String> inserts = new List<String>();
            List<String> list = new List<String>();
            foreach (string linha in lista)
            {
                grupo++;
                string insert;
                String[] Split = linha.Split(new String[] { "%//%" }, StringSplitOptions.None);
                if (list.Count == 0)
                {
                    //String[] cc = Split[0].Split(new String[] { "-" }, StringSplitOptions.None);
                    String cc = Split[0].Replace("-", "");
                    insert = Split[1] + "%//%" + "1, " + grupo + ", " + subgrupo + ", '" + Split[2] + "', " + Split[Split.Count() - 1].Replace("\r\n", "");//(cc[0] + cc[1]) + ", NULL, 0, 1";
                    strAux = insert;
                    list.Add(insert);
                }
                else
                {
                    String[] Class = Split[1].Split(new String[] { "." }, StringSplitOptions.None);
                    String[] SplitI = strAux.Split(new String[] { "%//%" }, StringSplitOptions.None);
                    String[] ClassI = SplitI[0].Split(new String[] { "." }, StringSplitOptions.None);

                    if (Convert.ToInt32(Class[0]) == Convert.ToInt32(ClassI[0]))
                    {
                        if (Convert.ToInt32(Class[1]) == Convert.ToInt32(ClassI[1]))
                        {
                            if (Convert.ToInt32(Class[2]) == Convert.ToInt32(ClassI[2]))
                            {
                                if (Convert.ToInt32(Class[3]) == Convert.ToInt32(ClassI[3]))
                                {
                                    if (Convert.ToInt32(Class[4]) == Convert.ToInt32(ClassI[4]))
                                    {
                                        if (Convert.ToInt32(Class[5]) == Convert.ToInt32(ClassI[5]))
                                        {
                                            grupo--;
                                            subgrupo = -1;
                                        }
                                        else
                                        {
                                            grupo--;
                                            subgrupo++;
                                        }
                                    }
                                    else
                                    {
                                        grupo--;
                                        subgrupo++;
                                    }
                                }
                                else
                                    subgrupo = 0;
                            }
                            else
                                subgrupo = 0;
                        }
                        else
                            subgrupo = 0;
                    }
                    else
                        subgrupo = 0;

                    if (subgrupo != -1)
                    {
                        //String[] cc = Split[0].Split(new String[] { "-" }, StringSplitOptions.None);
                        String cc = Split[0].Replace("-", "");
                        insert = Split[1] + "%//%" + "1, " + grupo + ", " + subgrupo + ", '" + Split[2] + "', " + Split[Split.Count() - 1].Replace("\r\n", "");//(cc[0] + cc[1]) + ", NULL, 0, 1";
                        strAux = insert;
                        list.Add(insert);
                    }
                }
            }
            foreach (string l in list)
            {
                String[] split = l.Split(new String[] { "%//%" }, StringSplitOptions.None);
                inserts.Add(beginInsert + split[1] + endInsert);
            }

            return inserts;
        }

        private void btnDestionation_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (String.IsNullOrEmpty(txtSource.Text))
                    MessageBox.Show("Insira um Arquivo Fonte!");
                else
                {
                    FolderBrowserDialog destination = new FolderBrowserDialog();

                    while ((destination.ShowDialog() == DialogResult.Cancel))
                    {
                        MessageBox.Show("Por favor, selecione um local onde será salvo o arquivo.");
                    }
                    string name = "\\script-" + 
                                    DateTime.Now.Year.ToString() + 
                                    ((DateTime.Now.Month.ToString().Count() == 1) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) +
                                    ((DateTime.Now.Day.ToString().Count() == 1) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) +
                                    ((DateTime.Now.Hour.ToString().Count() == 1) ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) +
                                    ((DateTime.Now.Minute.ToString().Count() == 1) ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) +
                                    ".sql";
                    saveArquivo = destination.SelectedPath + name;
                    System.IO.StreamWriter arqDestination = new System.IO.StreamWriter(saveArquivo, true, Encoding.ASCII);
                    foreach (string l in Lista)
                    {
                        arqDestination.Write(l);
                        arqDestination.Flush();
                    }
                    arqDestination.Close();
                    result = true;
                }
            }
            catch (FieldAccessException ex)
            {
                MessageBox.Show("ERRO no arquivo: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
            if (result)
                MessageBox.Show("Arquivo gerado com sucesso!\r\n" +
                                "Local: " + saveArquivo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormInicial != null)
                FormInicial.Close();
        }
    }
}
