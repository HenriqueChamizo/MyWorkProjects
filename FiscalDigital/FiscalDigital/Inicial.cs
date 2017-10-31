using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FiscalDigital
{
    public partial class Inicial : Form
    {
        public bool CheckboxVisible = true;

        public Inicial()
        {
            InitializeComponent();
        }

        private void Inicial_Load(object sender, EventArgs e)
        {
            if (CheckboxVisible)
                cbNaoExibir.Visible = true;
            else
                cbNaoExibir.Visible = false;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (cbNaoExibir.Checked)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                //
                string path2 = path.Remove(path.LastIndexOf("\\"));
                path = path2.Remove(path2.LastIndexOf("\\") + 1);
                path += "log";

                System.IO.Directory.CreateDirectory(path);

                Log("true", path);
            }
            Close();
        }

        private void Log(string Texto, string path)
        {
            using (System.IO.StreamWriter file = File.AppendText(path + "\\Log.txt"))
            {
                string linha = DateTime.Now.ToString() + " " + Texto + ".\n";

                file.Write(linha);
                file.WriteLine("");
            }
        }
    }
}
