﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jquey_crud.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string DOJ { get; set; }
    }
}