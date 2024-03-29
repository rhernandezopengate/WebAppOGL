﻿using System;
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
    public class oc_productosController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_productos
        [Authorize]
        public ActionResult Index()
        {
            var oc_productos = db.oc_productos.Include(o => o.UoM);
            return View(oc_productos.ToList());
        }

        public JsonResult AutoComplete(string prefix)
        {
            var proveedores = (from p in db.oc_productos
                               where p.Descripcion.Contains(prefix)
                               orderby p.Descripcion ascending
                               select new
                               {
                                   label = p.Descripcion,
                                   val = p.Id
                               }).ToList();

            return Json(proveedores);
        }

        // GET: oc_productos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_productos oc_productos = db.oc_productos.Find(id);
            if (oc_productos == null)
            {
                return HttpNotFound();
            }
            return View(oc_productos);
        }

        // GET: oc_productos/Create
        [Authorize]
        public ActionResult CreateParcial()
        {
            ViewBag.UoM_Id = new SelectList(db.UoM.OrderBy(x => x.Descripcion), "Id", "Descripcion");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParcial([Bind(Include = "Id,Descripcion,UoM_Id")] oc_productos oc_productos)
        {
            if (ModelState.IsValid)
            {
                oc_productos prod = new oc_productos();
                prod.Descripcion = oc_productos.Descripcion.ToUpper().Trim();
                prod.UoM_Id = oc_productos.UoM_Id;

                db.oc_productos.Add(prod);
                db.SaveChanges();
                return Json(new { respuesta = "Correcto" }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.UoM_Id = new SelectList(db.UoM, "Id", "Descripcion", oc_productos.UoM_Id);
            return Json(new { respuesta = "Error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.UoM_Id = new SelectList(db.UoM.OrderBy(x => x.Descripcion), "Id", "Descripcion");
            return View();
        }

        // POST: oc_productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,UoM_Id")] oc_productos oc_productos)
        {
            if (ModelState.IsValid)
            {
                oc_productos prod = new oc_productos();
                prod.Descripcion = oc_productos.Descripcion.ToUpper().Trim();
                prod.UoM_Id = oc_productos.UoM_Id;

                db.oc_productos.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UoM_Id = new SelectList(db.UoM, "Id", "Descripcion", oc_productos.UoM_Id);
            return View(oc_productos);
        }

        // GET: oc_productos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_productos oc_productos = db.oc_productos.Find(id);
            if (oc_productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.UoM_Id = new SelectList(db.UoM, "Id", "Descripcion", oc_productos.UoM_Id);
            return View(oc_productos);
        }

        // POST: oc_productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,UoM_Id")] oc_productos oc_productos)
        {
            if (ModelState.IsValid)
            {
                oc_productos prod = db.oc_productos.Find(oc_productos.Id);
                prod.Descripcion = oc_productos.Descripcion.ToUpper();
                prod.UoM_Id = oc_productos.UoM_Id;

                db.Entry(prod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UoM_Id = new SelectList(db.UoM, "Id", "Descripcion", oc_productos.UoM_Id);
            return View(oc_productos);
        }

        // GET: oc_productos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_productos oc_productos = db.oc_productos.Find(id);
            if (oc_productos == null)
            {
                return HttpNotFound();
            }
            return View(oc_productos);
        }

        // POST: oc_productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_productos oc_productos = db.oc_productos.Find(id);
            db.oc_productos.Remove(oc_productos);
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
