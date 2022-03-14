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
    public class oc_statusdirectorController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_statusdirector
        public ActionResult Index()
        {
            return View(db.oc_statusdirector.ToList());
        }

        // GET: oc_statusdirector/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusdirector oc_statusdirector = db.oc_statusdirector.Find(id);
            if (oc_statusdirector == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusdirector);
        }

        // GET: oc_statusdirector/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_statusdirector/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_statusdirector oc_statusdirector)
        {
            if (ModelState.IsValid)
            {
                db.oc_statusdirector.Add(oc_statusdirector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_statusdirector);
        }

        // GET: oc_statusdirector/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusdirector oc_statusdirector = db.oc_statusdirector.Find(id);
            if (oc_statusdirector == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusdirector);
        }

        // POST: oc_statusdirector/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_statusdirector oc_statusdirector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_statusdirector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_statusdirector);
        }

        // GET: oc_statusdirector/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statusdirector oc_statusdirector = db.oc_statusdirector.Find(id);
            if (oc_statusdirector == null)
            {
                return HttpNotFound();
            }
            return View(oc_statusdirector);
        }

        // POST: oc_statusdirector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_statusdirector oc_statusdirector = db.oc_statusdirector.Find(id);
            db.oc_statusdirector.Remove(oc_statusdirector);
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
