using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOGL.Entities.OrdenesCompra;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    public class oc_ordenesdecomprasController : Controller
    {
        db_a3f19c_administracionEntities2 db = new db_a3f19c_administracionEntities2();
        // GET: oc_ordenesdecompras
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InsertarOC(List<detalleproductos> detalleproductos)
        {
            using (db_a3f19c_administracionEntities2 entities = new db_a3f19c_administracionEntities2())
            {
                //Truncate Table to delete all old records.
                //entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

                //Check for NULL.
                //if (customers == null)
                //{
                //    customers = new List<Customer>();
                //}

                //Loop and insert records.
                foreach (var items in detalleproductos)
                {
                    Console.WriteLine(items.codigo + " " + items.producto);                   
                }

                //int insertedRecords = entities.SaveChanges();
                return Json("Hola");
            }
        }
    }
}