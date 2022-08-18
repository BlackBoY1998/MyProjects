using DemoTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DemoTest.Controllers
{
    public class HomeController : Controller
    {
        string cs = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123";
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            Login l = new Login();
            l.Email = frm["floatingInput"];
            l.password = frm["floatingPassword"];
            var a=Login(l.Email, l.password);
            if (a == false)
            {
                return View();
            }
            else
            {
                Thread.Sleep(9000);
                return View("Home");
            }
        }
        public ActionResult Home()
        {
            return View();
        }
        public bool Login(string Email,string password)
        {
            SqlConnection con=new SqlConnection(cs);
            bool Isvalid=false;
            string query = "select * from [dbo].[User] where EmailId=@Email And Password=@password";
            SqlCommand cmd=new SqlCommand(query,con);
            {
                cmd.Parameters.AddWithValue("@Email",Email);
                cmd.Parameters.AddWithValue("@password",password);
                SqlDataAdapter sda=new SqlDataAdapter(cmd);
                DataTable dt=new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    Isvalid = true;
                }
            }
           return Isvalid;
        }
    }
}