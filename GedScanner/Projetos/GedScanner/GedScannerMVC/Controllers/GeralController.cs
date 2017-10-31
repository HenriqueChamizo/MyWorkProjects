using Model;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class GeralController : GEDController
    {
        // GET: Geral
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        protected bool CheckSessions()
        {
            LoginsInternos login = Session["Usuario"] as LoginsInternos;
            if (login == null || login.cliente == null)
                return false;
            else
                return true;
        }
    }
}