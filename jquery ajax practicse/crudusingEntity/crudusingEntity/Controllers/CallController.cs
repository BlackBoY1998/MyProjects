using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudusingEntity.Controllers
{
    public class CallController : Controller
    {
        CallBalanceClopsEntities db = new CallBalanceClopsEntities();
        // GET: Call
        public ActionResult Index()
        {
            List<LocalIVRFlow> lstLocalIVRFlow = new List<LocalIVRFlow>();
            // lstLocalIVRFlow =  (from item in db.LocalIVRFlow select item).ToList<LocalIVRFlow>();
            lstLocalIVRFlow = (from item in db.LocalIVRFlows
                               join cg in db.Campaigns on item.CampaignID equals cg.ID
                               select new
                               {
                                   item.FlowName,
                                   item.FlowDisplayName,
                                   cg.Name,
                               }).ToList<LocalIVRFlow>();

            LocalIVRFlow obj = new LocalIVRFlow();
            var tuple = new Tuple<LocalIVRFlow, IEnumerable<LocalIVRFlow>>(obj, lstLocalIVRFlow);

          
            return View(tuple);
        }
    }
}