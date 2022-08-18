using crudusingEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace crudusingEntity.Controllers
{
    public class LeadController : Controller
    {
        RegEntities re = new RegEntities();
        LeadManagements L;
        // GET: Lead
        public ActionResult Index()
        {
             var leads = re.LeadManagements;
            List<LeadManagements> list=new List<LeadManagements>();
            //list.Add(leads);
           
            var model = new LeadManagementViewModel
            {
                //Leads=,
                //Leads = re.LeadManagement.ToList()
            };

            return View(model);
        }
    }
}