using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;
using WebAppOGL.Entities.Sistemas;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using Rotativa;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_asignacion_celularesController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities1 dbAdmin = new db_a3f19c_administracionEntities1();


        public ActionResult ReporteAsignacionCelulares(int Id = 0) 
        {
            ViewBag.Id = Id;
            ViewBag.fecha_documento = fechaconletra();

            sis_asignacion_celulares asignacion = db.sis_asignacion_celulares.Where(x => x.Id == Id).FirstOrDefault();

            var nombres = (from e in dbAdmin.adm_empleados
                           where e.Id == asignacion.adm_empleados_Id
                           select new { e.Nombres, e.Apellido_Paterno, e.Apellido_Materno }).FirstOrDefault();

            ViewBag.Nombres = nombres.Nombres + " " + nombres.Apellido_Paterno + " " + nombres.Apellido_Materno;


            return View(asignacion);
        }

        public ActionResult ImprimirVista(int? id)
        {
            return new ActionAsPdf("ReporteAsignacionCelulares", new { Id = id }) { FileName = "Carta_Responsiva_Celular.pdf" };
        }


        public string fechaconletra()
        {
            DateTime fecha_actual = DateTime.Now;

            string mes = "";

            if (fecha_actual.Month == 1)
            {
                mes = "Enero";
            }
            else if (fecha_actual.Month == 2)
            {
                mes = "Febrero";
            }
            else if (fecha_actual.Month == 3)
            {
                mes = "Marzo";
            }
            else if (fecha_actual.Month == 4)
            {
                mes = "Abril";
            }
            else if (fecha_actual.Month == 5)
            {
                mes = "Mayo";
            }
            else if (fecha_actual.Month == 6)
            {
                mes = "Junio";
            }
            else if (fecha_actual.Month == 7)
            {
                mes = "Julio";
            }
            else if (fecha_actual.Month == 8)
            {
                mes = "Agosto";
            }
            else if (fecha_actual.Month == 9)
            {
                mes = "Septiembre";
            }
            else if (fecha_actual.Month == 10)
            {
                mes = "Octubre";
            }
            else if (fecha_actual.Month == 11)
            {
                mes = "Noviembre";
            }
            else
            {
                mes = "Diciembre";
            }

            string fecha_string = "A " + fecha_actual.Day + " de " + mes + " del " + fecha_actual.Year;

            return fecha_string;
        }

        public ActionResult AsignacionCelularesParcial() 
        {
            return View();
        }

        // GET: sis_asignacion_celulares
        public ActionResult Index()
        {
            return View(db.sis_asignacion_celulares.ToList());
        }


        [HttpPost]
        public ActionResult ObtenerAsignacionCelulares()
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var nombreempleado = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<AsignacionCelularesViewModel> lista = new List<AsignacionCelularesViewModel>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_AsignacionCelulares_Index @nombreempleado";
                    var query = new SqlCommand(sql, con);

                    if (nombreempleado != "")
                    {
                        query.Parameters.AddWithValue("@nombreempleado", nombreempleado);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@nombreempleado", DBNull.Value);
                    }

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var asignacion = new AsignacionCelularesViewModel();

                            asignacion.Id = Convert.ToInt32(dr["Id"]);
                            asignacion.Area = dr["Area"].ToString();
                            asignacion.Empleado = dr["Empleado"].ToString();
                            asignacion.Sucursal = dr["Sucursal"].ToString();
                            asignacion.Cuenta = dr["Cuenta"].ToString();
                            asignacion.ModeloCelular = dr["Celular"].ToString();

                            if (dr["FechaAlta"].ToString() != "")
                            {
                                asignacion.FechaAlta = Convert.ToDateTime(dr["FechaAlta"].ToString()).Date.ToShortDateString();
                            }
                            else
                            {
                                asignacion.FechaAlta = "";
                            }

                            lista.Add(asignacion);
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


        // GET: sis_asignacion_celulares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);

            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }

            var nombres = (from e in dbAdmin.adm_empleados
                           where e.Id == sis_asignacion_celulares.adm_empleados_Id
                           select new { e.Nombres, e.Apellido_Paterno, e.Apellido_Materno }).FirstOrDefault();


            ViewBag.Nombres = nombres.Nombres + " " + nombres.Apellido_Paterno + " " + nombres.Apellido_Materno;

            var area = (from e in dbAdmin.adm_area
                        where e.Id == sis_asignacion_celulares.adm_area_Id
                        select new { e.Descripcion }).FirstOrDefault();

            ViewBag.area = area.Descripcion;

            var sucursal = (from e in dbAdmin.adm_sucursales
                            where e.Id == sis_asignacion_celulares.adm_sucursales_Id
                            select new { e.Descripcion }).FirstOrDefault();

            ViewBag.sucursal = sucursal.Descripcion;

            var cuenta = (from e in dbAdmin.adm_cuentas
                          where e.Id == sis_asignacion_celulares.adm_cuentas_Id
                          select new { e.Descripcion }).FirstOrDefault();

            ViewBag.cuenta = cuenta.Descripcion;

            ViewBag.status = sis_asignacion_celulares.sis_celulares.sis_estatusequipo.Descripcion;

            return View(sis_asignacion_celulares);
        }

        // GET: sis_asignacion_celulares/Create
        public ActionResult Create()
        {
            ViewBag.sis_celulares_Id = new SelectList(db.sis_celulares, "Id", "IMEI");
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres");
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion");
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion");
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion");

            return View();
        }

        // POST: sis_asignacion_celulares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_celulares_Id,adm_cuentas_Id")] sis_asignacion_celulares sis_asignacion_celulares)
        {
            if (ModelState.IsValid)
            {
                db.sis_asignacion_celulares.Add(sis_asignacion_celulares);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_asignacion_celulares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }

            ViewBag.sis_celulares_Id = new SelectList(db.sis_celulares, "Id", "IMEI", sis_asignacion_celulares.sis_celulares_Id);
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres", sis_asignacion_celulares.adm_empleados_Id);
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion", sis_asignacion_celulares.adm_area_Id);
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion", sis_asignacion_celulares.adm_sucursales_Id);
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion", sis_asignacion_celulares.adm_cuentas_Id);

            return View(sis_asignacion_celulares);
        }

        // POST: sis_asignacion_celulares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_celulares_Id,adm_cuentas_Id")] sis_asignacion_celulares sis_asignacion_celulares)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_asignacion_celulares).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_asignacion_celulares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            if (sis_asignacion_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_celulares);
        }

        // POST: sis_asignacion_celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_asignacion_celulares sis_asignacion_celulares = db.sis_asignacion_celulares.Find(id);
            db.sis_asignacion_celulares.Remove(sis_asignacion_celulares);
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
