using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class SharedController : GEDController
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }
    }
}