using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Repository
{
    public class RolesData:IRoles
    {
        public int getRolesofUserbyRolename(string Rolename)
        {
            using (var _context = new ApplicationDbContext())
            {
                var roleID = (from role in _context.Role
                              where role.RoleName == Rolename
                              select role.RoleID).SingleOrDefault();

                return roleID;
            }
        }
    }
}