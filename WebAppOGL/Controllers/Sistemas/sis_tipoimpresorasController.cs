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
    public class sis_tipoimpresorasController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_tipoimpresoras
        public ActionResult Index()
        {
            return View(db.sis_tipoimpresoras.ToList());
        }

        // GET: sis_tipoimpresoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoimpresoras sis_tipoimpresoras = db.sis_tipoimpresoras.Find(id);
            if (sis_tipoimpresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoimpresoras);
        }

        // GET: sis_tipoimpresoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_tipoimpresoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_tipoimpresoras sis_tipoimpresoras)
        {
            if (ModelState.IsValid)
            {
                db.sis_tipoimpresoras.Add(sis_tipoimpresoras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_tipoimpresoras);
        }

        // GET: sis_tipoimpresoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoimpresoras sis_tipoimpresoras = db.sis_tipoimpresoras.Find(id);
            if (sis_tipoimpresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoimpresoras);
        }

        // POST: sis_tipoimpresoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_tipoimpresoras sis_tipoimpresoras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_tipoimpresoras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_tipoimpresoras);
        }

        // GET: sis_tipoimpresoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_tipoimpresoras sis_tipoimpresoras = db.sis_tipoimpresoras.Find(id);
            if (sis_tipoimpresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_tipoimpresoras);
        }

        // POST: sis_tipoimpresoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_tipoimpresoras sis_tipoimpresoras = db.sis_tipoimpresoras.Find(id);
            db.sis_tipoimpresoras.Remove(sis_tipoimpresoras);
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
