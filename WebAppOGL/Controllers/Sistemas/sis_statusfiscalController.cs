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
    public class sis_statusfiscalController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_statusfiscal
        public ActionResult Index()
        {
            return View(db.sis_statusfiscal.ToList());
        }

        // GET: sis_statusfiscal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_statusfiscal sis_statusfiscal = db.sis_statusfiscal.Find(id);
            if (sis_statusfiscal == null)
            {
                return HttpNotFound();
            }
            return View(sis_statusfiscal);
        }

        // GET: sis_statusfiscal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sis_statusfiscal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] sis_statusfiscal sis_statusfiscal)
        {
            if (ModelState.IsValid)
            {
                db.sis_statusfiscal.Add(sis_statusfiscal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sis_statusfiscal);
        }

        // GET: sis_statusfiscal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_statusfiscal sis_statusfiscal = db.sis_statusfiscal.Find(id);
            if (sis_statusfiscal == null)
            {
                return HttpNotFound();
            }
            return View(sis_statusfiscal);
        }

        // POST: sis_statusfiscal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] sis_statusfiscal sis_statusfiscal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_statusfiscal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sis_statusfiscal);
        }

        // GET: sis_statusfiscal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_statusfiscal sis_statusfiscal = db.sis_statusfiscal.Find(id);
            if (sis_statusfiscal == null)
            {
                return HttpNotFound();
            }
            return View(sis_statusfiscal);
        }

        // POST: sis_statusfiscal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_statusfiscal sis_statusfiscal = db.sis_statusfiscal.Find(id);
            db.sis_statusfiscal.Remove(sis_statusfiscal);
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
