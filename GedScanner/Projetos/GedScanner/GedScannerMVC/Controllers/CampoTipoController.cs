using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassView;
using Model.Enuns;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class CampoTipoController : GEDController
    {
        private ArquivoTipoBD BD;

        // GET: CampoTipo
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            return View(MountView());
        }

        public ActionResult Novo()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            return View();
        }

        private ArquivoTipo MountView()
        {
            #region Commants
            //TypesErrors erro = new TypesErrors();
            //BD = new ArquivoTipoBD(Session);
            //List<Model.Ged.ArquivoTipo> tipos = new List<Model.Ged.ArquivoTipo>();
            //BD.GetArquivosTipos(ref tipos, ref erro);
            //ArquivoTipo view = new ArquivoTipo();
            //if (tipos != null)
            //{
            //    #region columns
            //    List<TableColunas> columns = new List<TableColunas>();
            //    TableColunas column = new TableColunas();
            //    column.tabindex = 0;
            //    column.aria_controls = "dataTables";
            //    column.rowspan = 1;
            //    column.colspan = 1;
            //    column.aria_sort = "ascending";
            //    column.aria_label = "GEDTIPO_IND";
            //    //column.Style = "width:20px";
            //    column.CssClass = "col-sm-1 cell";
            //    column.Text = "Ind";
            //    columns.Add(column);

            //    column = new TableColunas();
            //    column.tabindex = 0;
            //    column.aria_controls = "dataTables";
            //    column.rowspan = 1;
            //    column.colspan = 1;
            //    column.aria_sort = "ascending";
            //    column.aria_label = "GEDTIPO_DESCRICAO";
            //    //column.Style = "width:310px";
            //    column.CssClass = "col-sm-4 cell";
            //    column.Text = "Descrição";
            //    columns.Add(column);

            //    column = new TableColunas();
            //    column.tabindex = 0;
            //    column.aria_controls = "dataTables";
            //    column.rowspan = 1;
            //    column.colspan = 1;
            //    column.aria_sort = "ascending";
            //    column.aria_label = "GEDTIPO_EXPORTA";
            //    //column.Style = "width:40px";
            //    column.CssClass = "col-sm-2 cell";
            //    column.Text = "Exporta";
            //    columns.Add(column);

            //    column = new TableColunas();
            //    column.tabindex = 0;
            //    column.aria_controls = "dataTables";
            //    column.rowspan = 1;
            //    column.colspan = 1;
            //    column.aria_sort = "ascending";
            //    column.aria_label = "GEDTIPO_DT_INICIO";
            //    //column.Style = "width:100px";
            //    column.CssClass = "col-sm-3 cell";
            //    column.Text = "Criado em";
            //    columns.Add(column);

            //    column = new TableColunas();
            //    column.tabindex = 0;
            //    column.aria_controls = "dataTables";
            //    column.rowspan = 1;
            //    column.colspan = 1;
            //    column.aria_sort = "ascending";
            //    column.aria_label = "GEDTIPO_LOGININSERT";
            //    //column.Style = "width:100px";
            //    column.CssClass = "col-sm-2 cell";
            //    column.Text = "Criado por";
            //    columns.Add(column);
            //    #endregion

            //    #region rows
            //    List<TableLinhas> rows = new List<TableLinhas>();
            //    TableLinhas row;
            //    TableCelulas cell;

            //    foreach (Model.Ged.ArquivoTipo tipo in tipos)
            //    {
            //        row = new TableLinhas();
            //        row.cells = new List<TableCelulas>();

            //        cell = new TableCelulas();
            //        cell.CssClass = "col-sm-1 cell";
            //        cell.Value = tipo.GEDTIPO_IND.ToString();
            //        row.cells.Add(cell);
            //        cell = new TableCelulas();
            //        cell.CssClass = "col-sm-4 cell";
            //        cell.Value = tipo.GEDTIPO_DESCRICAO.ToString();
            //        cell.link = (new TableLink()
            //        {
            //            ID = "link-" + tipo.GEDTIPO_IND.ToString(),
            //            //url = "~/ArquivoTipo/Detail/" + tipo.GEDTIPO_IND.ToString(), 
            //            action = "Detail/" + tipo.GEDTIPO_IND.ToString(),
            //            controller = "ArquivoTipo",
            //            route = tipo
            //        });
            //        row.cells.Add(cell);
            //        cell = new TableCelulas();
            //        cell.CssClass = "col-sm-2 cell";
            //        cell.Value = tipo.GEDTIPO_EXPORTA ? "Sim" : "Não";
            //        cell.Style = tipo.GEDTIPO_EXPORTA ? "color: green" : "color: red";
            //        row.cells.Add(cell);
            //        cell = new TableCelulas();
            //        cell.CssClass = "col-sm-3 cell";
            //        cell.Value = tipo.GEDTIPO_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
            //        row.cells.Add(cell);
            //        cell = new TableCelulas();
            //        cell.CssClass = "col-sm-2 cell";
            //        cell.Value = tipo.GEDTIPO_LOGININSERT.ToString();
            //        row.cells.Add(cell);

            //        rows.Add(row);
            //    }
            //    #endregion

            //    TableOfWeb table = new TableOfWeb(columns, rows, "Sem Tipo de Arquivo Cadastrado!");
            //    table.LeftTitle = "Tipos de Arquivos";
            //    //table.LeftValue = "Fechados";

            //    view.table = table;
            //}
            //else
            //    view.mensagem = "Erro de valor nulo!";
            #endregion
            ArquivoTipo view = new ArquivoTipo();
            TableModel tableClass = new TableModel("GetPageTableArquivosTipos", Request, Session, TableType.StripedUnBorder, TableDataType.ArquivoTipo)
            {
                id = "TableArquivosTipos",
                boxSolid = null
            };

            view.tableModel = tableClass;

            return view;
        }
    }
}