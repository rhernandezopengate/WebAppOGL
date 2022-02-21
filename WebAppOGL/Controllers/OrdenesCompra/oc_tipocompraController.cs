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
    public class oc_tipocompraController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_tipocompra
        public ActionResult Index()
        {
            return View(db.oc_tipocompra.ToList());
        }

        // GET: oc_tipocompra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_tipocompra oc_tipocompra = db.oc_tipocompra.Find(id);
            if (oc_tipocompra == null)
            {
                return HttpNotFound();
            }
            return View(oc_tipocompra);
        }

        // GET: oc_tipocompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_tipocompra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_tipocompra oc_tipocompra)
        {
            if (ModelState.IsValid)
            {
                db.oc_tipocompra.Add(oc_tipocompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_tipocompra);
        }

        // GET: oc_tipocompra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_tipocompra oc_tipocompra = db.oc_tipocompra.Find(id);
            if (oc_tipocompra == null)
            {
                return HttpNotFound();
            }
            return View(oc_tipocompra);
        }

        // POST: oc_tipocompra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_tipocompra oc_tipocompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_tipocompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_tipocompra);
        }

        // GET: oc_tipocompra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_tipocompra oc_tipocompra = db.oc_tipocompra.Find(id);
            if (oc_tipocompra == null)
            {
                return HttpNotFound();
            }
            return View(oc_tipocompra);
        }

        // POST: oc_tipocompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_tipocompra oc_tipocompra = db.oc_tipocompra.Find(id);
            db.oc_tipocompra.Remove(oc_tipocompra);
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
