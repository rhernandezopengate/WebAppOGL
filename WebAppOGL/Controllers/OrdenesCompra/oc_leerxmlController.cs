using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebAppOGL.Entities.Sistemas;

namespace WebAppOGL.Controllers.OrdenesCompra
{
    public class oc_leerxmlController : Controller
    {
        // GET: oc_leerxml

        public ActionResult Index()
        {
            if (TempData["employeeData"] == null)
            {
                ViewBag.ShowList = false;
                return View();
            }
            else
            {
                List<XML> empList = (List<XML>)TempData["employeeData"];
                ViewBag.ShowList = true;
                return View(empList);
            }
        }

        [HttpPost]
        public ActionResult UploadXML()
        {
            try
            {
                List<XML> empList = new List<XML>();
                var xmlFile = Request.Files[0];
                if (xmlFile != null && xmlFile.ContentLength > 0)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(xmlFile.InputStream);
                    XmlNodeList empNodes = xmlDocument.SelectNodes("Employee/emp");
                    foreach (XmlNode emp in empNodes)
                    {
                        empList.Add(new XML()
                        {
                            Id = Convert.ToInt32(emp["id"].InnerText),
                            Name = emp["name"].InnerText,
                            Gender = emp["gender"].InnerText,
                            Mobile = emp["mobile"].InnerText
                        });
                    }
                    TempData["employeeData"] = empList;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

    }
}