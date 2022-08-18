using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudusingEntity.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace crudusingEntity.Controllers
{
    public class RegController : Controller
    {
        RegEntities re = new RegEntities();
        // GET: Reg


        public ActionResult Demo()
        {
            return RedirectToAction("Index");
        }
        public ActionResult Index(int? page)
        {
            try
            {
                var query = re.Registrations.ToList();
                ExceptionLogging.Logger("Sucesss Entry");
                return View(query.ToList().ToPagedList(page ?? 1, 1));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Index");
            }   
            
        }
        public ActionResult Details(int id)
        {
            try
            {
                string ID =Request.QueryString["id"].ToString();
                Registration obj = re.Registrations.Find(id);
                SessionEncDec sec = new SessionEncDec();
                var q=sec.EncSession("Test");
                var p = sec.DecSessionToArray(q);
                ExceptionLogging.Logger("Sucesss Entry");
                if (obj == null)
                {
                    ExceptionLogging.Logger("Details Not Found");
                    return HttpNotFound();
                }
                return View(obj);
            }
            catch(Exception e)
            {

                ExceptionLogging.SendErrorToText(e);
                return View("Index");
                
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Registration obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    re.Registrations.Add(obj);
                    re.SaveChanges();
                    ExceptionLogging.Logger("Sucesss Created Account");
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
              
            }
            return View();
            
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Registration obj = re.Registrations.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Registration obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    re.Entry(obj).State = EntityState.Modified;
                    re.SaveChanges();
                    ExceptionLogging.Logger("Sucesss Edited Account");
                    return RedirectToAction("Index");
                }

                return View(obj);
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Registration obj = re.Registrations.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            try
            {
                Registration reg = re.Registrations.Find(Id);
                re.Registrations.Remove(reg);
                re.SaveChanges();
                ExceptionLogging.Logger("Sucesssfully Deleted Account");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return View();
        }
        public ActionResult checkBox()
        {
             List<SelectListItem> stateNames = new List<SelectListItem>();  
            List<Zone> Zones = re.Zones.ToList();
            Zones.ForEach(x =>
            {
                stateNames.Add(new SelectListItem { Text = x.zoneName, Value = x.zoneId.ToString() });
            });
            StateAndZonecs sz = new StateAndZonecs();
            sz.StateNames = stateNames;
            return View(sz);  
            //if(state!=null)
            //{
            //    ViewBag.data = state;
            //}
            //return View()
        }

        [HttpPost]  
        public ActionResult GetDistrict(string ZoneId)  
        {  
            int statId;  
            List<SelectListItem> districtNames = new List<SelectListItem>();  
            if (!string.IsNullOrEmpty(ZoneId))  
            {  
                statId = Convert.ToInt32(ZoneId);  
            List<State> districts = re.States.Where(x => x.ZoneID == statId).ToList();  
                districts.ForEach(x =>  
                {  
                    districtNames.Add(new SelectListItem { Text = x.StateName, Value = x.StateID.ToString() });  
                });  
            }  
            return Json(districtNames, JsonRequestBehavior.AllowGet);  
        }
        //public JsonResult fetchzone(int id)
        //{
        // var Zone = re.Zones.Where(x => x.zoneId == id);
        // return Json(Zone, JsonRequestBehavior.AllowGet);
        //}
  
    }  
       
}