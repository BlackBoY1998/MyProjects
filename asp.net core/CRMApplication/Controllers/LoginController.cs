using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using CRMApplication.Helpers;
using CRMApplication.Library;
using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using Newtonsoft.Json;

namespace CRMApplication.Controllers
{
    public class LoginController : Controller
    {
        Registration reg;
        private IAssignRoles _IAssignRoles;
        private ICacheManager _ICacheManager;
        public LoginController()
        {
            reg = new Registration();
            _IAssignRoles = new AssignRolesData();
            _ICacheManager = new CacheManager();
        }
        // GET: UserLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel LogObj)
        {
            try
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ViewBag.errormessage = "Error: captcha entered is not valid.";

                    return View(LogObj);
                }
                if (!string.IsNullOrEmpty(LogObj.Username) && !string.IsNullOrEmpty(LogObj.Password))
                {
                    string username = LogObj.Username;
                    string password = EncryptionLibrary.EncryptText(LogObj.Password);
                    LogObj.Password = password;
                    HttpClient hc = new HttpClient(); //class for sending HTTP request and Receving Http Request
                                                      //hc.BaseAddress = new Uri("https://localhost:44304/api/Login/ValidateUser?"+"username="+username+"&"+"password="+password);
                    hc.BaseAddress = new Uri("https://localhost:44304/api/Login/");
                    var InsertStd = hc.PostAsJsonAsync<LoginViewModel>("ValidateUser", LogObj);
                    InsertStd.Wait();


                    var read = InsertStd.Result;
                    if (read.IsSuccessStatusCode) //success status code is 200s
                    {
                        var display = read.Content.ReadAsAsync<Registration>();
                        display.Wait();
                        reg = display.Result;

                        if (reg != null)
                        {
                            if (reg.RegistrationID == 0 || reg.RegistrationID < 0)
                            {
                                ViewBag.errormessage = "Entered Invalid Username and Password";
                            }
                            else
                            {
                                var RoleID = reg.RoleID;
                                remove_Anonymous_Cookies(); //Remove Anonymous_Cookies

                                Session["RoleID"] = Convert.ToString(reg.RoleID);
                                Session["Username"] = Convert.ToString(reg.Username);
                                if (RoleID == 1)
                                {
                                    Session["AdminUser"] = Convert.ToString(reg.RegistrationID);

                                    if (reg.ForceChangePassword == 1)
                                    {
                                        return RedirectToAction("ChangePassword", "UserProfile");
                                    }

                                    return RedirectToAction("Dashboard", "Admin");
                                }
                                else if (RoleID == 2)
                                {
                                    if (!_IAssignRoles.CheckIsUserAssignedRole(reg.RegistrationID))
                                    {
                                        ViewBag.errormessage = "Approval Pending";
                                        return View(LogObj);
                                    }

                                    Session["UserID"] = Convert.ToString(reg.RegistrationID);

                                    if (reg.ForceChangePassword == 1)
                                    {
                                        return RedirectToAction("ChangePassword", "UserProfile");
                                    }

                                    return RedirectToAction("Dashboard", "User");
                                }
                                else if (RoleID == 3)
                                {
                                    Session["SuperAdmin"] = Convert.ToString(reg.RegistrationID);
                                    return RedirectToAction("Dashboard", "SuperAdmin");
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.errormessage = "Entered Invalid Username and Password";
                        return View(LogObj);
                    }
                   
                }
                return View(LogObj);
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }
        [HttpGet]
        public ActionResult Logout()
        {

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SuperAdmin"])))
                {
                    _ICacheManager.Clear("AdminCount");
                    _ICacheManager.Clear("UsersCount");
                    _ICacheManager.Clear("ProjectCount");
                }

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();

                HttpCookie Cookies = new HttpCookie("WebTime");
                Cookies.Value = "";
                Cookies.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(Cookies);
                HttpContext.Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [NonAction]
        public void remove_Anonymous_Cookies()
        {
            try
            {

                if (Request.Cookies["WebTime"] != null)
                {
                    var option = new HttpCookie("WebTime");
                    option.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(option);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}