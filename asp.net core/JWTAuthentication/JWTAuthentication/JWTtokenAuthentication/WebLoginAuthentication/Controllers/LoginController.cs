using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebLoginAuthentication.Model;
using WebLoginAuthentication.Models;

namespace WebLoginAuthentication.Controllers
{
    public class LoginController : Controller
    {
        public static string WebURl = "https://localhost:44352/";

        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login( LoginModel objloginmodel)
        {
            Token objtoken=null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebURl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var json = JsonConvert.SerializeObject(objloginmodel);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var responsemessage = await client.PostAsync("api/Authenticate/login", stringContent);
                if (responsemessage.IsSuccessStatusCode)
                {
                    var resultmessage = responsemessage.Content.ReadAsStringAsync().Result;
                    objtoken = JsonConvert.DeserializeObject<Token>(resultmessage.ToString());
                    Session["Token"] = objtoken.token;
                    return RedirectToAction("Home");

                }
            }
            return View();
        }
        public async Task<ActionResult> Home()
        {
            WeatherForecast wf = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebURl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: Session["Token"].ToString());
                var responsemessage = await client.GetAsync("api/Values");
                if (responsemessage.IsSuccessStatusCode)
                {
                    var resultmessage = responsemessage.Content.ReadAsStringAsync().Result;
                    wf = JsonConvert.DeserializeObject<WeatherForecast>(resultmessage.ToString());
                }
                return View();
            }
        }
    }
}