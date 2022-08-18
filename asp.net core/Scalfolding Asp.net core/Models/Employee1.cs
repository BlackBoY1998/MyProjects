using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class Employee1
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
