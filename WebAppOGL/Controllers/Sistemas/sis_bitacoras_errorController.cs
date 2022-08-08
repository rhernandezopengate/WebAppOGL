using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Bitacora;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_bitacoras_errorController : Controller
    {
        private db_a3f19c_administracionEntities4 db = new db_a3f19c_administracionEntities4();

        // GET: sis_bitacoras_error
        public ActionResult Index()
        {
            return View(db.sis_bitacoras_error.ToList());
        }

        // GET: sis_bitacoras_error/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_bitacoras_error sis_bitacoras_error = db.sis_bitacoras_error.Find(id);
            if (sis_bitacoras_error == null)
            {
                return HttpNotFound();
            }
            return View(sis_bitacoras_error);
        }

        // GET: sis_bitacoras_error/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_bitacoras_error/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Controlador,Vista,Mensaje")] sis_bitacoras_error sis_bitacoras_error)
        {
            if (ModelState.IsValid)
            {
                db.sis_bitacoras_error.Add(sis_bitacoras_error);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_bitacoras_error);
        }

        // GET: sis_bitacoras_error/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_bitacoras_error sis_bitacoras_error = db.sis_bitacoras_error.Find(id);
            if (sis_bitacoras_error == null)
            {
                return HttpNotFound();
            }
            return View(sis_bitacoras_error);
        }

        // POST: sis_bitacoras_error/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Controlador,Vista,Mensaje")] sis_bitacoras_error sis_bitacoras_error)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_bitacoras_error).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_bitacoras_error);
        }

        // GET: sis_bitacoras_error/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_bitacoras_error sis_bitacoras_error = db.sis_bitacoras_error.Find(id);
            if (sis_bitacoras_error == null)
            {
                return HttpNotFound();
            }
            return View(sis_bitacoras_error);
        }

        // POST: sis_bitacoras_error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_bitacoras_error sis_bitacoras_error = db.sis_bitacoras_error.Find(id);
            db.sis_bitacoras_error.Remove(sis_bitacoras_error);
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
