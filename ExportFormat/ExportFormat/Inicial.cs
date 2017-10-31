using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ExportFormat
{
    public partial class frmInicial : Form
    {
        public frmInicial()
        {
            InitializeComponent();
        }

        private void frmInicial_Load(object sender, EventArgs e)
        {
            if (cmbModulos.Items.Count == 0)
            {
                cmbModulos.Items.Insert(0, "     ...");
                cmbModulos.Items.Insert(1, "     Script Zenega");
                cmbModulos.Items.Insert(2, "     PER DCOMP");
                cmbModulos.SelectedIndex = 0;
            }
        }

        private void btnSeleciona_Click(object sender, EventArgs e)
        {
            Form form;
            if (cmbModulos.SelectedIndex == 0)
                MessageBox.Show("Selecione um módulo!!");
            else if (cmbModulos.SelectedIndex == 1)
            {
                this.Hide();
                form = new Form1(this);
                form.Show();
            }
            else if (cmbModulos.SelectedIndex == 2)
            {
                this.Hide();
                form = new PerdComp(this);
                form.Show();
            }
        }
    }
}
