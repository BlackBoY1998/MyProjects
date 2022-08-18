using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapiusingsp.Models;

namespace Webapiusingsp.Controllers
{
    public class UserController : Controller
    {
        UserEntities ldb;
        
        public UserController()
        {
            ldb = new UserEntities();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Userclass user)
        {
            if (ModelState.IsValid)
            {
                var v = ldb.Users.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (v != null)
                {
                    return RedirectToAction("Index","StudentUI");
                }
                else
                {
                    ModelState.AddModelError("Password", "Please check username and Password");
                    return View(user);
                }
            }
            return View(user);

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Getusers()
        {
            var add = ldb.Usertsp(0, "", "", "", "",DateTime.UtcNow, "", "", "", "", "", "Get").ToList();
            ldb.SaveChanges();
            return Json(add, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(Userclass user)
        {
            var Insert = ldb.Usertsp(0, user.Name, user.UserName, user.Password, user.Gender, user.DOB, user.EmailId, user.Mobile, user.Landline, user.Address, user.Pincode, "Insert").FirstOrDefault();
            return View(Insert);
        }
        [HttpPost]
        public ActionResult Edit(Userclass user)
        {
            ldb.Usertsp(user.Id, user.Name, user.UserName, user.Password, user.Gender, user.DOB, user.EmailId, user.Mobile, user.Landline, user.Address, user.Pincode, "Update");
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ldb.Usertsp(id, "", "", "", "", null, "", "", "", "", "", "Delete");
            return new EmptyResult();
        }
    
    }
}