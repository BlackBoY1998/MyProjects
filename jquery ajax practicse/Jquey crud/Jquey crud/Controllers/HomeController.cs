using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jquey_crud.Models;
using System.Data.SqlClient;
using System.Data;

namespace Jquey_crud.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        string cs = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AkashEmployee;User ID=sa;Password=ccntspl@123";
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Employee e)
        {
            return Json(AddUser(e), JsonRequestBehavior.AllowGet);
        }
        public int AddUser(Employee e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("AddUser",con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("employeeid", e.EmployeeId);
            cmd.Parameters.AddWithValue("@name", e.Name);
            cmd.Parameters.AddWithValue("@salary", e.Salary);
            cmd.Parameters.AddWithValue("@DOJ", e.DOJ);
            int i = cmd.ExecuteNonQuery();
            return i;

        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult TrannsferTable()
        {
            return View();
        }
    }
}