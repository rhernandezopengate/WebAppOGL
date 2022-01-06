using System;
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

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_asignacionController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities1 dbAdmin = new db_a3f19c_administracionEntities1();


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

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<AsignacionEquiposViewModel> lista = new List<AsignacionEquiposViewModel>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_AsignacionEquipos_Index @nombreempleado";
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
                            var asignacion = new AsignacionEquiposViewModel();

                            asignacion.Id = Convert.ToInt32(dr["Id"]);
                            asignacion.Area = dr["Area"].ToString();
                            asignacion.NombreEmpleado = dr["Nombres"].ToString();
                            asignacion.Sucursal = dr["Sucursal"].ToString();
                            asignacion.Cuenta = dr["Cuenta"].ToString();
                            asignacion.Nombreequipo = dr["Nombre_Equipo"].ToString();

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

            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Modelo", sis_asignacion.sis_equipos_Id);
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
            ViewBag.sis_equipos_Id = new SelectList(db.sis_equipos, "Id", "Modelo", sis_asignacion.sis_equipos_Id);
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
