using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime? Doj { get; set; }
        public TimeSpan? TalkTime { get; set; }
    }
}
