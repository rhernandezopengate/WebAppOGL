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
    public class adm_bodegaController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_bodega
        public ActionResult Index()
        {
            return View(db.adm_bodega.ToList());
        }

        // GET: adm_bodega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_bodega adm_bodega = db.adm_bodega.Find(id);
            if (adm_bodega == null)
            {
                return HttpNotFound();
            }
            return View(adm_bodega);
        }

        // GET: adm_bodega/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_bodega/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] adm_bodega adm_bodega)
        {
            if (ModelState.IsValid)
            {
                db.adm_bodega.Add(adm_bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_bodega);
        }

        // GET: adm_bodega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_bodega adm_bodega = db.adm_bodega.Find(id);
            if (adm_bodega == null)
            {
                return HttpNotFound();
            }
            return View(adm_bodega);
        }

        // POST: adm_bodega/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] adm_bodega adm_bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_bodega);
        }

        // GET: adm_bodega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_bodega adm_bodega = db.adm_bodega.Find(id);
            if (adm_bodega == null)
            {
                return HttpNotFound();
            }
            return View(adm_bodega);
        }

        // POST: adm_bodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_bodega adm_bodega = db.adm_bodega.Find(id);
            db.adm_bodega.Remove(adm_bodega);
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
