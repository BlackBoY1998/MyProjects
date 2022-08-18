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
        LoginEntities ldb;
        public UserController()
        {
            ldb = new LoginEntities();
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
                ////ldb.Users.Add(user);
                ////ldb.SaveChanges();
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
    
    }
}