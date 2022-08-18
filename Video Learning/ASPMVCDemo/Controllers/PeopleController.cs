using ASPMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCDemo.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListPeople()
        {
            List<PersonModel> people = new List<PersonModel>();
            people.Add(new PersonModel { FirstName = "Test", LastName = "Demo", Age = 38 });
            people.Add(new PersonModel { FirstName = "jason", LastName = "Smith", Age = 35 });
            return View(people);
        }
    }
}