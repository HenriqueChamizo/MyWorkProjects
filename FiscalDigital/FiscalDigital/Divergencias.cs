using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiscalDigital
{
    public partial class Divergencias : Form
    {
        List<NotaFiscal> notasFiscais;
        List<RegistroAnalitico> registrosAnaliticos;
        List<RegistroAnalitico> regsDivergentes;
        public Divergencias(List<NotaFiscal> notas, List<RegistroAnalitico> registros, List<RegistroAnalitico> divergentes)
        {
            InitializeComponent();

            notasFiscais = notas;
            registrosAnaliticos = registros;
            regsDivergentes = divergentes;
        }

        private void Divergencias_Load(object sender, EventArgs e)
        {
            lstVDivergencias.View = View.Details;
            ColumnHeader Indice = new ColumnHeader();
            Indice.Text = "Indice";
            lstVDivergencias.Columns.Add(Indice);
            ColumnHeader Fiscal = new ColumnHeader();
            Fiscal.Text = "Ind Nt Fiscal";
            lstVDivergencias.Columns.Add(Fiscal);
            ColumnHeader Aliquota = new ColumnHeader();
            Aliquota.Text = "Aliquota";
            lstVDivergencias.Columns.Add(Aliquota);
            ColumnHeader CFOP = new ColumnHeader();
            CFOP.Text = "CFOP";
            lstVDivergencias.Columns.Add(CFOP);
            ColumnHeader ValorOP = new ColumnHeader();
            ValorOP.Text = "Valor Operação";
            lstVDivergencias.Columns.Add(ValorOP);
            ColumnHeader ValorIM = new ColumnHeader();
            ValorIM.Text = "Valor Imposto";
            lstVDivergencias.Columns.Add(ValorIM);
            ColumnHeader ValorIMRe = new ColumnHeader();
            ValorIMRe.Text = "Imposto Calculado";
            lstVDivergencias.Columns.Add(ValorIMRe);
            foreach (RegistroAnalitico ra in regsDivergentes)
            {
                ListViewItem item;
                item = new ListViewItem();
                item.Text = ra.ind.ToString();

                //preenche o listview com itens
                item.SubItems.Add(ra.IndNtFiscal.ToString());
                item.SubItems.Add(ra.Aliquota);
                item.SubItems.Add(ra.Cfop);
                item.SubItems.Add(ra.ValorOperacao);
                item.SubItems.Add(ra.IcmsDebitado);
                item.SubItems.Add((Convert.ToDouble(ra.ValorOperacao) * (Convert.ToDouble(ra.Aliquota) / 100)).ToString("N2"));
                lstVDivergencias.Items.Add(item);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
