using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Role { get; set; }
    }
}
