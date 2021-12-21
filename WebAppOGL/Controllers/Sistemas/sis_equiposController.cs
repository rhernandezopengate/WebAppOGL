using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Sistemas;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_equiposController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_equipos
        public ActionResult Index()
        {
            var sis_equipos = db.sis_equipos.Include(s => s.sis_mantenimiento).Include(s => s.sis_marcas).Include(s => s.sis_tipoequipos);
            return View(sis_equipos.ToList());
        }

        // GET: sis_equipos/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_equipos);
        }

        // GET: sis_equipos/Create
        public ActionResult Create()
        {
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion");
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion");
            return View();
        }

        // POST: sis_equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_Alta,Fecha_Compra,Modelo,Numero_Serie,Numero_Parte,Nombre_Equipo,MAC_Ethernet,MAC_WiFi,CPU,RAM,STORAGE,sis_marcas_Id,sis_tipoequipos_Id,sis_mantenimiento_Id")] sis_equipos sis_equipos)
        {
            if (ModelState.IsValid)
            {
                sis_equipos.Fecha_Alta = DateTime.Now;
                db.sis_equipos.Add(sis_equipos);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            return View(sis_equipos);
        }

        // POST: sis_equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_Alta,Fecha_Compra,Modelo,Numero_Serie,Numero_Parte,Nombre_Equipo,MAC_Ethernet,MAC_WiFi,CPU,RAM,STORAGE,sis_marcas_Id,sis_tipoequipos_Id,sis_mantenimiento_Id")] sis_equipos sis_equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_equipos).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_equipos);
        }

        // POST: sis_equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            db.sis_equipos.Remove(sis_equipos);
            db.SaveChanges();
            return Json("Correcto", JsonRequestBehavior.AllowGet);
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
