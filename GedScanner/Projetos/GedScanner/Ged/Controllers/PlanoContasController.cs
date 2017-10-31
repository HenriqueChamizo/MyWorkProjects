using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ged.Models;
using System;

namespace Ged.Controllers
{
    public class PlanoContasController : Controller
    {
        private GedContext db = new GedContext();

        // GET: PlanoContas
        public ActionResult Index()
        {
            return View(db.PlanoContas.ToList());
        }

        // GET: PlanoContas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoContas planoContas = db.PlanoContas.Find(id);
            if (planoContas == null)
            {
                return HttpNotFound();
            }
            return View(planoContas);
        }

        // GET: PlanoContas/Create
        public ActionResult Create()
        {
            PlanoContas plano = new PlanoContas();
            plano.Codigo = "0000000000";
            plano.Inicio = DateTime.Now.ToString();
            return View(plano);
        }

        // POST: PlanoContas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Descricao,Codigo,Fechado,Inicio,LoginInsert")] PlanoContas planoContas)
        {
            if (ModelState.IsValid)
            {
                db.PlanoContas.Add(planoContas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planoContas);
        }

        // GET: PlanoContas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoContas planoContas = db.PlanoContas.Find(id);
            if (planoContas == null)
            {
                return HttpNotFound();
            }
            return View(planoContas);
        }

        // POST: PlanoContas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Descricao,Codigo,Fechado,Inicio,LoginInsert")] PlanoContas planoContas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planoContas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planoContas);
        }

        // GET: PlanoContas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoContas planoContas = db.PlanoContas.Find(id);
            if (planoContas == null)
            {
                return HttpNotFound();
            }
            return View(planoContas);
        }

        // POST: PlanoContas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanoContas planoContas = db.PlanoContas.Find(id);
            db.PlanoContas.Remove(planoContas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
