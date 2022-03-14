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
    public class oc_solicitantesController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_solicitantes
        public ActionResult Index()
        {
            var oc_solicitantes = db.oc_solicitantes.Include(o => o.oc_supervisores);
            return View(oc_solicitantes.ToList());
        }

        // GET: oc_solicitantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_solicitantes oc_solicitantes = db.oc_solicitantes.Find(id);
            if (oc_solicitantes == null)
            {
                return HttpNotFound();
            }
            return View(oc_solicitantes);
        }

        // GET: oc_solicitantes/Create
        public ActionResult Create()
        {
            ViewBag.oc_supervisores_Id = new SelectList(db.oc_supervisores, "Id", "NombreCompleto");
            return View();
        }

        // POST: oc_solicitantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,adm_empleados_Id,oc_supervisores_Id")] oc_solicitantes oc_solicitantes)
        {
            if (ModelState.IsValid)
            {
                db.oc_solicitantes.Add(oc_solicitantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.oc_supervisores_Id = new SelectList(db.oc_supervisores, "Id", "NombreCompleto", oc_solicitantes.oc_supervisores_Id);
            return View(oc_solicitantes);
        }

        // GET: oc_solicitantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_solicitantes oc_solicitantes = db.oc_solicitantes.Find(id);
            if (oc_solicitantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.oc_supervisores_Id = new SelectList(db.oc_supervisores, "Id", "NombreCompleto", oc_solicitantes.oc_supervisores_Id);
            return View(oc_solicitantes);
        }

        // POST: oc_solicitantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,adm_empleados_Id,oc_supervisores_Id")] oc_solicitantes oc_solicitantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_solicitantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oc_supervisores_Id = new SelectList(db.oc_supervisores, "Id", "NombreCompleto", oc_solicitantes.oc_supervisores_Id);
            return View(oc_solicitantes);
        }

        // GET: oc_solicitantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_solicitantes oc_solicitantes = db.oc_solicitantes.Find(id);
            if (oc_solicitantes == null)
            {
                return HttpNotFound();
            }
            return View(oc_solicitantes);
        }

        // POST: oc_solicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_solicitantes oc_solicitantes = db.oc_solicitantes.Find(id);
            db.oc_solicitantes.Remove(oc_solicitantes);
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
