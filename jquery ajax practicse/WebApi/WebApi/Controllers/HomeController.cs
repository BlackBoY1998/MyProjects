using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult GetAllEmployee()
        {
            IList<EmployeeModel> employee = null;
            using (var x=new EmployeeEntities())
            {
                employee = x.TblEmployee1.Select(c => new EmployeeModel()
                {
                    Employeeid = c.EmployeeId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Salary = c.salary
                }).ToList<EmployeeModel>();
            }
            if (employee.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }
        public IHttpActionResult PostEmployee(EmployeeModel e)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
               using (var v = new EmployeeEntities())
               {
                  v.TblEmployee1.Add(new TblEmployee1
                  {
                      EmployeeId = e.Employeeid,
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      salary = e.Salary
                  });
                  v.SaveChanges();
               }
            return Ok(e);
        }
        public IHttpActionResult PutEmployee(EmployeeModel e)
        {
            using (var x = new EmployeeEntities())
            {
                var employee = x.TblEmployee1.Where(c => c.EmployeeId == e.Employeeid).FirstOrDefault();
                if (employee != null)
                {
                 employee.FirstName=e.FirstName;
                 employee.LastName = e.LastName;
                 employee.salary = e.Salary;
                 x.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
               
            }
            return Ok();
           
        }
    }
}
