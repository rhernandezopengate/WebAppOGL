using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesServicio;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_rutasController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_rutas
        public ActionResult Index()
        {
            var os_rutas = db.os_rutas.Include(o => o.os_origen);
            return View(os_rutas.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            foreach (var item in db.os_rutas.OrderBy(x => x.Codigo).ToList())
            {
                lista.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Codigo
                });
            }

            return Json(lista);
        }

        // GET: os_rutas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_rutas os_rutas = db.os_rutas.Find(id);
            if (os_rutas == null)
            {
                return HttpNotFound();
            }
            return View(os_rutas);
        }

        // GET: os_rutas/Create
        public ActionResult Create()
        {
            ViewBag.os_origen_Id = new SelectList(db.os_origen, "Id", "Descripcion");
            return View();
        }

        // POST: os_rutas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,os_origen_Id")] os_rutas os_rutas)
        {
            if (ModelState.IsValid)
            {
                db.os_rutas.Add(os_rutas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.os_origen_Id = new SelectList(db.os_origen, "Id", "Descripcion", os_rutas.os_origen_Id);
            return View(os_rutas);
        }

        // GET: os_rutas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_rutas os_rutas = db.os_rutas.Find(id);
            if (os_rutas == null)
            {
                return HttpNotFound();
            }
            ViewBag.os_origen_Id = new SelectList(db.os_origen, "Id", "Descripcion", os_rutas.os_origen_Id);
            return View(os_rutas);
        }

        // POST: os_rutas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,os_origen_Id")] os_rutas os_rutas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(os_rutas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.os_origen_Id = new SelectList(db.os_origen, "Id", "Descripcion", os_rutas.os_origen_Id);
            return View(os_rutas);
        }

        // GET: os_rutas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_rutas os_rutas = db.os_rutas.Find(id);
            if (os_rutas == null)
            {
                return HttpNotFound();
            }
            return View(os_rutas);
        }

        // POST: os_rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_rutas os_rutas = db.os_rutas.Find(id);
            db.os_rutas.Remove(os_rutas);
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
