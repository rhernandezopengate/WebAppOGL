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
    public class oc_supervisoresController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_supervisores
        public ActionResult Index()
        {
            return View(db.oc_supervisores.ToList());
        }

        // GET: oc_supervisores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_supervisores oc_supervisores = db.oc_supervisores.Find(id);
            if (oc_supervisores == null)
            {
                return HttpNotFound();
            }
            return View(oc_supervisores);
        }

        // GET: oc_supervisores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_supervisores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,adm_empleados_Id")] oc_supervisores oc_supervisores)
        {
            if (ModelState.IsValid)
            {
                db.oc_supervisores.Add(oc_supervisores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_supervisores);
        }

        // GET: oc_supervisores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_supervisores oc_supervisores = db.oc_supervisores.Find(id);
            if (oc_supervisores == null)
            {
                return HttpNotFound();
            }
            return View(oc_supervisores);
        }

        // POST: oc_supervisores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,adm_empleados_Id")] oc_supervisores oc_supervisores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_supervisores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_supervisores);
        }

        // GET: oc_supervisores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_supervisores oc_supervisores = db.oc_supervisores.Find(id);
            if (oc_supervisores == null)
            {
                return HttpNotFound();
            }
            return View(oc_supervisores);
        }

        // POST: oc_supervisores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_supervisores oc_supervisores = db.oc_supervisores.Find(id);
            db.oc_supervisores.Remove(oc_supervisores);
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
