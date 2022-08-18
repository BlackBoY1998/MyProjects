using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Passwordpolicy.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }

        public DateTime NewAccount { get; set; }
       
    }
}