using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.Sistemas;

namespace WebAppOGL.Controllers.Sistemas
{
    [Authorize]
    public class sis_dashboardController : Controller
    {
        // GET: sis_dashboard

        db_a3f19c_administracionEntities db = new db_a3f19c_administracionEntities();



        public ActionResult Index()
        {
            ViewBag.EquiposOG = db.sis_equipos.Where(x => x.sis_mantenimiento_Id == 2).Count();

            ViewBag.EquiposHLF = db.sis_equipos.Where(x => x.sis_mantenimiento_Id == 1).Count();

            ViewBag.CelularesOGL = db.sis_celulares.Count();

            return View();
        }

        public ActionResult Plantillas()
        {
            return View();
        }
    }
}