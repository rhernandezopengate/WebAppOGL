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
    public class sis_estatusequipoController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_estatusequipo
        [Authorize(Roles = "sistemas")]
        public ActionResult Index()
        {
            return View(db.sis_estatusequipo.ToList());
        }

        // GET: sis_estatusequipo/Details/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_estatusequipo sis_estatusequipo = db.sis_estatusequipo.Find(id);
            if (sis_estatusequipo == null)
            {
                return HttpNotFound();
            }
            return View(sis_estatusequipo);
        }

        // GET: sis_estatusequipo/Create
        [Authorize(Roles = "sistemas")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_estatusequipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_estatusequipo sis_estatusequipo)
        {
            if (ModelState.IsValid)
            {
                sis_estatusequipo status = new sis_estatusequipo();
                status.Descripcion = sis_estatusequipo.Descripcion.ToUpper();

                db.sis_estatusequipo.Add(status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_estatusequipo);
        }

        // GET: sis_estatusequipo/Edit/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_estatusequipo sis_estatusequipo = db.sis_estatusequipo.Find(id);
            if (sis_estatusequipo == null)
            {
                return HttpNotFound();
            }
            return View(sis_estatusequipo);
        }

        // POST: sis_estatusequipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_estatusequipo sis_estatusequipo)
        {
            if (ModelState.IsValid)
            {
                sis_estatusequipo status = db.sis_estatusequipo.Find(sis_estatusequipo.Id);
                status.Descripcion = sis_estatusequipo.Descripcion.ToUpper();


                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_estatusequipo);
        }

        // GET: sis_estatusequipo/Delete/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_estatusequipo sis_estatusequipo = db.sis_estatusequipo.Find(id);
            if (sis_estatusequipo == null)
            {
                return HttpNotFound();
            }
            return View(sis_estatusequipo);
        }

        // POST: sis_estatusequipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_estatusequipo sis_estatusequipo = db.sis_estatusequipo.Find(id);
            db.sis_estatusequipo.Remove(sis_estatusequipo);
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
