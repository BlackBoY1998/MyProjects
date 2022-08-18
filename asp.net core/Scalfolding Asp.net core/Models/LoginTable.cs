using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class LoginTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? NewAccountDate { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
    }
}
