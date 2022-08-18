using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectUsingajaxandJquery.Models;
using System.Threading;

namespace ProjectUsingajaxandJquery.Controllers
{
    public class UserController : Controller
    {
        UserOperation u = new UserOperation();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListofUser()
        {
            Thread.Sleep(4000);
            return Json(u.ListAllusers(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUser(User user)
        {
            Thread.Sleep(4000);
            return Json(u.AddUser(user), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(User user)
        {
            Thread.Sleep(4000);
            return Json(u.updateuser(user), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int Id)
        {
            return Json(u.deleteuser(Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult details(int Id)
        {
            var user = u.ListAllusers().Find(x => x.Id.Equals(Id));
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}