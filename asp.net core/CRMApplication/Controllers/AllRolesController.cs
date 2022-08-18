using CRMApplication.ConsumeMethods;
using CRMApplication.Filters;
using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace CRMApplication.Controllers
{
    [ValidateSuperAdminSession]
    public class AllRolesController : Controller
    {
        IAssignRoles _IAssignRoles;

        private AllRolesConsume ObjAllRoles;
        public AllRolesController()
        {
            ObjAllRoles = new AllRolesConsume();
            _IAssignRoles = new AssignRolesData();
        }

        // GET: AllRoles
        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult LoadRolesData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IAssignRoles.ShowallRoles(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult RemovefromRole(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(RegistrationID))
                {
                    return RedirectToAction("Roles");
                }

                //var role = _IAssignRoles.RemovefromUserRole(RegistrationID);
                Task<bool> role= ObjAllRoles.RemovefromUserRole("RemovefromUserRole", RegistrationID);
                return Json(role.Result);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


    }
}