using GedScannerMVC.ClassView;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GedScannerMVC.Controllers
{
    public class UtilitiesController : Controller
    {
        // GET: Utilities
        public ActionResult Index()
        {
            return View();
        }

        public string Alert(string dado)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic dados = serializer.DeserializeObject(dado);

            Alert alert = new Alert();

            foreach (KeyValuePair<string, object> jsonLines in dados)
            {
                var jsonLinesKey = jsonLines.Key;
                switch (jsonLinesKey)
                {
                    case "type_color":
                        switch (Convert.ToInt32(jsonLines.Value))
                        {
                            case 0:
                                alert.type_color = TypesBootstrapColors.info;
                                break;
                            case 1:
                                alert.type_color = TypesBootstrapColors.primary;
                                break;
                            case 2:
                                alert.type_color = TypesBootstrapColors.success;
                                break;
                            case 3:
                                alert.type_color = TypesBootstrapColors.warning;
                                break;
                            case 4:
                                alert.type_color = TypesBootstrapColors.danger;
                                break;
                        }
                        break;
                    case "title":
                        alert.title = jsonLines.Value.ToString();
                        break;
                    case "text":
                        alert.text = jsonLines.Value.ToString();
                        break;
                }
            }

            string retorno;// = PartialView(alert).ToString().Replace("\r", "").Replace("\n", "");
            retorno = @"
            <div class='alert alert-danger alert-dismissible'>
                <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>";

            if (!string.IsNullOrEmpty(alert.title))
                retorno = retorno + "<h4><i class='icon fa fa-ban'></i> " + alert.title + "</h4>";
            retorno = retorno + alert.text;
            retorno = retorno + "</div>";

            return retorno.Replace("\r", "").Replace("\n", "");
        }
    }
}