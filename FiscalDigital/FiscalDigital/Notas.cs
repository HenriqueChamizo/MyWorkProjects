using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiscalDigital
{
    public partial class Notas : Form
    {
        List<Participante> participantes = new List<Participante>();
        List<NotaFiscal> ntsFiscais = new List<NotaFiscal>();
        List<ItemNotaFiscal> itensNtsFiscais = new List<ItemNotaFiscal>();
        List<RegistroAnalitico> regsAnaliticos = new List<RegistroAnalitico>();
        List<RegistroAnalitico> regsDivergentes;
        Read read = new Read();
        double Entradas = 0;
        double Saidas = 0;
        double Pmc = 0;
        double Iva = 0;
        double CredAcul = 0;

        public Notas(Form form, List<NotaFiscal> nts, List<ItemNotaFiscal> itensNts, List<Participante> parts, List<RegistroAnalitico> regs, double ENTRADAS, 
                     double SAIDAS, double PMC, double IVA, double CREDACUL, List<RegistroAnalitico> divergentes)
        {
            InitializeComponent();
            Form frm1 = form;
            string cfop;
            regsDivergentes = divergentes;
            foreach (NotaFiscal nt in nts)
            {
                ntsFiscais.Add(new NotaFiscal()
                    {
                        ind = nt.ind,
                        CodPart = nt.CodPart,
                        CreditoEstimado = nt.CreditoEstimado,
                        CreditoGerado = nt.CreditoGerado,
                        DtNotaSaida = nt.DtNotaSaida,
                        Iva = nt.Iva,
                        NumDocSaida = nt.NumDocSaida,
                        PercCrdout = nt.PercCrdout,
                        PercentMedio = nt.PercentMedio,
                        SerieSaida = nt.SerieSaida,
                        TipoDocSaida = nt.TipoDocSaida,
                        TipoOper = nt.TipoOper,
                        ValorCrdout = nt.ValorCrdout,
                        ValorIcms = nt.ValorIcms,
                        ValorSai = nt.ValorSai
                    });

                foreach (ItemNotaFiscal iNt in itensNts)
                {
                    if (iNt.IndNtFiscal == nt.ind)
                    {
                        cfop = iNt.Cfop.Replace(".", "");
                        if (read.ValidaCFOP(cfop, 1))
                        {
                            itensNtsFiscais.Add(new ItemNotaFiscal()
                                {
                                    ind = iNt.ind,
                                    IndNtFiscal = iNt.IndNtFiscal,
                                    Cfop = cfop,
                                    IcmsDebitado = iNt.IcmsDebitado,
                                    ValorBaseCalculo = iNt.ValorBaseCalculo,
                                    ValorItem = iNt.ValorItem
                                }
                            );
                        }
                    }
                }

                foreach (RegistroAnalitico ra in regs)
                {
                    if (ra.IndNtFiscal == nt.ind)
                    {
                        ItemNotaFiscal item = itensNtsFiscais.Find(f => f.IndNtFiscal == ra.IndNtFiscal);
                        if (item == null)
                        {
                            cfop = ra.Cfop.Replace(".", "");
                            if (read.ValidaCFOP(cfop, 1))
                            {
                                regsAnaliticos.Add(new RegistroAnalitico()
                                    {
                                        ind = ra.ind,
                                        Cfop = cfop,
                                        IcmsDebitado = ra.IcmsDebitado,
                                        IndNtFiscal = ra.IndNtFiscal,
                                        ValorBaseCalculo = ra.ValorBaseCalculo,
                                        ValorOperacao = ra.ValorOperacao
                                    });
                            }
                        }
                    }
                }
            }
            participantes = parts;
            Entradas = ENTRADAS;
            Saidas = SAIDAS;
            Pmc = PMC;
            Iva = IVA;
            CredAcul = CREDACUL;


            lstNotasFiscais.Items.Add("N° da Nota - Valor da Nota - Valor do ICMS");
            List<ItemNotaFiscal> lstItemNts = new List<ItemNotaFiscal>();
            List<RegistroAnalitico> lstRegAnaNotas = new List<RegistroAnalitico>();
            foreach (NotaFiscal nt in ntsFiscais)
            {
                lstItemNts.Clear();
                lstRegAnaNotas.Clear();

                lstItemNts = itensNtsFiscais.FindAll(f => f.IndNtFiscal == nt.ind);
                if (lstItemNts.Count() >= 1)
                {
                    ListViewItem item;
                    item = new ListViewItem();
                    item.Text = nt.NumDocSaida;

                    //preenche o listview com itens
                    item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2"));
                    item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2"));
                    lstVNotasFiscais.Items.Add(item);



                    string i = nt.NumDocSaida + " - R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2") + " - R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2");
                    lstNotasFiscais.Items.Add(i);
                }

                lstRegAnaNotas = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);
                if (lstRegAnaNotas.Count() >= 1)
                {
                    ListViewItem item;
                    item = new ListViewItem();
                    item.Text = nt.NumDocSaida;

                    //preenche o listview com itens
                    item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2"));
                    item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2"));
                    lstVNotasFiscais.Items.Add(item);

                    string i = nt.NumDocSaida + " - R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2") + " - R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2");
                    lstNotasFiscais.Items.Add(i);
                }
            }
            #region Código Antigo (Comentado)

            //List<ItemNotaFiscal> lstItemNts = new List<ItemNotaFiscal>();
            //List<RegistroAnalitico> lstRegAnaNotas = new List<RegistroAnalitico>();
            //List<DataGridView> lstDataGrid = new List<DataGridView>();
            //foreach (NotaFiscal nt in ntsFiscais)
            //{
            //    lstItemNts.Clear();
            //    lstRegAnaNotas.Clear();
            //    foreach (ItemNotaFiscal intf in itensNtsFiscais)
            //    {
            //        if (intf.IndNtFiscal == nt.ind)
            //        {
            //            lstItemNts.Add(new ItemNotaFiscal()
            //                {
            //                    ind = intf.ind,
            //                    IndNtFiscal = intf.IndNtFiscal,
            //                    Cfop = intf.Cfop,
            //                    IcmsDebitado = intf.IcmsDebitado,
            //                    ValorBaseCalculo = intf.ValorBaseCalculo,
            //                    ValorItem = intf.ValorItem
            //                });
            //            lstDataGrid.Add(new DataGridView()
            //                {
            //                    Indice = intf.ind,
            //                    Numero = nt.NumDocSaida,
            //                    Cfop = intf.Cfop,
            //                    Saida = nt.ValorSai,
            //                    Item = intf.ValorItem
            //                });
            //        }
            //    }
            //    if (lstItemNts == null)
            //    {
            //        foreach (RegistroAnalitico ra in regsAnaliticos)
            //        {
            //            if (ra.IndNtFiscal == nt.ind)
            //            {
            //                if (read.ValidaCFOP(ra.Cfop.Replace(".", ""), 1))
            //                {
            //                    lstRegAnaNotas.Add(new RegistroAnalitico()
            //                        {
            //                            ind = ra.ind,
            //                            IndNtFiscal = ra.IndNtFiscal,
            //                            Cfop = ra.Cfop,
            //                            IcmsDebitado = ra.IcmsDebitado,
            //                            ValorBaseCalculo = ra.ValorBaseCalculo
            //                        });
            //                    lstDataGrid.Add(new DataGridView()
            //                    {
            //                        Indice = ra.ind,
            //                        Numero = nt.NumDocSaida,
            //                        Cfop = ra.Cfop,
            //                        Saida = nt.ValorSai,
            //                        Item = "RA"
            //                    });
            //                }
            //            }
            //        }
            //        if (lstRegAnaNotas != null)
            //        {
            //            foreach (RegistroAnalitico ra in lstRegAnaNotas)
            //            {
            //                if (ra.IndNtFiscal == nt.ind)
            //                {
            //                    string item = nt.NumDocSaida + " - R$ " + nt.ValorSai + " - R$ " + nt.ValorSai;
            //                    lstNotasFiscais.Items.Add(item);
            //                }
            //            }
            //        }
            //        foreach (ItemNotaFiscal iNt in lstItemNts)
            //        {
            //            if (iNt.IndNtFiscal == nt.ind)
            //            {
            //                string item = nt.NumDocSaida + " - R$ " + nt.ValorSai + " - R$ " + iNt.ValorItem;
            //                lstNotasFiscais.Items.Add(item);
            //            }
            //        }
            //    }
            //}

            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(DataGridView));
            //DataTable table = new DataTable();
            //foreach (PropertyDescriptor prop in properties)
            //    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            //foreach (DataGridView item in lstDataGrid)
            //{
            //    DataRow row = table.NewRow();
            //    foreach (PropertyDescriptor prop in properties)
            //        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            //    table.Rows.Add(row);
            //}
            //dgvListaItens.DataSource = table;

            #endregion
        }

        private void Limpar(int i = 0)
        {
            if (i == 0)
            {
                #region TUDO
                //CFOP
                txtVlrIcmsCFOP.Clear();
                txtVlrContabilCFOP.Clear();
                txtBsCalculoCFOP.Clear();

                //Detalhes Nota Fiscal
                txtCredAcumulado.Clear();
                txtCredEstimado.Clear();
                txtData.Clear();
                txtNumero.Clear();
                txtSerie.Clear();
                txtTipo.Clear();
                txtVlrDoc.Clear();
                txtVlrIcms.Clear();

                //Detalhes Item Nota
                txtIndItem.Clear();
                txtVlrItem.Clear();
                txtVlrItemIcms.Clear();
                txtVlrBsItem.Clear();
                txtCfopItem.Clear();
                lstItens.Items.Clear();
                #endregion
            }
            else if (i == 1)
            {
                #region TUDO menos CFOP
                //Detalhes Nota Fiscal
                txtCredAcumulado.Clear();
                txtCredEstimado.Clear();
                txtData.Clear();
                txtNumero.Clear();
                txtSerie.Clear();
                txtTipo.Clear();
                txtVlrDoc.Clear();
                txtVlrIcms.Clear();

                //Detalhes Item Nota
                txtIndItem.Clear();
                txtVlrItem.Clear();
                txtVlrItemIcms.Clear();
                txtVlrBsItem.Clear();
                txtCfopItem.Clear();
                lstItens.Items.Clear();
                #endregion
            }
            else if (i == 2)
            {
                #region Detalhes ItemNota
                //Detalhes Item Nota
                txtIndItem.Clear();
                txtVlrItem.Clear();
                txtVlrItemIcms.Clear();
                txtVlrBsItem.Clear();
                txtCfopItem.Clear();
                #endregion
            }
        }

        private void Notas_Load(object sender, EventArgs e)
        {
            #region ListView Notas Fiscais
            lstVNotasFiscais.View = View.Details;
            lstVNotasFiscais.MultiSelect = true;

            ColumnHeader NNota = new ColumnHeader();
            NNota.Text = "Nº da Nota";
            NNota.Width = 100;
            NNota.TextAlign = HorizontalAlignment.Center;
            lstVNotasFiscais.Columns.Add(NNota);
            ColumnHeader VlrNota = new ColumnHeader();
            VlrNota.Text = "Valor Nota";
            VlrNota.Width = 100;
            VlrNota.TextAlign = HorizontalAlignment.Right;
            lstVNotasFiscais.Columns.Add(VlrNota);
            ColumnHeader VlrIcms = new ColumnHeader();
            VlrIcms.Text = "Valor ICMS";
            VlrIcms.Width = 100;
            VlrIcms.TextAlign = HorizontalAlignment.Right;
            lstVNotasFiscais.Columns.Add(VlrIcms);
            #endregion
            #region ListView Itens Notas Fiscais
            lstVItens.View = View.Details;
            lstVItens.MultiSelect = true;

            ColumnHeader Tipo = new ColumnHeader();
            Tipo.Text = "Tipo";
            Tipo.Width = 40;
            Tipo.TextAlign = HorizontalAlignment.Center;
            lstVItens.Columns.Add(Tipo);
            ColumnHeader Indice = new ColumnHeader();
            Indice.Text = "Índice";
            Indice.Width = 45;
            Indice.TextAlign = HorizontalAlignment.Right;
            lstVItens.Columns.Add(Indice);
            ColumnHeader VlrItem = new ColumnHeader();
            VlrItem.Text = "Valor Operação";
            VlrItem.Width = 100;
            VlrItem.TextAlign = HorizontalAlignment.Right;
            lstVItens.Columns.Add(VlrItem);
            ColumnHeader VlrICMS = new ColumnHeader();
            VlrICMS.Text = "Valor ICMS";
            VlrICMS.Width = 100;
            VlrICMS.TextAlign = HorizontalAlignment.Right;
            lstVItens.Columns.Add(VlrICMS);
            #endregion

            txtEntradas.Text = "R$ " + Entradas.ToString("N2");
            txtSaidas.Text = "R$ " + Saidas.ToString("N2");
            txtPmc.Text = Pmc.ToString("N2") + " %";
            txtIva.Text = "R$ " + Iva.ToString("N2");
            txtCreditAcul.Text = "R$ " + CredAcul.ToString("N2");

            if (regsDivergentes.Count == 0)
                divergênciasToolStripMenuItem.Visible = false;
            else
                divergênciasToolStripMenuItem.Visible = true;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Limpar();
            if (!String.IsNullOrEmpty(txtCfopFilter.Text))
            {
                double BaseCalculo = 0;
                double ValorContabil = 0;
                double ValorIcms = 0;
                bool valid;
                lstNotasFiscais.Items.Clear();
                lstVNotasFiscais.Items.Clear();
                lstNotasFiscais.Items.Add("N° da Nota - Valor da Nota - Valor do ICMS");
                foreach (NotaFiscal nt in ntsFiscais)
                {
                    valid = false;
                    List<ItemNotaFiscal> itens = null;
                    itens = itensNtsFiscais.FindAll(f => f.IndNtFiscal == nt.ind);
                    if (itens.Count() >= 1)
                    {
                        foreach (ItemNotaFiscal ints in itens)
                        {
                            //Fazer aparecer as notas com o filtro pelo CFOP
                            if (ints.Cfop.Replace(".", "") == txtCfopFilter.Text)
                            {
                                valid = true;
                                BaseCalculo = BaseCalculo + Convert.ToDouble(ints.ValorBaseCalculo);
                                ValorContabil = ValorContabil + Convert.ToDouble(ints.ValorItem);
                                ValorIcms = ValorIcms + Convert.ToDouble(ints.IcmsDebitado);
                            }
                        }

                        if (valid)
                        {
                            ListViewItem item;
                            item = new ListViewItem();
                            item.Text = nt.NumDocSaida;

                            //preenche o listview com itens
                            item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2"));
                            item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2"));
                            lstVNotasFiscais.Items.Add(item);

                            string itemm = nt.NumDocSaida + " - R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2") + " - R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2");
                            lstNotasFiscais.Items.Add(itemm);
                        }

                    }
                    else
                    {
                        valid = false;
                        List<RegistroAnalitico> regA = null;
                        regA = regsAnaliticos.FindAll(f => f.IndNtFiscal == nt.ind);
                        if (regA.Count() >= 1)
                        {
                            foreach (RegistroAnalitico ra in regA)
                            {
                                if (ra.Cfop.Replace(".", "") == txtCfopFilter.Text)
                                {
                                    valid = true;
                                    BaseCalculo = BaseCalculo + Convert.ToDouble(ra.ValorBaseCalculo);
                                    
                                    //if (ra.ValorOperacao == null)
                                    //    ValorContabil = ValorContabil + Convert.ToDouble(nt.ValorSai);
                                    //else
                                        ValorContabil = ValorContabil + Convert.ToDouble(ra.ValorOperacao);

                                    ValorIcms = ValorIcms + Convert.ToDouble(ra.IcmsDebitado);
                                }
                            }

                            if (valid)
                            {
                                ListViewItem item;
                                item = new ListViewItem();
                                item.Text = nt.NumDocSaida;

                                //preenche o listview com itens
                                item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2"));
                                item.SubItems.Add("R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2"));
                                lstVNotasFiscais.Items.Add(item);

                                string itemm = nt.NumDocSaida + " - R$ " + Convert.ToDouble(nt.ValorSai).ToString("N2") + " - R$ " + Convert.ToDouble(nt.ValorIcms).ToString("N2");
                                lstNotasFiscais.Items.Add(itemm);
                            }
                        }
                    }
                }

                if (BaseCalculo != 0)
                    txtBsCalculoCFOP.Text = "R$ " + BaseCalculo.ToString("N2");
                if (ValorContabil != 0)
                    txtVlrContabilCFOP.Text = "R$ " + ValorContabil.ToString("N2");
                if (ValorIcms != 0)
                    txtVlrIcmsCFOP.Text = "R$ " + ValorIcms.ToString("N2");
            }
        }

        private void lstNotasFiscais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNotasFiscais.SelectedItem != null && lstNotasFiscais.SelectedIndex != 0)
            {
                Limpar(2);
                string item = lstNotasFiscais.SelectedItem.ToString();
                String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                string numeroNota = split[0];
                NotaFiscal ntSel = ntsFiscais.Find(f => f.NumDocSaida == numeroNota);
                List<ItemNotaFiscal> lstItensNt = null;
                List<RegistroAnalitico> lstRegAna = null;
                if (ntSel != null)
                {
                    lstItensNt = itensNtsFiscais.FindAll(f => f.IndNtFiscal == ntSel.ind);

                    //Detalhes da nota
                    txtVlrIcms.Text = "R$ " + Convert.ToDouble(ntSel.ValorIcms).ToString("N2");
                    txtVlrDoc.Text = "R$ " + Convert.ToDouble(ntSel.ValorSai).ToString("N2");
                    txtTipo.Text = ntSel.TipoDocSaida.ToString();
                    txtCredEstimado.Text = ntSel.CreditoEstimado == null ? "" : "R$ " + Convert.ToDouble(ntSel.CreditoEstimado).ToString("N2");
                    txtCredAcumulado.Text = ntSel.CreditoGerado == null ? "" : "R$ " + Convert.ToDouble(ntSel.CreditoGerado).ToString("N2");
                    if (!String.IsNullOrEmpty(ntSel.DtNotaSaida))
                    {
                        string dia = ntSel.DtNotaSaida.Substring(0, 2);
                        string mes = ntSel.DtNotaSaida.Substring(2, 2);
                        string ano = ntSel.DtNotaSaida.Substring(4, 4);
                        txtData.Text = Convert.ToDateTime(dia + "/" + mes + "/" + ano).ToString().Replace(" 00:00:00", "");
                    }
                    else
                        txtData.Text = "";
                    txtSerie.Text = ntSel.SerieSaida.ToString();
                    txtNumero.Text = ntSel.NumDocSaida.ToString();

                    if (lstItensNt.Count >= 1)
                    {
                        lstItens.Items.Clear();
                        lstItens.Items.Add("Tipo - Indice - Valor do Item - Valor do ICMS");
                        //Lista de itens
                        foreach (ItemNotaFiscal it in lstItensNt)
                        {
                            string indIt = it.ind.ToString();
                            string i = "I - " + indIt + " - R$ " + Convert.ToDouble(it.ValorItem).ToString("N2") + " - R$ " + Convert.ToDouble(it.IcmsDebitado).ToString("N2");
                            lstItens.Items.Add(i);
                        }
                    }
                    else
                    {
                        lstRegAna = regsAnaliticos.FindAll(f => f.IndNtFiscal == ntSel.ind);
                        if (lstRegAna.Count() >= 1)
                        {
                            lstItens.Items.Clear();
                            lstItens.Items.Add("Tipo - Indice - Valor da Operação - Valor do ICMS");
                            //Lista de Registros
                            foreach (RegistroAnalitico ra in lstRegAna)
                            {
                                string indRa = ra.ind.ToString();
                                string i = "R - " + indRa + " - R$ " + Convert.ToDouble(ra.ValorOperacao).ToString("N2") + " - R$ " + Convert.ToDouble(ra.IcmsDebitado).ToString("N2");
                                lstItens.Items.Add(i);
                            }
                        }
                        else
                        {
                            lstItens.Items.Clear();
                            lstItens.Items.Add("Esta nota não tem itens");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro na seleção da nota.\r\nContate o Administrador.");
                }
            }
            else
            {
                Limpar(1);
            }
        }

        private void resetarToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstItens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstItens.SelectedItem != null && lstItens.SelectedIndex != 0)
            {
                string item = lstItens.SelectedItem.ToString();
                String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                string ItemOrRa = split[0];
                if (ItemOrRa == "I")
                {
                    int indItem = Convert.ToInt32(split[1]);
                    ItemNotaFiscal iNt = itensNtsFiscais.Find(f => f.ind == indItem);
                    if (iNt != null)
                    {
                        txtIndItem.Text = iNt.ind.ToString();
                        txtVlrItem.Text = "R$ " + Convert.ToDouble(iNt.ValorItem).ToString("N2");
                        txtVlrItemIcms.Text = "R$ " + Convert.ToDouble(iNt.IcmsDebitado).ToString("N2");
                        txtVlrBsItem.Text = "R$ " + Convert.ToDouble(iNt.ValorBaseCalculo).ToString("N2");
                        txtCfopItem.Text = iNt.Cfop;
                        lblTitleItemRa.Text = "Detalhes do Item da Nota";
                        lblVlrItemRa.Text = "Valor do Item";
                    }
                }
                else if (ItemOrRa == "R")
                {
                    int indRa = Convert.ToInt32(split[1]);
                    RegistroAnalitico raNt = regsAnaliticos.Find(f => f.ind == indRa);
                    if (raNt != null)
                    {
                        txtIndItem.Text = raNt.ind.ToString();
                        txtVlrItem.Text = "R$ " + Convert.ToDouble(raNt.ValorOperacao).ToString("N2");
                        txtVlrItemIcms.Text = "R$ " + Convert.ToDouble(raNt.IcmsDebitado).ToString("N2");
                        txtVlrBsItem.Text = "R$ " + Convert.ToDouble(raNt.ValorBaseCalculo).ToString("N2");
                        txtCfopItem.Text = raNt.Cfop;
                        lblTitleItemRa.Text = "Detalhes do Registro da Nota";
                        lblVlrItemRa.Text = "Valor do Registro";
                    }
                }
            }
            else
            {
                Limpar(2);
            }
        }

        private void divergênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Divergencias div = new Divergencias(ntsFiscais, regsAnaliticos, regsDivergentes);
            div.Show();
        }

        private void lstVNotasFiscais_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstVItens.Items.Clear();
            if (lstVNotasFiscais.SelectedItems != null)
            {
                Limpar(1);
                ListView.SelectedListViewItemCollection items = null;
                ListViewItem item = null;
                items = lstVNotasFiscais.SelectedItems;
                foreach (ListViewItem it in items)
                {
                    item = it;
                }
                
                //string item = lstNotasFiscais.SelectedItem.ToString();
                //String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                string numeroNota = "";
                try
                {
                    numeroNota = item.SubItems[0].ToString().Replace("ListViewSubItem: {", "").Replace("}", "").Replace(" ", "");
                    NotaFiscal ntSel = ntsFiscais.Find(f => f.NumDocSaida == numeroNota);
                    List<ItemNotaFiscal> lstItensNt = null;
                    List<RegistroAnalitico> lstRegAna = null;
                    if (ntSel != null)
                    {
                        lstItensNt = itensNtsFiscais.FindAll(f => f.IndNtFiscal == ntSel.ind);

                        //Detalhes da nota
                        txtVlrIcms.Text = "R$ " + Convert.ToDouble(ntSel.ValorIcms).ToString("N2");
                        txtVlrDoc.Text = "R$ " + Convert.ToDouble(ntSel.ValorSai).ToString("N2");
                        txtTipo.Text = ntSel.TipoDocSaida.ToString();
                        txtCredEstimado.Text = ntSel.CreditoEstimado == null ? "" : "R$ " + Convert.ToDouble(ntSel.CreditoEstimado).ToString("N2");
                        txtCredAcumulado.Text = ntSel.CreditoGerado == null ? "" : "R$ " + Convert.ToDouble(ntSel.CreditoGerado).ToString("N2");
                        if (!String.IsNullOrEmpty(ntSel.DtNotaSaida))
                        {
                            string dia = ntSel.DtNotaSaida.Substring(0, 2);
                            string mes = ntSel.DtNotaSaida.Substring(2, 2);
                            string ano = ntSel.DtNotaSaida.Substring(4, 4);
                            txtData.Text = Convert.ToDateTime(dia + "/" + mes + "/" + ano).ToString().Replace(" 00:00:00", "");
                        }
                        else
                            txtData.Text = "";
                        txtSerie.Text = ntSel.SerieSaida.ToString();
                        txtNumero.Text = ntSel.NumDocSaida.ToString();

                        if (lstItensNt.Count >= 1)
                        {
                            lstItens.Items.Clear();
                            lstItens.Items.Add("Tipo - Indice - Valor do Item - Valor do ICMS");
                            //Lista de itens
                            foreach (ItemNotaFiscal it in lstItensNt)
                            {
                                string indIt = it.ind.ToString();
                                string i = "I - " + indIt + " - R$ " + Convert.ToDouble(it.ValorItem).ToString("N2") + " - R$ " + Convert.ToDouble(it.IcmsDebitado).ToString("N2");
                                lstItens.Items.Add(i);


                                ListViewItem iten;
                                iten = new ListViewItem();
                                iten.Text = "I";

                                //preenche o listview com itens
                                iten.SubItems.Add(it.ind.ToString());
                                iten.SubItems.Add("R$ " + Convert.ToDouble(it.ValorItem).ToString("N2"));
                                iten.SubItems.Add("R$ " + Convert.ToDouble(it.IcmsDebitado).ToString("N2"));
                                lstVItens.Items.Add(iten);
                            }
                        }
                        else
                        {
                            lstRegAna = regsAnaliticos.FindAll(f => f.IndNtFiscal == ntSel.ind);
                            if (lstRegAna.Count() >= 1)
                            {
                                lstItens.Items.Clear();
                                lstItens.Items.Add("Tipo - Indice - Valor da Operação - Valor do ICMS");
                                //Lista de Registros
                                foreach (RegistroAnalitico ra in lstRegAna)
                                {
                                    string indRa = ra.ind.ToString();
                                    string i = "R - " + indRa + " - R$ " + Convert.ToDouble(ra.ValorOperacao).ToString("N2") + " - R$ " + Convert.ToDouble(ra.IcmsDebitado).ToString("N2");
                                    lstItens.Items.Add(i);



                                    ListViewItem iten;
                                    iten = new ListViewItem();
                                    iten.Text = "R";

                                    //preenche o listview com itens
                                    iten.SubItems.Add(ra.ind.ToString());
                                    iten.SubItems.Add("R$ " + Convert.ToDouble(ra.ValorOperacao).ToString("N2"));
                                    iten.SubItems.Add("R$ " + Convert.ToDouble(ra.IcmsDebitado).ToString("N2"));
                                    lstVItens.Items.Add(iten);
                                }
                            }
                            else
                            {
                                lstItens.Items.Clear();
                                lstItens.Items.Add("Esta nota não tem itens");

                                lstVItens.Items.Clear();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um erro na seleção da nota.\r\nContate o Administrador.");
                    }
                }
                catch { } 
                    //MessageBox.Show("COCÔ"); }
            }
            else
            {
                Limpar(1);
            }
        }

        private void lstVItens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVItens.SelectedItems != null)
            {
                Limpar(2);
                ListView.SelectedListViewItemCollection items = null;
                ListViewItem item = null;
                items = lstVItens.SelectedItems;
                foreach (ListViewItem it in items)
                {
                    item = it;
                }

                string ItemOrRa = "";

                try
                {
                    //string item = lstItens.SelectedItem.ToString();
                    //String[] split = item.Split(new String[] { " - " }, StringSplitOptions.None);
                    ItemOrRa = item.SubItems[0].ToString().Replace("ListViewSubItem: {", "").Replace("}", "").Replace(" ", "");
                    if (ItemOrRa == "I")
                    {
                        int indItem = Convert.ToInt32(item.SubItems[1].ToString().Replace("ListViewSubItem: {", "").Replace("}", "").Replace(" ", ""));
                        ItemNotaFiscal iNt = itensNtsFiscais.Find(f => f.ind == indItem);
                        if (iNt != null)
                        {
                            txtIndItem.Text = iNt.ind.ToString();
                            txtVlrItem.Text = "R$ " + Convert.ToDouble(iNt.ValorItem).ToString("N2");
                            txtVlrItemIcms.Text = "R$ " + Convert.ToDouble(iNt.IcmsDebitado).ToString("N2");
                            txtVlrBsItem.Text = "R$ " + Convert.ToDouble(iNt.ValorBaseCalculo).ToString("N2");
                            txtCfopItem.Text = iNt.Cfop;
                            lblTitleItemRa.Text = "Detalhes do Item da Nota";
                            lblVlrItemRa.Text = "Valor do Item";
                        }
                    }
                    else if (ItemOrRa == "R")
                    {
                        int indRa = Convert.ToInt32(item.SubItems[1].ToString().Replace("ListViewSubItem: {", "").Replace("}", "").Replace(" ", ""));
                        RegistroAnalitico raNt = regsAnaliticos.Find(f => f.ind == indRa);
                        if (raNt != null)
                        {
                            txtIndItem.Text = raNt.ind.ToString();
                            txtVlrItem.Text = "R$ " + Convert.ToDouble(raNt.ValorOperacao).ToString("N2");
                            txtVlrItemIcms.Text = "R$ " + Convert.ToDouble(raNt.IcmsDebitado).ToString("N2");
                            txtVlrBsItem.Text = "R$ " + Convert.ToDouble(raNt.ValorBaseCalculo).ToString("N2");
                            txtCfopItem.Text = raNt.Cfop;
                            lblTitleItemRa.Text = "Detalhes do Registro da Nota";
                            lblVlrItemRa.Text = "Valor do Registro";
                        }
                    }
                }
                catch { }
            }
            else
            {
                Limpar(2);
            }
        }
    }
}
