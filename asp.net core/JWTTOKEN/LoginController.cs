using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WMS.Models;
using WMS.Models.VM;
 

namespace WMS.Controllers
{
    [RoutePrefix("Api/Login")]
    public class LoginController : ApiController
    {
        WMSEntities wmsEN = new WMSEntities();
        [Route("UserLogin")]
        [HttpPost]
        public ResponseVM UserLogin(LoginVM objVM)
        {

            var objlst = wmsEN.Usp_Login(objVM.UserName, UtilityVM.Encryptdata(objVM.Passward), "").ToList<Usp_Login_Result>().FirstOrDefault();
            if (objlst.Status == -1)
                return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
            //if (objlst.Status == 2)
            //    return new ResponseVM { Status = "Invalid", Message = "Already Logged In." };
            if (objlst.Status == 0)
                return new ResponseVM { Status = "Inactive", Message = "User Inactive." };
            else
                return new ResponseVM { Status = "Success", Message = TokenManager.GenerateToken(objVM.UserName) };
        }
        [Route("Validate")]
        [HttpGet]
        public ResponseVM Validate(string token, string username)
        {
            int UserId = new UserRepository().GetUser(username);
            if (UserId == 0) return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return new ResponseVM
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new ResponseVM { Status = "Invalid", Message = "Invalid Token." };
        }
    }
}
