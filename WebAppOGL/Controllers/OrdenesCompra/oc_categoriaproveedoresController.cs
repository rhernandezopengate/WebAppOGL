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
    public class oc_categoriaproveedoresController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_categoriaproveedores
        public ActionResult Index()
        {
            return View(db.oc_categoriaproveedores.ToList());
        }

        // GET: oc_categoriaproveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoriaproveedores oc_categoriaproveedores = db.oc_categoriaproveedores.Find(id);
            if (oc_categoriaproveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoriaproveedores);
        }

        // GET: oc_categoriaproveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_categoriaproveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_categoriaproveedores oc_categoriaproveedores)
        {
            if (ModelState.IsValid)
            {
                db.oc_categoriaproveedores.Add(oc_categoriaproveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_categoriaproveedores);
        }

        // GET: oc_categoriaproveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoriaproveedores oc_categoriaproveedores = db.oc_categoriaproveedores.Find(id);
            if (oc_categoriaproveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoriaproveedores);
        }

        // POST: oc_categoriaproveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_categoriaproveedores oc_categoriaproveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_categoriaproveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_categoriaproveedores);
        }

        // GET: oc_categoriaproveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoriaproveedores oc_categoriaproveedores = db.oc_categoriaproveedores.Find(id);
            if (oc_categoriaproveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoriaproveedores);
        }

        // POST: oc_categoriaproveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_categoriaproveedores oc_categoriaproveedores = db.oc_categoriaproveedores.Find(id);
            db.oc_categoriaproveedores.Remove(oc_categoriaproveedores);
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
