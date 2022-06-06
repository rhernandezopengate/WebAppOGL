using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;
using WebAppOGL.Entities.OrdenesServicio;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_conceptosController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_conceptos
        public ActionResult Index()
        {
            return View(db.os_conceptos.ToList());
        }


        public JsonResult AutoComplete(string prefix)
        {
            var proveedores = (from p in db.os_conceptos
                               where p.Descripcion.Contains(prefix)
                               orderby p.Descripcion ascending
                               select new
                               {
                                   label = p.Descripcion,
                                   val = p.Id
                               }).ToList();

            return Json(proveedores);
        }


        // GET: os_conceptos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_conceptos os_conceptos = db.os_conceptos.Find(id);
            if (os_conceptos == null)
            {
                return HttpNotFound();
            }
            return View(os_conceptos);
        }

        // GET: os_conceptos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: os_conceptos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] os_conceptos os_conceptos)
        {
            if (ModelState.IsValid)
            {
                db.os_conceptos.Add(os_conceptos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(os_conceptos);
        }

        // GET: os_conceptos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_conceptos os_conceptos = db.os_conceptos.Find(id);
            if (os_conceptos == null)
            {
                return HttpNotFound();
            }
            return View(os_conceptos);
        }

        // POST: os_conceptos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] os_conceptos os_conceptos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(os_conceptos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(os_conceptos);
        }

        // GET: os_conceptos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_conceptos os_conceptos = db.os_conceptos.Find(id);
            if (os_conceptos == null)
            {
                return HttpNotFound();
            }
            return View(os_conceptos);
        }

        // POST: os_conceptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_conceptos os_conceptos = db.os_conceptos.Find(id);
            db.os_conceptos.Remove(os_conceptos);
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
