using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        
        public ActionResult Index()
        {
            ViewBag.data = "welcome to page....";
           
            return View();
        }
       //[HttpPost]
       // public ActionResult Index(string name)
       // {
       //     ViewBag.Message = String.Format("Hello{0}.\\ncurrent Date and time:{1}", name, DateTime.Now.ToString());
       //     return View();
       // }  

    }
}