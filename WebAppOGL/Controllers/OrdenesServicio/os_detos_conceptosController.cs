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
    public class os_detos_conceptosController : Controller
    {
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_detos_conceptos
        public ActionResult Index()
        {
            var os_detos_conceptos = db.os_detos_conceptos.Include(o => o.os_conceptos).Include(o => o.os_ordenesservicio);
            return View(os_detos_conceptos.ToList());
        }

        // GET: os_detos_conceptos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_detos_conceptos os_detos_conceptos = db.os_detos_conceptos.Find(id);
            if (os_detos_conceptos == null)
            {
                return HttpNotFound();
            }
            return View(os_detos_conceptos);
        }

        // GET: os_detos_conceptos/Create
        public ActionResult Create(int id)
        {
            ViewBag.os_conceptos_Id = new SelectList(db.os_conceptos, "Id", "Descripcion");
            ViewBag.os_ordenesservicio_Id = id;
            return View();
        }

        // POST: os_detos_conceptos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Cantidad,PrecioUnitario,Subtotal,os_ordenesservicio_Id,os_conceptos_Id")] os_detos_conceptos os_detos_conceptos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    os_detos_conceptos.Subtotal = os_detos_conceptos.Cantidad * os_detos_conceptos.PrecioUnitario;
                    db.os_detos_conceptos.Add(os_detos_conceptos);
                    db.SaveChanges();
                    return Json(new { respuesta = "Correcto", }, JsonRequestBehavior.AllowGet);
                }
                ViewBag.os_conceptos_Id = new SelectList(db.os_conceptos, "Id", "Descripcion", os_detos_conceptos.os_conceptos_Id);
                ViewBag.os_ordenesservicio_Id = new SelectList(db.os_ordenesservicio, "Id", "Folio", os_detos_conceptos.os_ordenesservicio_Id);
                return Json(new { respuesta = "Error", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception _ex)
            {
                return Json(new { respuesta = "Error" + _ex.Message.ToString() }, JsonRequestBehavior.AllowGet);                
            }           
        }

        // GET: os_detos_conceptos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_detos_conceptos os_detos_conceptos = db.os_detos_conceptos.Find(id);
            if (os_detos_conceptos == null)
            {
                return HttpNotFound();
            }
            ViewBag.os_conceptos_Id = new SelectList(db.os_conceptos, "Id", "Descripcion", os_detos_conceptos.os_conceptos_Id);
            ViewBag.os_ordenesservicio_Id = new SelectList(db.os_ordenesservicio, "Id", "Folio", os_detos_conceptos.os_ordenesservicio_Id);
            return View(os_detos_conceptos);
        }

        // POST: os_detos_conceptos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Cantidad,PrecioUnitario,Subtotal,os_ordenesservicio_Id,os_conceptos_Id")] os_detos_conceptos os_detos_conceptos)
        {
            if (ModelState.IsValid)
            {
                os_ordenesservicio os = db.os_ordenesservicio.Find(os_detos_conceptos.os_ordenesservicio_Id);

                os_detos_conceptos.Subtotal = os_detos_conceptos.Cantidad * os_detos_conceptos.PrecioUnitario;

                db.Entry(os_detos_conceptos).State = EntityState.Modified;

                db.SaveChanges();

                return Json(
                new
                {
                    respuesta = "Correcto",

                }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.os_conceptos_Id = new SelectList(db.os_conceptos, "Id", "Descripcion", os_detos_conceptos.os_conceptos_Id);
            ViewBag.os_ordenesservicio_Id = new SelectList(db.os_ordenesservicio, "Id", "Folio", os_detos_conceptos.os_ordenesservicio_Id);
            return Json(new { respuesta = "Error" }, JsonRequestBehavior.AllowGet);
        }
                
        public ActionResult RecalcularTotales(int IdOrden) 
        {
            try
            {
                decimal? subtotal = 0;                
                decimal? iva;
                decimal? procentajeiva = decimal.Parse("0.16");
                decimal? total;
                decimal? subtotalretencion = 0;
                decimal? retencion;
                decimal? procentajeretencion = decimal.Parse("0.04");

                List<os_detos_conceptos> listaConceptos = db.os_detos_conceptos.Where(x => x.os_ordenesservicio_Id.Equals(IdOrden)).ToList();

                //calculamos la retencion
                foreach (var item in listaConceptos)
                {
                    if (item.os_conceptos_Id == 1)
                    {
                        subtotalretencion += item.Subtotal;
                    }
                }

                foreach (var item in listaConceptos)
                {
                    subtotal += item.Subtotal;
                }

                iva = subtotal * procentajeiva;

                retencion = subtotalretencion * procentajeretencion;

                total = (subtotal + iva) - retencion;

                os_ordenesservicio ordenservicio = db.os_ordenesservicio.Where(x => x.Id.Equals(IdOrden)).FirstOrDefault();

                ordenservicio.Subtotal = subtotal;
                ordenservicio.Retencion = retencion;
                ordenservicio.IVA = iva;
                ordenservicio.Total = total;

                db.SaveChanges();

                return Json(new { 
                    respuesta = "Correcto",
                    Subtotal = subtotal,
                    Iva = iva,
                    Retencion = retencion,
                    Total = total
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception _ex)
            {
                return Json(new { respuesta = "Error" + _ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: os_detos_conceptos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            os_detos_conceptos os_detos_conceptos = db.os_detos_conceptos.Find(id);
            if (os_detos_conceptos == null)
            {
                return HttpNotFound();
            }
            return View(os_detos_conceptos);
        }

        // POST: os_detos_conceptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            os_detos_conceptos os_detos_conceptos = db.os_detos_conceptos.Find(id);
            db.os_detos_conceptos.Remove(os_detos_conceptos);
            db.SaveChanges();
            return Json(
               new
               {
                   respuesta = "Correcto",

               }, JsonRequestBehavior.AllowGet);
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
