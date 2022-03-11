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
    public class oc_lugarentregaController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_lugarentrega
        public ActionResult Index()
        {
            return View(db.oc_lugarentrega.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaElementos = new List<SelectListItem>();

            foreach (var item in db.oc_lugarentrega.ToList())
            {
                listaElementos.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Descripcion
                });
            }

            return Json(listaElementos);
        }

        // GET: oc_lugarentrega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_lugarentrega oc_lugarentrega = db.oc_lugarentrega.Find(id);
            if (oc_lugarentrega == null)
            {
                return HttpNotFound();
            }
            return View(oc_lugarentrega);
        }

        // GET: oc_lugarentrega/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_lugarentrega/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_lugarentrega oc_lugarentrega)
        {
            if (ModelState.IsValid)
            {
                oc_lugarentrega lugar = new oc_lugarentrega();
                lugar.Descripcion = oc_lugarentrega.Descripcion.ToUpper();


                db.oc_lugarentrega.Add(lugar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_lugarentrega);
        }

        // GET: oc_lugarentrega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_lugarentrega oc_lugarentrega = db.oc_lugarentrega.Find(id);
            if (oc_lugarentrega == null)
            {
                return HttpNotFound();
            }
            return View(oc_lugarentrega);
        }

        // POST: oc_lugarentrega/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_lugarentrega oc_lugarentrega)
        {
            if (ModelState.IsValid)
            {
                oc_lugarentrega lugar = db.oc_lugarentrega.Find(oc_lugarentrega.Id);
                lugar.Descripcion = oc_lugarentrega.Descripcion.ToUpper();

                db.Entry(lugar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_lugarentrega);
        }

        // GET: oc_lugarentrega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_lugarentrega oc_lugarentrega = db.oc_lugarentrega.Find(id);
            if (oc_lugarentrega == null)
            {
                return HttpNotFound();
            }
            return View(oc_lugarentrega);
        }

        // POST: oc_lugarentrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_lugarentrega oc_lugarentrega = db.oc_lugarentrega.Find(id);
            db.oc_lugarentrega.Remove(oc_lugarentrega);
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
