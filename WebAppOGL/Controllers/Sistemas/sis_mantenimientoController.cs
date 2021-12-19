using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Sistemas;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_mantenimientoController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_mantenimiento
        public ActionResult Index()
        {
            return View(db.sis_mantenimiento.ToList());
        }

        // GET: sis_mantenimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_mantenimiento sis_mantenimiento = db.sis_mantenimiento.Find(id);
            if (sis_mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(sis_mantenimiento);
        }

        // GET: sis_mantenimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_mantenimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_mantenimiento sis_mantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.sis_mantenimiento.Add(sis_mantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_mantenimiento);
        }

        // GET: sis_mantenimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_mantenimiento sis_mantenimiento = db.sis_mantenimiento.Find(id);
            if (sis_mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(sis_mantenimiento);
        }

        // POST: sis_mantenimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_mantenimiento sis_mantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_mantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_mantenimiento);
        }

        // GET: sis_mantenimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_mantenimiento sis_mantenimiento = db.sis_mantenimiento.Find(id);
            if (sis_mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(sis_mantenimiento);
        }

        // POST: sis_mantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_mantenimiento sis_mantenimiento = db.sis_mantenimiento.Find(id);
            db.sis_mantenimiento.Remove(sis_mantenimiento);
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
