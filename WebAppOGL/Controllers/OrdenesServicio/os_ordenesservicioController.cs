using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;
using WebAppOGL.Entities.OrdenesServicio;
using System.Linq.Dynamic;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_ordenesservicioController : Controller
    {
        db_a3f19c_administracionEntities1 db1 = new db_a3f19c_administracionEntities1();
        private db_a3f19c_administracionEntities3 db = new db_a3f19c_administracionEntities3();

        // GET: os_ordenesservicio
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerOrdenesServicio()
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

                List<os_ordenesservicio> lista = new List<os_ordenesservicio>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_Ordenes_Servicio_Index @folio";
                    
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
                            var oc = new os_ordenesservicio();

                            oc.Id = Convert.ToInt32(dr["Id"]);
                            oc.Folio = dr["Folio"].ToString();
                            oc.Semana = Convert.ToInt32(dr["Semana"]);
                            oc.Remision = dr["Remision"].ToString();
                            oc.Proveedor = dr["Proveedor"].ToString();
                            oc.Ruta = dr["Ruta"].ToString();
                            oc.Total = Convert.ToDecimal(dr["Total"]);
                            oc.CostoVenta = Convert.ToDecimal(dr["CostoVenta"]);
                            oc.Profit = string.Format("{0:C}", Math.Round((double)(oc.CostoVenta - oc.Total), 2));

                            oc.Utilidad = (Math.Round((double)((oc.CostoVenta - oc.Total) * 100) / (double)oc.CostoVenta, 2)).ToString() + "%";

                            if (dr["FechaSolicitud"].ToString() != "")
                            {
                                oc.FechaSolicitudString = Convert.ToDateTime(dr["FechaSolicitud"].ToString()).Date.ToShortDateString();
                            }
                            else
                            {
                                oc.FechaSolicitudString = "";
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


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreatePost(os_ordenesservicio encabezado, List<os_detos_conceptos> listaconceptos)
        {
            try
            {
                os_ordenesservicio ordenesservicio = new os_ordenesservicio();
                
                var user = User.Identity.GetUserId();
                string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
                int empleado = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;
                
                ordenesservicio.adm_empleados_Id = empleado;
                ordenesservicio.os_statuscompras_Id = 1;                     
                ordenesservicio.Folio = "OS-OGL-" + (db.os_ordenesservicio.Count() + 1);
                ordenesservicio.FechaAlta = DateTime.Now;

                ordenesservicio.os_rutas_Id = encabezado.os_rutas_Id;
                ordenesservicio.adm_cuentas_Id = encabezado.adm_cuentas_Id;
                ordenesservicio.oc_proveedores_Id = encabezado.oc_proveedores_Id;
                ordenesservicio.FechaSolicitud = encabezado.FechaSolicitud;               
                ordenesservicio.CostoVenta = encabezado.CostoVenta;
                ordenesservicio.Observaciones = encabezado.Observaciones;
                ordenesservicio.Semana = GetWeekNumber((DateTime)encabezado.FechaSolicitud);
                ordenesservicio.Remision = encabezado.Remision;

                ordenesservicio.Subtotal = encabezado.Subtotal;
                ordenesservicio.IVA = encabezado.IVA;
                ordenesservicio.Retencion = encabezado.Retencion;
                ordenesservicio.Total = encabezado.Total;

                db.os_ordenesservicio.Add(ordenesservicio);

                foreach (var item in listaconceptos)
                {
                    os_detos_conceptos detalle = new os_detos_conceptos();

                    detalle.Codigo = item.Codigo;
                    detalle.Cantidad = item.Cantidad;
                    detalle.PrecioUnitario = item.PrecioUnitario;
                    detalle.Subtotal = item.Subtotal;
                    detalle.os_conceptos_Id = db.os_conceptos.Where(x => x.Descripcion.Equals(item.Concepto.ToUpper().Trim())).FirstOrDefault().Id;
                    detalle.os_ordenesservicio_Id = ordenesservicio.Id;

                    db.os_detos_conceptos.Add(detalle);
                }
                
                db.SaveChanges();

                return Json(new { respuesta = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { respuesta = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetWeekNumber(DateTime fecha_solicitud)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(fecha_solicitud, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
    }
}