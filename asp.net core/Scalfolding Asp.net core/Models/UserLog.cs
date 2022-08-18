using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class UserLog
    {
        public int UserLogId { get; set; }
        public int? UserId { get; set; }
        public string MachineName { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string UserIpaddress { get; set; }
        public string LoginLogoutRemarks { get; set; }
        public string WinLoginName { get; set; }
    }
}
