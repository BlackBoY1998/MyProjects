using CRMApplication.Library;
using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMApplication.ConsumeMethods;
using System.Threading.Tasks;
using CRMApplication.Filters;

namespace CRMApplication.Controllers
{
    [ValidateUserSession]
    public class UserProfileController : Controller
    {
        private UserProfileConsume objUser;
        public UserProfileController()
        {
            objUser = new UserProfileConsume();
        }
        // GET: UserProfile
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel changepasswordmodel)
        {
            try
            {
                var password = EncryptionLibrary.EncryptText(changepasswordmodel.OldPassword);

                Task<string> storedPassword = objUser.GetPasswordbyUserID("GetPasswordbyUserID", Convert.ToInt32(Session["UserID"]));

                if (storedPassword.Result.ToString() ==  password)
                {
                    Task<bool> result = objUser.UpdatePassword("UpdatePassword", EncryptionLibrary.EncryptText(changepasswordmodel.NewPassword), Convert.ToInt32(Session["UserID"]));

                    if (Convert.ToBoolean(result.Result))
                    {
                        ModelState.Clear();
                        ViewBag.message = "Password Changed Successfully";
                        return View(changepasswordmodel);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something Went Wrong Please try Again after some time");
                        return View(changepasswordmodel);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Entered Wrong Old Password");
                    return View(changepasswordmodel);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}