using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    public class oc_ordenesdecomprasController : Controller
    {
        // GET: oc_ordenesdecompras
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}