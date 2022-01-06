using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;

namespace WebAppOGL.Controllers.Administracion
{
    public class adm_sucursalesController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_sucursales
        public ActionResult Index()
        {
            return View(db.adm_sucursales.ToList());
        }

        // GET: adm_sucursales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_sucursales adm_sucursales = db.adm_sucursales.Find(id);
            if (adm_sucursales == null)
            {
                return HttpNotFound();
            }
            return View(adm_sucursales);
        }

        // GET: adm_sucursales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_sucursales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] adm_sucursales adm_sucursales)
        {
            if (ModelState.IsValid)
            {
                db.adm_sucursales.Add(adm_sucursales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_sucursales);
        }

        // GET: adm_sucursales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_sucursales adm_sucursales = db.adm_sucursales.Find(id);
            if (adm_sucursales == null)
            {
                return HttpNotFound();
            }
            return View(adm_sucursales);
        }

        // POST: adm_sucursales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] adm_sucursales adm_sucursales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_sucursales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_sucursales);
        }

        // GET: adm_sucursales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_sucursales adm_sucursales = db.adm_sucursales.Find(id);
            if (adm_sucursales == null)
            {
                return HttpNotFound();
            }
            return View(adm_sucursales);
        }

        // POST: adm_sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_sucursales adm_sucursales = db.adm_sucursales.Find(id);
            db.adm_sucursales.Remove(adm_sucursales);
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
