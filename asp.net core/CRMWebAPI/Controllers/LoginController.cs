using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMWebAPI.Repository;
using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;

namespace CRMWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        private ILogin _ILogin;
        public LoginController()
        {
            _ILogin = new LoginData();
        }
        public IHttpActionResult ValidateUser(LoginViewModel loginViewModel)
        {
            string username = loginViewModel.Username;
            string password = loginViewModel.Password;
            var result = _ILogin.ValidateUser(username, password);
            if(result==null)
            {
                return NotFound();
            }
            else
            {

                return Ok(result);
            }

        }
    }
}
