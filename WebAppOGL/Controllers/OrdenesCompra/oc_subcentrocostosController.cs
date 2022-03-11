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
    public class oc_subcentrocostosController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_subcentrocostos
        public ActionResult Index()
        {
            return View(db.oc_subcentrocostos.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaElementos = new List<SelectListItem>();

            foreach (var item in db.oc_subcentrocostos.OrderBy(x => x.Descripcion).ToList())
            {
                listaElementos.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Descripcion
                });
            }

            return Json(listaElementos);
        }

        // GET: oc_subcentrocostos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_subcentrocostos oc_subcentrocostos = db.oc_subcentrocostos.Find(id);
            if (oc_subcentrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_subcentrocostos);
        }

        // GET: oc_subcentrocostos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_subcentrocostos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_subcentrocostos oc_subcentrocostos)
        {
            if (ModelState.IsValid)
            {
                oc_subcentrocostos costos = new oc_subcentrocostos();
                costos.Descripcion = oc_subcentrocostos.Descripcion.ToUpper();

                db.oc_subcentrocostos.Add(costos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_subcentrocostos);
        }

        // GET: oc_subcentrocostos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_subcentrocostos oc_subcentrocostos = db.oc_subcentrocostos.Find(id);
            if (oc_subcentrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_subcentrocostos);
        }

        // POST: oc_subcentrocostos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_subcentrocostos oc_subcentrocostos)
        {
            if (ModelState.IsValid)
            {
                oc_subcentrocostos costos = db.oc_subcentrocostos.Find(oc_subcentrocostos.Id);
                costos.Descripcion = oc_subcentrocostos.Descripcion.ToUpper();


                db.Entry(costos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_subcentrocostos);
        }

        // GET: oc_subcentrocostos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_subcentrocostos oc_subcentrocostos = db.oc_subcentrocostos.Find(id);
            if (oc_subcentrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_subcentrocostos);
        }

        // POST: oc_subcentrocostos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_subcentrocostos oc_subcentrocostos = db.oc_subcentrocostos.Find(id);
            db.oc_subcentrocostos.Remove(oc_subcentrocostos);
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
