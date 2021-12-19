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
    public class sis_tipoequiposController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_tipoequipos
        public ActionResult Index()
        {
            return View(db.sis_tipoequipos.ToList());
        }

        // GET: sis_tipoequipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoequipos sis_tipoequipos = db.sis_tipoequipos.Find(id);
            if (sis_tipoequipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoequipos);
        }

        // GET: sis_tipoequipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_tipoequipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_tipoequipos sis_tipoequipos)
        {
            if (ModelState.IsValid)
            {
                db.sis_tipoequipos.Add(sis_tipoequipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_tipoequipos);
        }

        // GET: sis_tipoequipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoequipos sis_tipoequipos = db.sis_tipoequipos.Find(id);
            if (sis_tipoequipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoequipos);
        }

        // POST: sis_tipoequipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_tipoequipos sis_tipoequipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_tipoequipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_tipoequipos);
        }

        // GET: sis_tipoequipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoequipos sis_tipoequipos = db.sis_tipoequipos.Find(id);
            if (sis_tipoequipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoequipos);
        }

        // POST: sis_tipoequipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_tipoequipos sis_tipoequipos = db.sis_tipoequipos.Find(id);
            db.sis_tipoequipos.Remove(sis_tipoequipos);
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
