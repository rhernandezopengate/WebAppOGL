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
    public class oc_statussupervisorController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_statussupervisor
        public ActionResult Index()
        {
            return View(db.oc_statussupervisor.ToList());
        }

        // GET: oc_statussupervisor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statussupervisor oc_statussupervisor = db.oc_statussupervisor.Find(id);
            if (oc_statussupervisor == null)
            {
                return HttpNotFound();
            }
            return View(oc_statussupervisor);
        }

        // GET: oc_statussupervisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_statussupervisor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_statussupervisor oc_statussupervisor)
        {
            if (ModelState.IsValid)
            {
                db.oc_statussupervisor.Add(oc_statussupervisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_statussupervisor);
        }

        // GET: oc_statussupervisor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statussupervisor oc_statussupervisor = db.oc_statussupervisor.Find(id);
            if (oc_statussupervisor == null)
            {
                return HttpNotFound();
            }
            return View(oc_statussupervisor);
        }

        // POST: oc_statussupervisor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_statussupervisor oc_statussupervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_statussupervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_statussupervisor);
        }

        // GET: oc_statussupervisor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_statussupervisor oc_statussupervisor = db.oc_statussupervisor.Find(id);
            if (oc_statussupervisor == null)
            {
                return HttpNotFound();
            }
            return View(oc_statussupervisor);
        }

        // POST: oc_statussupervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_statussupervisor oc_statussupervisor = db.oc_statussupervisor.Find(id);
            db.oc_statussupervisor.Remove(oc_statussupervisor);
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
