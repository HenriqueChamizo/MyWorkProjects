using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassView;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class DashboardController : GEDController
    {
        DashboardBD BD;

        // GET: Dashboard
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            TypesErrors erro = new TypesErrors();
            Model.Dashboard dashboard = new Model.Dashboard();
            BD = new DashboardBD(Session);
            BD.GetDashboard(ref dashboard, ref erro);
            
            return View(dashboard);
        }

        public PartialViewResult MaisArquivos()
        {
            //List<Model.Ged.Arquivo> arquivos = new List<Model.Ged.Arquivo>();
            //TypesErrors erro = new TypesErrors();
            //BD = new DashboardBD(Session);
            //BD.GetArquivosNaoClassificados(ref arquivos, ref erro);

            #region Comments
            //#region columns Fechados
            //List<TableColunas> columnsNClass = new List<TableColunas>();
            //TableColunas column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDARQ_IND";
            ////column.Style = "width:15px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Ind";
            //columnsNClass.Add(column);

            //column = new ClassView.TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDARQ_DESCRICAO";
            ////column.Style = "width:300px";
            //column.CssClass = "col-sm-4 cell";
            //column.Text = "Descrição";
            //columnsNClass.Add(column);

            //column = new ClassView.TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDARQ_DISPONIVELEM";
            ////column.Style = "width:120px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Criado em";
            //columnsNClass.Add(column);

            //column = new ClassView.TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DESCRICAO";
            ////column.Style = "width:40px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Lote";
            //columnsNClass.Add(column);

            //column = new ClassView.TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDARQ_TAMANHO";
            ////column.Style = "width:40px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Tam";
            //columnsNClass.Add(column);
            //#endregion

            //#region rows Fechados
            //List<TableLinhas> rows = new List<TableLinhas>();
            //TableLinhas row;
            //TableCelulas cell;

            //foreach (Model.Ged.Arquivo arquivo in arquivos)
            //{
            //    row = new TableLinhas();
            //    row.cells = new List<TableCelulas>();

            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Value = arquivo.GEDARQ_IND.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-4 cell";
            //    cell.Value = arquivo.GEDARQ_DESCRICAO.ToString();
            //    ClassFrm.FrmLoteArquivo lotearquivo = new ClassFrm.FrmLoteArquivo()
            //    {
            //        indice = arquivo.GEDARQ_IND.ToString(),
            //        typesPage = "1"
            //    };
            //    cell.link = (new ClassView.TableLink()
            //    {
            //        ID = "link-" + arquivo.GEDARQ_IND.ToString(),
            //url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
            //    });
            //    row.cells.Add(cell);
            //    cell = new ClassView.TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
            //    row.cells.Add(cell);
            //    cell = new ClassView.TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = arquivo.GEDARQ_LOTE.GLOTE_DESCRICAO;
            //    row.cells.Add(cell);
            //    cell = new ClassView.TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = string.Format("{0:N2}", (Convert.ToDouble(arquivo.GEDARQ_TAMANHO) / 1000000d)) + "MB";
            //    row.cells.Add(cell);

            //    rows.Add(row);
            //}

            //TableOfWeb tableFechados = new ClassView.TableOfWeb(columnsNClass, rows, "Sem Arquivos não classificados!");
            //tableFechados.LeftTitle = "Arquivos";
            //tableFechados.LeftValue = "Não Classificados";
            //#endregion


            //BoxSolid box = new BoxSolid(TableDataType.PlanoContas, null);
            //box.box_type_class = TypesBootstrapColors.primary;
            //box.box_title = "Arquivos não Classificados";
            //box.table = tableFechados;
            //box.isCollapsed = true;

            //return PartialView("BoxSolid", box);
            #endregion

            TableModel tableModel = new TableModel("GetArquivoNClassificados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard)
            {
                id = "MoreForUp", 
                //table = tableFechados,
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.primary,
                    box_title = "Arquivos não classificados",
                    isCollapsed = true, 
                    isSubBox = true
                }
            };

            return PartialView("Table", tableModel);
        }

        public PartialViewResult GetArquivoNClassificados(int rows, int pages, int page, string itens, int item)
        {
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Model.Pagination pagination = new Model.Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetArquivoNClassificados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard, pagination)
            {
                id = "MoreForUp",
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.primary,
                    box_title = "Arquivos não classificados",
                    isCollapsed = false,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        public PartialViewResult FechadosLotes()
        {
            //List<Model.Ged.Lote> lotes = new List<Model.Ged.Lote>();
            //TypesErrors erro = new TypesErrors();
            //BD = new DashboardBD(Session);
            //BD.GetLotesEmAberto(ref lotes, ref erro);

            #region Comments
            //#region columns Abertos
            //TableColunas column;
            //List<TableColunas> columns = new List<TableColunas>();
            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_IND";
            ////column.Style = "width:15px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Ind";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DESCRICAO";
            ////column.Style = "width:350px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Descrição";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DISPONIVELEM";
            ////column.Style = "width:120px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Criado em";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DISPONIVELPOR";
            ////column.Style = "width:120px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Criado por";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "N_ARQUIVOS";
            ////column.Style = "width:60px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Arquivos";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "N_ARQUIVOS_NCLASSIC";
            ////column.Style = "width:60px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Sem Classificação";
            //columns.Add(column);
            //#endregion
            
            //#region rows Abertos
            //List<TableLinhas> rows;
            //rows = new List<TableLinhas>();
            //TableLinhas row;
            //TableCelulas cell;

            //foreach (Model.Ged.Lote lote in lotes)
            //{
            //    row = new TableLinhas();
            //    row.cells = new List<TableCelulas>();

            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = lote.GLOTE_IND.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = lote.GLOTE_DESCRICAO.ToString();
            //    cell.link = (new TableLink()
            //    {
            //        ID = "link-" + lote.GLOTE_IND.ToString(),
            //        url = "~/Lote/Detail/" + lote.GLOTE_IND.ToString()
            //    });
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = lote.GLOTE_DISPONIVELEM.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = lote.GLOTE_DISPONIVELPOR.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Style = "color: GREEN";
            //    cell.Value = lote.N_ARQUIVOS.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Style = "color: RED";
            //    cell.Value = lote.N_ARQUIVOS_NCLASSIC.ToString();
            //    row.cells.Add(cell);

            //    rows.Add(row);
            //}
            //#endregion

            //TableOfWeb table = new TableOfWeb(columns, rows, "Sem Lote aberto!");
            //table.LeftTitle = "Lote";
            //table.LeftValue = "Abertos";

            //BoxSolid box = new BoxSolid(TableDataType.PlanoContas, null);
            //box.box_type_class = TypesBootstrapColors.success;
            //box.box_title = "Lotes não Fechados";
            //box.table = table;
            //box.isCollapsed = true;

            //return PartialView("BoxSolid", box);
            #endregion

            TableModel tableModel = new TableModel("GetLotesNFechados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard)
            {
                id = "MoreForUp",
                //table = table,
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.success,
                    box_title = "Lotes não Fechados",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", tableModel);
        }

        public PartialViewResult GetLotesNFechados(int rows, int pages, int page, string itens, int item)
        {
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Model.Pagination pagination = new Model.Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetLotesNFechados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard, pagination)
            {
                id = "MoreForUp",
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.success,
                    box_title = "Lotes não Fechados",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        public PartialViewResult EnviadosLotes()
        {
            //List<Model.Ged.Lote> lotes = new List<Model.Ged.Lote>();
            //TypesErrors erro = new TypesErrors();
            //BD = new DashboardBD(Session);
            //BD.GetLotesNaoEnviados(ref lotes, ref erro);

            #region Comments
            //#region columns Abertos
            //TableColunas column;
            //List<TableColunas> columns = new List<TableColunas>();
            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_IND";
            ////column.Style = "width:15px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Ind";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DESCRICAO";
            ////column.Style = "width:350px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Descrição";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DISPONIVELEM";
            ////column.Style = "width:120px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Criado em";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GLOTE_DISPONIVELPOR";
            ////column.Style = "width:120px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Criado por";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "N_ARQUIVOS";
            ////column.Style = "width:60px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Arquivos";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "N_ARQUIVOS_NCLASSIC";
            ////column.Style = "width:60px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Sem Classificação";
            //columns.Add(column);
            //#endregion

            //#region rows Abertos
            //List<TableLinhas> rows;
            //rows = new List<TableLinhas>();
            //TableLinhas row;
            //TableCelulas cell;

            //foreach (Model.Ged.Lote lote in lotes)
            //{
            //    row = new TableLinhas();
            //    row.cells = new List<TableCelulas>();

            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = lote.GLOTE_IND.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = lote.GLOTE_DESCRICAO.ToString();
            //    cell.link = (new TableLink()
            //    {
            //        ID = "link-" + lote.GLOTE_IND.ToString(),
            //        url = "~/Lote/Detail/" + lote.GLOTE_IND.ToString()
            //    });
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = lote.GLOTE_DISPONIVELEM.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = lote.GLOTE_DISPONIVELPOR.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Style = "color: GREEN";
            //    cell.Value = lote.N_ARQUIVOS.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Style = "color: RED";
            //    cell.Value = lote.N_ARQUIVOS_NCLASSIC.ToString();
            //    row.cells.Add(cell);

            //    rows.Add(row);
            //}
            //#endregion

            //TableOfWeb table = new TableOfWeb(columns, rows, "Sem Lote enviados!");
            //table.LeftTitle = "Lote";
            //table.LeftValue = "Enviados";

            //BoxSolid box = new BoxSolid(TableDataType.PlanoContas, null);
            //box.box_type_class = TypesBootstrapColors.warning;
            //box.box_title = "Lotes não Enviados";
            //box.table = table;
            //box.isCollapsed = true;

            //return PartialView("BoxSolid", box);
            #endregion

            TableModel tableModel = new TableModel("GetLotesNEnviados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard)
            {
                id = "MoreForUp",
                //table = table,
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.warning,
                    box_title = "Lotes não Enviados",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", tableModel);
        }

        public PartialViewResult GetLotesNEnviados(int rows, int pages, int page, string itens, int item)
        {
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Model.Pagination pagination = new Model.Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetLotesNEnviados", Request, Session, TableType.BoxSolid, TableDataType.Dashboard, pagination)
            {
                id = "MoreForUp",
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.warning,
                    box_title = "Lotes não Enviados",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        public PartialViewResult ExportadosArquivos()
        {
            //TypesErrors erro = new TypesErrors();
            //BD = new DashboardBD(Session);
            //List<Model.Ged.ArquivoTipo> tipos = new List<Model.Ged.ArquivoTipo>();
            //BD.GetArquivosTipos(ref tipos, ref erro);

            #region Comments
            //#region columns
            //List<TableColunas> columns = new List<TableColunas>();
            //TableColunas column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDTIPO_IND";
            ////column.Style = "width:20px";
            //column.CssClass = "col-sm-1 cell";
            //column.Text = "Ind";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDTIPO_DESCRICAO";
            ////column.Style = "width:310px";
            //column.CssClass = "col-sm-4 cell";
            //column.Text = "Descrição";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDTIPO_EXPORTA";
            ////column.Style = "width:40px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Exporta";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDTIPO_DT_INICIO";
            ////column.Style = "width:100px";
            //column.CssClass = "col-sm-3 cell";
            //column.Text = "Criado em";
            //columns.Add(column);

            //column = new TableColunas();
            //column.tabindex = 0;
            //column.aria_controls = "dataTables";
            //column.rowspan = 1;
            //column.colspan = 1;
            //column.aria_sort = "ascending";
            //column.aria_label = "GEDTIPO_LOGININSERT";
            ////column.Style = "width:100px";
            //column.CssClass = "col-sm-2 cell";
            //column.Text = "Criado por";
            //columns.Add(column);
            //#endregion

            //#region rows
            //List<TableLinhas> rows = new List<TableLinhas>();
            //TableLinhas row;
            //TableCelulas cell;

            //foreach (Model.Ged.ArquivoTipo tipo in tipos)
            //{
            //    row = new TableLinhas();
            //    row.cells = new List<TableCelulas>();

            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-1 cell";
            //    cell.Value = tipo.GEDTIPO_IND.ToString();
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-4 cell";
            //    cell.Value = tipo.GEDTIPO_DESCRICAO.ToString();
            //    cell.link = (new TableLink()
            //    {
            //        ID = "link-" + tipo.GEDTIPO_IND.ToString(),
            //        //url = "~/ArquivoTipo/Detail/" + tipo.GEDTIPO_IND.ToString(), 
            //        action = "Detail/" + tipo.GEDTIPO_IND.ToString(),
            //        controller = "ArquivoTipo",
            //        route = tipo
            //    });
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Value = tipo.GEDTIPO_EXPORTA ? "Sim" : "Não";
            //    cell.Style = tipo.GEDTIPO_EXPORTA ? "color: green" : "color: red";
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-3 cell";
            //    cell.Value = tipo.GEDTIPO_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
            //    row.cells.Add(cell);
            //    cell = new TableCelulas();
            //    cell.CssClass = "col-sm-2 cell";
            //    cell.Value = tipo.GEDTIPO_LOGININSERT.ToString();
            //    row.cells.Add(cell);

            //    rows.Add(row);
            //}
            //#endregion

            //TableOfWeb table = new TableOfWeb(columns, rows, "Sem Tipo de Arquivo Cadastrado!");
            //table.LeftTitle = "Tipos de Arquivos";

            //BoxSolid box = new BoxSolid(TableDataType.PlanoContas, null);
            //box.box_type_class = TypesBootstrapColors.danger;
            //box.box_title = "Arquivos Exportaveis";
            //box.table = table;
            //box.isCollapsed = true;

            //return PartialView("BoxSolid", box);
            #endregion

            TableModel tableModel = new TableModel("GetArquivosTipos", Request, Session, TableType.BoxSolid, TableDataType.Dashboard)
            {
                id = "MoreForUp", 
                //table = table,
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.danger,
                    box_title = "Arquivos Exportaveis",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", tableModel);
        }

        public PartialViewResult GetArquivosTipos(int rows, int pages, int page, string itens, int item)
        {
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Model.Pagination pagination = new Model.Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetArquivosTipos", Request, Session, TableType.BoxSolid, TableDataType.Dashboard, pagination)
            {
                id = "MoreForUp",
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.danger,
                    box_title = "Lotes não Enviados",
                    isCollapsed = true,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        public string DataTable(int? id = null)
        {
            string retorno;

            retorno = @"{
              ""draw"": 1,
              ""recordsTotal"": 57,
              ""recordsFiltered"": 57,
              ""data"": [
                [
                  ""Airi"",
                  ""Satou"",
                  ""Accountant"",
                  ""Tokyo"",
                  ""28th Nov 08"",
                  ""$162,700""
                ],
                [
                  ""Angelica"",
                  ""Ramos"",
                  ""Chief Executive Officer (CEO)"",
                  ""London"",
                  ""9th Oct 09"",
                  ""$1,200,000""
                ],
                [
                  ""Ashton"",
                  ""Cox"",
                  ""Junior Technical Author"",
                  ""San Francisco"",
                  ""12th Jan 09"",
                  ""$86,000""
                ],
                [
                  ""Bradley"",
                  ""Greer"",
                  ""Software Engineer"",
                  ""London"",
                  ""13th Oct 12"",
                  ""$132,000""
                ],
                [
                  ""Brenden"",
                  ""Wagner"",
                  ""Software Engineer"",
                  ""San Francisco"",
                  ""7th Jun 11"",
                  ""$206,850""
                ],
                [
                  ""Brielle"",
                  ""Williamson"",
                  ""Integration Specialist"",
                  ""New York"",
                  ""2nd Dec 12"",
                  ""$372,000""
                ],
                [
                  ""Bruno"",
                  ""Nash"",
                  ""Software Engineer"",
                  ""London"",
                  ""3rd May 11"",
                  ""$163,500""
                ],
                [
                  ""Caesar"",
                  ""Vance"",
                  ""Pre-Sales Support"",
                  ""New York"",
                  ""12th Dec 11"",
                  ""$106,450""
                ],
                [
                  ""Cara"",
                  ""Stevens"",
                  ""Sales Assistant"",
                  ""New York"",
                  ""6th Dec 11"",
                  ""$145,600""
                ],
                [
                  ""Cedric"",
                  ""Kelly"",
                  ""Senior Javascript Developer"",
                  ""Edinburgh"",
                  ""29th Mar 12"",
                  ""$433,060""
                ]
              ]
            }";

            return retorno;
        }

        [HttpPost]
        public JsonResult GetData(DataTableOfWeb dataTableOfWeb)
        {
            // put a debug here to see the values
            // do anything else to handel to posted data
            return Json(dataTableOfWeb);
        }
    }
}