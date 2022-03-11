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
    public class oc_proveedoresController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_proveedores
        public ActionResult Index()
        {
            return View(db.oc_proveedores.ToList());
        }        

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaProveedores = new List<SelectListItem>();

            foreach (var item in db.oc_proveedores.OrderBy(x => x.NombreComercial).ToList())
            {
                listaProveedores.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.NombreComercial
                });
            }

            return Json(listaProveedores);
        }

        // GET: oc_proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_proveedores oc_proveedores = db.oc_proveedores.Find(id);
            if (oc_proveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_proveedores);
        }

        // GET: oc_proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RFC,RazonSocial,DiasCredito,NombreComercial")] oc_proveedores oc_proveedores)
        {
            if (ModelState.IsValid)
            {
                db.oc_proveedores.Add(oc_proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_proveedores);
        }

        // GET: oc_proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_proveedores oc_proveedores = db.oc_proveedores.Find(id);
            if (oc_proveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_proveedores);
        }

        // POST: oc_proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RFC,RazonSocial,DiasCredito,NombreComercial")] oc_proveedores oc_proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oc_proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_proveedores);
        }

        // GET: oc_proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_proveedores oc_proveedores = db.oc_proveedores.Find(id);
            if (oc_proveedores == null)
            {
                return HttpNotFound();
            }
            return View(oc_proveedores);
        }

        // POST: oc_proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_proveedores oc_proveedores = db.oc_proveedores.Find(id);
            db.oc_proveedores.Remove(oc_proveedores);
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
