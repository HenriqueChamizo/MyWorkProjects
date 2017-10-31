using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassView;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class LoteController : GEDController
    {
        LoteBD BD;

        // GET: Lote
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home"); 

            return View(MountView(TypesPages.index));
        }

        public PartialViewResult Arquivo()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Arquivo(ClassFrm.FrmLoteArquivo arquivo)
        {
            int id = Convert.ToInt32(arquivo.indice);
            ClassView.Lote view = new ClassView.Lote();
            view.detail = new ClassView.LoteDetail() { indice = arquivo.indice };
            if (arquivo.typesPage == "0")
                view.typePage = TypesPages.ajaxPartial;
            else if (arquivo.typesPage == "1")
                view.typePage = TypesPages.ajaxDetail;
            return PartialView(view);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            else
            {
                //return View(new ClassView.Lote() { detail = new ClassView.LoteDetail() { indice = id.Value.ToString() } });
                return View(MountView(TypesPages.detail, id));
            }
        }

        [HttpPost]
        public PartialViewResult Lote(ClassFrm.FrmLoteLote lote)
        {
            int id = Convert.ToInt32(lote.indice);
            return PartialView(MountView(TypesPages.detail, id));
        }

        [HttpPost]
        public PartialViewResult LoteArquivo(ClassFrm.FrmLoteLoteArquivo loteArquivo)
        {
            int id = Convert.ToInt32(loteArquivo.indice);
            return PartialView(MountView(TypesPages.ajaxDetail, id));
        }

        [HttpPost]
        public ActionResult Salvar(ClassFrm.FrmLoteIndex form)
        {
            string erros = "";
            if (CheckValues(form.descricao, ref erros))
            {
                LoginsInternos login = login = Session["Usuario"] as LoginsInternos;
                Model.Ged.Lote lote = new Model.Ged.Lote();
                lote.GLOTE_IND = form.indice.Contains("Índice: ") ? Convert.ToInt32(form.indice.Replace("Índice: ", "")) : 0;
                lote.GLOTE_DESCRICAO = form.descricao;
                lote.GLOTE_DISPONIVELEM = DateTime.Now;
                lote.GLOTE_DISPONIVELPOR = login.LOGI_IND;
                if (form.finalizar)
                {
                    lote.GLOTE_FECHADOEM = DateTime.Now;
                    lote.GLOTE_FECHADOPOR = login.LOGI_IND;
                }
                else
                {
                    lote.GLOTE_FECHADOEM = DateTime.MinValue;
                    lote.GLOTE_FECHADOPOR = 0;
                }
                lote.GLOTE_ENVIADO = form.enviar;

                TypesErrors erro = new TypesErrors();
                BD = new LoteBD(Session);
                if (Convert.ToBoolean(form.edit))
                    BD.SetLoteEdit(lote, login.LOGI_IND, ref erro);
                else
                    BD.SetLoteInsert(lote, login.LOGI_IND, ref erro);
            }
            return View("Index", MountView(TypesPages.index));
        }

        private ClassView.Lote MountView(TypesPages typePage, int? id = null)
        {
            TypesErrors erro = new TypesErrors();
            ClassView.Lote view = new ClassView.Lote();
            view.typePage = typePage;
            BD = new LoteBD(Session);
            switch (typePage)
            {
                case TypesPages.index:
                    #region Index
                    List<Model.Ged.Lote> todos = new List<Model.Ged.Lote>();
                    BD.GetLotes(ref todos, ref erro);
                    view.index = new ClassView.LoteIndex();
                    view.index.tableFinalizadosVisible = true;
                    if (todos.Count() > 0)
                    {
                        List<Model.Ged.Lote> abertos = todos.FindAll(f => f.GLOTE_FECHADOEM == DateTime.MinValue && f.GLOTE_FECHADOPOR == 0);
                        List<Model.Ged.Lote> fechados = todos.FindAll(f => f.GLOTE_FECHADOEM != DateTime.MinValue && f.GLOTE_FECHADOPOR != 0);

                        view.index.indice = "Insira um novo";

                        List<object> objsAbertos = new List<object>();
                        foreach (Model.Ged.Lote l in abertos)
                        {
                            objsAbertos.Add(l as object);
                        }
                        TableModel tableAbertos = new TableModel("GetPageTableAbertos", Request, TableType.BoxSolid, TableDataType.Lote, objsAbertos)
                        {
                            id = "TableLoteAbertos",
                            boxSolid = new BoxSolid()
                            {
                                box_title = "Lotes: Abertos",
                                box_type_class = TypesBootstrapColors.primary,
                                isCollapsed = false,
                                isSubBox = false
                            }
                        };
                        List<object> objsFechados = new List<object>();
                        foreach (Model.Ged.Lote l in fechados)
                        {
                            objsFechados.Add(l as object);
                        }
                        TableModel tableFechados = new TableModel("GetPageTableFechados", Request, TableType.BoxSolid, TableDataType.Lote, objsFechados)
                        {
                            id = "TableLoteFechados",
                            boxSolid = new BoxSolid()
                            {
                                box_title = "Lotes: Fechados",
                                box_type_class = TypesBootstrapColors.primary,
                                isCollapsed = false,
                                isSubBox = false
                            }
                        };


                        view.index.tableAbertos = tableAbertos;
                        view.index.tableFechados = tableFechados;


                        #region Comments
                        //#region columns Abertos
                        //List<ClassView.TableColunas> columnsAbertos = new List<ClassView.TableColunas>();
                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_IND";
                        ////column.Style = "width:15px";
                        //column.CssClass = "col-sm-1 cell";
                        //column.Text = "Ind";
                        //columnsAbertos.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DESCRICAO";
                        ////column.Style = "width:350px";
                        //column.CssClass = "col-sm-4 cell";
                        //column.Text = "Descrição";
                        //columnsAbertos.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DISPONIVELEM";
                        ////column.Style = "width:120px";
                        //column.CssClass = "col-sm-3 cell";
                        //column.Text = "Criado em";
                        //columnsAbertos.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DISPONIVELPOR";
                        ////column.Style = "width:75px";
                        //column.CssClass = "col-sm-2 cell";
                        //column.Text = "Criado por";
                        //columnsAbertos.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_ENVIADO";
                        ////column.Style = "width:60px";
                        //column.CssClass = "col-sm-2 cell";
                        //column.Text = "Enviado";
                        //columnsAbertos.Add(column);
                        //#endregion

                        //#region columns Fechados
                        //List<ClassView.TableColunas> columnsFechados = new List<ClassView.TableColunas>();
                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_IND";
                        ////column.Style = "width:15px";
                        //column.CssClass = "col-sm-1 cell";
                        //column.Text = "Ind";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DESCRICAO";
                        ////column.Style = "width:250px";
                        //column.CssClass = "col-sm-4 cell";
                        //column.Text = "Descrição";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DISPONIVELEM";
                        ////column.Style = "width:120px";
                        //column.CssClass = "col-sm-2 cell";
                        //column.Text = "Criado em";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_DISPONIVELPOR";
                        ////column.Style = "width:75px";
                        //column.CssClass = "col-sm-1 cell";
                        //column.Text = "por";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_FECHADOEM";
                        ////column.Style = "width:120px";
                        //column.CssClass = "col-sm-2 cell";
                        //column.Text = "Fechado em";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_FECHADOPOR";
                        ////column.Style = "width:75px";
                        //column.CssClass = "col-sm-1 cell";
                        //column.Text = "por";
                        //columnsFechados.Add(column);

                        //column = new ClassView.TableColunas();
                        //column.tabindex = 0;
                        //column.aria_controls = "dataTables";
                        //column.rowspan = 1;
                        //column.colspan = 1;
                        //column.aria_sort = "ascending";
                        //column.aria_label = "GLOTE_ENVIADO";
                        ////column.Style = "width:60px";
                        //column.CssClass = "col-sm-1 cell";
                        //column.Text = "Env";
                        //columnsFechados.Add(column);
                        //#endregion

                        //#region rows Abertos
                        //rows = new List<ClassView.TableLinhas>();

                        //foreach (Model.Ged.Lote lotea in abertos)
                        //{
                        //    row = new ClassView.TableLinhas();
                        //    row.cells = new List<ClassView.TableCelulas>();

                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-1 cell";
                        //    cell.Value = lotea.GLOTE_IND.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-4 cell";
                        //    cell.Value = lotea.GLOTE_DESCRICAO.ToString();
                        //    cell.link = (new ClassView.TableLink()
                        //    {
                        //        ID = "link-" + lotea.GLOTE_IND.ToString(),
                        //        url = "~/Lote/Detail/" + lotea.GLOTE_IND.ToString()
                        //    });
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-3 cell";
                        //    cell.Value = lotea.GLOTE_DISPONIVELEM.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-2 cell";
                        //    cell.Value = lotea.GLOTE_DISPONIVELPOR.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-2 cell";
                        //    cell.Style = "color: " + (lotea.GLOTE_ENVIADO ? "GREEN" : "RED");
                        //    cell.Value = lotea.GLOTE_ENVIADO ? "SIM" : "NÃO";
                        //    row.cells.Add(cell);

                        //    rows.Add(row);
                        //}

                        //tableAbertos = new ClassView.TableOfWeb(columnsAbertos, rows, "Sem Lote aberto!");
                        //tableAbertos.LeftTitle = "Lote";
                        //tableAbertos.LeftValue = "Abertos";
                        //#endregion

                        //#region rows Fechados
                        //rows = new List<ClassView.TableLinhas>();

                        //foreach (Model.Ged.Lote lotef in fechados)
                        //{
                        //    row = new ClassView.TableLinhas();
                        //    row.cells = new List<ClassView.TableCelulas>();

                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-1 cell";
                        //    cell.Value = lotef.GLOTE_IND.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-4 cell";
                        //    cell.Value = lotef.GLOTE_DESCRICAO.ToString();
                        //    cell.link = (new ClassView.TableLink()
                        //    {
                        //        ID = "link-" + lotef.GLOTE_IND.ToString(),
                        //        url = "~/Lote/Detail/" + lotef.GLOTE_IND.ToString()
                        //    });
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-2 cell";
                        //    cell.Value = lotef.GLOTE_DISPONIVELEM.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-1 cell";
                        //    cell.Value = lotef.GLOTE_DISPONIVELPOR.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-2 cell";
                        //    cell.Value = lotef.GLOTE_FECHADOEM.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-1 cell";
                        //    cell.Value = lotef.GLOTE_FECHADOPOR.ToString();
                        //    row.cells.Add(cell);
                        //    cell = new ClassView.TableCelulas();
                        //    cell.CssClass = "col-sm-1 cell";
                        //    cell.Style = "color: " + (lotef.GLOTE_ENVIADO ? "GREEN" : "RED");
                        //    cell.Value = lotef.GLOTE_ENVIADO ? "SIM" : "NÃO";
                        //    row.cells.Add(cell);

                        //    rows.Add(row);
                        //}

                        //tableFechados = new ClassView.TableOfWeb(columnsFechados, rows, "Sem Lote fechado!");
                        //tableFechados.LeftTitle = "Lote";
                        //tableFechados.LeftValue = "Fechados";
                        //#endregion

                        //view.index.tableAbertos = tableAbertos;
                        //view.index.tableFechados = tableFechados;
                        #endregion
                    }
                    else
                    {
                        view.index.indice = "Nenhum Lote inserido! Insira um novo";
                        view.index.tableFinalizadosVisible = false;
                    }

                    view.index.descricao = "";
                    view.index.datainicio = "Data e tempo atual";
                    view.index.finalizar = false;
                    view.index.ckbFinalizarVisible = false;
                    view.index.ckbFinalizarEnable = false;
                    view.index.edit = false;
                    view.index.ckbsLotesVisibles = true;
                    #endregion
                    break;
                case TypesPages.detail:
                    #region Detail
                    view.detail = new ClassView.LoteDetail();
                    Model.Ged.Lote lote = new Model.Ged.Lote();
                    lote.GLOTE_IND = id.Value;
                    BD.GetLoteById(ref lote, ref erro);
                    if (lote.GLOTE_IND != 0)
                    {
                        view.detail.indice = "Índice: " + lote.GLOTE_IND.ToString();
                        view.detail.descricao = lote.GLOTE_DESCRICAO;
                        view.detail.datainicio = lote.GLOTE_DISPONIVELEM.ToString("dd/MM/yyyy HH:mm:ss");
                        view.detail.finalizar = lote.GLOTE_FECHADOEM == DateTime.MinValue ? false : true;
                        view.detail.enviar = lote.GLOTE_ENVIADO;
                        view.detail.disabledAll = lote.GLOTE_ENVIADO;
                        view.detail.ckbFinalizarVisible = true;
                        view.detail.ckbEnviarVisible = true;
                        view.detail.ckbFinalizarEnable = !lote.GLOTE_ENVIADO;
                        view.detail.ckbEnviarEnable = view.detail.finalizar;

                        view.detail.ckbsLotesVisibles = false;
                        view.detail.tableFinalizadosVisible = false;
                    }
                    view.detail.edit = true;
                    #endregion
                    break;
                case TypesPages.ajaxDetail:
                    #region ajaxDetail 
                    view.loteArquivo = new ClassView.LoteArquivo();
                    view.detail = new ClassView.LoteDetail() { indice = id.Value.ToString() };

                    #region Comments
                    //#region columns Classificados
                    //List<ClassView.TableColunas> columnsClass = new List<ClassView.TableColunas>();
                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDARQ_IND";
                    ////column.Style = "width:15px";
                    //column.CssClass = "col-sm-1 cell";
                    //column.Text = "Ind";
                    //columnsClass.Add(column);

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
                    //columnsClass.Add(column);

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
                    //columnsClass.Add(column);

                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDARQ_DISPONIVELPOR";
                    ////column.Style = "width:75px";
                    //column.CssClass = "col-sm-1 cell";
                    //column.Text = "por";
                    //columnsClass.Add(column);

                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDTIPO_DESCRICAO";
                    ////column.Style = "width:75px";
                    //column.CssClass = "col-sm-2 cell";
                    //column.Text = "Tipo";
                    //columnsClass.Add(column);

                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDARQ_EXTENSAO";
                    ////column.Style = "width:40px";
                    //column.CssClass = "col-sm-1 cell";
                    //column.Text = "Ext";
                    //columnsClass.Add(column);

                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDARQ_TAMANHO";
                    ////column.Style = "width:15px";
                    //column.CssClass = "col-sm-1 cell";
                    //column.Text = "Tam";
                    //columnsClass.Add(column);
                    //#endregion

                    //#region columns Fechados
                    //List<ClassView.TableColunas> columnsNClass = new List<ClassView.TableColunas>();
                    //column = new ClassView.TableColunas();
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
                    //column.aria_label = "GEDARQ_DISPONIVELPOR";
                    ////column.Style = "width:75px";
                    //column.CssClass = "col-sm-2 cell";
                    //column.Text = "Criado por";
                    //columnsNClass.Add(column);

                    //column = new ClassView.TableColunas();
                    //column.tabindex = 0;
                    //column.aria_controls = "dataTables";
                    //column.rowspan = 1;
                    //column.colspan = 1;
                    //column.aria_sort = "ascending";
                    //column.aria_label = "GEDARQ_EXTENSAO";
                    ////column.Style = "width:40px";
                    //column.CssClass = "col-sm-1 cell";
                    //column.Text = "Ext";
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

                    //#region rows Abertos
                    //rows = new List<ClassView.TableLinhas>();

                    //foreach (Model.Ged.Arquivo arquivo in classificados)
                    //{
                    //    row = new ClassView.TableLinhas();
                    //    row.cells = new List<ClassView.TableCelulas>();

                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    cell.Value = arquivo.GEDARQ_IND.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
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
                    //        url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
                    //    });
                    //    //cell.AjaxLink = (new ClassView.TableAjaxLink()
                    //    //{
                    //    //    ID = "link-" + arquivo.GEDARQ_IND.ToString(),
                    //    //    action = "Arquivo",
                    //    //    controller = "Lote",
                    //    //    route = lotearquivo,
                    //    //    ajaxOptions = new System.Web.Mvc.Ajax.AjaxOptions()
                    //    //    {
                    //    //        HttpMethod = "POST",
                    //    //        InsertionMode = System.Web.Mvc.Ajax.InsertionMode.Replace,
                    //    //        UpdateTargetId = "ArquivoAtual"
                    //    //    }
                    //    //});
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-2 cell";
                    //    cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    cell.Value = arquivo.GEDARQ_DISPONIVELPOR.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-2 cell";
                    //    //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                    //    cell.Value = arquivo.GEDARQ_CONTARQUIVOTIPO.CATIP_ARQUIVOTIPO.GEDTIPO_DESCRICAO;
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                    //    cell.Value = arquivo.GEDARQ_EXTENSAO;
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                    //    cell.Value = arquivo.GEDARQ_TAMANHO.ToString();
                    //    row.cells.Add(cell);

                    //    rows.Add(row);
                    //}

                    //tableAbertos = new ClassView.TableOfWeb(columnsClass, rows, "Sem Arquivos classificados no Lote!");
                    //tableAbertos.LeftTitle = "Arquivos";
                    //tableAbertos.LeftValue = "Classificados";
                    //#endregion

                    //#region rows Fechados
                    //rows = new List<ClassView.TableLinhas>();

                    //foreach (Model.Ged.Arquivo arquivo in nclassificados)
                    //{
                    //    row = new ClassView.TableLinhas();
                    //    row.cells = new List<ClassView.TableCelulas>();

                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-2 cell";
                    //    cell.Value = arquivo.GEDARQ_IND.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
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
                    //        url = (Request.ApplicationPath + "/Arquivo/Detail/" + arquivo.GEDARQ_IND.ToString()).Replace("//", "/")
                    //    });
                    //    //cell.AjaxLink = (new ClassView.TableAjaxLink()
                    //    //{
                    //    //    ID = "link-" + arquivo.GEDARQ_IND.ToString(),
                    //    //    action = "Arquivo",
                    //    //    controller = "Lote",
                    //    //    route = lotearquivo,
                    //    //    ajaxOptions = new System.Web.Mvc.Ajax.AjaxOptions()
                    //    //    {
                    //    //        HttpMethod = "POST",
                    //    //        InsertionMode = System.Web.Mvc.Ajax.InsertionMode.Replace,
                    //    //        UpdateTargetId = "ArquivoAtual"
                    //    //    }
                    //    //});
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-2 cell";
                    //    cell.Value = arquivo.GEDARQ_DISPONIVELEM.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-2 cell";
                    //    cell.Value = arquivo.GEDARQ_DISPONIVELPOR.ToString();
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                    //    cell.Value = arquivo.GEDARQ_EXTENSAO;
                    //    row.cells.Add(cell);
                    //    cell = new ClassView.TableCelulas();
                    //    cell.CssClass = "col-sm-1 cell";
                    //    //cell.Style = "color: " + (arquivo.GLOTE_ENVIADO ? "GREEN" : "RED");
                    //    cell.Value = arquivo.GEDARQ_TAMANHO.ToString();
                    //    row.cells.Add(cell);

                    //    rows.Add(row);
                    //}

                    //tableFechados = new ClassView.TableOfWeb(columnsNClass, rows, "Sem Arquivos não classificados!");
                    //tableFechados.LeftTitle = "Arquivos";
                    //tableFechados.LeftValue = "Não Classificados";
                    //#endregion
                    #endregion

                    TableModel tableClass = new TableModel("GetPageTableClassificados", Request, Session, TableType.BoxSolid, TableDataType.Lote, id.Value)
                    {
                        id = "TableClassificados",
                        boxSolid = new BoxSolid()
                        {
                            box_title = "Arquivos: Classificados",
                            box_type_class = TypesBootstrapColors.primary,
                            isCollapsed = false,
                            isSubBox = false
                        }
                    };

                    TableModel tableNClass = new TableModel("GetPageTableNClassificados", Request, Session, TableType.BoxSolid, TableDataType.Lote, id.Value)
                    {
                        id = "TableNClassificados",
                        boxSolid = new BoxSolid()
                        {
                            box_title = "Arquivos: Não Classificados",
                            box_type_class = TypesBootstrapColors.primary,
                            isCollapsed = false,
                            isSubBox = false
                        }
                    };

                    view.loteArquivo.tableClass = tableClass;
                    view.loteArquivo.tableNClass = tableNClass;
                    #endregion
                    break;
            }
            return view;
        }

        public PartialViewResult GetPageTableAbertos(int rows, int pages, int page, string itens, int item)
        {
            LoteBD BD = new LoteBD(Session);
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Pagination pagination = new Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetPageTableAbertos", Request, BD.session, TableType.BoxSolid, TableDataType.Lote, pagination)
            {
                id = "TableLoteAbertos",
                boxSolid = new BoxSolid()
                {
                    box_title = "Lotes: Abertos",
                    box_type_class = TypesBootstrapColors.primary,
                    isCollapsed = false,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        public PartialViewResult GetPageTableFechados(int rows, int pages, int page, string itens, int item)
        {
            LoteBD BD = new LoteBD(Session);
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Pagination pagination = new Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetPageTableFechados", Request, BD.session, TableType.BoxSolid, TableDataType.Lote, pagination)
            {
                id = "TableLoteFechados",
                boxSolid = new BoxSolid()
                {
                    box_title = "Lotes: Fechados",
                    box_type_class = TypesBootstrapColors.primary,
                    isCollapsed = false,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        }

        private bool CheckValues(string value, ref string erro)
        {
            if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(value.Replace(" ", "")))
            {
                erro = "A descrição precisa estar preenchida!";
                return false;
            }
            else
                return true;
        }
    }
}