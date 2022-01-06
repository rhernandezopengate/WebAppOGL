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
    public class adm_areaController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_area
        public ActionResult Index()
        {
            return View(db.adm_area.ToList());
        }

        // GET: adm_area/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_area adm_area = db.adm_area.Find(id);
            if (adm_area == null)
            {
                return HttpNotFound();
            }
            return View(adm_area);
        }

        // GET: adm_area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_area/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] adm_area adm_area)
        {
            if (ModelState.IsValid)
            {
                db.adm_area.Add(adm_area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_area);
        }

        // GET: adm_area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_area adm_area = db.adm_area.Find(id);
            if (adm_area == null)
            {
                return HttpNotFound();
            }
            return View(adm_area);
        }

        // POST: adm_area/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] adm_area adm_area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_area);
        }

        // GET: adm_area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_area adm_area = db.adm_area.Find(id);
            if (adm_area == null)
            {
                return HttpNotFound();
            }
            return View(adm_area);
        }

        // POST: adm_area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_area adm_area = db.adm_area.Find(id);
            db.adm_area.Remove(adm_area);
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
