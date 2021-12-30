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
    public class sis_celularesController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_celulares
        public ActionResult Index()
        {
            var sis_celulares = db.sis_celulares.Include(s => s.sis_estatusequipo).Include(s => s.sis_mantenimiento).Include(s => s.sis_marcas);
            return View(sis_celulares.ToList());
        }

        // GET: sis_celulares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_celulares);
        }

        // GET: sis_celulares/Create
        public ActionResult Create()
        {
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion");
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            return View();
        }

        // POST: sis_celulares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,Modelo,Numero_Producto,IMEI,Color,Cargador,OS,Almacenamiento,RAM,Procesador,Pantalla,Camara_Frontal,Camara_Trasera,sis_mantenimiento_Id,sis_estatusequipo_Id,sis_marcas_Id")] sis_celulares sis_celulares)
        {
            if (ModelState.IsValid)
            {
                db.sis_celulares.Add(sis_celulares);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return View(sis_celulares);
        }

        // GET: sis_celulares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return View(sis_celulares);
        }

        // POST: sis_celulares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,Modelo,Numero_Producto,IMEI,Color,Cargador,OS,Almacenamiento,RAM,Procesador,Pantalla,Camara_Frontal,Camara_Trasera,sis_mantenimiento_Id,sis_estatusequipo_Id,sis_marcas_Id")] sis_celulares sis_celulares)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_celulares).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return View(sis_celulares);
        }

        // GET: sis_celulares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_celulares);
        }

        // POST: sis_celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            db.sis_celulares.Remove(sis_celulares);
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
