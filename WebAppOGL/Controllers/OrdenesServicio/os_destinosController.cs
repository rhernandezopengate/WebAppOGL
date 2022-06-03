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
    public class os_destinosController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_destinos
        public ActionResult Index()
        {
            return View(db.os_destinos.ToList());
        }

        // GET: os_destinos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_destinos os_destinos = db.os_destinos.Find(id);
            if (os_destinos == null)
            {
                return HttpNotFound();
            }
            return View(os_destinos);
        }

        // GET: os_destinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: os_destinos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] os_destinos os_destinos)
        {
            if (ModelState.IsValid)
            {
                db.os_destinos.Add(os_destinos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(os_destinos);
        }

        // GET: os_destinos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_destinos os_destinos = db.os_destinos.Find(id);
            if (os_destinos == null)
            {
                return HttpNotFound();
            }
            return View(os_destinos);
        }

        // POST: os_destinos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] os_destinos os_destinos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(os_destinos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(os_destinos);
        }

        // GET: os_destinos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_destinos os_destinos = db.os_destinos.Find(id);
            if (os_destinos == null)
            {
                return HttpNotFound();
            }
            return View(os_destinos);
        }

        // POST: os_destinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_destinos os_destinos = db.os_destinos.Find(id);
            db.os_destinos.Remove(os_destinos);
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
