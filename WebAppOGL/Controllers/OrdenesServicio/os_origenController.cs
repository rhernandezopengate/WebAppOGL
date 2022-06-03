using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesServicio;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_origenController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_origen
        public ActionResult Index()
        {
            return View(db.os_origen.ToList());
        }

        // GET: os_origen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_origen os_origen = db.os_origen.Find(id);
            if (os_origen == null)
            {
                return HttpNotFound();
            }
            return View(os_origen);
        }

        // GET: os_origen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: os_origen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] os_origen os_origen)
        {
            if (ModelState.IsValid)
            {
                db.os_origen.Add(os_origen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(os_origen);
        }

        // GET: os_origen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_origen os_origen = db.os_origen.Find(id);
            if (os_origen == null)
            {
                return HttpNotFound();
            }
            return View(os_origen);
        }

        // POST: os_origen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] os_origen os_origen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(os_origen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(os_origen);
        }

        // GET: os_origen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_origen os_origen = db.os_origen.Find(id);
            if (os_origen == null)
            {
                return HttpNotFound();
            }
            return View(os_origen);
        }

        // POST: os_origen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_origen os_origen = db.os_origen.Find(id);
            db.os_origen.Remove(os_origen);
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
