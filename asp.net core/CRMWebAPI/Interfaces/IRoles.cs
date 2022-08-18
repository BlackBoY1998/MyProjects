using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Interfaces
{
    public interface IRoles
    {
        int getRolesofUserbyRolename(string Rolename);
    }
}