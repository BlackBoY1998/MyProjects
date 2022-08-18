using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crudusingEntity.Models
{
    public class LoginModelcs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> NewAccountDate { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsLoggedin { get; set; }
    }
}