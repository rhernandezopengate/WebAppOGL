﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;
using WebAppOGL.Entities.Sistemas;
using System.Linq.Dynamic;
using Rotativa;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_asignacionController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities1 dbAdmin = new db_a3f19c_administracionEntities1();


        public ActionResult AsignacionEquipos() 
        {
            return View();        
        }

        public ActionResult AsignacionTerminales()
        {
            return View();
        }

        public ActionResult AsignacionCelulares()
        {
            return View();
        }

        public ActionResult AsignacionImpresoras()
        {
            return View();
        }


        public ActionResult AsignacionEquiposParcial() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerAsignacionEquipos() 
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var nombreempleado = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                var numeroserie = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<AsignacionEquiposViewModel> lista = new List<AsignacionEquiposViewModel>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_AsignacionEquipos_Index @nombreempleado, @numeroserie";
                    var query = new SqlCommand(sql, con);

                    if (nombreempleado != "")
                    {
                        query.Parameters.AddWithValue("@nombreempleado", nombreempleado);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@nombreempleado", DBNull.Value);
                    }

                    if (numeroserie != "")
                    {
                        query.Parameters.AddWithValue("@numeroserie", numeroserie);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@numeroserie", DBNull.Value);
                    }

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var asignacion = new AsignacionEquiposViewModel();

                            asignacion.Id = Convert.ToInt32(dr["Id"]);
                            asignacion.Area = dr["Area"].ToString();
                            asignacion.NombreEmpleado = dr["Nombres"].ToString();
                            asignacion.Sucursal = dr["Sucursal"].ToString();
                            asignacion.Cuenta = dr["Cuenta"].ToString();
                            asignacion.Nombreequipo = dr["Numero_Serie"].ToString();

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
        
        
        public ActionResult ImprimirVista(int? id) 
        {
            return new ActionAsPdf("VistaReporteAsignacion", new { Id = id }) { FileName = "Carta_Responsiva.pdf" };
        }

        public ActionResult VistaReporteAsignacion(int Id = 0) 
        {
            ViewBag.Id = Id;
            ViewBag.fecha_documento = fechaconletra();

            sis_asignacion asignacion = db.sis_asignacion.Where(x => x.Id == Id).FirstOrDefault();

            var nombres = (from e in dbAdmin.adm_empleados
                          where e.Id == asignacion.adm_empleados_Id
                          select new { e.Nombres, e.Apellido_Paterno, e.Apellido_Materno }).FirstOrDefault();


            ViewBag.Nombres = nombres.Nombres + " " + nombres.Apellido_Paterno + " " + nombres.Apellido_Materno;

            return View(asignacion);
        }

        // GET: sis_asignacion
        public ActionResult Index()
        {
            var sis_asignacion = db.sis_asignacion.Include(s => s.sis_equipos);
            return View(sis_asignacion.ToList());
        }

        // GET: sis_asignacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion sis_asignacion = db.sis_asignacion.Find(id);

            if (sis_asignacion == null)
            {
                return HttpNotFound();
            }

            var nombres = (from e in dbAdmin.adm_empleados
                           where e.Id == sis_asignacion.adm_empleados_Id
                           select new { e.Nombres, e.Apellido_Paterno, e.Apellido_Materno }).FirstOrDefault();


            ViewBag.Nombres = nombres.Nombres + " " + nombres.Apellido_Paterno + " " + nombres.Apellido_Materno;

            var area = (from e in dbAdmin.adm_area
                        where e.Id == sis_asignacion.adm_area_Id
                        select new { e.Descripcion }).FirstOrDefault();

            ViewBag.area = area.Descripcion;

            var sucursal = (from e in dbAdmin.adm_sucursales
                        where e.Id == sis_asignacion.adm_sucursales_Id
                        select new { e.Descripcion }).FirstOrDefault();

            ViewBag.sucursal = sucursal.Descripcion;

            var cuenta = (from e in dbAdmin.adm_cuentas
                            where e.Id == sis_asignacion.adm_cuentas_Id
                            select new { e.Descripcion }).FirstOrDefault();

            ViewBag.cuenta = cuenta.Descripcion;

            ViewBag.status = sis_asignacion.sis_equipos.sis_estatusequipo.Descripcion;

            return View(sis_asignacion);
        }

        // GET: sis_asignacion/Create
        public ActionResult Create()
        {
            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Numero_Serie");
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres");
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion");
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion");
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion");

            return View();
        }

        // POST: sis_asignacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_equipos_Id,adm_cuentas_Id")] sis_asignacion sis_asignacion)
        {
            if (ModelState.IsValid)
            {
                db.sis_asignacion.Add(sis_asignacion);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Modelo", sis_asignacion.sis_equipos_Id);
            ViewBag.adm_empleados_Id = new SelectList(db.sis_equipos, "Id", "Nombres", sis_asignacion.adm_empleados_Id);
            ViewBag.adm_area_Id = new SelectList(db.sis_equipos, "Id", "Descripcion", sis_asignacion.adm_area_Id);
            ViewBag.adm_sucursales_Id = new SelectList(db.sis_equipos, "Id", "Descripcion", sis_asignacion.adm_sucursales_Id);
            ViewBag.adm_cuentas_Id = new SelectList(db.sis_equipos, "Id", "Descripcion", sis_asignacion.adm_cuentas_Id);
            


            return Json("Error", JsonRequestBehavior.AllowGet);
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

        // GET: sis_asignacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion sis_asignacion = db.sis_asignacion.Find(id);
            if (sis_asignacion == null)
            {
                return HttpNotFound();
            }


            

            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Numero_Serie", sis_asignacion.sis_equipos_Id);
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres", sis_asignacion.adm_empleados_Id);
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion", sis_asignacion.adm_area_Id);
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion", sis_asignacion.adm_sucursales_Id);
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion", sis_asignacion.adm_cuentas_Id);

            return View(sis_asignacion);
        }

        // POST: sis_asignacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,adm_empleados_Id,adm_area_Id,adm_sucursales_Id,sis_equipos_Id,adm_cuentas_Id")] sis_asignacion sis_asignacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_asignacion).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Numero_Serie", sis_asignacion.sis_equipos_Id);
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion", sis_asignacion.adm_area_Id);
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion", sis_asignacion.adm_cuentas_Id);
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres", sis_asignacion.adm_empleados_Id);
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion", sis_asignacion.adm_sucursales_Id);

            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_asignacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion sis_asignacion = db.sis_asignacion.Find(id);
            if (sis_asignacion == null)
            {
                return HttpNotFound();
            }

            sis_asignacion.NombreEmpleado = dbAdmin.adm_empleados.Where(x => x.Id.Equals(sis_asignacion.adm_empleados_Id)).FirstOrDefault().Nombres;
            sis_asignacion.Area = dbAdmin.adm_area.Where(x => x.Id.Equals(sis_asignacion.adm_area_Id)).FirstOrDefault().Descripcion;

            string ns = db.sis_equipos.Where(x => x.Id.Equals(sis_asignacion.sis_equipos_Id)).FirstOrDefault().Numero_Serie;
            string modelo = db.sis_equipos.Where(x => x.Id.Equals(sis_asignacion.sis_equipos_Id)).FirstOrDefault().Modelo;

            sis_asignacion.NumeroSerie = modelo  + " con numero de serie: " + ns;
            sis_asignacion.Sucursal = dbAdmin.adm_sucursales.Where(x => x.Id.Equals(sis_asignacion.adm_sucursales_Id)).FirstOrDefault().Descripcion;

            return View(sis_asignacion);
        }

        // POST: sis_asignacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_asignacion sis_asignacion = db.sis_asignacion.Find(id);
            db.sis_asignacion.Remove(sis_asignacion);
            db.SaveChanges();
            return Json("Correcto", JsonRequestBehavior.AllowGet);
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
