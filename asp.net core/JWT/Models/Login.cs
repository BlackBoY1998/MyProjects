using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailID { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Role { get; set; }
    }
}