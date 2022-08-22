using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppOGL.Controllers.OrdenesServicio
{
    public class os_dashboardController : Controller
    {
        // GET: os_dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FacturasOS()
        {
            return View();
        }
    }
}