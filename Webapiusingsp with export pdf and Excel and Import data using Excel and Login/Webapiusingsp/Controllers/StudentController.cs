using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webapiusingsp.Models;

namespace Webapiusingsp.Controllers
{
    public class StudentController : ApiController
    {
        Entities st; //object of Entity Framework
        public StudentController()
        {
            st = new Entities();
        }
        public IHttpActionResult GetStudent()
        {
            var result = st.Studentsp(0,"","","Get").ToList();
            return Ok(result);//return status as 200
        }
        public IHttpActionResult InsertStudnet(Student student)
        {
            var Insert = st.Studentsp(0, student.StdName, student.StdEmail, "Insert").ToList();
            return Ok(Insert);
        }
        public IHttpActionResult GetStudentById(int id)
        {
            var Studentdetails = st.Studentsp(id, "", "", "Select").Select(x => new StudentClass()
            {
                StudentId = x.StdId,
                StudentName = x.StdName,
                StudentEmail = x.StdEmail

            }).FirstOrDefault<StudentClass>();
            return Ok(Studentdetails);
        }
        public IHttpActionResult Put(StudentClass std)
        {
            var updatestudent = st.Studentsp(std.StudentId, std.StudentName, std.StudentEmail, "Update").ToList();
            return Ok(updatestudent);
        }
        public IHttpActionResult Delete(int id)
        {
            var DeleteStudent = st.Studentsp(id, "", "", "Delete").Select(x => new StudentClass()
                {
                    StudentId = x.StdId,
                    StudentName = x.StdName,
                    StudentEmail = x.StdEmail
                }).FirstOrDefault<StudentClass>();
            st.SaveChanges();
            return Ok();
        }
    }
}
