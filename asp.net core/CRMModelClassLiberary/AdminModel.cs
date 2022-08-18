using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
        [NotMapped]
        public class AdminModel
        {
            public string RegistrationID { get; set; }
            public string Name { get; set; }
        }
}
