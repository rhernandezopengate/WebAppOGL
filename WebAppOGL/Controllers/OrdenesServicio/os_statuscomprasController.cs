using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;
using WebAppOGL.Entities.OrdenesServicio;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_statuscomprasController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_statuscompras
        public ActionResult Index()
        {
            return View(db.os_statuscompras.ToList());
        }

        // GET: os_statuscompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_statuscompras os_statuscompras = db.os_statuscompras.Find(id);
            if (os_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(os_statuscompras);
        }

        // GET: os_statuscompras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: os_statuscompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] os_statuscompras os_statuscompras)
        {
            if (ModelState.IsValid)
            {
                db.os_statuscompras.Add(os_statuscompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(os_statuscompras);
        }

        // GET: os_statuscompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_statuscompras os_statuscompras = db.os_statuscompras.Find(id);
            if (os_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(os_statuscompras);
        }

        // POST: os_statuscompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] os_statuscompras os_statuscompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(os_statuscompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(os_statuscompras);
        }

        // GET: os_statuscompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_statuscompras os_statuscompras = db.os_statuscompras.Find(id);
            if (os_statuscompras == null)
            {
                return HttpNotFound();
            }
            return View(os_statuscompras);
        }

        // POST: os_statuscompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_statuscompras os_statuscompras = db.os_statuscompras.Find(id);
            db.os_statuscompras.Remove(os_statuscompras);
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
