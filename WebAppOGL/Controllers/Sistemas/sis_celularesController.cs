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
using WebAppOGL.Entities.Sistemas;
using System.Linq.Dynamic;

namespace WebAppOGL.Controllers.Sistemas
{
    public class sis_celularesController : Controller
    {
        private db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();

        // GET: sis_celulares
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerCelulares()
        {
            try
            {
                var Draw = Request.Form.GetValues("draw").FirstOrDefault();
                var Start = Request.Form.GetValues("start").FirstOrDefault();
                var Length = Request.Form.GetValues("length").FirstOrDefault();
                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                var imei = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

                int PageSize = Length != null ? Convert.ToInt32(Length) : 0;
                int Skip = Start != null ? Convert.ToInt32(Start) : 0;
                int TotalRecords = 0;

                List<sis_celulares> lista = new List<sis_celulares>();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    string sql = "exec SP_Celulares_Index @imei";
                    var query = new SqlCommand(sql, con);

                    if (imei != "")
                    {
                        query.Parameters.AddWithValue("@imei", imei);
                    }
                    else
                    {
                        query.Parameters.AddWithValue("@imei", DBNull.Value);
                    }

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // facturas
                            var celular = new sis_celulares();

                            celular.Id = Convert.ToInt32(dr["id"]);
                            celular.Modelo = dr["Modelo"].ToString();
                            celular.Numero_Producto = dr["Numero_Producto"].ToString();
                            celular.IMEI = dr["IMEI"].ToString();
                            celular.Color = dr["Color"].ToString();
                            celular.Cargador = dr["Cargador"].ToString();
                            celular.OS = dr["OS"].ToString();
                            celular.Almacenamiento = dr["Almacenamiento"].ToString();
                            celular.RAM = dr["RAM"].ToString();
                            celular.Procesador = dr["Procesador"].ToString();
                            celular.Pantalla = dr["Pantalla"].ToString();
                            celular.Camara_Frontal = dr["Camara_Frontal"].ToString();
                            celular.Camara_Trasera = dr["Camara_Trasera"].ToString();
                            celular.MAC_Address = dr["MAC_Address"].ToString();

                            celular.Mantenimiento = dr["Estado"].ToString();
                            celular.Estado = dr["Mantenimiento"].ToString();
                            celular.Marca = dr["Marca"].ToString();

                            lista.Add(celular);
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

        // GET: sis_celulares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_celulares);
        }

        // GET: sis_celulares/Create
        public ActionResult Create()
        {
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion");
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion");
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion");
            return View();
        }

        // POST: sis_celulares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,Modelo,Numero_Producto,IMEI,Color,Cargador,OS,Almacenamiento,RAM,Procesador,Pantalla,Camara_Frontal,Camara_Trasera,sis_mantenimiento_Id,sis_estatusequipo_Id,sis_marcas_Id,MAC_Address")] sis_celulares sis_celulares)
        {
            if (ModelState.IsValid)
            {
                db.sis_celulares.Add(sis_celulares);
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }

            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_celulares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return View(sis_celulares);
        }

        // POST: sis_celulares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,Modelo,Numero_Producto,IMEI,Color,Cargador,OS,Almacenamiento,RAM,Procesador,Pantalla,Camara_Frontal,Camara_Trasera,sis_mantenimiento_Id,sis_estatusequipo_Id,sis_marcas_Id,MAC_Address")] sis_celulares sis_celulares)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sis_celulares).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Correcto", JsonRequestBehavior.AllowGet);
            }
            ViewBag.sis_estatusequipo_Id = new SelectList(db.sis_estatusequipo, "Id", "Descripcion", sis_celulares.sis_estatusequipo_Id);
            ViewBag.sis_mantenimiento_Id = new SelectList(db.sis_mantenimiento, "Id", "Descripcion", sis_celulares.sis_mantenimiento_Id);
            ViewBag.sis_marcas_Id = new SelectList(db.sis_marcas, "Id", "Descripcion", sis_celulares.sis_marcas_Id);
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        // GET: sis_celulares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            if (sis_celulares == null)
            {
                return HttpNotFound();
            }
            return View(sis_celulares);
        }

        // POST: sis_celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sis_celulares sis_celulares = db.sis_celulares.Find(id);
            db.sis_celulares.Remove(sis_celulares);
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
