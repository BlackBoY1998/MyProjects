using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using JWT.Models;
using JWT.Models.VM;
 

namespace WMS.Controllers
{
    [RoutePrefix("Api/Login")]
    public class LoginController : ApiController
    {
        UserEntities user = new UserEntities();
        //[Route("UserLogin")]
        //[HttpPost]
        //public ResponseVM UserLogin(Login objVM)
        //{

        //    var objlst = user.Users(objVM.UserName, UtilityVM.Encryptdata(objVM.Passward), "").ToList<Usp_Login_Result>().FirstOrDefault();
        //    if (objlst.Status == -1)
        //        return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
        //    //if (objlst.Status == 2)
        //    //    return new ResponseVM { Status = "Invalid", Message = "Already Logged In." };
        //    if (objlst.Status == 0)
        //        return new ResponseVM { Status = "Inactive", Message = "User Inactive." };
        //    else
        //        return new ResponseVM { Status = "Success", Message = TokenManager.GenerateToken(objVM.UserName) };
        //}
        //[Route("Validate")]
        //[HttpGet]
        //public ResponseVM Validate(string token, string username)
        //{
        //    int UserId = new UserRepository().GetUser(username);
        //    if (UserId == 0) return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
        //    string tokenUsername = TokenManager.ValidateToken(token);
        //    if (username.Equals(tokenUsername))
        //    {
        //        return new ResponseVM
        //        {
        //            Status = "Success",
        //            Message = "OK",
        //        };
        //    }
        //    return new ResponseVM { Status = "Invalid", Message = "Invalid Token." };
        //}

        //https://localhost:44330/Login/ValidLogin?username=akash&password=akash
        [HttpGet]
        public HttpResponseMessage ValidLogin(string username,string password)
        {
            if(username=="akash" && password == "akash")
            {
              
                return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(username));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "username and password Invalid");
            }
        }
        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetLogin()
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: "Sucessfully valid");
        }
    }
}
