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
    public class UserProfileController : ApiController
    {
        ILogin _ILogin;
        public UserProfileController()
        {
            _ILogin = new LoginData();
        }
        [HttpGet]
        public IHttpActionResult GetPasswordbyUserID(int UserID)
        {
            string result= _ILogin.GetPasswordbyUserID(UserID);
            return Ok(result);
        }
        [HttpGet]
        public IHttpActionResult UpdatePassword(string NewPassword,int UserID)
        {
            bool result= _ILogin.UpdatePassword(NewPassword, UserID);
            return Ok(result);
        }

    }
}
