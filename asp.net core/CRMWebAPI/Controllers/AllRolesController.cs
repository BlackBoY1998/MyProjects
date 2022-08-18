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
    public class AllRolesController : ApiController
    {
        IAssignRoles _IAssignRoles;
        public AllRolesController()
        {
            _IAssignRoles = new AssignRolesData();
        }
        public IHttpActionResult RemovefromUserRole(string RegistrationID)
        {
            var role=  _IAssignRoles.RemovefromUserRole(RegistrationID);
            return Ok(role);
        }
    }
}
