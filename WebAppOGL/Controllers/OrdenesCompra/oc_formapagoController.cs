using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    public class oc_formapagoController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_formapago
        public ActionResult Index()
        {
            return View(db.oc_formapago.ToList());
        }

        // GET: oc_formapago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_formapago oc_formapago = db.oc_formapago.Find(id);
            if (oc_formapago == null)
            {
                return HttpNotFound();
            }
            return View(oc_formapago);
        }

        // GET: oc_formapago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_formapago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_formapago oc_formapago)
        {
            if (ModelState.IsValid)
            {
                db.oc_formapago.Add(oc_formapago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_formapago);
        }

        // GET: oc_formapago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_formapago oc_formapago = db.oc_formapago.Find(id);
            if (oc_formapago == null)
            {
                return HttpNotFound();
            }
            return View(oc_formapago);
        }

        // POST: oc_formapago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_formapago oc_formapago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_formapago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_formapago);
        }

        // GET: oc_formapago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_formapago oc_formapago = db.oc_formapago.Find(id);
            if (oc_formapago == null)
            {
                return HttpNotFound();
            }
            return View(oc_formapago);
        }

        // POST: oc_formapago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_formapago oc_formapago = db.oc_formapago.Find(id);
            db.oc_formapago.Remove(oc_formapago);
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
