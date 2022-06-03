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
    public class sis_terminalesController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();


        // GET: sis_terminales
        [Authorize(Roles = "sistemas")]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerTerminales()
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var numeroserie = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<sis_terminales> lista = new List<sis_terminales>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_Terminales_Index @numeroserie";
                    var query = new SqlCommand(sql, con);

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
                            var equipos = new sis_terminales();

                            equipos.Id = Convert.ToInt32(dr["Id"]);
                            equipos.Modelo = dr["Modelo"].ToString();
                            equipos.NumeroSerie = dr["NumeroSerie"].ToString();
                            equipos.MAC = dr["MAC"].ToString();

                            equipos.Estado = dr["Estado"].ToString();
                            equipos.Marca = dr["Marca"].ToString();
                            equipos.TipoEquipo = dr["TipoEquipo"].ToString();

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


        // GET: sis_terminales/Details/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_terminales sis_terminales = db.sis_terminales.Find(id);
            if (sis_terminales == null)
            {
                return HttpNotFound();
            }
            return View(sis_terminales);
        }

        // GET: sis_terminales/Create
        [Authorize(Roles = "sistemas")]
        public ActionResult Create()
        {
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion");
            return View();
        }

        // POST: sis_terminales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Modelo,MAC,NumeroSerie,sis_marcas_Id,sis_estatusequipo_Id,sis_tipoequipos_Id")] sis_terminales sis_terminales)
        {
            if (ModelState.IsValid)
            {
                db.sis_terminales.Add(sis_terminales);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_terminales.sis_estatusequipo_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_terminales.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_terminales.sis_tipoequipos_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_terminales/Edit/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_terminales sis_terminales = db.sis_terminales.Find(id);
            if (sis_terminales == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_terminales.sis_estatusequipo_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_terminales.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_terminales.sis_tipoequipos_Id);
            return View(sis_terminales);
        }

        // POST: sis_terminales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Modelo,MAC,NumeroSerie,sis_marcas_Id,sis_estatusequipo_Id,sis_tipoequipos_Id")] sis_terminales sis_terminales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_terminales).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_terminales.sis_estatusequipo_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_terminales.sis_marcas_Id);
            ViewBag.sis_tipoequipos_Id = new SelectList(db.sis_tipoequipos, "Id", "Descripcion", sis_terminales.sis_tipoequipos_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_terminales/Delete/5
        [Authorize(Roles = "sistemas")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_terminales sis_terminales = db.sis_terminales.Find(id);
            if (sis_terminales == null)
            {
                return HttpNotFound();
            }
            return View(sis_terminales);
        }

        // POST: sis_terminales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_terminales sis_terminales = db.sis_terminales.Find(id);
            db.sis_terminales.Remove(sis_terminales);
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
