using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapiusingsp.Models;
using System.Net.Http;

namespace Webapiusingsp.Controllers
{
    public class StudentUIController : Controller
    {
        // GET: StudentUI
        public ActionResult Index()
        {
            IEnumerable<Student> stdobj = null;
            HttpClient hc = new HttpClient(); //class for sending HTTP request and Receving Http Request
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");
            var Consume = hc.GetAsync("Student"); //it will call method from api from URL
            Consume.Wait(); //wait for task for exexcution

            var read = Consume.Result;
            if(read.IsSuccessStatusCode) //success status code is 200
            {
                var display = read.Content.ReadAsAsync<IList<Student>>();
                display.Wait();
                stdobj = display.Result;
            }
            return View(stdobj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var InsertStd = hc.PostAsJsonAsync<Student>("Student", student);
            InsertStd.Wait();

            var save = InsertStd.Result;
            if(save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Details(int id)
        {
            StudentClass obj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var details = hc.GetAsync("Student?id=" + id.ToString());
            details.Wait();

            var read = details.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<StudentClass>();
                displaydetails.Wait();
                obj = displaydetails.Result;
            }
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            StudentClass obj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var details = hc.GetAsync("Student?id=" + id.ToString());
            details.Wait();

            var read = details.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<StudentClass>();
                displaydetails.Wait();
                obj = displaydetails.Result;
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(StudentClass std)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");

            var updateStudent = hc.PutAsJsonAsync<StudentClass>("Student", std);
            updateStudent.Wait();

            var save = updateStudent.Result;
            if (save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(std);

        }
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");

            var deletestudent = hc.DeleteAsync("Student/" + id.ToString());
            deletestudent.Wait();

            var save = deletestudent.Result;
            if (save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        public ActionResult CheckRadio(String Type)
        {
            return View();
        }
    }
}