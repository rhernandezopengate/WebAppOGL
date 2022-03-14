using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;
using WebAppOGL.Entities.Administracion;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq.Dynamic;

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

        public ActionResult AutorizarOC(int? id)
        {
            oc_ordenescompras oc = db.oc_ordenescompras.Find(id);
            
            ViewBag.oc_statuscompras_Id = new SelectList(db.oc_statuscompras, "Id", "Descripcion", oc.oc_statuscompras_Id);
            ViewBag.oc_statusdirector_Id = new SelectList(db.oc_statusdirector, "Id", "Descripcion", oc.oc_statusdirector_Id);
            ViewBag.oc_statusfinanzas_Id = new SelectList(db.oc_statusfinanzas, "Id", "Descripcion", oc.oc_statusfinanzas_Id);
            

            //supervisor
            var user = User.Identity.GetUserId();
            string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
            var empleado = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;

            if (oc.oc_solicitantes.oc_supervisores.adm_empleados_Id == empleado)
            {
                ViewBag.Usuario = "Supervisor";
                ViewBag.oc_statussupervisor_Id = new SelectList(db.oc_statussupervisor, "Id", "Descripcion", oc.oc_statussupervisor_Id);
            }     

            return View();
        }

        [HttpPost]
        public ActionResult ObtenerOrdenesCompra()
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var folio = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<oc_ordenescompras> lista = new List<oc_ordenescompras>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_OrdenesCompra_Index @folio";
                    var query = new SqlCommand(sql, con);

                    if (folio != "")
                    {
                        query.Parameters.AddWithValue("@folio", folio);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@folio", DBNull.Value);
                    }

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var oc = new oc_ordenescompras();

                            oc.Id = Convert.ToInt32(dr["Id"]);
                            oc.Folio = dr["Folio"].ToString();
                            oc.Proyecto = dr["Proyecto"].ToString();
                            oc.Proveedor = dr["Proveedor"].ToString();
                            oc.CentroCostos = dr["CentroCostos"].ToString();
                            oc.Cuenta = dr["Cuenta"].ToString();
                            oc.Solicitante = dr["Solicitante"].ToString();
                            oc.StatusCompras = dr["StatusCompras"].ToString();
                            oc.StatusDirector = dr["StatusDirector"].ToString();
                            oc.StatusFinanzas = dr["StatusFinanzas"].ToString();
                            oc.StatusSupervisor = dr["StatusSupervisor"].ToString();

                            if (dr["FechaAlta"].ToString() != "")
                            {
                                oc.FechaAltaString = Convert.ToDateTime(dr["FechaAlta"].ToString()).Date.ToShortDateString();
                            }
                            else
                            {
                                oc.FechaAltaString = "";
                            }

                            lista.Add(oc);
                        }
                    }
                }

                if (!(string.IsNullOrEmpty(SortColumn) && string.IsNullOrEmpty(SortColumnDir)))
                {
                    lista = lista.OrderBy(SortColumn + " " + SortColumnDir).ToList();
                }

                TotalRecords = lista.ToList().Count();
                var NewItems = lista.Skip(Skip).Take(PageSize == -1 ? TotalRecords : PageSize).ToList();

                return Json(new { draw = Draw, recordsFiltered = TotalRecords, recordsTotal = TotalRecords, data = NewItems }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex.Message.ToString());
                return null;
            }
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
                oc.oc_statuscompras_Id = 1;
                oc.oc_statusdirector_Id = 1;
                oc.oc_statusfinanzas_Id = 1;
                oc.oc_statussupervisor_Id = 1;

                var user = User.Identity.GetUserId();
                string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
                int solicitante = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;
                oc.oc_solicitantes_Id = solicitante;

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
            catch (Exception _ex)
            {
                return Json(" " + _ex.Message.ToString());               
            }      
        }
    }
}