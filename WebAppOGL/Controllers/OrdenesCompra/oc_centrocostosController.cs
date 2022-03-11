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
    public class oc_centrocostosController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_centrocostos
        public ActionResult Index()
        {
            return View(db.oc_centrocostos.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaElementos = new List<SelectListItem>();

            foreach (var item in db.oc_centrocostos.ToList())
            {
                listaElementos.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Descripcion
                });
            }

            return Json(listaElementos);
        }


        // GET: oc_centrocostos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_centrocostos oc_centrocostos = db.oc_centrocostos.Find(id);
            if (oc_centrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_centrocostos);
        }

        // GET: oc_centrocostos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_centrocostos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_centrocostos oc_centrocostos)
        {
            if (ModelState.IsValid)
            {
                oc_centrocostos centro = new oc_centrocostos();
                centro.Descripcion = oc_centrocostos.Descripcion.ToUpper();

                db.oc_centrocostos.Add(centro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_centrocostos);
        }

        // GET: oc_centrocostos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_centrocostos oc_centrocostos = db.oc_centrocostos.Find(id);
            if (oc_centrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_centrocostos);
        }

        // POST: oc_centrocostos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_centrocostos oc_centrocostos)
        {
            if (ModelState.IsValid)
            {
                oc_centrocostos centro = db.oc_centrocostos.Find(oc_centrocostos.Id);
                centro.Descripcion = oc_centrocostos.Descripcion.ToUpper();

                db.Entry(centro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_centrocostos);
        }

        // GET: oc_centrocostos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_centrocostos oc_centrocostos = db.oc_centrocostos.Find(id);
            if (oc_centrocostos == null)
            {
                return HttpNotFound();
            }
            return View(oc_centrocostos);
        }

        // POST: oc_centrocostos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_centrocostos oc_centrocostos = db.oc_centrocostos.Find(id);
            db.oc_centrocostos.Remove(oc_centrocostos);
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
