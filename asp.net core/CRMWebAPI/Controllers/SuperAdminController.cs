using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMWebAPI.Repository;
using CRMModelClassLiberary;
using Newtonsoft.Json;

namespace CRMWebAPI.Controllers
{
    public class SuperAdminController : ApiController
    {
        private IUsers _IUsers;
        private IProject _IProject;
        private IRegistration _IRegistration;
        private IRoles _Roles;
        private IAssignRoles _IAssignRoles;
        public SuperAdminController()
        {
            _IProject = new ProjectData();
            _IUsers = new UsersData();
            _IRegistration = new RegistrationData();
            _Roles = new RolesData();
            _IAssignRoles = new AssignRolesData();
        }
        public IHttpActionResult GetAdminCount()
        {
            var admincount = _IUsers.GetTotalAdminsCount();
            return Ok(admincount);
        }
        public IHttpActionResult GetTotalUsersCount()
        {
            var userscount = _IUsers.GetTotalUsersCount();
            return Ok(userscount);
        }
        public IHttpActionResult GetTotalProjectsCounts()
        {
            var projectcount = _IProject.GetTotalProjectsCounts();
            return Ok(projectcount);
        }
        public IHttpActionResult CheckUserNameExists(string username)
        {
            bool isUsernameExists = _IRegistration.CheckUserNameExists(username);
            return Ok(isUsernameExists);
        }

        [HttpPost]
        public IHttpActionResult AddUser(Registration registration)
        {
            var insert=_IRegistration.AddUser(registration);
            return Ok(insert);
        }

        [HttpGet]
        public IHttpActionResult GetRolesofUserbyRolename(string Role)
        {
           int role= _Roles.getRolesofUserbyRolename(Role);
            return Ok(role);
        }

        [HttpGet]
        public IHttpActionResult AssignRoles()
        {
            AssignRolesModel assignRolesModel = new AssignRolesModel();
            assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
            assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
            return Ok(assignRolesModel);
        }
        [HttpPost]
        public IHttpActionResult GetAssignRoles(AssignRolesModel objassign)
        {
            objassign.ListofAdmins = _IAssignRoles.ListofAdmins();
            objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
            return Ok(objassign);
        }
        [HttpPost]
        public IHttpActionResult SaveAssignedRoles(AssignRolesModel assignRolesModel) 
        {
          bool data =_IAssignRoles.SaveAssignedRoles(assignRolesModel);
          return Ok(data);
        }
    }
}
