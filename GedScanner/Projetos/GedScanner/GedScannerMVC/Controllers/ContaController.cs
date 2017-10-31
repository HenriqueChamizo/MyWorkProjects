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
    public class ContaController : GEDController
    {
        ContaBD bd;

        // GET: Conta/id
        public ActionResult Index(int? id)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            ClassView.Conta view;
            if (id == null)
                view = MountView(false);
            else
                view = MountView(true, id);

            if (view.Erro == "-1")
                return RedirectToAction("Novo");
            else
                return View(view);
        }

        [HttpPost]
        public ActionResult Salvar(FrmContaIndex form)
        {
            string[] values = new string[]
            {
                form.acesso,
                form.descricao,
                form.ddlTipos.Replace("-1", "")
            };
            string[] errors = new string[values.Length];
            if (CheckValues(values, ref errors))
            {
                Model.Conta conta = new Model.Conta();
                conta.CONT_IND = form.indice.Contains("Índice: ") ? Convert.ToInt32(form.indice.Replace("Índice: ", "")) : 0;
                conta.CONT_ACESSO = form.acesso;
                conta.CONT_DESCRICAO = form.descricao;
                conta.condition = Convert.ToBoolean(form.condition);
                conta.CONT_DT_INICIO = DateTime.Now;
                conta.CONT_CONTASTIPOS = new ContaTipo() { CONTP_IND = Convert.ToInt32(form.ddlTipos) };

                LoginsInternos login = Session["Usuario"] as LoginsInternos;

                TypesErrors erro = new TypesErrors();
                bd = new ContaBD(Session);
                if (Convert.ToBoolean(form.edit))
                    bd.SetContaEdit(conta, login.LOGI_IND, ref erro);
                else
                    bd.SetContaInsert(conta, login.LOGI_IND, ref erro);
            }
            return View("Index", MountView(false));
        }

        [HttpPost]
        public ActionResult SalvarTipo(FrmContaTipo form)
        {
            string[] values = new string[]
            {
                form.classificacao,
                form.descricao
            };
            string[] errors = new string[values.Length];
            if (CheckValues(values, ref errors))
            {
                Model.ContaTipo tipo = new Model.ContaTipo();
                tipo.CONTP_IND = form.indice.Contains("Índice: ") ? Convert.ToInt32(form.indice.Replace("Índice: ", "")) : 0;
                tipo.CONTP_CLASSIFICADOR = form.classificacao;
                tipo.CONTP_DESCRICAO = form.descricao;
                tipo.CONTP_DT_INICIO = DateTime.Now;

                LoginsInternos login = Session["Usuario"] as LoginsInternos;
                tipo.CONTP_LOGININSERT = login.LOGI_IND;
                TypesErrors erro = new TypesErrors();
                bd = new ContaBD(Session);
                if (Convert.ToBoolean(form.edit))
                    bd.SetContaTipoEdit(tipo, ref erro);
                else
                    bd.SetContaTipoInsert(tipo, ref erro);
            }
            return View("Tipo", MountViewTipo(false));
        }

        public ActionResult Novo()
        {
            return View("Index", MountView(true));
        }

        public ActionResult Tipo(int? id)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            ClassView.Conta view;
            if (id == null)
                view = MountViewTipo(false);
            else
                view = MountViewTipo(true, id);

            if (view.Erro == "-1")
                return RedirectToAction("Novo");
            else
                return View("Tipo", view);
        }

        public ActionResult NovoTipo()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            ClassView.Conta view = MountViewTipo(true);
            return View("Tipo", view);
        }

        private ClassView.Conta MountView(bool detail, int? id = null)
        {
            TypesErrors erro = new TypesErrors();
            bd = new ContaBD(Session);

            ClassView.Conta view = new ClassView.Conta();
            Model.PlanoContas plano = new Model.PlanoContas();
            Model.Conta conta = new Model.Conta();
            List<ContaTipo> tipos = new List<ContaTipo>();

            if (detail)
            {
                if (id != null)
                {
                    #region idIsNull
                    conta.CONT_IND = id.Value;
                    bd.GetContaById(ref conta, ref plano, ref tipos, ref erro);

                    view.indice = "Índice: " + conta.CONT_IND.ToString();
                    view.acesso = conta.CONT_ACESSO;
                    view.descricao = conta.CONT_DESCRICAO;
                    view.datainicio = conta.CONT_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                    view.condition = conta.condition;
                    view.edit = true;
                    #endregion
                }
                else
                {
                    #region idIsNotNull
                    bd.GetContasTipos(ref tipos, ref plano, ref erro);
                    
                    view.indice = "Inserir nova Conta";
                    view.acesso = "";
                    view.descricao = "";
                    view.datainicio = "Data e tempo atual";
                    view.condition = false;
                    view.edit = false;
                    #endregion
                }

                SelectListItem[] ddltipos = new SelectListItem[tipos.Count + 1];

                SelectListItem item = new SelectListItem()
                {
                    Text = "Selecione...",
                    Value = "-1"
                };
                ddltipos[0] = item;
                int count = 1;
                foreach (ContaTipo tipo in tipos)
                {
                    if (conta.CONT_CONTASTIPOS != null)
                    {
                        item = new SelectListItem()
                        {
                            Text = tipo.CONTP_CLASSIFICADOR + " - " + tipo.CONTP_DESCRICAO,
                            Value = tipo.CONTP_IND.ToString(),
                            Selected = conta.CONT_CONTASTIPOS.CONTP_IND == tipo.CONTP_IND
                        };
                    }
                    else
                    {
                        item = new SelectListItem()
                        {
                            Text = tipo.CONTP_CLASSIFICADOR + " - " + tipo.CONTP_DESCRICAO,
                            Value = tipo.CONTP_IND.ToString(),
                            Selected = false
                        };
                    }
                    ddltipos[count] = item;
                    count++;
                }
                view.ddlTipos = ddltipos;
                view.tableModel = null;

                HeaderConta header = new HeaderConta();
                header.indPlanoAtual = plano.PLAN_IND;
                header.planoAtual = plano.PLAN_DESCRICAO;
                view.header = header;
            }
            else
            {
                List<Model.Conta> contas = new List<Model.Conta>();
                bd.GetContasAndAtual(ref plano, ref contas, ref erro);
                
                HeaderConta header = new HeaderConta();
                header.indPlanoAtual = plano.PLAN_IND;
                header.planoAtual = plano.PLAN_DESCRICAO;
                view.header = header;

                TableModel tableModel = new TableModel("GetPageTable", Request, Session, TableType.StripedUnBorder, TableDataType.Conta)
                {
                    id = "TableConta", 
                    boxSolid = null
                };

                view.tableModel = tableModel;
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

            TableModel table = new TableModel("GetPageTable", Request, Session, TableType.StripedUnBorder, TableDataType.Conta, pagination)
            {
                id = "TableConta",
                boxSolid = null
            };

            return PartialView("Table", table);
        }

        private ClassView.Conta MountViewTipo(bool detail, int? id = null)
        {
            TypesErrors erro = new TypesErrors();
            bd = new ContaBD(Session);

            ClassView.Conta view = new ClassView.Conta();

            if (detail)
            {
                if (id != null)
                {
                    #region idIsNull
                    ContaTipo contaTipo = new ContaTipo();
                    contaTipo.CONTP_IND = id.Value;
                    bd.GetContaTipoById(ref contaTipo, ref erro);

                    view.indice = "Índice: " + contaTipo.CONTP_IND.ToString();
                    view.tipo = contaTipo;
                    view.datainicio = contaTipo.CONTP_DT_INICIO.ToString("dd/MM/yyyy HH:mm:ss");
                    //view.table = null;
                    view.edit = true;
                    #endregion
                }
                else
                {
                    #region idIsNotNull
                    view.indice = "Inserir novo Tipo de Conta";
                    view.tipo = new ContaTipo();
                    view.tipo.CONTP_CLASSIFICADOR = "";
                    view.tipo.CONTP_DESCRICAO = ""; 
                    view.datainicio = "Data e tempo atual";
                    view.condition = false;
                    view.edit = false;
                    #endregion
                }
                return view;
            }
            else
            {
                
                view.Erro = null;

                TableModel tableModel = new TableModel("GetPageTableTipo", Request, Session, TableType.StripedUnBorder, TableDataType.ContaTipo)
                {
                    id = "TableContaTipo",
                    boxSolid = null
                };

                view.tableModel = tableModel;
                return view;
            }
        }

        public PartialViewResult GetPageTableTipo(int rows, int pages, int page, string itens, int item)
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

            TableModel table = new TableModel("GetPageTableTipo", Request, Session, TableType.StripedUnBorder, TableDataType.ContaTipo)
            {
                id = "TableContaTipo",
                boxSolid = null
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

        private bool CheckValues(string[] values, ref string[] errors)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrEmpty(values[i]) && string.IsNullOrEmpty(values[i].Replace(" ", "")))
                    errors[i] = "O(s) valor(es) precisa(m) estar preenchido(s)!";
                else
                    errors[i] = "";
            }
            foreach (string erro in errors)
            {
                if (!string.IsNullOrEmpty(erro))
                    return false;
            }
            return true;
        }
    }
}