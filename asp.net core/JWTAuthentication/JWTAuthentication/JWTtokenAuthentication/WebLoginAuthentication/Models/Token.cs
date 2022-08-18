using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLoginAuthentication.Models
{
    public class Token
    {
        public string token { get; set; }
        public DateTime expirey { get; set; }
    }
}