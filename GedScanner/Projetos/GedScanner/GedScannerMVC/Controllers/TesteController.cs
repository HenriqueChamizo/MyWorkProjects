using System.Web.Mvc;

namespace GedScannerMVC.Controllers
{
    public class TesteController : GEDController
    {
        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teste/Index2
        public ActionResult Index2()
        {
            return View();
        }
    }
}