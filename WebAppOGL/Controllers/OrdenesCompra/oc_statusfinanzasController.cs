using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    public class oc_statusfinanzasController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_statusfinanzas
        [Authorize(Roles = "sistemas")]
        public ActionResult Index()
        {
            return View(db.oc_statusfinanzas.ToList());
        }

        // GET: oc_statusfinanzas/Details/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusfinanzas oc_statusfinanzas = db.oc_statusfinanzas.Find(id);
            if (oc_statusfinanzas == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusfinanzas);
        }

        // GET: oc_statusfinanzas/Create
        [Authorize(Roles = "sistemas")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_statusfinanzas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_statusfinanzas oc_statusfinanzas)
        {
            if (ModelState.IsValid)
            {
                db.oc_statusfinanzas.Add(oc_statusfinanzas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_statusfinanzas);
        }

        // GET: oc_statusfinanzas/Edit/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusfinanzas oc_statusfinanzas = db.oc_statusfinanzas.Find(id);
            if (oc_statusfinanzas == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusfinanzas);
        }

        // POST: oc_statusfinanzas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_statusfinanzas oc_statusfinanzas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_statusfinanzas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_statusfinanzas);
        }

        // GET: oc_statusfinanzas/Delete/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusfinanzas oc_statusfinanzas = db.oc_statusfinanzas.Find(id);
            if (oc_statusfinanzas == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusfinanzas);
        }

        // POST: oc_statusfinanzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_statusfinanzas oc_statusfinanzas = db.oc_statusfinanzas.Find(id);
            db.oc_statusfinanzas.Remove(oc_statusfinanzas);
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
