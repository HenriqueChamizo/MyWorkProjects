using Model.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc.Ajax;

namespace GedScannerMVC.ClassView
{
    public class TableColunas
    {
        public int tabindex;
        public string aria_controls;
        public int rowspan;
        public int colspan;
        public string aria_sort;
        public string aria_label;
        public string Text;
        public string Style;
        public string CssClass;
    }

    public class TableLinhas
    {
        public string CssClass;

        public List<TableCelulas> cells;
    }

    public class TableCelulas
    {
        public string ID;
        public string Name;
        public string Value;
        public string CssClass;
        public string Style;
        public bool CheckboxSwitch = false;
        public TableLink link;
        public TableAjaxLink AjaxLink;
    }

    public class TableLink
    {
        public string ID;
        public string href;
        public string url;
        public string action;
        public string controller;
        public object route;
        public List<string> parans;
    }

    public class TableAjaxLink
    {
        public string ID;
        public string action;
        public string controller;
        public object route;
        public AjaxOptions ajaxOptions;
    }

    public class TableOfWeb
    {
        public string LeftTitle { get; set; }
        public string LeftValue { get; set; }

        public string RightTitle { get; set; }
        public string RightValue { get; set; }

        public bool CheckboxSwitch { get; set; }

        public string Empty { get; set; }
        public string CssClass { get; set; }

        public List<TableColunas> columns;
        public List<TableLinhas> rows;
        
        public TableOfWeb(List<TableColunas> colunas, 
                          List<TableLinhas> linhas, 
                          string empty)
        {
            columns = colunas;
            rows = linhas;
            if (string.IsNullOrEmpty(empty))
                Empty = "Sem registros!";
            else
                Empty = empty;

            CheckboxSwitch = false;
        }
    }

    public enum TableDataType
    {
        [Description("PlanoContas")]
        PlanoContas = 1,

        [Description("Conta")]
        Conta = 2,

        [Description("Dashboard")]
        Dashboard = 3,

        [Description("Lote")]
        Lote = 4,

        [Description("ArquivoTipo")]
        ArquivoTipo = 5,

        [Description("ContaTipo")]
        ContaTipo = 6
    }

    public enum TableType
    {
        [Description("Default")]
        Default = 0, 

        [Description("BoxSolid")]
        BoxSolid = 1, 

        [Description("StripedUnBorder")]
        StripedUnBorder = 2
    }

    public class TableData
    {
        public TableDataType Type { get; set; }
        public System.Web.HttpSessionStateBase Session { get; set; }
        public System.Web.HttpRequestBase Request { get; set; }

        public TableData(TableDataType type)
        {
            Type = type;
        }

