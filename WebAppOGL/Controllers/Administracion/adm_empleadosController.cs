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
    public class adm_empleadosController : Controller
    {
        private db_a3f19c_administracionEntities1 db = new db_a3f19c_administracionEntities1();

        // GET: adm_empleados
        public ActionResult Index()
        {
            return View(db.adm_empleados.ToList());
        }

        // GET: adm_empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_empleados adm_empleados = db.adm_empleados.Find(id);
            if (adm_empleados == null)
            {
                return HttpNotFound();
            }
            return View(adm_empleados);
        }

        // GET: adm_empleados/Create
        public ActionResult Create()
        {
            ViewBag.adm_puestos_Id = new SelectList(db.adm_puestos, "Id", "Descripcion");
            ViewBag.adm_area_Id = new SelectList(db.adm_area, "Id", "Descripcion");
            return View();
        }

        // POST: adm_empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,Nombres,Apellido_Paterno,Apellido_Materno,adm_puestos_Id,Email,adm_area_Id")] adm_empleados adm_empleados)
        {
            if (ModelState.IsValid)
            {
                db.adm_empleados.Add(adm_empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adm_puestos_Id = new SelectList(db.adm_puestos, "Id", "Descripcion", adm_empleados.adm_puestos_Id);
            ViewBag.adm_area_Id = new SelectList(db.adm_area, "Id", "Descripcion", adm_empleados.adm_area_Id);

            return View(adm_empleados);
        }

        // GET: adm_empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_empleados adm_empleados = db.adm_empleados.Find(id);
            if (adm_empleados == null)
            {
                return HttpNotFound();
            }

            ViewBag.adm_puestos_Id = new SelectList(db.adm_puestos, "Id", "Descripcion", adm_empleados.adm_puestos_Id);
            ViewBag.adm_area_Id = new SelectList(db.adm_area, "Id", "Descripcion", adm_empleados.adm_area_Id);

            return View(adm_empleados);
        }

        // POST: adm_empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,Nombres,Apellido_Paterno,Apellido_Materno,adm_puestos_Id,Email,adm_area_Id")] adm_empleados adm_empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adm_puestos_Id = new SelectList(db.adm_puestos, "Id", "Descripcion", adm_empleados.adm_puestos_Id);
            ViewBag.adm_area_Id = new SelectList(db.adm_area, "Id", "Descripcion", adm_empleados.adm_area_Id);
            return View(adm_empleados);
        }

        // GET: adm_empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_empleados adm_empleados = db.adm_empleados.Find(id);
            if (adm_empleados == null)
            {
                return HttpNotFound();
            }
            return View(adm_empleados);
        }

        // POST: adm_empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_empleados adm_empleados = db.adm_empleados.Find(id);
            db.adm_empleados.Remove(adm_empleados);
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
