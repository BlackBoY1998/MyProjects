using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Interfaces
{
    public interface IRegistration
    {
        bool CheckUserNameExists(string Username);
        int AddUser(Registration entity);
        IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search);
        bool UpdatePassword(string RegistrationID, string Password);
    }
}