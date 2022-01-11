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
    public class adm_puestosController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_puestos
        public ActionResult Index()
        {
            return View(db.adm_puestos.ToList());
        }

        // GET: adm_puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_puestos adm_puestos = db.adm_puestos.Find(id);
            if (adm_puestos == null)
            {
                return HttpNotFound();
            }
            return View(adm_puestos);
        }

        // GET: adm_puestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_puestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] adm_puestos adm_puestos)
        {
            if (ModelState.IsValid)
            {
                db.adm_puestos.Add(adm_puestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_puestos);
        }

        // GET: adm_puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_puestos adm_puestos = db.adm_puestos.Find(id);
            if (adm_puestos == null)
            {
                return HttpNotFound();
            }
            return View(adm_puestos);
        }

        // POST: adm_puestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] adm_puestos adm_puestos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_puestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_puestos);
        }

        // GET: adm_puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_puestos adm_puestos = db.adm_puestos.Find(id);
            if (adm_puestos == null)
            {
                return HttpNotFound();
            }
            return View(adm_puestos);
        }

        // POST: adm_puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_puestos adm_puestos = db.adm_puestos.Find(id);
            db.adm_puestos.Remove(adm_puestos);
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
