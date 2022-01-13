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
    public class sis_asignacion_celularesController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        public ActionResult AsignacionCelularesParcial() 
        {
            return View();
        }


        // GET: sis_asignacion_celulares
        public ActionResult Index()
        {
            return View(db.sis_asignacion_celulares.ToList());
        }

        // GET: sis_asignacion_celulares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_celulares);
        }

        // GET: sis_asignacion_celulares/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_asignacion_celulares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_celulares_Id,adm_cuentas_Id")] sis_asignacion_celulares sis_asignacion_celulares)
        {
            if (ModelState.IsValid)
            {
                db.sis_asignacion_celulares.Add(sis_asignacion_celulares);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_asignacion_celulares);
        }

        // GET: sis_asignacion_celulares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_celulares);
        }

        // POST: sis_asignacion_celulares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_celulares_Id,adm_cuentas_Id")] sis_asignacion_celulares sis_asignacion_celulares)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_asignacion_celulares).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_asignacion_celulares);
        }

        // GET: sis_asignacion_celulares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_celulares);
        }

        // POST: sis_asignacion_celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            db.sis_asignacion_celulares.Remove(sis_asignacion_celulares);
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
