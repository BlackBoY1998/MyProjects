using CRMApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CRMModelClassLiberary;
using CRMApplication.ConsumeMethods;
using CRMWebAPI.Interfaces;
using CRMApplication.Library;
using CRMApplication.Filters;
using System.Threading.Tasks;

namespace CRMApplication.Controllers
{
    [ValidateSuperAdminSession]
    public class SuperAdminController : Controller
    {
       private SuperAdminConsume objSuperAdmin;
        ICacheManager _ICacheManager;
        public SuperAdminController()
        {
            objSuperAdmin = new SuperAdminConsume();
            _ICacheManager = new CacheManager();
        }
        // GET: SuperAdminUI
        public ActionResult Dashboard()
        {
            Task<int> AdminCount = objSuperAdmin.GetCount("GetAdminCount", "AdminCount");
            ViewBag.AdminCount = AdminCount.Result;
            Task<int> UsersCount = objSuperAdmin.GetCount("GetTotalUsersCount", "UsersCount");
            ViewBag.UsersCount = UsersCount.Result;
            Task<int> ProjectCount = objSuperAdmin.GetCount("GetTotalProjectsCounts", "ProjectCount");
            ViewBag.ProjectCount = ProjectCount.Result;
            return View();
        }
        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View(new Registration());
        }
        
        public ActionResult CreateAdmin(Registration registration)
        {
            try
            {
                Task<bool> isUsernameExists = objSuperAdmin.CheckUserNameExists("CheckUserNameExists", registration.Username);
                if(isUsernameExists.Result==true)
                {
                    ModelState.AddModelError("", errorMessage: "Username Already Used try unique one!");
                }
                else
                {
                    registration.CreatedOn = DateTime.Now;
                    Task<int> RoleID = objSuperAdmin.GetRole("GetRolesofUserbyRolename", "Admin");
                    registration.RoleID = RoleID.Result;
                    //registration.RoleID = _IRoles.getRolesofUserbyRolename("Admin");
                    registration.Password = EncryptionLibrary.EncryptText(registration.Password);
                    registration.ConfirmPassword = EncryptionLibrary.EncryptText(registration.ConfirmPassword);
                    Task<int> add = objSuperAdmin.AddUser("AddUser", registration);
                    if (add.Result > 0)
                    {
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        return RedirectToAction("CreateAdmin");
                    }
                    else
                    {
                        return View("CreateAdmin", registration);
                    }
                }
                return RedirectToAction("Dashboard");
            }
            catch 
            {
                throw;
                //return View();
            }
        }
        [HttpGet]
        public ActionResult AssignRoles()
        {
            try
            {
                AssignRolesModel assignRolesModel;
                assignRolesModel = objSuperAdmin.AssignRoles("AssignRoles");
                return View(assignRolesModel);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AssignRoles(AssignRolesModel objassign)
        {
            try
            {
                    if (objassign.ListofUser == null)
                    {
                        TempData["MessageErrorRoles"] = "There are no Users to Assign Roles";
                        objassign = objSuperAdmin.AssignRole("GetAssignRoles", objassign);
                        return View(objassign);
                    }


                    var SelectedCount = (from User in objassign.ListofUser
                                 where User.selectedUsers == true
                                 select User).Count();

                if (SelectedCount == 0)
                {
                    TempData["MessageErrorRoles"] = "You have not Selected any User to Assign Roles";
                   
                    objassign = objSuperAdmin.AssignRole("GetAssignRoles", objassign);
                    return View(objassign);
                }

                if (ModelState.IsValid)
                {
                    objassign.CreatedBy = Convert.ToInt32(Session["SuperAdmin"]);
                    objSuperAdmin.SaveAssignedRoles("SaveAssignedRoles", objassign);
                    TempData["MessageRoles"] = "Roles Assigned Successfully!";
                }

                objassign = new AssignRolesModel();
         
                objassign = objSuperAdmin.AssignRole("GetAssignRoles", objassign);

                return RedirectToAction("AssignRoles");
            }
            catch (Exception)
            {
                throw;
            }
        }
         
    }
}