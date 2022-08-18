using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRMWebAPI.Controllers
{
    public class DemoAssignController : ApiController
    {
        private IAssignRoles _IAssignRoles;
        public DemoAssignController()
        {
            _IAssignRoles = new AssignRolesData();
        }

        [HttpGet]
        public IHttpActionResult ListofAdmins()
        {
           var list= _IAssignRoles.ListofAdmins();
           return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetListofUnAssignedUsers()
        {
          var list= _IAssignRoles.GetListofUnAssignedUsers();
          return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult SaveAssignedRoles(AssignRolesModel AssignRolesModel)
        {
            bool result= _IAssignRoles.SaveAssignedRoles(AssignRolesModel);
            return Ok(result);
        }
    }
}
