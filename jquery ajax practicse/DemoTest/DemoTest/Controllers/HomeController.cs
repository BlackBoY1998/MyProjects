using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoTest.Models;

namespace DemoTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        CallBalanceClopsEntities cb = new CallBalanceClopsEntities();
        
        public ActionResult Index()
        {

            //List<LocalIVRFlow> lstLocalIVRFlow = new List<LocalIVRFlow>();
            //// lstLocalIVRFlow =  (from item in db.LocalIVRFlow select item).ToList<LocalIVRFlow>();
            //lstLocalIVRFlow = (from item in cb.LocalIVRFlows select item).ToList<LocalIVRFlow>();

            //LocalIVRFlow obj = new LocalIVRFlow();
            //var tuple = new Tuple<LocalIVRFlow, IEnumerable<LocalIVRFlow>>(obj, lstLocalIVRFlow);
            var data = cb.LocalIVRFlows.ToList();

            return View(data);
        }
    }
}