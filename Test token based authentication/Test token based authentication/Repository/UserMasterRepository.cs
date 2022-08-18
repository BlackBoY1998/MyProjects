using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test_token_based_authentication.Models;

namespace Test_token_based_authentication.Repository
{
    public class UserMasterRepository:IDisposable
    {
        UserEntities context;
        public UserMasterRepository()
        {
           context  = new UserEntities();
        }
        public User ValidateMaster(string UserName,string Password)
        {
          
            return context.Users.FirstOrDefault(user =>
            user.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase)
            && user.Password == Password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}