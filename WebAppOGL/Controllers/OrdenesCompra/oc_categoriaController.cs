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
    public class oc_categoriaController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_categoria
        public ActionResult Index()
        {
            return View(db.oc_categoria.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaElementos = new List<SelectListItem>();

            foreach (var item in db.oc_categoria.OrderBy(x => x.Descripcion).ToList())
            {
                listaElementos.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Descripcion
                });
            }

            return Json(listaElementos);
        }

        // GET: oc_categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoria oc_categoria = db.oc_categoria.Find(id);
            if (oc_categoria == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoria);
        }

        // GET: oc_categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_categoria oc_categoria)
        {
            if (ModelState.IsValid)
            {
                oc_categoria cate = new oc_categoria();
                cate.Descripcion = oc_categoria.Descripcion.ToUpper();


                db.oc_categoria.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_categoria);
        }

        // GET: oc_categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoria oc_categoria = db.oc_categoria.Find(id);
            if (oc_categoria == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoria);
        }

        // POST: oc_categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_categoria oc_categoria)
        {
            if (ModelState.IsValid)
            {
                oc_categoria cate = db.oc_categoria.Find(oc_categoria.Id);
                cate.Descripcion = oc_categoria.Descripcion.ToUpper();

                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_categoria);
        }

        // GET: oc_categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_categoria oc_categoria = db.oc_categoria.Find(id);
            if (oc_categoria == null)
            {
                return HttpNotFound();
            }
            return View(oc_categoria);
        }

        // POST: oc_categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_categoria oc_categoria = db.oc_categoria.Find(id);
            db.oc_categoria.Remove(oc_categoria);
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
