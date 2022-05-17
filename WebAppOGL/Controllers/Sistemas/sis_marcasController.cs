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
    public class sis_marcasController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_marcas
        [Authorize(Roles = "sistemas")]
        public ActionResult Index()
        {
            return View(db.sis_marcas.ToList());
        }

        // GET: sis_marcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_marcas sis_marcas = db.sis_marcas.Find(id);
            if (sis_marcas == null)
            {
                return HttpNotFound();
            }
            return View(sis_marcas);
        }

        // GET: sis_marcas/Create
        [Authorize(Roles = "sistemas")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_marcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_marcas sis_marcas)
        {
            if (ModelState.IsValid)
            {
                sis_marcas marca = new sis_marcas();
                marca.Descripcion = sis_marcas.Descripcion.ToUpper();
                
                db.sis_marcas.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_marcas);
        }

        // GET: sis_marcas/Edit/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_marcas sis_marcas = db.sis_marcas.Find(id);
            if (sis_marcas == null)
            {
                return HttpNotFound();
            }
            return View(sis_marcas);
        }

        // POST: sis_marcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_marcas sis_marcas)
        {
            if (ModelState.IsValid)
            {
                sis_marcas marca = db.sis_marcas.Find(sis_marcas.Id);
                marca.Descripcion = sis_marcas.Descripcion.ToUpper();

                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_marcas);
        }

        // GET: sis_marcas/Delete/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_marcas sis_marcas = db.sis_marcas.Find(id);
            if (sis_marcas == null)
            {
                return HttpNotFound();
            }
            return View(sis_marcas);
        }

        // POST: sis_marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_marcas sis_marcas = db.sis_marcas.Find(id);
            db.sis_marcas.Remove(sis_marcas);
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
