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
    public class adm_cuentasController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_cuentas
        public ActionResult Index()
        {
            return View(db.adm_cuentas.ToList());
        }

        // GET: adm_cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_cuentas adm_cuentas = db.adm_cuentas.Find(id);
            if (adm_cuentas == null)
            {
                return HttpNotFound();
            }
            return View(adm_cuentas);
        }

        // GET: adm_cuentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] adm_cuentas adm_cuentas)
        {
            if (ModelState.IsValid)
            {
                db.adm_cuentas.Add(adm_cuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_cuentas);
        }

        // GET: adm_cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_cuentas adm_cuentas = db.adm_cuentas.Find(id);
            if (adm_cuentas == null)
            {
                return HttpNotFound();
            }
            return View(adm_cuentas);
        }

        // POST: adm_cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] adm_cuentas adm_cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_cuentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_cuentas);
        }

        // GET: adm_cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_cuentas adm_cuentas = db.adm_cuentas.Find(id);
            if (adm_cuentas == null)
            {
                return HttpNotFound();
            }
            return View(adm_cuentas);
        }

        // POST: adm_cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_cuentas adm_cuentas = db.adm_cuentas.Find(id);
            db.adm_cuentas.Remove(adm_cuentas);
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
