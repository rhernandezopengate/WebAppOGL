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
    public class oc_statuscomprasController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_statuscompras
        [Authorize(Roles = "sistemas")]
        public ActionResult Index()
        {
            return View(db.oc_statuscompras.ToList());
        }

        // GET: oc_statuscompras/Details/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statuscompras oc_statuscompras = db.oc_statuscompras.Find(id);
            if (oc_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(oc_statuscompras);
        }

        // GET: oc_statuscompras/Create
        [Authorize(Roles = "sistemas")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_statuscompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_statuscompras oc_statuscompras)
        {
            if (ModelState.IsValid)
            {
                db.oc_statuscompras.Add(oc_statuscompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_statuscompras);
        }

        // GET: oc_statuscompras/Edit/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statuscompras oc_statuscompras = db.oc_statuscompras.Find(id);
            if (oc_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(oc_statuscompras);
        }

        // POST: oc_statuscompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_statuscompras oc_statuscompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_statuscompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_statuscompras);
        }

        // GET: oc_statuscompras/Delete/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statuscompras oc_statuscompras = db.oc_statuscompras.Find(id);
            if (oc_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(oc_statuscompras);
        }

        // POST: oc_statuscompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_statuscompras oc_statuscompras = db.oc_statuscompras.Find(id);
            db.oc_statuscompras.Remove(oc_statuscompras);
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
