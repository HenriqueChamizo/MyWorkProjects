using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESocialFormat
{
    public partial class ESocial : Form
    {
        private string arquivo;
        private string saveArquivo;

        public ESocial()
        {
            InitializeComponent();
        }

        private void ESocial_Load(object sender, EventArgs e)
        {

        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog source = new OpenFileDialog();
            source.InitialDirectory = "c:\\";
            source.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            source.FilterIndex = 2;
            source.RestoreDirectory = true;

            if (source.ShowDialog() == DialogResult.OK)
            {
                arquivo = source.FileName;

                if (String.IsNullOrEmpty(arquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        txtSource.Text = arquivo;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog destination = new FolderBrowserDialog();

                while ((destination.ShowDialog() == DialogResult.Cancel))
                {
                    MessageBox.Show("Por favor, selecione um local onde será salvo o arquivo.");
                }
                saveArquivo = destination.SelectedPath;
                txtDestination.Text = saveArquivo + "\\eSocial.txt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (String.IsNullOrEmpty(txtSource.Text))
                    MessageBox.Show("Insira um Arquivo Fonte E-Social!");
                else if (String.IsNullOrEmpty(txtDestination.Text))
                    MessageBox.Show("Insira Destino para o arquivo!");
                else
                {
                    List<String> Linhas = new List<String>();
                    String[] LinesTxt = null;
                    LinesTxt = System.IO.File.ReadAllLines(txtSource.Text, ASCIIEncoding.ASCII);
                    System.IO.StreamWriter arqDestination = new System.IO.StreamWriter(txtDestination.Text, true, Encoding.ASCII);
                    try
                    {
                        for (int x = 0; x < LinesTxt.Count(); x++)
                        {
                            String[] Line = LinesTxt[x].Split(new String[] { "  " }, StringSplitOptions.None);

                            string cpf = Line[2].Replace(".", "").Replace("-", "");
                            string pis = Line[3].Replace(" ", "");
                            string nome = Line[6].Trim();
                            string ind = (x + 1).ToString();
                            string data = Line[Line.Length - 1].Replace("/", "").Trim();

                            string linha = cpf + ";" + pis + ";" + nome + ind + ";" + data + "\r\n";

                            Linhas.Add(linha);
                        }

                        for (int i = 0; i < Linhas.Count(); i++)
                        {
                            arqDestination.Write(Linhas[i]);
                            arqDestination.Flush();
                        }
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        arqDestination.Close();
                        throw new Exception(ex.Message);
                    }
                    arqDestination.Close();
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
                                "Local: " + txtDestination.Text);
        }
    }
}
