using System;
using System.IO;
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
    public partial class EditaParticipante : Form
    {
        Participante part = new Participante();
        EnderecoPart end = new EnderecoPart();
        public Form frm;
        public bool close = false;
        private string ArqDigital;

        public EditaParticipante()
        {
            InitializeComponent();
        }

        public EditaParticipante(Form form, Participante participante, string arquivo, EnderecoPart endereco = null)
        {
            InitializeComponent();
            Limpa();

            //this.frm = form;

            part = participante;
            ArqDigital = arquivo;
            end = endereco;

            CamposEnable();
        }

        private void EditaParticipante_Load(object sender, EventArgs e)
        {

        }

        private void CamposEnable()
        {
            txtCodPart.Text = part.CodPart;
            if (!String.IsNullOrEmpty(txtCodPart.Text))
                txtCodPart.Enabled = false;

            txtNome.Text = part.Nome;
            if (!String.IsNullOrEmpty(txtNome.Text))
                txtNome.Enabled = false;

            txtCnpj.Text = part.CnpjCpf;
            txtCnpj.Enabled = true;
            txtCnpj.ReadOnly = true;
            Clipboard.Clear();
            Clipboard.SetText(txtCnpj.Text);

            txtCodPais.Text = part.CodPais;
            if (!String.IsNullOrEmpty(txtCodPais.Text))
                txtCodPais.Enabled = false;

            txtIe.Text = part.Ie;
            if (!String.IsNullOrEmpty(txtIe.Text))
                txtIe.Enabled = false;

            txtCodMun.Text = part.CodMunicipal;
            if (!String.IsNullOrEmpty(txtCodMun.Text))
                txtCodMun.Enabled = false;

            txtBairro.Text = part.Bairro;
            if (!String.IsNullOrEmpty(txtBairro.Text))
                txtBairro.Enabled = false;

            txtNumero.Text = part.Numero;
            if (!String.IsNullOrEmpty(txtNumero.Text))
                txtNumero.Enabled = false;

            txtComplemento.Text = part.Complemento;
            txtComplemento.Enabled = false;

            if (end != null)
            {
                if (!String.IsNullOrEmpty(part.Endereco))
                    txtEndereco.Text = part.Endereco;
                else
                    txtEndereco.Text = end.Rua;
                txtUf.Text = end.Uf;
                txtCep.Text = end.Cep;
            }
            else
            {
                txtEndereco.Text = part.Endereco;
                txtUf.Text = "";
                txtCep.Text = "";
            }

            if (!String.IsNullOrEmpty(txtEndereco.Text))
                txtEndereco.Enabled = false;
            if (!String.IsNullOrEmpty(txtUf.Text))
                txtUf.Enabled = false;
            if (!String.IsNullOrEmpty(txtCep.Text))
                txtCep.Enabled = false;
        }

        private void Limpa()
        {
            txtCodPart.Text = "";
            txtNome.Text = "";
            txtCnpj.Text = "";
            txtCodPais.Text = "";
            txtIe.Text = "";
            txtCodMun.Text = "";
            txtBairro.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtUf.Text = "";
            txtCep.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUf.Text) &&
                !String.IsNullOrEmpty(txtNumero.Text) &&
                !String.IsNullOrEmpty(txtCep.Text) &&
                !String.IsNullOrEmpty(txtEndereco.Text))
            {
                if (txtCep.Text.Count() == 8)
                {
                    bool erro = false;
                    bool edit = false;
                    string uf = "";
                    string cep = "";
                    string rua = "";
                    string num = "";
                    string input = "";

                    if (part.erroCEP != "0" ||
                        part.erroNUM != "0" ||
                        part.erroRUA != "0" ||
                        part.erroUF != "0")
                        erro = true;

                    if (erro)
                    {
                        if (part.erroUF != "0")
                            uf = "UF" + part.erroUF.ToString();
                        if (part.erroCEP != "0")
                            cep = "CEP" + part.erroCEP.ToString();
                        if (part.erroRUA != "0")
                            rua = "RUA" + part.erroRUA.ToString();
                        if (part.erroNUM != "0")
                            num = "NUM" + part.erroNUM.ToString();

                        try
                        {
                            StreamReader re = File.OpenText(ArqDigital);
                            input = re.ReadToEnd();
                            re.Close();

                            if (!String.IsNullOrEmpty(uf) && input.IndexOf(uf) > -1)
                            {
                                input = input.Replace(uf, txtUf.Text);
                                edit = true;
                            }
                            if (!String.IsNullOrEmpty(cep) && input.IndexOf(cep) > -1)
                            {
                                input = input.Replace(cep, txtCep.Text);
                                edit = true;
                            }
                            if (!String.IsNullOrEmpty(rua) && input.IndexOf(rua) > -1)
                            {
                                input = input.Replace(rua, txtEndereco.Text);
                                edit = true;
                            }
                            if (!String.IsNullOrEmpty(num) && input.IndexOf(num) > -1)
                            {
                                input = input.Replace(num, txtNumero.Text);
                                edit = true;
                            }

                            if (edit)
                            {
                                try
                                {
                                    StreamWriter sw = new StreamWriter(ArqDigital);
                                    sw.Write(input);
                                    sw.Flush();
                                    sw.Close();
                                    MessageBox.Show("O arquivo foi editado com sucesso!");
                                }
                                catch (EndOfStreamException sex)
                                {
                                    MessageBox.Show("Erro: " + sex.Message);
                                }
                                close = true;
                            }
                        }
                        catch (FileLoadException flex)
                        {
                            MessageBox.Show("Erro: " + flex.Message);
                        }
                        Close();
                    }
                }
                else
                    MessageBox.Show("O campo CEP deve ter 8 (oito) caracteres!");
            }
            else
                MessageBox.Show("Os campos: \r\nUF, Endereco, N° e CEP, não podem ser nulos!");
        }
    }
}
