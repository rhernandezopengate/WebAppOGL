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
using WebAppOGL.Entities.OrdenesCompra;
using WebAppOGL.Entities.Sistemas;
using System.Linq.Dynamic;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_asignacion_terminalesController : Controller
    {
        private db_a3f19c_administracionEntities1 dbAdmin = new db_a3f19c_administracionEntities1();
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();
        private db_a3f19c_administracionEntities2 dbOC = new db_a3f19c_administracionEntities2();

        public ActionResult AsiganacionTerminalesParcial() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerAsignacionTerminales()
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

                List<sis_asignacion_terminales> lista = new List<sis_asignacion_terminales>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_AsignacionTerminales_Index @nombreempleado";
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
                            var asignacion = new sis_asignacion_terminales();

                            asignacion.Id = Convert.ToInt32(dr["Id"]);
                            asignacion.Area = dr["Area"].ToString();
                            asignacion.Empleado = dr["Empleado"].ToString();
                            asignacion.Sucursal = dr["Sucursal"].ToString();
                            asignacion.Cuenta = dr["Cuenta"].ToString();
                            asignacion.Modelo = dr["Modelo"].ToString() + " - " +dr["NumeroSerie"].ToString();                            

                            if (dr["FechaAlta"].ToString() != "")
                            {
                                asignacion.FechaAltaString = Convert.ToDateTime(dr["FechaAlta"].ToString()).Date.ToShortDateString();
                            }
                            else
                            {
                                asignacion.FechaAltaString = "";
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



        // GET: sis_asignacion_terminales
        public ActionResult Index()
        {
            return View(db.sis_asignacion_terminales.ToList());
        }

        // GET: sis_asignacion_terminales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_terminales sis_asignacion_terminales = db.sis_asignacion_terminales.Find(id);
            if (sis_asignacion_terminales == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_terminales);
        }

        // GET: sis_asignacion_terminales/Create
        public ActionResult Create()
        {
            ViewBag.sis_terminales_Id = new SelectList(db.sis_terminales, "Id", "NumeroSerie");
            ViewBag.adm_empleados_Id = new SelectList(dbAdmin.adm_empleados, "Id", "Nombres");
            ViewBag.adm_area_Id = new SelectList(dbAdmin.adm_area, "Id", "Descripcion");
            ViewBag.adm_sucursales_Id = new SelectList(dbAdmin.adm_sucursales, "Id", "Descripcion");
            ViewBag.adm_cuentas_Id = new SelectList(dbAdmin.adm_cuentas, "Id", "Descripcion");
            ViewBag.oc_proveedores_Id = new SelectList(dbOC.oc_proveedores, "Id", "NombreComercial");

            return View();
        }

        // POST: sis_asignacion_terminales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,sis_terminales_Id,adm_empleados_Id,adm_area_Id,adm_cuentas_Id,adm_sucursales_Id,oc_proveedores_Id")] sis_asignacion_terminales sis_asignacion_terminales)
        {
            if (ModelState.IsValid)
            {
                db.sis_asignacion_terminales.Add(sis_asignacion_terminales);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            return View(sis_asignacion_terminales);
        }

        // GET: sis_asignacion_terminales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_terminales sis_asignacion_terminales = db.sis_asignacion_terminales.Find(id);
            if (sis_asignacion_terminales == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_terminales);
        }

        // POST: sis_asignacion_terminales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,sis_terminales_Id,adm_empleados_Id,adm_area_Id,adm_cuentas_Id,adm_sucursales_Id,oc_proveedores_Id")] sis_asignacion_terminales sis_asignacion_terminales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_asignacion_terminales).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            return View(sis_asignacion_terminales);
        }

        // GET: sis_asignacion_terminales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_asignacion_terminales sis_asignacion_terminales = db.sis_asignacion_terminales.Find(id);
            if (sis_asignacion_terminales == null)
            {
                return HttpNotFound();
            }
            return View(sis_asignacion_terminales);
        }

        // POST: sis_asignacion_terminales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_asignacion_terminales sis_asignacion_terminales = db.sis_asignacion_terminales.Find(id);
            db.sis_asignacion_terminales.Remove(sis_asignacion_terminales);
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
