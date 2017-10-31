using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chamizo.Class;
using Chamizo.Control;
using System.Data.OleDb;
using System.IO;

namespace ExportFormat
{
    public partial class PerdComp : Form
    {
        protected String FileSource;
        protected String FileDestination;
        protected String DateFileDestionation;
        private FormatData formatData;
        public Form FormInicial = null;

        public PerdComp()
        {
            InitializeComponent();
        }

        public PerdComp(Form form)
        {
            InitializeComponent();
            FormInicial = form;
        }

        private void PerdComp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormInicial != null)
                FormInicial.Close();
        }

        private void PerdComp_Load(object sender, EventArgs e)
        {
            btnDestination.Enabled = false;
            rdbTypeFileYes.Checked = true;
            rdbTypeFileNo.Checked = false;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            try
            {
                FileControl filecontrol = new FileControl();
                FileSource = filecontrol.OpenFile(TypeFile.Excel);

                txtSource.Text = FileSource;
                btnDestination.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            FileControl filecontrol = new FileControl();
            FormatData formatData = new FormatData();
            FileDestination = filecontrol.OpenFolder();
            try
            {
                string dia = formatData.FormatIntForDate(DateTime.Now.Day);
                string mes = formatData.FormatIntForDate(DateTime.Now.Month);
                string ano = formatData.FormatIntForDate(DateTime.Now.Year);
                string hora = formatData.FormatIntForDate(DateTime.Now.Hour);
                string minuto = formatData.FormatIntForDate(DateTime.Now.Minute);
                DateFileDestionation = ano + mes + dia + hora + minuto;
                if (rdbTypeFileYes.Checked)
                    txtDestination.Text = FileDestination + "\\" + DateFileDestionation + "\\";
                else
                    txtDestination.Text = FileDestination + "\\" + DateFileDestionation + ".txt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void CreateFileValue(string SourceFile, string DestinationFile, bool forMonth = false)
        {
            String[] rowValue = new String[5];
            formatData = new FormatData();

            OleDbConnection _olecon;
            String _StringConexao = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=False';", SourceFile);
            _olecon = new OleDbConnection(_StringConexao);
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [NFES$]", _olecon);
            DataSet DtsValues = new DataSet();
            StreamWriter DestinationFromFile;
            try
            {
                _olecon.Open();
                adapter.Fill(DtsValues);

                rowValue = new String[DtsValues.Tables[0].Rows.Count];
                int i = 0;
                foreach (DataRow row in DtsValues.Tables[0].Rows)
                {
                    if (!(row[0] is DBNull))
                    {
                        string tipo = "S13"; //+ formatData.CompleteLenForString((i + 1).ToString(), 2, "0", false);
                        string tipoC = "01";
                        string cnpjP = formatData.CompleteLenForString(formatData.FormatStringClear(row[4].ToString()), 14, " ");
                        string ano = row[0].ToString();
                        string mes = Convert.ToDateTime(row[2]).ToString("MM");
                        string orgao = "0";
                        string codRec = "2631";
                        string cnpjT = formatData.CompleteLenForString(formatData.FormatStringClear(row[6].ToString()), 14, " ");
                        string nfs = formatData.CompleteLenForString(formatData.FormatStringClear(row[1].ToString()), 9, "0", false);
                        string serie = "001";
                        string data = formatData.FormatDateForString(Convert.ToDateTime(row[2])).Replace("/", "");
                        string cnpjR = cnpjP;
                        string vlrbruto = ValueFileMoney(row[8].ToString());
                        string vlrretido = ValueFileMoney(row[10].ToString());
                        string gfip = formatData.FormatStringClear(row[15].ToString());
                        string dr = "\r\n";
                        string split = "";

                        if (gfip == "Sim")
                        {
                            rowValue[i] = tipo + split + tipoC + split + cnpjP + split + ano + split + mes + split + orgao + split + codRec + split + cnpjT + split +
                                            nfs + split + serie + split + data + split + cnpjR + split + vlrbruto + split + vlrretido + dr;
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                _olecon.Close();
            }


            if (!forMonth)
            {
                DestinationFromFile = new StreamWriter(txtDestination.Text, true, Encoding.ASCII);
                try
                {
                    for (int i = 0; i < rowValue.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(rowValue[i]))
                        {
                            DestinationFromFile.Write(rowValue[i]);
                            DestinationFromFile.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    DestinationFromFile.Close();
                }
            }
            else
            {
                List<String> ValueForMonth;
                for (int x = 1; x <= 12; x++)
                {
                    ValueForMonth = new List<String>();
                    for (int y = 0; y < rowValue.Length; y++)
                    {
                        if (!String.IsNullOrEmpty(rowValue[y]))
                        {
                            int mes = Convert.ToInt32(rowValue[y].Substring(23, 2));
                            if (mes == x)
                                ValueForMonth.Add(rowValue[y]);
                        }
                    }

                    if (ValueForMonth.Count != 0)
                    {
                        String PathFile = FileDestination + "\\" + DateFileDestionation + "\\";
                        String FileName = formatData.FormatIntForDate(x) + "_" + DateFileDestionation + ".txt";
                        String CompletePathFile = PathFile + FileName;
                        
                        if (!Directory.Exists(PathFile))
                            Directory.CreateDirectory(PathFile);

                        DestinationFromFile = new StreamWriter(CompletePathFile, true, Encoding.ASCII);
                        // FileStream fileS = new FileStream(CompletePathFile, FileMode.CreateNew);
                        try
                        {
                            foreach (String s in ValueForMonth)
                            {
                                DestinationFromFile.Write(s);
                                DestinationFromFile.Flush();
                                //fileS.Write(Encoding.ASCII.GetBytes(s), 0, Int32.MaxValue);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro: " + ex.Message);
                        }
                        finally
                        {
                            ValueForMonth.Clear();
                            DestinationFromFile.Close();
                            //fileS.Close();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CreateFileValue(FileSource, txtDestination.Text, rdbTypeFileYes.Checked);
                MessageBox.Show("Arquivo criado em: " + txtDestination.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private String ValueFileMoney(String value)
        {
            String[] split = value.Split(new String[] { "," }, StringSplitOptions.None);
            String Antes = split[0];
            String Depois = split.Count() > 1 ? split[1] : "00";
            while (Antes.Length < 12)
                Antes = "0" + Antes;
            if (Depois != "00")
                while (Depois.Length < 2)
                    Depois = Depois + "0";

            //return Antes + "," + Depois;
            return Antes + Depois;
        }
    }
}
