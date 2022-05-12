using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    [Authorize]
    public class oc_dashboardController : Controller
    {
        db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();
        
        // GET: oc_dashboard
        public ActionResult Index()
        {
            ViewBag.OrdenesPendientes = db.oc_ordenescompras.Where(x => x.oc_statuscompras_Id == 1).Count();

            return View();
        }
    }
}