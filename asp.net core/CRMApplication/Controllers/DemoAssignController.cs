using CRMApplication.ConsumeMethods;
using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRMApplication.Controllers
{
    public class DemoAssignController : Controller
    {
        private IAssignRoles _IAssignRoles;
        private DemoAssignConsume ObjDemoConsume;
        public DemoAssignController()
        {
            _IAssignRoles = new AssignRolesData();
            ObjDemoConsume = new DemoAssignConsume();
        }
        // GET: DemoAssign
        public ActionResult Index()
        {
            try
            {
                AssignRolesModel assignRolesModel = new AssignRolesModel();
                //assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                //assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                assignRolesModel.ListofAdmins = ObjDemoConsume.ListofAdmins("ListofAdmins");
                assignRolesModel.ListofUser = ObjDemoConsume.GetListofUnAssignedUsers("GetListofUnAssignedUsers");
                return View(assignRolesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(List<UserModel> list ,AssignRolesModel assignRolesModel)
        {
            try
            {
                if (assignRolesModel.ListofUser == null)
                {
                    TempData["MessageErrorRoles"] = "There are no Users to Assign Roles";
                    //assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                    //assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    assignRolesModel.ListofAdmins = ObjDemoConsume.ListofAdmins("ListofAdmins");
                    assignRolesModel.ListofUser = ObjDemoConsume.GetListofUnAssignedUsers("GetListofUnAssignedUsers");
                    return View(assignRolesModel);
                }

                if (ModelState.IsValid)
                {
                    assignRolesModel.CreatedBy = Convert.ToInt32(Session["SuperAdmin"]);
                    //_IAssignRoles.SaveAssignedRoles(assignRolesModel);
                    ObjDemoConsume.SaveAssignedRoles("SaveAssignedRoles", assignRolesModel);
                    TempData["MessageRoles"] = "Roles Assigned Successfully!";
                }

                assignRolesModel = new AssignRolesModel();
                //assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                //assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                assignRolesModel.ListofAdmins = ObjDemoConsume.ListofAdmins("ListofAdmins");
                assignRolesModel.ListofUser = ObjDemoConsume.GetListofUnAssignedUsers("GetListofUnAssignedUsers");

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}