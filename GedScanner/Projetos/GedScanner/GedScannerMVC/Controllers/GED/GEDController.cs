using Model;
using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public abstract class GEDController : Controller
    {
        public bool CheckSessions()
        {
            LoginsInternos login = Session["Usuario"] as LoginsInternos;
            if (login == null || login.cliente == null)
                return false;
            else
                return true;
        }
    }
}