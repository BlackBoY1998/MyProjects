using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Passwordpolicy.Models;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Passwordpolicy.Controllers
{
    public class LoginController : Controller
    {
        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123";
        LoginIEntities Le;
  
        public LoginController()
        {
            Le = new LoginIEntities();
        }
        // GET: Login
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Login L)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Regex reg1 = new Regex((@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@@$%^&*-])"));
                    if (reg1.Match(L.Password).Success)
                    {
                        var v = Le.LoginTables.Where(a => a.UserName.Equals(L.UserName) && a.Password.Equals(L.Password)).FirstOrDefault();

                        if (v != null)
                        {
                            TempData["Id"] = v.Id;
                            TempData["Name"] = v.Name;
                            TempData["Date"] = v.NewAccountDate;
                            TempData["UserName"] = v.UserName;
                            TempData["Pwd"] = v.Password;
                            TempData["Pwd2"] = v.Password2;
                            TempData["Pwd3"] = v.Password3;

                            var check = LoginData();
                            if (check == false)
                            {
                                ViewBag.DateExpired = "DateExpiredError";
                                return RedirectToAction("ExpiredPassword", "Login");
                            }

                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {

                            ViewBag.Error = "check Password";
                            return View(L);
                        }
                    }

                    else
                    {
                        ViewBag.Error = "check Password";

                    }
                }
            }
            catch(Exception e)
            {
                ViewBag.System = "System Error";
            }
              return View(L);
            }

       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExpiredPassword()
        {
            try
            {
                var login = new Login();
                login.UserName = TempData["UserName"].ToString();

                return View(login);
            }
            catch (Exception e)
            {
                ViewBag.System = "System Error";
                return View();
            }
        }
        public ActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUser(Login login)
        {
            try
            {
                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;
                Regex reg1 = new Regex((@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@@$%^&*-])"));
                if (reg1.Match(login.Password).Success)
                {
                    int i = 2;
                    conn();
                    SqlCommand cmd = new SqlCommand("checkpassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", login.Id);
                    cmd.Parameters.AddWithValue("@Name", login.Name);
                    cmd.Parameters.AddWithValue("@UserName", login.UserName);
                    cmd.Parameters.AddWithValue("@Password", login.Password);
                    cmd.Parameters.AddWithValue("@NewAccountdate", date);
                    cmd.Parameters.AddWithValue("@Password2", string.Empty);
                    cmd.Parameters.AddWithValue("@Password3", string.Empty);
                    cmd.Parameters.AddWithValue("@Option", i);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.pwd = "check Password";
                    return View(login);

                }
            }
            catch(Exception e)
            {
                ViewBag.System = "System Error";
                return View(login);
            }
               
        }
        [HttpPost]
        public ActionResult ExpiredPassword(Login login)
        {
            try
            {
                string UserEnterdPassword = login.Password;
                string Name = TempData["Name"].ToString();
                string currentpwd = TempData["Pwd"].ToString();
                string Password2 = TempData["Pwd2"].ToString();
                string Password3 = TempData["Pwd3"].ToString();
                TempData.Keep();
                if (UserEnterdPassword == currentpwd)
                {
                    ViewBag.Error = "Password Match";
                    return View();
                }
                else if (UserEnterdPassword == Password2 || UserEnterdPassword == Password3)
                {

                    ViewBag.Error = "Password Match";
                    return View();
                }
                else
                {
                    conn();
                    int Id = Convert.ToInt32(TempData["Id"]);
                    string username = login.UserName;
                    string Password = login.Password;
                    var dateAndTime = DateTime.Now;
                    var date = dateAndTime.Date;
                    Regex reg1 = new Regex((@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@@$%^&*-])"));
                    if (reg1.Match(Password).Success)
                    {




                        int i = 1;
                        SqlCommand cmd = new SqlCommand("checkpassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Password", UserEnterdPassword);
                        cmd.Parameters.AddWithValue("@NewAccountdate", date);
                        cmd.Parameters.AddWithValue("@Password2", currentpwd);
                        cmd.Parameters.AddWithValue("@Password3", Password2);
                        cmd.Parameters.AddWithValue("@Option", i);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ViewBag.success = "Pwd";
                        return View("Index");


                    }
                    else
                    {
                        ViewBag.pwd = "check Password";
                        return View(login);
                    }
                }
            }
            catch(Exception e)
            {
                ViewBag.System = "System Error";
                return View(login);
            }
            //return View();
            
        }
        private void conn()
        {
            con = new SqlConnection(constr);
        }
        private bool LoginData()
        {
            DateTime Userdate = Convert.ToDateTime(TempData["Date"]);
            DateTime Currentdate = DateTime.Now;//DateTime.Today
            TimeSpan Totalday = Currentdate - Userdate;
            int t = Totalday.Days;
            int e = 90;
            if(t>= e)
            {
                return false;
               
            }
            else
            {
                return true;
            } 
        }
      
     }
}