using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
    [NotMapped]
    public class AssignRolesModel
    {
        public List<AdminModel> ListofAdmins { get; set; }

        [Required(ErrorMessage = "Choose Admin")]
        public int RegistrationID { get; set; }
        public List<UserModel> ListofUser { get; set; }
        public int? AssignToAdmin { get; set; }
        public int? CreatedBy { get; set; }

    }
}