        public TableOfWeb GetDataTable(List<object> objs, int typePage = 0)
        {
            List<TableColunas> columns;
            TableColunas column;
            List<TableLinhas> rows;
            TableLinhas row;
            TableCelulas cell;
            TableOfWeb table;
            switch (Type)
            {
                case TableDataType.Dashboard:
                    #region Dashboard
                    if (typePage == 1)
                    {
                        #region Convert Object
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        foreach (object obj in objs)
                        {
                            arquivos.Add(obj as Model.Ged.Arquivo);
                        }
                        #endregion

                        #region columns
                        columns = new List<TableColunas>();
                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_IND";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DESCRICAO";
                        //column.Style = "width:300px";
                        column.CssClass = "col-sm-4 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DISPONIVELEM";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DESCRICAO";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Lote";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_TAMANHO";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Tam";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<TableLinhas>();

                        foreach (Model.Ged.Arquivo arquivo in arquivos)
                        {
                            row = new TableLinhas();
                            row.cells = new List<TableCelulas>();

                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = arquivo.GEDARQ_IND.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-4 cell";
                            cell.Value = arquivo.GEDARQ_DESCRICAO.ToString();
                            ClassFrm.FrmLoteArquivo lotearquivo = new ClassFrm.FrmLoteArquivo()
                            {
                                indice = arquivo.GEDARQ_IND.ToString(),
                                typesPage = "1"
                            };
                            cell.link = (new ClassView.TableLink()
                            {
                                ID = "link-" + arquivo.GEDARQ_IND.ToString(),
                                url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
                            });
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = arquivo.GEDARQ_LOTE.GLOTE_DESCRICAO;
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = string.Format("{0:N2}", (Convert.ToDouble(arquivo.GEDARQ_TAMANHO) / 1000000d)) + "MB";
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new ClassView.TableOfWeb(columns, rows, "Sem Arquivos não classificados!");
                        table.LeftTitle = "Arquivos";
                        table.LeftValue = "Não Classificados";
                    }
                    else if (typePage == 2)
                    {
                        #region Convert Object
                        List<Model.Ged.Lote> lotesf = new List<Model.Ged.Lote>();
                        foreach (object obj in objs)
                        {
                            lotesf.Add(obj as Model.Ged.Lote);
                        }
                        #endregion

                        #region columns
                        columns = new List<TableColunas>();
                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_IND";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DESCRICAO";
                        //column.Style = "width:350px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DISPONIVELEM";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DISPONIVELPOR";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Criado por";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "N_ARQUIVOS";
                        //column.Style = "width:60px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Arquivos";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "N_ARQUIVOS_NCLASSIC";
                        //column.Style = "width:60px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Sem Classificação";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<TableLinhas>();

                        foreach (Model.Ged.Lote lote in lotesf)
                        {
                            row = new TableLinhas();
                            row.cells = new List<TableCelulas>();

                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = lote.GLOTE_IND.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = lote.GLOTE_DESCRICAO.ToString();
                            cell.link = (new TableLink()
                            {
                                ID = "link-" + lote.GLOTE_IND.ToString(),
                                url = "~/Lote/Detail/" + lote.GLOTE_IND.ToString()
                            });
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = lote.GLOTE_DISPONIVELEM.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = lote.GLOTE_DISPONIVELPOR.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Style = "color: GREEN";
                            cell.Value = lote.N_ARQUIVOS.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Style = "color: RED";
                            cell.Value = lote.N_ARQUIVOS_NCLASSIC.ToString();
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new TableOfWeb(columns, rows, "Sem Lote aberto!");
                        table.LeftTitle = "Lote";
                        table.LeftValue = "Abertos";
                    }
                    else if (typePage == 3)
                    {
                        #region Convert Object
                        List<Model.Ged.Lote> lotese = new List<Model.Ged.Lote>();
                        foreach (object obj in objs)
                        {
                            lotese.Add(obj as Model.Ged.Lote);
                        }
                        #endregion

                        #region columns
                        columns = new List<TableColunas>();
                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_IND";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DESCRICAO";
                        //column.Style = "width:350px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DISPONIVELEM";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DISPONIVELPOR";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Criado por";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "N_ARQUIVOS";
                        //column.Style = "width:60px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Arquivos";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "N_ARQUIVOS_NCLASSIC";
                        //column.Style = "width:60px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Sem Classificação";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<TableLinhas>();

                        foreach (Model.Ged.Lote lote in lotese)
                        {
                            row = new TableLinhas();
                            row.cells = new List<TableCelulas>();

                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = lote.GLOTE_IND.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = lote.GLOTE_DESCRICAO.ToString();
                            cell.link = (new TableLink()
                            {
                                ID = "link-" + lote.GLOTE_IND.ToString(),
                                url = "~/Lote/Detail/" + lote.GLOTE_IND.ToString()
                            });
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = lote.GLOTE_DISPONIVELEM.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = lote.GLOTE_DISPONIVELPOR.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Style = "color: GREEN";
                            cell.Value = lote.N_ARQUIVOS.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Style = "color: RED";
                            cell.Value = lote.N_ARQUIVOS_NCLASSIC.ToString();
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new TableOfWeb(columns, rows, "Sem Lote enviados!");
                        table.LeftTitle = "Lote";
                        table.LeftValue = "Enviados";
                    }
                    else if (typePage == 4)
                    {
                        #region Convert Object
                        List<Model.Ged.ArquivoTipo> tipos = new List<Model.Ged.ArquivoTipo>();
                        foreach (object obj in objs)
                        {
                            tipos.Add(obj as Model.Ged.ArquivoTipo);
                        }
                        #endregion

                        #region columns
                        columns = new List<TableColunas>();
                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_IND";
                        //column.Style = "width:20px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_DESCRICAO";
                        //column.Style = "width:310px";
                        column.CssClass = "col-sm-4 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_EXPORTA";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Exporta";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_DT_INICIO";
                        //column.Style = "width:100px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_LOGININSERT";
                        //column.Style = "width:100px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Criado por";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<TableLinhas>();
                        foreach (Model.Ged.ArquivoTipo tipo in tipos)
                        {
                            row = new TableLinhas();
                            row.cells = new List<TableCelulas>();

                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = tipo.GEDTIPO_IND.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-4 cell";
                            cell.Value = tipo.GEDTIPO_DESCRICAO.ToString();
                            cell.link = (new TableLink()
                            {
                                ID = "link-" + tipo.GEDTIPO_IND.ToString(),
                                //url = "~/ArquivoTipo/Detail/" + tipo.GEDTIPO_IND.ToString(), 
                                action = "Detail/" + tipo.GEDTIPO_IND.ToString(),
                                controller = "ArquivoTipo",
                                route = tipo
                            });
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = tipo.GEDTIPO_EXPORTA ? "Sim" : "Não";
                            cell.Style = tipo.GEDTIPO_EXPORTA ? "color: green" : "color: red";
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = tipo.GEDTIPO_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = tipo.GEDTIPO_LOGININSERT.ToString();
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new TableOfWeb(columns, rows, "Sem Tipo de Arquivo Cadastrado!");
                        table.LeftTitle = "Tipos de Arquivos";
                    }
                    else
                        table = new TableOfWeb(null, null, null);
                    #endregion
                    return table;
                case TableDataType.PlanoContas:
                    #region PlanoContas
                    #region Convert Object
                    List<Model.PlanoContas> planosContas = new List<Model.PlanoContas>();
                    foreach (object obj in objs)
                    {
                        planosContas.Add(obj as Model.PlanoContas);
                    }
                    #endregion

                    #region columns
                    columns = new List<TableColunas>();
                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "PLAN_IND";
                    //column.Style = "min-width:20px";
                    column.CssClass = "col-sm-1 cell";
                    column.Text = "Ind";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "PLAN_DESCRICAO";
                    //column.Style = "min-width:200px";
                    column.CssClass = "col-sm-4 cell";
                    column.Text = "Descrição";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "PLAN_CODIGO";
                    //column.Style = "min-width:40px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Código";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "PLAN_DT_INICIO";
                    //column.Style = "min-width:100px";
                    column.Text = "Criado em";
                    column.CssClass = "col-sm-3 cell";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "PLAN_LOGININSERT";
                    //column.Style = "min-width:100px";
                    column.Text = "Criado por";
                    column.CssClass = "col-sm-2 cell";
                    columns.Add(column);
                    #endregion

                    #region rows
                    rows = new List<TableLinhas>();

                    List<Model.PlanoContas> PlanConts = new List<Model.PlanoContas>();
                    foreach (Model.PlanoContas plano in planosContas)
                    {
                        if (plano.PLAN_FECHADO)
                        {
                            //view.tableFinalizadasVisible = true;

                            List<Model.PlanoContas> codigos = planosContas.FindAll(f => f.PLAN_CODIGO == plano.PLAN_CODIGO);
                            if (codigos.Count == 1)
                                PlanConts.Add(plano);
                            else
                            {
                                bool maior = false;
                                foreach (Model.PlanoContas planoM in codigos)
                                {
                                    if (plano.PLAN_DT_INICIO > planoM.PLAN_DT_INICIO)
                                        maior = true;
                                    else
                                        maior = false;
                                }
                                if (maior)
                                    PlanConts.Add(plano);
                            }
                        }
                    }

                    foreach (Model.PlanoContas plano in PlanConts)
                    {
                        row = new TableLinhas();
                        row.cells = new List<TableCelulas>();

                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-1 cell";
                        cell.Value = plano.PLAN_IND.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-4 cell";
                        cell.Value = plano.PLAN_DESCRICAO.ToString();
                        cell.link = (new TableLink()
                        {
                            ID = "link-" + plano.PLAN_IND.ToString(),
                            url = "~/PlanoContas/Plano/" + plano.PLAN_IND.ToString()
                        });
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = plano.PLAN_CODIGO.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-3 cell";
                        cell.Value = plano.PLAN_DT_INICIO.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = plano.PLAN_LOGININSERT.ToString();
                        row.cells.Add(cell);

                        rows.Add(row);
                    }
                    #endregion

                    table = new TableOfWeb(columns, rows, "Sem Plano de contas Cadastrado!");
                    table.LeftTitle = "Plano de Contas";
                    table.LeftValue = "Fechados";
                    #endregion
                    return table;
                case TableDataType.Conta:
                    #region Conta
                    #region Convert Object
                    List<Model.Conta> contas = new List<Model.Conta>();
                    foreach (object obj in objs)
                    {
                        contas.Add(obj as Model.Conta);
                    }
                    #endregion

                    #region columns
                    columns = new List<TableColunas>();
                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONT_IND";
                    column.CssClass = "col-sm-1 cell";
                    //column.Style = "width:20px";
                    column.Text = "Ind";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONT_DESCRICAO";
                    column.CssClass = "col-sm-3 cell";
                    //column.Style = "width:300px";
                    column.Text = "Descrição";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONT_ACESSO";
                    column.CssClass = "col-sm-2 cell";
                    //column.Style = "width:40px";
                    column.Text = "Código";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONT_DT_INICIO";
                    column.CssClass = "col-sm-2 cell";
                    //column.Style = "width:110px";
                    column.Text = "Criado em";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONT_LOGININSERT";
                    //column.Style = "width:100px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Criado por";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "VINCULADO";
                    //column.Style = "width:90px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Vinculado?";
                    columns.Add(column);
                    #endregion

                    #region rows
                    rows = new List<TableLinhas>();

                    foreach (Model.Conta cont in contas)
                    {
                        row = new TableLinhas();
                        row.cells = new List<TableCelulas>();

                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-1 cell";
                        cell.Value = cont.CONT_IND.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-3 cell";
                        cell.Value = cont.CONT_DESCRICAO.ToString();
                        cell.link = (new TableLink()
                        {
                            ID = "link-" + cont.CONT_IND.ToString(),
                            url = "~/Conta/Index/" + cont.CONT_IND.ToString()
                        });
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = cont.CONT_ACESSO.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = cont.CONT_DT_INICIO.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = cont.CONT_LOGININSERT.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = cont.condition ? "Sim" : "Não";
                        cell.Style = "color: " + (cont.condition ? "green" : "red");
                        row.cells.Add(cell);

                        rows.Add(row);
                    }
                    #endregion

                    table = new TableOfWeb(columns, rows, "Sem Conta Cadastrada!");
                    table.LeftTitle = "Contas";
                    table.LeftValue = "Todas";
                    table.CheckboxSwitch = true;
                    #endregion
                    return table;
                case TableDataType.Lote:
                    #region Lote
                    if (typePage == 1)
                    {
                        #region Convert Object
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        foreach (object obj in objs)
                        {
                            arquivos.Add(obj as Model.Ged.Arquivo);
                        }
                        #endregion

                        #region columns
                        columns = new List<TableColunas>();
                        column = new TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_IND";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DESCRICAO";
                        //column.Style = "width:300px";
                        column.CssClass = "col-sm-4 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DISPONIVELEM";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GLOTE_DESCRICAO";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-3 cell";
                        column.Text = "Lote";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_TAMANHO";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Tam";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<TableLinhas>();

                        foreach (Model.Ged.Arquivo arquivo in arquivos)
                        {
                            row = new TableLinhas();
                            row.cells = new List<TableCelulas>();

                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = arquivo.GEDARQ_IND.ToString();
                            row.cells.Add(cell);
                            cell = new TableCelulas();
                            cell.CssClass = "col-sm-4 cell";
                            cell.Value = arquivo.GEDARQ_DESCRICAO.ToString();
                            ClassFrm.FrmLoteArquivo lotearquivo = new ClassFrm.FrmLoteArquivo()
                            {
                                indice = arquivo.GEDARQ_IND.ToString(),
                                typesPage = "1"
                            };
                            cell.link = (new ClassView.TableLink()
                            {
                                ID = "link-" + arquivo.GEDARQ_IND.ToString(),
                                url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
                            });
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-3 cell";
                            cell.Value = arquivo.GEDARQ_LOTE.GLOTE_DESCRICAO;
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = string.Format("{0:N2}", (Convert.ToDouble(arquivo.GEDARQ_TAMANHO) / 1000000d)) + "MB";
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new ClassView.TableOfWeb(columns, rows, "Sem Arquivos não classificados!");
                        table.LeftTitle = "Arquivos";
                        table.LeftValue = "Não Classificados";
                    }
                    else if (typePage == 2)
                    {
                        #region Convert Object
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        foreach (object obj in objs)
                        {
                            arquivos.Add(obj as Model.Ged.Arquivo);
                        }
                        #endregion

                        #region columns
                        columns = new List<ClassView.TableColunas>();
                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_IND";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Ind";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DESCRICAO";
                        //column.Style = "width:300px";
                        column.CssClass = "col-sm-4 cell";
                        column.Text = "Descrição";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DISPONIVELEM";
                        //column.Style = "width:120px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Criado em";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_DISPONIVELPOR";
                        //column.Style = "width:75px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "por";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDTIPO_DESCRICAO";
                        //column.Style = "width:75px";
                        column.CssClass = "col-sm-2 cell";
                        column.Text = "Tipo";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_EXTENSAO";
                        //column.Style = "width:40px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Ext";
                        columns.Add(column);

                        column = new ClassView.TableColunas();
                        column.tabindex = 0;
                        column.aria_controls = "dataTables";
                        column.rowspan = 1;
                        column.colspan = 1;
                        column.aria_sort = "ascending";
                        column.aria_label = "GEDARQ_TAMANHO";
                        //column.Style = "width:15px";
                        column.CssClass = "col-sm-1 cell";
                        column.Text = "Tam";
                        columns.Add(column);
                        #endregion

                        #region rows
                        rows = new List<ClassView.TableLinhas>();

                        foreach (Model.Ged.Arquivo arquivo in arquivos)
                        {
                            row = new ClassView.TableLinhas();
                            row.cells = new List<ClassView.TableCelulas>();

                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = arquivo.GEDARQ_IND.ToString();
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-4 cell";
                            cell.Value = arquivo.GEDARQ_DESCRICAO.ToString();
                            ClassFrm.FrmLoteArquivo lotearquivo = new ClassFrm.FrmLoteArquivo()
                            {
                                indice = arquivo.GEDARQ_IND.ToString(),
                                typesPage = "1"
                            };
                            cell.link = (new ClassView.TableLink()
                            {
                                ID = "link-" + arquivo.GEDARQ_IND.ToString(),
                                url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
                            });
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            cell.Value = arquivo.GEDARQ_DISPONIVELPOR.ToString();
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-2 cell";
                            //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                            cell.Value = arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_DESCRICAO;
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                            cell.Value = arquivo.GEDARQ_EXTENSAO;
                            row.cells.Add(cell);
                            cell = new ClassView.TableCelulas();
                            cell.CssClass = "col-sm-1 cell";
                            //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                            cell.Value = arquivo.GEDARQ_TAMANHO.ToString();
                            row.cells.Add(cell);

                            rows.Add(row);
                        }
                        #endregion

                        table = new ClassView.TableOfWeb(columns, rows, "Sem Arquivos classificados no Lote!");
                        table.LeftTitle = "Arquivos";
                        table.LeftValue = "Classificados";
                    }
                    else
                    {
                        #region Convert Object
                        List<Model.Ged.Lote> lotes = new List<Model.Ged.Lote>();
                        foreach (object obj in objs)
                        {
                            lotes.Add(obj as Model.Ged.Lote);
                        }
                        #endregion

                        bool abertos;
                        if (lotes.Count == 0)
                            abertos = true;
                        else
                        {
                            if (lotes.Exists(f => f.GLOTE_FECHADOEM == DateTime.MinValue && f.GLOTE_FECHADOPOR == 0))
                                abertos = true;
                            else
                                abertos = false;
                        }

                        if (abertos)
                        {
                            #region columns
                            List<ClassView.TableColunas> columnsAbertos = new List<ClassView.TableColunas>();
                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_IND";
                            //column.Style = "width:15px";
                            column.CssClass = "col-sm-1 cell";
                            column.Text = "Ind";
                            columnsAbertos.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DESCRICAO";
                            //column.Style = "width:350px";
                            column.CssClass = "col-sm-4 cell";
                            column.Text = "Descrição";
                            columnsAbertos.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DISPONIVELEM";
                            //column.Style = "width:120px";
                            column.CssClass = "col-sm-3 cell";
                            column.Text = "Criado em";
                            columnsAbertos.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DISPONIVELPOR";
                            //column.Style = "width:75px";
                            column.CssClass = "col-sm-2 cell";
                            column.Text = "Criado por";
                            columnsAbertos.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_ENVIADO";
                            //column.Style = "width:60px";
                            column.CssClass = "col-sm-2 cell";
                            column.Text = "Enviado";
                            columnsAbertos.Add(column);
                            #endregion

                            #region rows
                            rows = new List<ClassView.TableLinhas>();

                            foreach (Model.Ged.Lote lotea in lotes)
                            {
                                row = new ClassView.TableLinhas();
                                row.cells = new List<ClassView.TableCelulas>();

                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-1 cell";
                                cell.Value = lotea.GLOTE_IND.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-4 cell";
                                cell.Value = lotea.GLOTE_DESCRICAO.ToString();
                                cell.link = (new ClassView.TableLink()
                                {
                                    ID = "link-" + lotea.GLOTE_IND.ToString(),
                                    url = "~/Lote/Detail/" + lotea.GLOTE_IND.ToString()
                                });
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-3 cell";
                                cell.Value = lotea.GLOTE_DISPONIVELEM.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-2 cell";
                                cell.Value = lotea.GLOTE_DISPONIVELPOR.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-2 cell";
                                cell.Style = "color: " + (lotea.GLOTE_ENVIADO ? "GREEN" : "RED");
                                cell.Value = lotea.GLOTE_ENVIADO ? "SIM" : "NÃO";
                                row.cells.Add(cell);

                                rows.Add(row);
                            }
                            #endregion

                            table = new ClassView.TableOfWeb(columnsAbertos, rows, "Sem Lotes abertos!");
                            table.CheckboxSwitch = true;
                        }
                        else
                        {
                            #region columns
                            List<ClassView.TableColunas> columnsFechados = new List<ClassView.TableColunas>();
                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_IND";
                            //column.Style = "width:15px";
                            column.CssClass = "col-sm-1 cell";
                            column.Text = "Ind";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DESCRICAO";
                            //column.Style = "width:250px";
                            column.CssClass = "col-sm-4 cell";
                            column.Text = "Descrição";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DISPONIVELEM";
                            //column.Style = "width:120px";
                            column.CssClass = "col-sm-2 cell";
                            column.Text = "Criado em";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_DISPONIVELPOR";
                            //column.Style = "width:75px";
                            column.CssClass = "col-sm-1 cell";
                            column.Text = "por";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_FECHADOEM";
                            //column.Style = "width:120px";
                            column.CssClass = "col-sm-2 cell";
                            column.Text = "Fechado em";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_FECHADOPOR";
                            //column.Style = "width:75px";
                            column.CssClass = "col-sm-1 cell";
                            column.Text = "por";
                            columnsFechados.Add(column);

                            column = new ClassView.TableColunas();
                            column.tabindex = 0;
                            column.aria_controls = "dataTables";
                            column.rowspan = 1;
                            column.colspan = 1;
                            column.aria_sort = "ascending";
                            column.aria_label = "GLOTE_ENVIADO";
                            //column.Style = "width:60px";
                            column.CssClass = "col-sm-1 cell";
                            column.Text = "Env";
                            columnsFechados.Add(column);
                            #endregion

                            #region rows
                            rows = new List<ClassView.TableLinhas>();

                            foreach (Model.Ged.Lote lotef in lotes)
                            {
                                row = new ClassView.TableLinhas();
                                row.cells = new List<ClassView.TableCelulas>();

                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-1 cell";
                                cell.Value = lotef.GLOTE_IND.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-4 cell";
                                cell.Value = lotef.GLOTE_DESCRICAO.ToString();
                                cell.link = (new ClassView.TableLink()
                                {
                                    ID = "link-" + lotef.GLOTE_IND.ToString(),
                                    url = "~/Lote/Detail/" + lotef.GLOTE_IND.ToString()
                                });
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-2 cell";
                                cell.Value = lotef.GLOTE_DISPONIVELEM.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-1 cell";
                                cell.Value = lotef.GLOTE_DISPONIVELPOR.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-2 cell";
                                cell.Value = lotef.GLOTE_FECHADOEM.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-1 cell";
                                cell.Value = lotef.GLOTE_FECHADOPOR.ToString();
                                row.cells.Add(cell);
                                cell = new ClassView.TableCelulas();
                                cell.CssClass = "col-sm-1 cell";
                                cell.Style = "color: " + (lotef.GLOTE_ENVIADO ? "GREEN" : "RED");
                                cell.Value = lotef.GLOTE_ENVIADO ? "SIM" : "NÃO";
                                row.cells.Add(cell);

                                rows.Add(row);
                            }
                            #endregion


                            table = new ClassView.TableOfWeb(columnsFechados, rows, "Sem Lotes fechado!");
                        }
                    }
                    #endregion
                    return table;
                case TableDataType.ArquivoTipo:
                    #region ArquivoTipo
                    #region Convert Object
                    List<Model.Ged.ArquivoTipo> arqtipos = new List<Model.Ged.ArquivoTipo>();
                    foreach (object obj in objs)
                    {
                        arqtipos.Add(obj as Model.Ged.ArquivoTipo);
                    }
                    #endregion

                    #region columns
                    columns = new List<TableColunas>();
                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "GEDTIPO_IND";
                    //column.Style = "width:20px";
                    column.CssClass = "col-sm-1 cell";
                    column.Text = "Ind";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "GEDTIPO_DESCRICAO";
                    //column.Style = "width:310px";
                    column.CssClass = "col-sm-4 cell";
                    column.Text = "Descrição";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "GEDTIPO_EXPORTA";
                    //column.Style = "width:40px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Exporta";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "GEDTIPO_DT_INICIO";
                    //column.Style = "width:100px";
                    column.CssClass = "col-sm-3 cell";
                    column.Text = "Criado em";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "GEDTIPO_LOGININSERT";
                    //column.Style = "width:100px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Criado por";
                    columns.Add(column);
                    #endregion

                    #region rows
                    rows = new List<TableLinhas>();
                    foreach (Model.Ged.ArquivoTipo tipo in arqtipos)
                    {
                        row = new TableLinhas();
                        row.cells = new List<TableCelulas>();

                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-1 cell";
                        cell.Value = tipo.GEDTIPO_IND.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-4 cell";
                        cell.Value = tipo.GEDTIPO_DESCRICAO.ToString();
                        cell.link = (new TableLink()
                        {
                            ID = "link-" + tipo.GEDTIPO_IND.ToString(),
                            //url = "~/ArquivoTipo/Detail/" + tipo.GEDTIPO_IND.ToString(), 
                            action = "Detail/" + tipo.GEDTIPO_IND.ToString(),
                            controller = "ArquivoTipo",
                            route = tipo
                        });
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = tipo.GEDTIPO_EXPORTA ? "Sim" : "Não";
                        cell.Style = tipo.GEDTIPO_EXPORTA ? "color: green" : "color: red";
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-3 cell";
                        cell.Value = tipo.GEDTIPO_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = tipo.GEDTIPO_LOGININSERT.ToString();
                        row.cells.Add(cell);

                        rows.Add(row);
                    }
                    #endregion

                    table = new TableOfWeb(columns, rows, "Sem Tipo de Arquivo Cadastrado!");
                    table.LeftTitle = "Tipos de Arquivos";

                    #endregion
                    return table;
                case TableDataType.ContaTipo:
                    #region ContaTipo
                    #region Convert Object
                    List<Model.ContaTipo> cttipos = new List<Model.ContaTipo>();
                    foreach (object obj in objs)
                    {
                        cttipos.Add(obj as Model.ContaTipo);
                    }
                    #endregion

                    #region columns
                    columns = new List<TableColunas>();
                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONTP_IND";
                    column.CssClass = "col-sm-1 cell";
                    //column.Style = "width:20px";
                    column.Text = "Ind";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONTP_DESCRICAO";
                    column.CssClass = "col-sm-4 cell";
                    //column.Style = "width:300px";
                    column.Text = "Descrição";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONTP_CLASSIFICADOR";
                    column.CssClass = "col-sm-3 cell";
                    //column.Style = "width:40px";
                    column.Text = "Classificador";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONTP_DT_INICIO";
                    column.CssClass = "col-sm-2 cell";
                    //column.Style = "width:110px";
                    column.Text = "Criado em";
                    columns.Add(column);

                    column = new TableColunas();
                    column.tabindex = 0;
                    column.aria_controls = "dataTables";
                    column.rowspan = 1;
                    column.colspan = 1;
                    column.aria_sort = "ascending";
                    column.aria_label = "CONTP_LOGININSERT";
                    //column.Style = "width:100px";
                    column.CssClass = "col-sm-2 cell";
                    column.Text = "Criado por";
                    columns.Add(column);
                    #endregion

                    #region rows
                    rows = new List<TableLinhas>();
                    foreach (Model.ContaTipo tipo in cttipos)
                    {
                        row = new TableLinhas();
                        row.cells = new List<TableCelulas>();

                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-1 cell";
                        cell.Value = tipo.CONTP_IND.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-4 cell";
                        cell.Value = tipo.CONTP_DESCRICAO.ToString();
                        cell.link = (new TableLink()
                        {
                            ID = "link-" + tipo.CONTP_IND.ToString(),
                            url = "~/Conta/Tipo/" + tipo.CONTP_IND.ToString()
                        });
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-3 cell";
                        cell.Value = tipo.CONTP_CLASSIFICADOR.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = tipo.CONTP_DT_INICIO.ToString();
                        row.cells.Add(cell);
                        cell = new TableCelulas();
                        cell.CssClass = "col-sm-2 cell";
                        cell.Value = tipo.CONTP_LOGININSERT.ToString();
                        row.cells.Add(cell);

                        rows.Add(row);
                    }
                    #endregion

                    table = new TableOfWeb(columns, rows, "Sem Tipos de Contas Cadastrados!");
                    table.LeftTitle = "Tipo de";
                    table.LeftValue = "Contas";

                    #endregion
                    return table;
                default:
                    return new TableOfWeb(new List<TableColunas>(), new List<TableLinhas>(), "");
            }
        }
    }

    public class TableModel
    {
        public string id;
        public Model.Pagination pagination;
        public TableDataType typeTable;
        public TableType type;
        private TableData tabledata;
        public TableOfWeb table;
        public string ItensString;
        public BoxSolid boxSolid;
        public string action;
        public string controller;
        public HttpRequestBase request;
        public object param;

        public TableModel(string Action, HttpRequestBase Request, TableType tableType, TableDataType tableDataType, List<object> objs, object Param = null)
        {
            type = tableType;
            typeTable = tableDataType;
            controller = tableDataType.ToString();
            action = Action;
            request = Request;
            param = Param;

            switch (type)
            {
                case TableType.BoxSolid:
                    boxSolid = new BoxSolid();
                    break;
                default:
                    break;
            }


            SetPagination(objs.Count);

            tabledata = new TableData(tableDataType);
            table = tabledata.GetDataTable(objs.GetRange(0, (objs.Count < 10 ? objs.Count : 10)));

            ItensString = "";
            for (int i = 0; i < pagination.itens.Length; i++)
            {
                ItensString += "-" + pagination.itens[i];
            }
        }

        public TableModel(string Action, HttpRequestBase Request, HttpSessionStateBase Session, TableType tableType, TableDataType tableDataType, object Param = null)
        {
            type = tableType;
            typeTable = tableDataType;
            controller = tableDataType.ToString();
            action = Action;
            request = Request;
            param = Param;

            switch (type)
            {
                case TableType.BoxSolid:
                    boxSolid = new BoxSolid();
                    break;
                default:
                    break;
            }

            SetPagination(100);

            SetTableModel(Action, Request, Session, tableType, tableDataType, pagination, Param);
        }

        public TableModel(string Action, HttpRequestBase Request, HttpSessionStateBase Session, TableType tableType, TableDataType tableDataType, Model.Pagination pag, object Param = null)
        {
            SetTableModel(Action, Request, Session, tableType, tableDataType, pag, Param);
        }

        private void SetTableModel(string Action, HttpRequestBase Request, HttpSessionStateBase Session, TableType tableType, TableDataType tableDataType, Model.Pagination pag, object Param = null)
        {
            type = tableType;
            typeTable = tableDataType;
            controller = tableDataType.ToString();
            request = Request;
            action = Action;
            pagination = pag;
            ItensString = "";
            param = Param;

            TypesErrors erro = new TypesErrors();
            for (int i = 0; i < pagination.itens.Length; i++)
            {
                ItensString += "-" + pagination.itens[i];
            }

            switch (typeTable)
            {
                case TableDataType.Dashboard:
                    #region Dashboard
                    ClassBD.DashboardBD bdDash = new ClassBD.DashboardBD(Session);
                    if (Action == "GetArquivoNClassificados")
                    {
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        tabledata = new TableData(tableDataType);
                        tabledata.Request = request;
                        if (bdDash.GetArquivosNaoClassificados(ref arquivos, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Arquivo arquivo in arquivos)
                            {
                                objs.Add(arquivo);
                            }
                            table = tabledata.GetDataTable(objs, 1);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    else if (Action == "GetLotesNFechados")
                    {
                        List<Model.Ged.Lote> lotesf = new List<Model.Ged.Lote>();
                        tabledata = new TableData(tableDataType);
                        if (bdDash.GetLotesEmAberto(ref lotesf, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Lote lote in lotesf)
                            {
                                objs.Add(lote);
                            }
                            table = tabledata.GetDataTable(objs, 2);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    else if (Action == "GetLotesNEnviados")
                    {
                        List<Model.Ged.Lote> lotese = new List<Model.Ged.Lote>();
                        tabledata = new TableData(tableDataType);
                        if (bdDash.GetLotesNaoEnviados(ref lotese, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Lote lote in lotese)
                            {
                                objs.Add(lote);
                            }
                            table = tabledata.GetDataTable(objs, 3);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    else
                    {
                        List<Model.Ged.ArquivoTipo> arqtipos = new List<Model.Ged.ArquivoTipo>();
                        tabledata = new TableData(tableDataType);
                        if (bdDash.GetArquivosTipos(ref arqtipos, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.ArquivoTipo tipo in arqtipos)
                            {
                                objs.Add(tipo);
                            }
                            table = tabledata.GetDataTable(objs, 4);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    #endregion
                    break;
                case TableDataType.PlanoContas:
                    #region PlanoContas
                    ClassBD.PlanoContasBD bdPlano = new ClassBD.PlanoContasBD(Session);
                    List<Model.PlanoContas> planos = new List<Model.PlanoContas>();
                    tabledata = new TableData(tableDataType);
                    if (bdPlano.GetPlanoContas(ref planos, ref pagination, ref erro))
                    {
                        SetPagination(pagination.rows);
                        List<object> objs = new List<object>();
                        foreach (Model.PlanoContas plano in planos)
                        {
                            objs.Add(plano);
                        }
                        table = tabledata.GetDataTable(objs);
                    }
                    else
                        table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                               new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    #endregion
                    break;
                case TableDataType.Conta:
                    #region Conta
                    ClassBD.ContaBD bdConta = new ClassBD.ContaBD(Session);
                    List<Model.Conta> contas = new List<Model.Conta>();
                    tabledata = new TableData(tableDataType);
                    if (bdConta.GetContas(ref contas, ref pagination, ref erro))
                    {
                        SetPagination(pagination.rows);
                        List<object> objs = new List<object>();
                        foreach (Model.Conta conta in contas)
                        {
                            objs.Add(conta);
                        }
                        table = tabledata.GetDataTable(objs);
                    }
                    else
                        table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                               new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    #endregion
                    break;
                case TableDataType.Lote:
                    #region Lote
                    ClassBD.LoteBD bdLote = new ClassBD.LoteBD(Session);
                    tabledata = new TableData(tableDataType);
                    tabledata.Request = Request;
                    if (Action == "GetPageTableClassificados")
                    {
                        Model.Ged.Lote lot = new Model.Ged.Lote();
                        lot.GLOTE_IND = param == null ? 0 : (int)param;
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        if (bdLote.GetArquivosByLoteClassificados(lot, ref arquivos, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Arquivo arq in arquivos)
                            {
                                objs.Add(arq);
                            }
                            table = tabledata.GetDataTable(objs, 2);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    else if (Action == "GetPageTableNClassificados")
                    {
                        Model.Ged.Lote lot = new Model.Ged.Lote();
                        lot.GLOTE_IND = param == null ? 0 : (int)param;
                        List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
                        if (bdLote.GetArquivosByLoteNClassificados(lot, ref arquivos, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Arquivo arq in arquivos)
                            {
                                objs.Add(arq);
                            }
                            table = tabledata.GetDataTable(objs, 1);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    else
                    {
                        List<Model.Ged.Lote> lotes = new List<Model.Ged.Lote>();
                        tabledata = new TableData(tableDataType);
                        if (bdLote.GetLotes(ref lotes, ref pagination, ref erro))
                        {
                            SetPagination(pagination.rows);
                            List<object> objs = new List<object>();
                            foreach (Model.Ged.Lote lote in lotes)
                            {
                                objs.Add(lote);
                            }
                            table = tabledata.GetDataTable(objs);
                        }
                        else
                            table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                                   new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    }
                    #endregion
                    break;
                case TableDataType.ArquivoTipo:
                    #region ArquivoTipo
                    ClassBD.ArquivoTipoBD bdTipo = new ClassBD.ArquivoTipoBD(Session);
                    List<Model.Ged.ArquivoTipo> tipos = new List<Model.Ged.ArquivoTipo>();
                    tabledata = new TableData(tableDataType);
                    if (bdTipo.GetArquivosTipos(ref tipos, ref pagination, ref erro))
                    {
                        SetPagination(pagination.rows);
                        List<object> objs = new List<object>();
                        foreach (Model.Ged.ArquivoTipo tipo in tipos)
                        {
                            objs.Add(tipo);
                        }
                        table = tabledata.GetDataTable(objs);
                    }
                    else
                        table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                               new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    #endregion
                    break;
                case TableDataType.ContaTipo:
                    #region ContaTipo
                    ClassBD.ContaBD bdCTipo = new ClassBD.ContaBD(Session);
                    List<Model.ContaTipo> ctipos = new List<Model.ContaTipo>();
                    tabledata = new TableData(tableDataType);
                    if (bdCTipo.GetContasTipos(ref ctipos, ref pagination, ref erro))
                    {
                        SetPagination(pagination.rows);
                        List<object> objs = new List<object>();
                        foreach (Model.ContaTipo tipo in ctipos)
                        {
                            objs.Add(tipo);
                        }
                        table = tabledata.GetDataTable(objs);
                    }
                    else
                        table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } },
                                               new List<TableLinhas>() { new TableLinhas() {
                                                   cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
                    #endregion
                    break;
                default:
                    break;
            }
        }

        private void SetPagination(int count)
        {
            if (pagination != null && count == pagination.rows)
            {
                int rowsp = pagination.rows;
                int[] itens = new int[] { 10, 25, 50, 100 };
                int item = pagination.item;
                double dpages = Convert.ToDouble(rowsp) / Convert.ToDouble(itens[item]);
                int ipages = rowsp / itens[item];
                int pages = dpages > ipages ? ipages + 1 : ipages;
                int page = pagination.page;

                pagination = new Model.Pagination(rowsp, pages, page, itens, item);
            }
            else
            {
                int rowsp = count;
                int[] itens = new int[] { 10, 25, 50, 100 };
                int item = 0;
                double dpages = Convert.ToDouble(rowsp) / Convert.ToDouble(itens[item]);
                int ipages = rowsp / itens[item];
                int pages = dpages > ipages ? ipages + 1 : ipages;
                int page = 1;

                pagination = new Model.Pagination(rowsp, pages, page, itens, item);
            }
        }
    }
}