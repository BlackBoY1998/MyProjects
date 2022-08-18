using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Interfaces
{
    public interface ILogin
    {
        Registration ValidateUser(string userName, string passWord);
        bool UpdatePassword(string NewPassword, int UserID);
        string GetPasswordbyUserID(int UserID);
    }
}