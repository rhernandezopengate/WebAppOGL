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
    public class oc_det_ordenes_productosController : Controller
    {
        private db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();

        public ActionResult EditarConcepto(int? id) 
        {
            oc_det_ordenes_productos det = db.oc_det_ordenes_productos.Find(id);
            det.producto = db.oc_productos.Where(x => x.Id.Equals(det.oc_productos_Id)).FirstOrDefault().Descripcion;

            return View(det);
        }

        [HttpPost]
        public ActionResult EditarConcepto(oc_det_ordenes_productos det)
        {
            oc_det_ordenes_productos detalle = db.oc_det_ordenes_productos.Find(det.Id);

            detalle.oc_productos_Id = db.oc_productos.Where(x => x.Descripcion.Contains(det.producto)).FirstOrDefault().Id;
            detalle.Cantidad = det.Cantidad;
            detalle.Precio = det.Precio;
            detalle.Subtotal = (det.Cantidad * det.Precio);

            db.SaveChanges();

            return Json(new { respuesta = "Correcto" }, JsonRequestBehavior.AllowGet);
        }

    }
}
