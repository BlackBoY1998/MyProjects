using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstAppoach
{
    public class Employee
    {
        //Employee Table coloumns
        //scalar properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }

        //foriegn key from table
        //navigation properties
        public Department Department { get; set; }
    }
}