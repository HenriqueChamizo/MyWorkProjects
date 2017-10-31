using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassView;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GedScannerMVC.Controllers
{
    public class ArquivoTipoController : GEDController
    {
        private ArquivoTipoBD BD;

        // GET: ArquivoTipo
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

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            if (id == null)
                return View("Index");

            TypesErrors erro = new TypesErrors();
            BD = new ArquivoTipoBD(Session);
            Model.Ged.ArquivoTipo tipo = BD.GetArquivoTipo(id.Value, ref erro);
            ArquivoTipo view = new ArquivoTipo();
            view.descricao = tipo.GEDTIPO_DESCRICAO;
            view.exporta = tipo.GEDTIPO_EXPORTA;
            return View(view);
        }

        public PartialViewResult AddCampoDinamicoNovo(int count)
        {
            Thread.Sleep(500);
            TypesErrors erro = new TypesErrors();
            BD = new ArquivoTipoBD(Session);
            List<Model.Ged.CampoTipo> tipos = BD.GetCampoTipos(ref erro);
            ArquivoTipo view = new ArquivoTipo();
            view.campo = new CampoDinamico();
            view.campo.NameDescricao = "0-descricao-" + count.ToString();
            view.campo.IdDescricao = "descricao-" + count.ToString();
            view.campo.ClassDescricao = "form-control";
            view.campo.PlaceHolderDescricao = "Descrição";
            view.campo.NameObrigacao = "obrigacao-" + count.ToString();
            view.campo.IdObrigacao = "obrigacao-" + count.ToString();
            view.campo.ClassObrigacao = "checkbox-inline";
            view.campo.NameTipo = "tipo-" + count.ToString();
            view.campo.IdTipo = "tipo-" + count.ToString();
            view.campo.ClassTipo = "form-control";
            view.campo.tipos = tipos;
            return PartialView("CamposDinamico", view);
        }

        public PartialViewResult AddCamposDinamicoDetail(int id)
        {
            Thread.Sleep(500);
            int count = 0;
            TypesErrors erro = new TypesErrors();
            BD = new ArquivoTipoBD(Session);
            List<Model.Ged.CampoDetail> campos = BD.GetCamposByArquivoTipo(id, ref erro);
            ArquivoTipo view = new ArquivoTipo();
            view.campos = new List<CampoDinamico>();
            CampoDinamico dinamico;
            foreach (Model.Ged.CampoDetail campo in campos)
            {
                dinamico = new CampoDinamico();
                dinamico.NameDescricao = campo.CAMP_IND.ToString() + "-descricao-" + count.ToString();
                dinamico.IdDescricao = "descricao-" + count.ToString();
                dinamico.ClassDescricao = "form-control";
                dinamico.PlaceHolderDescricao = "Descrição";
                dinamico.ValueDescricao = campo.CAMP_DESCRICAO;

                dinamico.NameObrigacao = campo.CAMP_IND.ToString() + "-obrigacao-" + count.ToString();
                dinamico.IdObrigacao = "obrigacao-" + count.ToString();
                dinamico.ClassObrigacao = "checkbox-inline";
                dinamico.CheckedObrigacao = campo.CAMP_OBRIGATORIO ? "checked" : null;

                dinamico.NameTipo = campo.CAMP_IND.ToString() + "-tipo-" + count.ToString();
                dinamico.IdTipo = "tipo-" + count.ToString();
                dinamico.ClassTipo = "form-control";
                dinamico.ValueOptionTipo = campo.CAMP_TIPO.CAPTIP_IND;
                dinamico.tipos = campo.tipos;
                view.campos.Add(dinamico);
                count++;
            }
            return PartialView("CamposDinamico", view);
        }

        private ArquivoTipo MountView()
        {
            ArquivoTipo view = new ArquivoTipo();
            TableModel tableClass = new TableModel("GetPageTableArquivosTipos", Request, Session, TableType.StripedUnBorder, TableDataType.ArquivoTipo)
            {
                id = "TableArquivosTipos",
                boxSolid = null 
            };

            view.tableModel = tableClass;

            return view;
        }

        public PartialViewResult GetPageTableArquivosTipos(int rows, int pages, int page, string itens, int item)
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

            TableModel table = new TableModel("GetPageTableArquivosTipos", Request, Session, TableType.StripedUnBorder, TableDataType.ArquivoTipo, pagination)
            {
                id = "TableArquivosTipos",
                boxSolid = null
            };

            return PartialView("Table", table);
        }

        //public string Salvar(IDictionary<string, object> json)
        //{
        //    return json.First(f=>f.Key != null).Key;
        //}

        public string Salvar(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic objectJson = serializer.DeserializeObject(json);

            LoginsInternos login = Session["Usuario"] as LoginsInternos;
            DateTime now = DateTime.Now;
            Model.Ged.ArquivoTipo tipo = new Model.Ged.ArquivoTipo();
            tipo.GEDTIPO_DT_INICIO = now;
            tipo.GEDTIPO_LOGININSERT = login.LOGI_IND;
            List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
            Model.Ged.Campo campo;
            foreach (KeyValuePair<string, object> jsonLines in objectJson)
            {
                var jsonLinesKey = jsonLines.Key;
                switch (jsonLinesKey)
                {
                    case "descricao":
                        tipo.GEDTIPO_DESCRICAO = Convert.ToString(jsonLines.Value);
                        break;
                    case "exporta":
                        tipo.GEDTIPO_EXPORTA = Convert.ToBoolean(jsonLines.Value);
                        break;
                    case "campos":
                        dynamic jsonLine = jsonLines.Value;
                        foreach (Dictionary<string, object> dictionaryLines in jsonLine)
                        {
                            campo = new Model.Ged.Campo();
                            campo.CAMP_DT_INICIO = now;
                            campo.CAMP_LOGININSERT = login.LOGI_IND;
                            foreach (KeyValuePair<string, object> keyValueLine in dictionaryLines)
                            {
                                var keyValueLineKey = keyValueLine.Key;
                                switch (keyValueLineKey)
                                {
                                    case "CAMP_DESCRICAO":
                                        campo.CAMP_DESCRICAO = Convert.ToString(keyValueLine.Value);
                                        break;
                                    case "CAMP_OBRIGATORIO":
                                        campo.CAMP_OBRIGATORIO = Convert.ToBoolean(keyValueLine.Value);
                                        break;
                                    case "CAMP_TIPO":
                                        campo.CAMP_TIPO = new Model.Ged.CampoTipo();
                                        campo.CAMP_TIPO.CAPTIP_IND = Convert.ToInt32(keyValueLine.Value);
                                        break;
                                }
                            }
                            campos.Add(campo);
                        }
                        break;
                }
            }

            TypesErrors erro = new TypesErrors();
            BD = new ArquivoTipoBD(Session);
            BD.SetArquivoTipoAndCamposInsert(tipo, campos, ref erro);

            return Url.Action("Index", "ArquivoTipo");
        }

        public string SalvarDetail(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic objectJson = serializer.DeserializeObject(json);

            LoginsInternos login = Session["Usuario"] as LoginsInternos;
            DateTime now = DateTime.Now;
            Model.Ged.ArquivoTipo tipo = new Model.Ged.ArquivoTipo();
            tipo.GEDTIPO_DT_INICIO = now;
            tipo.GEDTIPO_LOGININSERT = login.LOGI_IND;
            List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
            Model.Ged.Campo campo;
            foreach (KeyValuePair<string, object> jsonLines in objectJson)
            {
                var jsonLinesKey = jsonLines.Key;
                switch (jsonLinesKey)
                {
                    case "id":
                        tipo.GEDTIPO_IND = Convert.ToInt32(jsonLines.Value);
                        break;
                    case "descricao":
                        tipo.GEDTIPO_DESCRICAO = Convert.ToString(jsonLines.Value);
                        break;
                    case "exporta":
                        tipo.GEDTIPO_EXPORTA = Convert.ToBoolean(jsonLines.Value);
                        break;
                    case "campos":
                        dynamic jsonLine = jsonLines.Value;
                        foreach (Dictionary<string, object> dictionaryLines in jsonLine)
                        {
                            campo = new Model.Ged.Campo();
                            campo.CAMP_DT_INICIO = now;
                            campo.CAMP_LOGININSERT = login.LOGI_IND;
                            foreach (KeyValuePair<string, object> keyValueLine in dictionaryLines)
                            {
                                var keyValueLineKey = keyValueLine.Key;
                                switch (keyValueLineKey)
                                {
                                    case "CAMP_IND":
                                        campo.CAMP_IND = Convert.ToInt32(keyValueLine.Value);
                                        break;
                                    case "CAMP_DESCRICAO":
                                        campo.CAMP_DESCRICAO = Convert.ToString(keyValueLine.Value);
                                        break;
                                    case "CAMP_OBRIGATORIO":
                                        campo.CAMP_OBRIGATORIO = Convert.ToBoolean(keyValueLine.Value);
                                        break;
                                    case "CAMP_TIPO":
                                        campo.CAMP_TIPO = new Model.Ged.CampoTipo();
                                        campo.CAMP_TIPO.CAPTIP_IND = Convert.ToInt32(keyValueLine.Value);
                                        break;
                                }
                            }
                            campos.Add(campo);
                        }
                        break;
                }
            }

            TypesErrors erro = new TypesErrors();
            BD = new ArquivoTipoBD(Session);
            BD.SetArquivoTipoAndCamposEdit(tipo, campos, ref erro);

            return Url.Action("Index", "ArquivoTipo");
        }
    }
}