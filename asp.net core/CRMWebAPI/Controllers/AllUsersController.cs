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
    public class AllUsersController : ApiController
    {
        private IUsers _IUsers;
        public AllUsersController()
        {
            _IUsers = new UsersData();
        }
        [HttpGet]
        public IHttpActionResult GetUserDetailsByRegistrationID(int? RegistrationID)
        {
            var userDetailsResponse= _IUsers.GetUserDetailsByRegistrationID(RegistrationID);
            return Ok(userDetailsResponse);
        }
        [HttpGet]
        public IHttpActionResult GetAdminDetailsByRegistrationID(int? RegistrationID)
        {
            var userDetailsResponse = _IUsers.GetAdminDetailsByRegistrationID(RegistrationID);
            return Ok(userDetailsResponse);
        }
    }
}
