using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
    public class UserModel
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public bool selectedUsers { get; set; }
        public string AssignToAdmin { get; set; }

    }
}
