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
using Rotativa;

namespace WebAppOGL.Controllers.OrdenesCompra
{

    public class oc_ordenesdecomprasController : Controller
    {
        db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();
        db_a3f19c_administracionEntities1 db1 = new db_a3f19c_administracionEntities1();

        // GET: oc_ordenesdecompras
        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
            int empleado = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;

            if (User.IsInRole("compras") || User.IsInRole("finanzas") || User.IsInRole("direccion"))
            {
                ViewBag.Identificador = 3;
                ViewBag.SupervisorId = 0;
                ViewBag.EmpleadoId = 0;
            }
            else
            {
                var supervisor = db.oc_supervisores.Where(x => x.adm_empleados_Id.Equals(empleado)).FirstOrDefault();

                if (supervisor != null)
                {
                    var solicitante = db.oc_solicitantes.Where(x => x.adm_empleados_Id.Equals(empleado)).FirstOrDefault();
                    ViewBag.SupervisorId = supervisor.Id;
                    ViewBag.EmpleadoId = solicitante.Id;
                    ViewBag.Identificador = 1;
                }
                else
                {                   
                    ViewBag.SupervisorId = 0;
                    var solicitante = db.oc_solicitantes.Where(x => x.adm_empleados_Id.Equals(empleado)).FirstOrDefault();
                    ViewBag.EmpleadoId = solicitante.Id;
                    ViewBag.Identificador = 2;                 
                }
            }


            //validar si es supervisor
            

            return View();
        }

        [HttpPost]
        public ActionResult ObtenerOrdenesCompra(int idempleado, int idsupervisor, int identificador)
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

                    string sql = "exec SP_OrdenesCompra_Index @folio, @identificador, @idempleado, @idsupervisor";
                    var query = new SqlCommand(sql, con);

                    if (folio != "")
                    {
                        query.Parameters.AddWithValue("@folio", folio);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@folio", DBNull.Value);
                    }

                    if (identificador != 0)
                    {
                        query.Parameters.AddWithValue("@identificador", identificador);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@identificador", DBNull.Value);
                    }

                    if (idsupervisor != 0)
                    {
                        query.Parameters.AddWithValue("@idsupervisor", idsupervisor);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@idsupervisor", DBNull.Value);
                    }

