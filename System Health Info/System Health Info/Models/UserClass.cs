using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System_Health_Info.Models
{
    public class UserClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
    }
}