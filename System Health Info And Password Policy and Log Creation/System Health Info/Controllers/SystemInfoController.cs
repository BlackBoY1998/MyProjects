using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System_Health_Info.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.IO;
using System.Text.RegularExpressions;
using log4net;
namespace System_Health_Info.Controllers
{
    public class SystemInfoController : Controller
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(SystemInfoController));
        public static int Count = 0;
        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123";
        LoginIEntities Le;
       public SystemInfoController()
        {
            Le = new LoginIEntities();
        }
        // GET: User
        public ActionResult Login()
        {

            var result = Session["Succeess"];
            if (result != null)
            {
                Count++;
                //ViewBag.success = result;
                TempData["success"] = result;
                if(Count>1)
                {
                    TempData.Remove("success");
                }
                
            }

            Log.Debug("Login Page Loaded Succesfully....");
            Log.Info("Login Page Loaded Succesfully....");
            Log.Warn("Login Page Loaded Succesfully....");
                           
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
                                return RedirectToAction("ExpiredPassword", "SystemInfo");
                            }

                            Log.Debug("User Logged in Succesfully Redirected to Info page");
                            Log.Info("User Logged in Succesfully Redirected to Info page");
                            Log.Warn("User Logged in Succesfully Redirected to Info page");
                            //throw new NullReferenceException();
                            return RedirectToAction("Info", "SystemInfo");
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
            catch (Exception e)
            {
                ViewBag.System = "System Error";
                Log.Error("There is Error in While Gathering User Login Data", e);
                Log.Fatal("There is Error in While Gathering  User Login Data", e);
                //throw;
            }
            return View(L);
            
        }
        public ActionResult Info()
        {
            try
            {
                //for  Ip address
                //IPHostEntry host;
                //string localIpaddr = "?";
                //host = Dns.GetHostEntry(Dns.GetHostName());
                //foreach (IPAddress ip in host.AddressList)
                //{
                //    if (ip.AddressFamily.ToString() == "InterNetwork")
                //    {
                //        localIpaddr = ip.ToString();
                //        ViewBag.Ip = localIpaddr;
                //    }
                //}

                //string IP = Request.UserHostAddress;

                string Ip = Request.UserHostAddress;
                ViewBag.Ip = Ip;
                ///for windows LoginName
                string WinLog = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //for MachineName
                //string MachineName2 = Environment.MachineName;
                string MachineName1 = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                ViewBag.Machine = MachineName1;
                ViewBag.WinLog = WinLog;
                int id = Convert.ToInt32(TempData["Id"]);
                TempData.Keep();


                DriveInfo[] allDrives = DriveInfo.GetDrives();
                List<string> driveInfo = new List<string>();
                foreach (DriveInfo d in allDrives)
                {
                    driveInfo.Add(d.Name);
                    if (d.IsReady == true)
                    {
                        double a = d.AvailableFreeSpace;
                        double t = d.TotalSize;
                        double p = t - a;
                        double percentFree = (p * 100) / t;

                        float num = (float)Math.Round(percentFree * 100f) / 100f;

                        int num1 = (int)Math.Round(num);

                        double size = t / 1024 / 1024 / 1024;
                        double totalSize = (double)Math.Round(size);

                        string ta = totalSize.ToString();

                        double Used = p / 1024 / 1024 / 1024;
                        double UsedSize = (double)Math.Round(Used);
                        //driveInfo.Add(d.AvailableFreeSpace.ToString());
                        string Us = UsedSize.ToString();
                        string name = Us + "GB" + "/" + ta + "GB";
                        driveInfo.Add(name);
                        driveInfo.Add(num1.ToString());
                        //driveInfo.Add(d.TotalSize.ToString());

                    }
                }
                ViewBag.Drive = driveInfo;

                LoginData(id);
                serverName();
                DBName();
                LastLoginTime();
                LastIpAddress();
                Lastuser();
                DataSizeDB();
                LogSizeDB();
                Log.Debug("Gathering Logged In User System Information");
                Log.Info("Gathering Logged In User System Information");
                Log.Warn("Gathering Logged In User System Information");
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error";
                Log.Error("Error in while gathering System Information of Logged In User", e);
                Log.Fatal("Error in while gathering System Information of Logged In User", e);
                //throw;
                return View();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            int id = Convert.ToInt32(Session["Id"]);
            LogoutData(id);
            //Session.Abandon();
            Log.Debug("User Logged out Succesfully Redirected to Login page");
            Log.Info("User Logged out Succesfully Redirected to Login page");
            Log.Warn("User Logged out Succesfully Redirected to Login page");
            return RedirectToAction("Login");  
        }

        private void conn()
        {
            con = new SqlConnection(constr);
        }
        private void LoginData(int Id)
        {
            string Ip=ViewBag.Ip;
            TempData["Ip"] = Ip;
            string status="Login";
            string Machine=ViewBag.Machine;
            TempData["machine"] = Machine;
            string Winlog = ViewBag.WinLog;
            TempData["winlog"] = Winlog;
            conn();
            SqlCommand cmd = new SqlCommand("TrackUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Id);
            cmd.Parameters.AddWithValue("@MachineName",Machine );
            cmd.Parameters.AddWithValue("@UserIPAddress", Ip);
            cmd.Parameters.AddWithValue("@LoginLogoutRemarks",status);
            cmd.Parameters.AddWithValue("@WinLoginName",Winlog);
            cmd.Parameters.AddWithValue("@status",0);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Log.Debug("Posting User Logged In Data in Database");
            Log.Info("Posting User Logged In Data in Database");
            Log.Warn("Posting User Logged In Data in Database");

        }
        private void LogoutData(int Id)
        {
            string Ip = TempData["Ip"].ToString();
            string status = "Logout";
            string Machine =TempData["machine"].ToString();
            string Winlog = TempData["winlog"].ToString();
            conn();
            SqlCommand cmd = new SqlCommand("TrackUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Id);
            cmd.Parameters.AddWithValue("@MachineName", Machine);
            cmd.Parameters.AddWithValue("@UserIPAddress", Ip);
            cmd.Parameters.AddWithValue("@LoginLogoutRemarks", status);
            cmd.Parameters.AddWithValue("@WinLoginName", Winlog);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Log.Debug("Posting User Logged Out Data in Database");
            Log.Info("Posting User Logged Out Data in Database");
            Log.Warn("Posting User Logged Out Data in Database");

        }
        private void serverName()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT @@SERVERNAME";
            cmd.CommandType = CommandType.Text;
            con.Open();
            string server = cmd.ExecuteScalar().ToString();
            ViewBag.server = server;
            con.Close();
            Log.Debug("Getting Users Server Info");
            Log.Info("Getting Users Server Info");
            Log.Warn("Getting Users Server Info");
        }
        private void DBName()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT DB_NAME()";
            cmd.CommandType = CommandType.Text;
            con.Open();
            string DB = cmd.ExecuteScalar().ToString();
            //ViewBag.Db = DB;
            TempData["DB"] = DB;
            con.Close();
            Log.Debug("Getting Database Name");
            Log.Info("Getting Database Name");
            Log.Warn("Getting Database Name");

        }
        private void LastLoginTime()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select LoginDateTime from UserLog where UserLogId=(select MAX(UserLogId)-1 from UserLog)";
            cmd.CommandType = CommandType.Text;
            con.Open();
            DateTime lastloginTime = Convert.ToDateTime(cmd.ExecuteScalar());
            ViewBag.lastTime = lastloginTime;
            con.Close();
            Log.Debug("Getting LastLoginTime");
            Log.Info("Getting LastLoginTime");
            Log.Warn("Getting LastLoginTime");

        }
        private void LastIpAddress()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select UserIPAddress from UserLog where UserLogId=(select MAX(UserLogId)-1 from UserLog)";
            cmd.CommandType = CommandType.Text;
            con.Open();
            string LastIpaddress = cmd.ExecuteScalar().ToString();
            ViewBag.LastIpAddress = LastIpaddress;
            con.Close();
            Log.Debug("Getting Last Ip Address");
            Log.Info("Getting Last Ip Address");
            Log.Warn("Getting Last Ip Address");

        }
        private void Lastuser()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select UserName From [dbo].[LoginTable] where Id=(select UserId from UserLog where UserLogId=(select MAX(UserLogId)-1 from UserLog))";
            cmd.CommandType = CommandType.Text;
            con.Open();
            string LastUserName = cmd.ExecuteScalar().ToString();
            ViewBag.LastUserName = LastUserName;
            con.Close();
            Log.Debug("Getting Last User");
            Log.Info("Getting Last  User");
            Log.Warn("Getting Last User");

        }
        private void DataSizeDB()
        {
            string DB = TempData["DB"].ToString();
            int i = 1;
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DataFileSizeMB";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DBName", DB);
            cmd.Parameters.AddWithValue("@option", i);
            con.Open();
            string DataSize = cmd.ExecuteScalar().ToString();
            ViewBag.DataSizes = DataSize;
            con.Close();
            Log.Debug("Getting Database Size in Data");
            Log.Info("Getting Database Size in Data");
            Log.Warn("Getting Database Size in Data");

        }
        private void LogSizeDB()
        {
            string DB = TempData["DB"].ToString();
            int i = 0;
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DataFileSizeMB";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DBName", DB);
            cmd.Parameters.AddWithValue("@option", i);
            con.Open();
            string LogSize = cmd.ExecuteScalar().ToString();
            ViewBag.LogSize = LogSize;
            con.Close();
            Log.Debug("Getting Database Size in Log");
            Log.Info("Getting Database Size in Log");
            Log.Warn("Getting Database Size in Log");
        }
        private bool LoginData()
        {
            Log.Debug("Checking Password Expiration");
            Log.Info("Checking Password Expiration");
            Log.Warn("Checking Password Expiration");
            DateTime Userdate = Convert.ToDateTime(TempData["Date"]);
            DateTime Currentdate = DateTime.Now;//DateTime.Today
            TimeSpan Totalday = Currentdate - Userdate;
            int t = Totalday.Days;
            int e = 90;
            if (t >= e)
            {
                return false;

            }
            else
            {
                return true;
            }

        }
        public ActionResult ExpiredPassword()
        {
            try
            {
                var login = new Login();
                login.UserName = TempData["UserName"].ToString();
                Log.Debug("Password Expired!!! Password Reset Page is Loaded");
                Log.Info("Password Expired!!! Password Reset Page is Loaded");
                Log.Warn("Password Expired!!! Password Reset Page is Loaded");
                return View(login);
            }
            catch (Exception e)
            {
                ViewBag.System = "System Error";
                Log.Error("Error while Getting Username", e);
                Log.Fatal("Error while Getting Username", e);
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
                    Log.Debug("Adding New User and Redirecting To LoginPage...");
                    Log.Info("Adding New User and Redirecting To LoginPage...");
                    Log.Warn("Adding New User and Redirecting To LoginPage...");
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.pwd = "check Password";
                    return View(login);

                }
            }
            catch (Exception e)
            {
                ViewBag.System = "System Error";
                Log.Error("Error while Posting New UserData", e);
                Log.Fatal("Error while Posting New UserData", e);
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
                        Session["Succeess"] = "Pwd";
                        Log.Debug("Adding New Password of User and Redirecting To LoginPage...");
                        Log.Info("Adding New Password of User and Redirecting To LoginPage...");
                        Log.Warn("Adding New Password of User and Redirecting To LoginPage...");
                        return RedirectToAction("Login");


                    }
                    else
                    {
                        ViewBag.pwd = "check Password";
                        return View(login);
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.System = "System Error";
                Log.Error("Error while Posting Users New Password", e);
                Log.Fatal("Error while Posting Users New Password", e);
                return View(login);
            }
            //return View();

        }
      
    }
}