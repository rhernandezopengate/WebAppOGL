using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;
using WebAppOGL.Entities.Administracion;

namespace WebAppOGL.Controllers.OrdenesCompra
{    
    public class oc_ordenesdecomprasController : Controller
    {
        db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();
        db_a3f19c_administracionEntities1 db1 = new db_a3f19c_administracionEntities1();
        // GET: oc_ordenesdecompras
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertarOC(List<detalleproductos> detalleproductos, encabezadooc encabezado)
        {
            try
            {
                oc_ordenescompras oc = new oc_ordenescompras();

                int folio = db.oc_ordenescompras.Count();

                oc.Folio = "OC-" + (folio + 1);
                oc.FechaAlta = DateTime.Now;
                oc.FechaCreacion = DateTime.Now;
                oc.adm_cuentas_Id = encabezado.CuentaId;
                oc.oc_centrocostos_Id = encabezado.CentroCostosId;
                oc.oc_subcentrocostos_Id = encabezado.SubCentroCostosId;
                oc.oc_lugarentrega_Id = encabezado.LugarEntregaId;
                oc.oc_proveedores_Id = encabezado.ProveedorId;
                oc.oc_categoria_Id = encabezado.CategoriaId;
                oc.oc_tipocompra_Id = encabezado.TipoCompraId;
                oc.oc_divisa_Id = encabezado.DivisaId;
                oc.oc_formapago_Id = encabezado.FormaPagoId;
                oc.Proyecto = encabezado.Proyecto;
                oc.Justificacion = encabezado.Justificacion;

                var user = User.Identity.GetUserId();
                string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
                oc.adm_empleados_Id = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;

                db.oc_ordenescompras.Add(oc);

                List<oc_det_ordenes_productos> listaDetalle = new List<oc_det_ordenes_productos>();

                foreach (var items in detalleproductos)
                {
                    oc_det_ordenes_productos detalleoc = new oc_det_ordenes_productos();
                    detalleoc.oc_productos_Id = db.oc_productos.Where(x => x.Descripcion.Contains(items.producto)).FirstOrDefault().Id;
                    detalleoc.Codigo = items.codigo;
                    detalleoc.Cantidad = items.cantidad;
                    detalleoc.Precio = items.precio;
                    detalleoc.Subtotal = items.subtotal;

                    listaDetalle.Add(detalleoc);
                }

                db.oc_det_ordenes_productos.AddRange(listaDetalle);

                db.SaveChanges();

                return Json(new { respuesta = "OK" });
            }
            catch (Exception)
            {
                return Json(detalleproductos.Count());               
            }      
        }
    }
}