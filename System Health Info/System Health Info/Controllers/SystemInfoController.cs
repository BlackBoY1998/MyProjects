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
namespace System_Health_Info.Controllers
{
    public class SystemInfoController : Controller
    {
        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123";
       UserEntities ldb;
       public SystemInfoController()
        {
            ldb = new UserEntities();
        }
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserClass user)
        {
            if (ModelState.IsValid)
            {
                var v = ldb.Users.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (v != null)
                {
                    Session["Id"] = v.Id;
                    Session["UserName"] = v.UserName;
                    FormsAuthentication.SetAuthCookie(v.UserName,false);
                    return RedirectToAction("Info", "SystemInfo");
                }
                else
                {
                    ModelState.AddModelError("Password", "Please check username and Password");
                    return View(user);
                }

                
            }
            return View(user);
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
                int id = Convert.ToInt32(Session["Id"]);


                //string[] drives = System.IO.Directory.GetLogicalDrives();
                //List<string> lst = new List<string>();
                //foreach (string str in drives)
                //{
                //    lst.Add(str);
                //}

                //ViewBag.list = lst;

                ////List<float> driveInfo = new List<float>();
                //foreach (var drive in DriveInfo.GetDrives())
                //{
                //    //driveInfo.Add(drive.TotalFreeSpace);
                //    //driveInfo.Add(drive.TotalSize);
                //    double freeSpace = drive.TotalFreeSpace;
                //    double totalSpace = drive.TotalSize;
                //    double percentFree = (freeSpace / totalSpace) * 100;
                //    float num = (float)percentFree;
                //    lst.Add(num.ToString());
                //    //double Used = (totalSpace - freeSpace / totalSpace) * 100;
                //    //float num2 = (float)Used;
                //}
                //ViewBag.Drive = driveInfo;

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
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error";
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
        }
        private void Lastuser()
        {
            conn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select UserName From [dbo].[User] where Id=(select UserId from UserLog where UserLogId=(select MAX(UserLogId)-1 from UserLog))";
            cmd.CommandType = CommandType.Text;
            con.Open();
            string LastUserName = cmd.ExecuteScalar().ToString();
            ViewBag.LastUserName = LastUserName;
            con.Close();
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
        }
    }
}