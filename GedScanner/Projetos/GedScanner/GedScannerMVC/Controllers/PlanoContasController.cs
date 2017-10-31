using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassFrm;
using GedScannerMVC.ClassView;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class PlanoContasController : GEDController
    {
        PlanoContasBD BD;

        // GET: PlanoContas
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");
            
            return View(MountView());
        }

        public ActionResult Plano(int? id)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            if (id == null)
                return View("Index", MountView());
            else
            {
                TypesErrors erro = new TypesErrors();
                BD = new PlanoContasBD(Session);
                Model.PlanoContas plano = new Model.PlanoContas();
                plano.PLAN_IND = id.Value;
                BD.GetPlanoContasById(ref plano, ref erro);

                ClassView.PlanoContas view = new ClassView.PlanoContas();
                view.indice = "Índice: " + plano.PLAN_IND.ToString();
                view.codigo = plano.PLAN_CODIGO;
                view.descricao = plano.PLAN_DESCRICAO;
                view.datainicio = plano.PLAN_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                view.finalizar = plano.PLAN_FECHADO.ToString();

                view.ckbFinalizarVisible = true;
                if (plano.condition)
                {
                    view.ckbFinalizarText = "Só poderá ser aberto quando NÃO existir outro Plano de Contas Aberto!";
                    view.ckbFinalizarEnable = false;
                }
                else
                    view.ckbFinalizarEnable = true;
                
                view.ckbsContasVisibles = false;
                view.tableFinalizadasVisible = false;
                return View("Plano", view);
            }
        }

        //protected bool CheckSessions()
        //{
        //    LoginsInternos login = Session["Usuario"] as LoginsInternos;
        //    if (login == null || login.cliente == null)
        //        return false;
        //    else
        //        return true;
        //}

        [HttpPost]
        public ActionResult Salvar(FrmPlanoContasIndex form)
        {
            string erros = "";
            if (CheckValues(form.descricao, ref erros))
            {
                Model.PlanoContas planocontas = new Model.PlanoContas();
                planocontas.PLAN_IND = form.indice.Contains("Índice: ") ? Convert.ToInt32(form.indice.Replace("Índice: ", "")) : 0;
                planocontas.PLAN_CODIGO = form.codigo;
                planocontas.PLAN_DESCRICAO = form.descricao;
                planocontas.PLAN_FECHADO = Convert.ToBoolean(form.finalizar);
                //planocontas.PLAN_DT_INICIO = form.datainicio == "Data e tempo atual" ? DateTime.Now : Convert.ToDateTime(form.datainicio);
                planocontas.PLAN_DT_INICIO = DateTime.Now;
                //planocontas.PLAN_DT_INICIO = DateTime.Now;

                LoginsInternos login = Session["Usuario"] as LoginsInternos;

                TypesErrors erro = new TypesErrors();
                BD = new PlanoContasBD(Session);
                if (Convert.ToBoolean(form.edit))
                    BD.SetPlanoContasEdit(planocontas, login.LOGI_IND, ref erro);
                else
                    BD.SetPlanoContasInsert(planocontas, login.LOGI_IND, ref erro);
            }
            return View("Index", MountView());
        }

        private ClassView.PlanoContas MountView()
        {
            TypesErrors erro = new TypesErrors();
            BD = new PlanoContasBD(Session);
            List<Model.PlanoContas> planosContas = new List<Model.PlanoContas>();
            ClassView.PlanoContas view = new ClassView.PlanoContas();

            if (BD.GetPlanoContas(ref planosContas, ref erro))
            {
                Model.PlanoContas planoC = planosContas.Find(f => f.PLAN_FECHADO == false);
                if (planoC != null)
                {
                    view.indice = "Índice: " + planoC.PLAN_IND.ToString();
                    view.codigo = planoC.PLAN_CODIGO;
                    view.descricao = planoC.PLAN_DESCRICAO;
                    view.datainicio = planoC.PLAN_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                    view.finalizar = planoC.PLAN_FECHADO.ToString();
                    view.ckbFinalizarVisible = true;
                }
                else
                {
                    planoC = new Model.PlanoContas();
                    planoC.PLAN_CODIGO = "0";
                    foreach (Model.PlanoContas pc in planosContas)
                    {
                        if (Convert.ToInt32(pc.PLAN_CODIGO) > Convert.ToInt32(planoC.PLAN_CODIGO))
                            planoC = pc;
                    }
                    view.indice = "Nenhum Plano de Contas aberto! Insira um novo";
                    view.codigo = GetMaxValue((Convert.ToInt32(planoC.PLAN_CODIGO) + 1).ToString(), 10, "0");
                    view.descricao = "";
                    view.datainicio = "Data e tempo atual";
                    view.finalizar = false.ToString();
                    view.ckbFinalizarVisible = false;

                    view.ckbsContasVisibles = false;
                    view.tableFinalizadasVisible = true;
                    view.contas = new List<Model.Conta>();
                }

                #region Comments

                //#region columns
                //List<TableColunas> columns = new List<TableColunas>();
                //TableColunas column = new TableColunas();
                //column.tabindex = 0;
                //column.aria_controls = "dataTables";
                //column.rowspan = 1;
                //column.colspan = 1;
                //column.aria_sort = "ascending";
                //column.aria_label = "PLAN_IND";
                ////column.Style = "min-width:20px";
                //column.CssClass = "col-sm-1 cell";
                //column.Text = "Ind";
                //columns.Add(column);

                //column = new TableColunas();
                //column.tabindex = 0;
                //column.aria_controls = "dataTables";
                //column.rowspan = 1;
                //column.colspan = 1;
                //column.aria_sort = "ascending";
                //column.aria_label = "PLAN_DESCRICAO";
                ////column.Style = "min-width:200px";
                //column.CssClass = "col-sm-4 cell";
                //column.Text = "Descrição";
                //columns.Add(column);

                //column = new TableColunas();
                //column.tabindex = 0;
                //column.aria_controls = "dataTables";
                //column.rowspan = 1;
                //column.colspan = 1;
                //column.aria_sort = "ascending";
                //column.aria_label = "PLAN_CODIGO";
                ////column.Style = "min-width:40px";
                //column.CssClass = "col-sm-2 cell";
                //column.Text = "Código";
                //columns.Add(column);

                //column = new TableColunas();
                //column.tabindex = 0;
                //column.aria_controls = "dataTables";
                //column.rowspan = 1;
                //column.colspan = 1;
                //column.aria_sort = "ascending";
                //column.aria_label = "PLAN_DT_INICIO";
                ////column.Style = "min-width:100px";
                //column.Text = "Criado em";
                //column.CssClass = "col-sm-3 cell";
                //columns.Add(column);

                //column = new TableColunas();
                //column.tabindex = 0;
                //column.aria_controls = "dataTables";
                //column.rowspan = 1;
                //column.colspan = 1;
                //column.aria_sort = "ascending";
                //column.aria_label = "PLAN_LOGININSERT";
                ////column.Style = "min-width:100px";
                //column.Text = "Criado por";
                //column.CssClass = "col-sm-2 cell";
                //columns.Add(column);
                //#endregion

                //#region rows
                //List<TableLinhas> rows = new List<TableLinhas>();
                //TableLinhas row;
                //TableCelulas cell;

                //List<Model.PlanoContas> PlanConts = new List<Model.PlanoContas>();
                //foreach (Model.PlanoContas plano in planosContas)
                //{
                //    if (plano.PLAN_FECHADO)
                //    {
                //        view.tableFinalizadasVisible = true;

                //        List<Model.PlanoContas> codigos = planosContas.FindAll(f => f.PLAN_CODIGO == plano.PLAN_CODIGO);
                //        if (codigos.Count == 1)
                //            PlanConts.Add(plano);
                //        else
                //        {
                //            bool maior = false;
                //            foreach (Model.PlanoContas planoM in codigos)
                //            {
                //                if (plano.PLAN_DT_INICIO > planoM.PLAN_DT_INICIO)
                //                    maior = true;
                //                else
                //                    maior = false;
                //            }
                //            if (maior)
                //                PlanConts.Add(plano);
                //        }
                //    }
                //}

                //foreach (Model.PlanoContas plano in PlanConts)
                //{
                //    row = new TableLinhas();
                //    row.cells = new List<TableCelulas>();

                //    cell = new TableCelulas();
                //    cell.CssClass = "col-sm-1 cell";
                //    cell.Value = plano.PLAN_IND.ToString();
                //    row.cells.Add(cell);
                //    cell = new TableCelulas();
                //    cell.CssClass = "col-sm-4 cell";
                //    cell.Value = plano.PLAN_DESCRICAO.ToString();
                //    cell.link = (new TableLink()
                //    {
                //        ID = "link-" + plano.PLAN_IND.ToString(),
                //        url = "~/PlanoContas/Plano/" + plano.PLAN_IND.ToString()
                //    });
                //    row.cells.Add(cell);
                //    cell = new TableCelulas();
                //    cell.CssClass = "col-sm-2 cell";
                //    cell.Value = plano.PLAN_CODIGO.ToString();
                //    row.cells.Add(cell);
                //    cell = new TableCelulas();
                //    cell.CssClass = "col-sm-3 cell";
                //    cell.Value = plano.PLAN_DT_INICIO.ToString();
                //    row.cells.Add(cell);
                //    cell = new TableCelulas();
                //    cell.CssClass = "col-sm-2 cell";
                //    cell.Value = plano.PLAN_LOGININSERT.ToString();
                //    row.cells.Add(cell);

                //    rows.Add(row);
                //}
                //#endregion

                #endregion

                List<object> objs = new List<object>();
                foreach (Model.PlanoContas plano in planosContas)
                {
                    objs.Add(plano as object);
                }

                TableModel table = new TableModel("GetPageTable", Request, TableType.BoxSolid, TableDataType.PlanoContas, objs)
                {
                    id = "TablePlanoContas",
                    boxSolid = new BoxSolid()
                    {
                        box_type_class = TypesBootstrapColors.primary,
                        box_title = "Relação Plano de Contas",
                        isCollapsed = false,
                        isSubBox = false
                    }
                };

                view.tableModel = table;
            }
            else
            {
                view.indice = "Nenhum Plano de Contas inserido! Insira um novo";
                view.codigo = GetMaxValue("0", 10, "0");
                view.descricao = "";
                view.datainicio = "Data e tempo atual";
                view.finalizar = false.ToString();
                view.ckbFinalizarVisible = false;

                view.ckbsContasVisibles = false;
                view.tableFinalizadasVisible = false;
                view.contas = new List<Model.Conta>();
            }

            return view;
        }

        public PartialViewResult GetPageTable(int rows, int pages, int page, string itens, int item)
        {
            string[] itensstring = itens.Split(new string[] { "-" }, StringSplitOptions.None);
            int[] Itens = new int[itensstring.Length];
            for (int i = 0; i < itensstring.Length; i++)
            {
                Itens[i] = Convert.ToInt32(itensstring[i]);
                if (Itens[i] == item)
                    item = i;
            }
            Pagination pagination = new Pagination(rows, pages, page, Itens, item);

            TableModel table = new TableModel("GetPageTable", Request, Session, TableType.BoxSolid, TableDataType.PlanoContas, pagination)
            {
                id = "TablePlanoContas",
                boxSolid = new BoxSolid()
                {
                    box_type_class = TypesBootstrapColors.primary,
                    box_title = "Relação Plano de Contas",
                    isCollapsed = false,
                    isSubBox = true
                }
            };

            return PartialView("Table", table);
        } 

        private string GetMaxValue(string value, int length, string acres)
        {
            string returnValue = value;
            for (int i = 0; i < (length - value.Length); i++)
            {
                if (returnValue.Length != length)
                    returnValue = acres + returnValue;
            }
            return returnValue;
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