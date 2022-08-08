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
    public class oc_detproveedorcategoriaController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_detproveedorcategoria
        public ActionResult Index()
        {
            var oc_detproveedorcategoria = db.oc_detproveedorcategoria.Include(o => o.oc_categoriaproveedores).Include(o => o.oc_proveedores);
            return View(oc_detproveedorcategoria.ToList());
        }

        // GET: oc_detproveedorcategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_detproveedorcategoria oc_detproveedorcategoria = db.oc_detproveedorcategoria.Find(id);
            if (oc_detproveedorcategoria == null)
            {
                return HttpNotFound();
            }
            return View(oc_detproveedorcategoria);
        }

        // GET: oc_detproveedorcategoria/Create
        public ActionResult Create()
        {
            ViewBag.oc_categoriaproveedores_Id = new SelectList(db.oc_categoriaproveedores, "Id", "Descripcion");
            ViewBag.oc_proveedores_Id = new SelectList(db.oc_proveedores.OrderBy(x => x.NombreComercial), "Id", "NombreComercial");
            return View();
        }

        // POST: oc_detproveedorcategoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,oc_proveedores_Id,oc_categoriaproveedores_Id")] oc_detproveedorcategoria oc_detproveedorcategoria)
        {
            if (ModelState.IsValid)
            {
                db.oc_detproveedorcategoria.Add(oc_detproveedorcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.oc_categoriaproveedores_Id = new SelectList(db.oc_categoriaproveedores, "Id", "Descripcion", oc_detproveedorcategoria.oc_categoriaproveedores_Id);
            ViewBag.oc_proveedores_Id = new SelectList(db.oc_proveedores, "Id", "RFC", oc_detproveedorcategoria.oc_proveedores_Id);
            return View(oc_detproveedorcategoria);
        }

        // GET: oc_detproveedorcategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_detproveedorcategoria oc_detproveedorcategoria = db.oc_detproveedorcategoria.Find(id);
            if (oc_detproveedorcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.oc_categoriaproveedores_Id = new SelectList(db.oc_categoriaproveedores, "Id", "Descripcion", oc_detproveedorcategoria.oc_categoriaproveedores_Id);
            ViewBag.oc_proveedores_Id = new SelectList(db.oc_proveedores, "Id", "RFC", oc_detproveedorcategoria.oc_proveedores_Id);
            return View(oc_detproveedorcategoria);
        }

        // POST: oc_detproveedorcategoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,oc_proveedores_Id,oc_categoriaproveedores_Id")] oc_detproveedorcategoria oc_detproveedorcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_detproveedorcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oc_categoriaproveedores_Id = new SelectList(db.oc_categoriaproveedores, "Id", "Descripcion", oc_detproveedorcategoria.oc_categoriaproveedores_Id);
            ViewBag.oc_proveedores_Id = new SelectList(db.oc_proveedores, "Id", "RFC", oc_detproveedorcategoria.oc_proveedores_Id);
            return View(oc_detproveedorcategoria);
        }

        // GET: oc_detproveedorcategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_detproveedorcategoria oc_detproveedorcategoria = db.oc_detproveedorcategoria.Find(id);
            if (oc_detproveedorcategoria == null)
            {
                return HttpNotFound();
            }
            return View(oc_detproveedorcategoria);
        }

        // POST: oc_detproveedorcategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_detproveedorcategoria oc_detproveedorcategoria = db.oc_detproveedorcategoria.Find(id);
            db.oc_detproveedorcategoria.Remove(oc_detproveedorcategoria);
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
