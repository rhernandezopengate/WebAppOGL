using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Administracion;
using WebAppOGL.Entities.OrdenesServicio;

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


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreatePost(os_ordenesservicio orden)
        {
            try
            {
                os_ordenesservicio ordenesservicio = new os_ordenesservicio();
                
                var user = User.Identity.GetUserId();
                string email = db1.AspNetUsers.Where(x => x.Id == user).Select(x => x.Email).FirstOrDefault().ToString();
                int empleado = db1.adm_empleados.Where(x => x.Email.Contains(email)).FirstOrDefault().Id;
                
                ordenesservicio.adm_empleados_Id = empleado;
                ordenesservicio.os_statuscompras_Id = 1;
                ordenesservicio.adm_cuentas_Id = orden.adm_cuentas_Id;
                ordenesservicio.oc_proveedores_Id = orden.oc_proveedores_Id;
                ordenesservicio.os_rutas_Id = orden.os_rutas_Id;                
                ordenesservicio.Folio = "OS-OGL-" + (db.os_ordenesservicio.Count() + 1);
                ordenesservicio.FechaAlta = DateTime.Now;
                ordenesservicio.FechaSolicitud = orden.FechaSolicitud;
                ordenesservicio.Semana = GetWeekNumber((DateTime)orden.FechaSolicitud);
                ordenesservicio.CostoVenta = orden.CostoVenta;
                ordenesservicio.Observaciones = orden.Observaciones;

                db.os_ordenesservicio.Add(ordenesservicio);
                
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