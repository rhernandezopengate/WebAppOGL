using System;
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
    public class sis_asignacion_impresorasController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities1 dbAdmin = new db_a3f19c_administracionEntities1();

        // GET: sis_asignacion_impresoras
        public ActionResult Index()
        {
            var sis_asignacion_impresoras = db.sis_asignacion_impresoras.Include(s => s.sis_impresoras).Include(s => s.sis_mantenimiento);
            return View(sis_asignacion_impresoras.ToList());
        }

        // GET: sis_asignacion_impresoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_impresoras sis_asignacion_impresoras = db.sis_asignacion_impresoras.Find(id);
            if (sis_asignacion_impresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_impresoras);
        }

        // GET: sis_asignacion_impresoras/Create
        public ActionResult Create()
        {
            ViewBag.sis_impresoras_Id = new SelectList(db.sis_impresoras, "Id", "Modelo");
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion");
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion");
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion");
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion");

            return View();
        }

        // POST: sis_asignacion_impresoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAsignacion,sis_impresoras_Id,adm_area_Id,adm_sucursales_Id,sis_mantenimiento_Id,adm_cuentas_Id")] sis_asignacion_impresoras sis_asignacion_impresoras)
        {
            if (ModelState.IsValid)
            {
                db.sis_asignacion_impresoras.Add(sis_asignacion_impresoras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sis_impresoras_Id = new SelectList(db.sis_impresoras, "Id", "Modelo", sis_asignacion_impresoras.sis_impresoras_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_asignacion_impresoras.sis_mantenimiento_Id);
            return View(sis_asignacion_impresoras);
        }

        // GET: sis_asignacion_impresoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_impresoras sis_asignacion_impresoras = db.sis_asignacion_impresoras.Find(id);
            if (sis_asignacion_impresoras == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_impresoras_Id = new SelectList(db.sis_impresoras, "Id", "Modelo", sis_asignacion_impresoras.sis_impresoras_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_asignacion_impresoras.sis_mantenimiento_Id);
            return View(sis_asignacion_impresoras);
        }

        // POST: sis_asignacion_impresoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAsignacion,sis_impresoras_Id,adm_area_Id,adm_sucursales_Id,sis_mantenimiento_Id,adm_cuentas_Id")] sis_asignacion_impresoras sis_asignacion_impresoras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_asignacion_impresoras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sis_impresoras_Id = new SelectList(db.sis_impresoras, "Id", "Modelo", sis_asignacion_impresoras.sis_impresoras_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_asignacion_impresoras.sis_mantenimiento_Id);
            return View(sis_asignacion_impresoras);
        }

        // GET: sis_asignacion_impresoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_impresoras sis_asignacion_impresoras = db.sis_asignacion_impresoras.Find(id);
            if (sis_asignacion_impresoras == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_impresoras);
        }

        // POST: sis_asignacion_impresoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_asignacion_impresoras sis_asignacion_impresoras = db.sis_asignacion_impresoras.Find(id);
            db.sis_asignacion_impresoras.Remove(sis_asignacion_impresoras);
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
