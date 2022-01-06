using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Sistemas;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_equiposController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_equipos
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerEquipos() 
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var nombreequipo = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<sis_equipos> lista = new List<sis_equipos>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_EquiposComputo_Index @nombreequipo";
                    var query = new SqlCommand(sql, con);

                    if (nombreequipo != "")
                    {
                        query.Parameters.AddWithValue("@nombreequipo", nombreequipo);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@nombreequipo", DBNull.Value);
                    }                   

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var equipos = new sis_equipos();

                            equipos.Id = Convert.ToInt32(dr["Id"]);                            
                            equipos.Modelo = dr["Modelo"].ToString();
                            equipos.Numero_Serie = dr["Numero_Serie"].ToString();
                            equipos.Numero_Parte = dr["Numero_Parte"].ToString();
                            equipos.Nombre_Equipo = dr["Nombre_Equipo"].ToString();

                            equipos.Estado = dr["Estado"].ToString();
                            equipos.Marca = dr["Marca"].ToString();
                            equipos.TipoEquipo = dr["TipoEquipo"].ToString();
                            equipos.Mantenimiento = dr["Mantenimiento"].ToString();

                            lista.Add(equipos);
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

        // GET: sis_equipos/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_equipos);
        }

        // GET: sis_equipos/Create
        public ActionResult Create()
        {
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion");
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion");
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");
            return View();
        }

        // POST: sis_equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_Alta,Fecha_Compra,Modelo,Numero_Serie,Numero_Parte,Nombre_Equipo,MAC_Ethernet,MAC_WiFi,CPU,RAM,STORAGE,sis_marcas_Id,sis_tipoequipos_Id,sis_mantenimiento_Id,Color,Cargador,sis_estatusequipo_Id,Pantalla")] sis_equipos sis_equipos)
        {
            if (ModelState.IsValid)
            {
                sis_equipos.Fecha_Alta = DateTime.Now;
                db.sis_equipos.Add(sis_equipos);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_equipos.sis_estatusequipo_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_equipos.sis_estatusequipo_Id);
            return View(sis_equipos);
        }

        // POST: sis_equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_Alta,Fecha_Compra,Modelo,Numero_Serie,Numero_Parte,Nombre_Equipo,MAC_Ethernet,MAC_WiFi,CPU,RAM,STORAGE,sis_marcas_Id,sis_tipoequipos_Id,sis_mantenimiento_Id,Color,Cargador,sis_estatusequipo_Id,Pantalla")] sis_equipos sis_equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_equipos).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_equipos.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_equipos.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_equipos.sis_tipoequipos_Id);
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_equipos.sis_estatusequipo_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            if (sis_equipos == null)
            {
                return HttpNotFound();
            }
            return View(sis_equipos);
        }

        // POST: sis_equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_equipos sis_equipos = db.sis_equipos.Find(id);
            db.sis_equipos.Remove(sis_equipos);
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
