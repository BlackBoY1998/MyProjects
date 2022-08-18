using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DempProject.DataAccessLayer;

namespace DempProject.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult BookAppointment()
        {
            return View();
        }
        public ActionResult Product()
        {
            ProductOperations p = new ProductOperations();
            var pro = p.ListAllProduct();
            ViewBag.data = pro;
            return View();
        }
    }
}