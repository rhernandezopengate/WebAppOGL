﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;
using WebAppOGL.Entities.Sistemas;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_impresorasController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities1 dbadmin = new db_a3f19c_administracionEntities1();

        // GET: sis_impresoras
        public ActionResult Index()
        {
            var sis_impresoras = db.sis_impresoras.Include(s => s.sis_estatusequipo).Include(s => s.sis_marcas).Include(s => s.sis_statusfiscal);
            return View(sis_impresoras);
        }

        // GET: sis_impresoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_impresoras sis_impresoras = db.sis_impresoras.Find(id);
            
            if (sis_impresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_impresoras);
        }

        // GET: sis_impresoras/Create
        public ActionResult Create()
        {
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            ViewBag.sis_statusfiscal_Id = new SelectList(db.sis_statusfiscal, "Id", "Descripcion");
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");               
            ViewBag.sis_tipoimpresoras_Id = new SelectList(db.sis_tipoimpresoras, "Id", "Descripcion");

            return View();
        }

        // POST: sis_impresoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Modelo,Modelo_Toner,Numero_Serie,Numero_Parte,MAC_Ethernet,Hostname,sis_marcas_Id,sis_statusfiscal_Id,sis_estatusequipo_Id,sis_tipoimpresoras_Id")] sis_impresoras sis_impresoras)
        {
            if (ModelState.IsValid)
            {
                db.sis_impresoras.Add(sis_impresoras);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            ViewBag.sis_statusfiscal_Id = new SelectList(db.sis_statusfiscal, "Id", "Descripcion");
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");
            ViewBag.sis_tipoimpresoras_Id = new SelectList(db.sis_tipoimpresoras, "Id", "Descripcion");

            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_impresoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_impresoras sis_impresoras = db.sis_impresoras.Find(id);
            if (sis_impresoras == null)
            {
                return HttpNotFound();
            }

            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_impresoras.sis_estatusequipo_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_impresoras.sis_marcas_Id);
            ViewBag.sis_statusfiscal_Id = new SelectList(db.sis_statusfiscal, "Id", "Descripcion", sis_impresoras.sis_statusfiscal_Id);
            ViewBag.sis_tipoimpresoras_Id = new SelectList(db.sis_tipoimpresoras, "Id", "Descripcion", sis_impresoras.sis_tipoimpresoras_Id);
            
            return View(sis_impresoras);
        }

        // POST: sis_impresoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Modelo,Modelo_Toner,Numero_Serie,Numero_Parte,MAC_Ethernet,Hostname,sis_marcas_Id,adm_sucursales_Id,sis_statusfiscal_Id,sis_estatusequipo_Id,sis_tipoimpresoras_Id,adm_area_Id")] sis_impresoras sis_impresoras)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(sis_impresoras).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_impresoras.sis_estatusequipo_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_impresoras.sis_marcas_Id);
            ViewBag.sis_statusfiscal_Id = new SelectList(db.sis_statusfiscal, "Id", "Descripcion", sis_impresoras.sis_statusfiscal_Id);
            ViewBag.sis_tipoimpresoras_Id = new SelectList(db.sis_impresoras, "Id", "Descripcion", sis_impresoras.sis_tipoimpresoras_Id);

            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_impresoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_impresoras sis_impresoras = db.sis_impresoras.Find(id);
            if (sis_impresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_impresoras);
        }

        // POST: sis_impresoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_impresoras sis_impresoras = db.sis_impresoras.Find(id);
            db.sis_impresoras.Remove(sis_impresoras);
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
