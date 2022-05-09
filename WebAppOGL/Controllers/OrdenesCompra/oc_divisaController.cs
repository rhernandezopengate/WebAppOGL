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
    public class oc_divisaController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        // GET: oc_divisa
        public ActionResult Index()
        {
            return View(db.oc_divisa.ToList());
        }

        public JsonResult ListaSelect()
        {
            List<SelectListItem> listaElementos = new List<SelectListItem>();

            foreach (var item in db.oc_divisa.ToList())
            {
                listaElementos.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Descripcion
                });
            }

            return Json(listaElementos);
        }


        // GET: oc_divisa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_divisa oc_divisa = db.oc_divisa.Find(id);
            if (oc_divisa == null)
            {
                return HttpNotFound();
            }
            return View(oc_divisa);
        }

        // GET: oc_divisa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oc_divisa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] oc_divisa oc_divisa)
        {
            if (ModelState.IsValid)
            {
                oc_divisa divisa = new oc_divisa();
                divisa.Descripcion = oc_divisa.Descripcion.ToUpper();

                db.oc_divisa.Add(divisa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oc_divisa);
        }

        // GET: oc_divisa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_divisa oc_divisa = db.oc_divisa.Find(id);
            if (oc_divisa == null)
            {
                return HttpNotFound();
            }
            return View(oc_divisa);
        }

        // POST: oc_divisa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] oc_divisa oc_divisa)
        {
            if (ModelState.IsValid)
            {
                oc_divisa divisa = db.oc_divisa.Find(oc_divisa.Id);
                divisa.Descripcion = oc_divisa.Descripcion.ToUpper();

                db.Entry(divisa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oc_divisa);
        }

        // GET: oc_divisa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oc_divisa oc_divisa = db.oc_divisa.Find(id);
            if (oc_divisa == null)
            {
                return HttpNotFound();
            }
            return View(oc_divisa);
        }

        // POST: oc_divisa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oc_divisa oc_divisa = db.oc_divisa.Find(id);
            db.oc_divisa.Remove(oc_divisa);
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