                    if (idempleado != 0)
                    {
                        query.Parameters.AddWithValue("@idempleado", idempleado);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@idempleado", DBNull.Value);
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

        public ActionResult Details(int? id) 
        {
            oc_ordenescompras oc = db.oc_ordenescompras.Find(id);
            oc.Cuenta = db1.adm_cuentas.Where(x => x.Id.Equals(oc.adm_cuentas_Id)).FirstOrDefault().Descripcion;

            List<oc_det_ordenes_productos> lista = db.oc_det_ordenes_productos.Where(x => x.oc_ordenescompras_Id.Equals(oc.Id)).ToList();

            List<detalleproductos> listaView = new List<detalleproductos>();

            foreach (var item in lista)
            {
                detalleproductos detalleproductos = new detalleproductos();

                detalleproductos.codigo = item.Codigo;
                detalleproductos.producto = item.oc_productos.Descripcion;
                detalleproductos.cantidad = (int)item.Cantidad;
                detalleproductos.precio = (decimal)item.Precio;
                detalleproductos.subtotal = (decimal)item.Subtotal;

                listaView.Add(detalleproductos);
            }

            double subtotal = 0;
            double iva = 0.16;

            foreach (var item in lista)
            {
                subtotal += (double)item.Subtotal;
            }

            ViewBag.Subtotal = string.Format("{0:C}", subtotal);

            iva = (double)(subtotal * double.Parse(iva.ToString()));

            iva = Math.Round(iva, 2);

            ViewBag.Iva = string.Format("{0:C}", iva);


            double total = subtotal + iva;

            ViewBag.Total = string.Format("{0:C}", total);

            ViewData["ListaOC"] = listaView;

            return View(oc);
        }

        public ActionResult ReporteOC(int? Id)
        {
            oc_ordenescompras oc = db.oc_ordenescompras.Find(Id);
            oc.Cuenta = db1.adm_cuentas.Where(x => x.Id.Equals(oc.adm_cuentas_Id)).FirstOrDefault().Descripcion;

            List<oc_det_ordenes_productos> lista = db.oc_det_ordenes_productos.Where(x => x.oc_ordenescompras_Id.Equals(oc.Id)).ToList();

            List<detalleproductos> listaView = new List<detalleproductos>();

            foreach (var item in lista)
            {
                detalleproductos detalleproductos = new detalleproductos();

                detalleproductos.codigo = item.Codigo;
                detalleproductos.producto = item.oc_productos.Descripcion;
                detalleproductos.cantidad = (int)item.Cantidad;
                detalleproductos.precio = (decimal)item.Precio;
                detalleproductos.subtotal = (decimal)item.Subtotal;

                listaView.Add(detalleproductos);
            }

            double subtotal = 0;
            double iva = 0.16;

            foreach (var item in lista)
            {
                subtotal += (double)item.Subtotal;
            }

            ViewBag.Subtotal = string.Format("{0:C}", subtotal); 

            iva = (double)(subtotal * double.Parse(iva.ToString()));

            iva = Math.Round(iva, 2);

            ViewBag.Iva = string.Format("{0:C}", iva);


            double total = subtotal + iva;

            ViewBag.Total = string.Format("{0:C}", total);

            ViewData["ListaOC"] = listaView;

            return View(oc);
        }

        public ActionResult ImprimirVista(int? id)
        {
            oc_ordenescompras oc = db.oc_ordenescompras.Find(id);
            return new ActionAsPdf("ReporteOC", new { Id = id })
            {
                FileName = "Reporte_"+ oc.Folio +".pdf",
                PageOrientation = Rotativa.Options.Orientation.Portrait,                
            };
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

            return View(oc);
        }

        [HttpPost]
        public ActionResult AutorizarOC(oc_ordenescompras oc) 
        {
            oc_ordenescompras _oc = db.oc_ordenescompras.Find(oc.Id);

            if (oc.oc_statuscompras_Id != null)
            {
                _oc.oc_statuscompras_Id = oc.oc_statuscompras_Id;
            }
            
            if (oc.oc_statusdirector_Id != null)
            {
                _oc.oc_statusdirector_Id = oc.oc_statusdirector_Id;
            }
            
            if (oc.oc_statusfinanzas_Id != null)
            {
                _oc.oc_statusfinanzas_Id = oc.oc_statusfinanzas_Id;
            }
            
            if (oc.oc_statussupervisor_Id != null)
            {
                _oc.oc_statussupervisor_Id = oc.oc_statussupervisor_Id;
            }   

            db.SaveChanges();

            return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        //CREANDO OC
        [Authorize]
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

                oc.Folio = "OC-" + (folio + 3000);
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
                oc.oc_solicitantes_Id = db.oc_solicitantes.Where(x => x.adm_empleados_Id == solicitante).FirstOrDefault().Id;

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

        [Authorize]
        //Editando OC
        public ActionResult Edit(int? id) 
        {
            oc_ordenescompras oc = db.oc_ordenescompras.Find(id);

            ViewBag.oc_categoria_Id = new SelectList(db.oc_categoria, "Id", "Descripcion", oc.oc_categoria_Id);

            ViewBag.oc_formapago_Id = new SelectList(db.oc_formapago, "Id", "Descripcion", oc.oc_formapago_Id);

            ViewBag.oc_centrocostos_Id = new SelectList(db.oc_centrocostos, "Id", "Descripcion", oc.oc_centrocostos_Id);

            ViewBag.oc_subcentrocostos_Id = new SelectList(db.oc_subcentrocostos, "Id", "Descripcion", oc.oc_subcentrocostos_Id);
            
            ViewBag.oc_tipocompra_Id = new SelectList(db.oc_tipocompra, "Id", "Descripcion", oc.oc_tipocompra_Id);

            ViewBag.oc_lugarentrega_Id = new SelectList(db.oc_lugarentrega, "Id", "Descripcion", oc.oc_lugarentrega_Id);

            ViewBag.oc_divisa_Id = new SelectList(db.oc_divisa, "Id", "Descripcion", oc.oc_divisa_Id);

            ViewBag.oc_proveedores_Id = new SelectList(db.oc_proveedores, "Id", "NombreComercial", oc.oc_proveedores_Id);

            ViewBag.adm_cuentas_Id = new SelectList(db1.adm_cuentas, "Id", "Descripcion", oc.adm_cuentas_Id);


            List<oc_det_ordenes_productos> lista = db.oc_det_ordenes_productos.Where(x => x.oc_ordenescompras_Id.Equals(oc.Id)).ToList();

            List<detalleproductos> listaView = new List<detalleproductos>();

            foreach (var item in lista)
            {
                detalleproductos detalleproductos = new detalleproductos();

                detalleproductos.Id = item.Id;
                detalleproductos.codigo = item.Codigo;
                detalleproductos.producto = item.oc_productos.Descripcion;
                detalleproductos.cantidad = (int)item.Cantidad;
                detalleproductos.precio = (decimal)item.Precio;
                detalleproductos.subtotal = (decimal)item.Subtotal;

                listaView.Add(detalleproductos);
            }

            ViewData["ListaOC"] = listaView;

            return View(oc);
        }

        public ActionResult EditandoOC(oc_ordenescompras encabezado) 
        {
            try
            {
                oc_ordenescompras orden = db.oc_ordenescompras.Find(encabezado.Id);

                orden.Justificacion = encabezado.Justificacion;

                orden.Proyecto = encabezado.Proyecto;

                orden.adm_cuentas_Id = encabezado.adm_cuentas_Id;

                orden.oc_centrocostos_Id = encabezado.oc_centrocostos_Id;

                orden.oc_subcentrocostos_Id = encabezado.oc_subcentrocostos_Id;

                orden.oc_proveedores_Id = encabezado.oc_proveedores_Id;

                orden.oc_categoria_Id = encabezado.oc_categoria_Id;

                orden.oc_formapago_Id = encabezado.oc_formapago_Id;

                orden.oc_lugarentrega_Id = encabezado.oc_lugarentrega_Id;

                orden.oc_tipocompra_Id = encabezado.oc_tipocompra_Id;

                orden.oc_divisa_Id = encabezado.oc_divisa_Id;

                db.SaveChanges();

                return Json(new { respuesta = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { respuesta = "Error" }, JsonRequestBehavior.AllowGet);
            }           
        }

        public ActionResult TablaConceptos(int id) 
        {
            ViewBag.oc_ordenescompras_Id = id;
            List<oc_det_ordenes_productos> det = db.oc_det_ordenes_productos.Where(x => x.oc_ordenescompras_Id.Equals(id)).ToList();

            decimal subtotal = 0;
            decimal impuesto = (decimal)0.16;
            decimal impuesto_final = (decimal)1.16;

            foreach (var item in det)
            {
                subtotal += (decimal)item.Subtotal;
            }

            decimal a = subtotal;
            decimal b = subtotal * impuesto;
            decimal c = subtotal * impuesto_final;

            ViewBag.Subtotal = Math.Round(a, 2);
            ViewBag.IVA = Math.Round(b, 2);
            ViewBag.Total = Math.Round(c, 2);

            return View();
        }

        [HttpPost]
        public ActionResult ObtenerConceptosOC(int id)
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

                List<detalleproductos> lista = new List<detalleproductos>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_DetOrdenesCompras_Editar @IdOrden";
                    
                    var query = new SqlCommand(sql, con);
                                                          
                    query.Parameters.AddWithValue("@IdOrden", id);
                                        
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var oc = new detalleproductos();

                            oc.Id = Convert.ToInt32(dr["Id"]);

                            oc.codigo = dr["Codigo"].ToString();

                            oc.producto = dr["Descripcion"].ToString();

                            oc.cantidad = Convert.ToInt32(dr["Cantidad"]);

                            oc.precio = Convert.ToDecimal(dr["Precio"]);

                            oc.subtotal = Convert.ToDecimal(dr["Subtotal"]);

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

    }
}