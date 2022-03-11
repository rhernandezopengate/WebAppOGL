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
    public class UoMsController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: UoMs
        public ActionResult Index()
        {
            return View(db.UoM.ToList());
        }

        // GET: UoMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UoM uoM = db.UoM.Find(id);
            if (uoM == null)
            {
                return HttpNotFound();
            }
            return View(uoM);
        }

        // GET: UoMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UoMs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] UoM uoM)
        {
            if (ModelState.IsValid)
            {
                UoM uni = new UoM();
                uni.Descripcion = uoM.Descripcion.ToUpper();

                db.UoM.Add(uni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uoM);
        }

        // GET: UoMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UoM uoM = db.UoM.Find(id);
            if (uoM == null)
            {
                return HttpNotFound();
            }
            return View(uoM);
        }

        // POST: UoMs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] UoM uoM)
        {
            if (ModelState.IsValid)
            {
                UoM uni = db.UoM.Find(uoM.Id);
                uni.Descripcion = uoM.Descripcion.ToUpper();

                db.Entry(uni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uoM);
        }

        // GET: UoMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UoM uoM = db.UoM.Find(id);
            if (uoM == null)
            {
                return HttpNotFound();
            }
            return View(uoM);
        }

        // POST: UoMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UoM uoM = db.UoM.Find(id);
            db.UoM.Remove(uoM);
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
