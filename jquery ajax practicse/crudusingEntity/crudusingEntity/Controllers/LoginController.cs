using crudusingEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudusingEntity.Controllers
{
    public class LoginController : Controller
    {
        RegEntities re = new RegEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Login(string username, string password)
        {
            var user = from data in re.LoginTables where data.Name == username && data.Password == password select data;
            if(user.Count() >0)
            {
                return Json(new { Success =true},JsonRequestBehavior.AllowGet);
            }
            else{

                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }

        } 
 
        [HttpPost]
        public ActionResult LoadPhoneType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for(int i=0;i<2;i++)
            {
                list.Add(new SelectListItem
                {
                   Value=re.Ptypes.ToList()[i].id.ToString(),
                   Text=re.Ptypes.ToList()[i].Ptypes


                });
            }
            return Json(list);
        }
    }
}