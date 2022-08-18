using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebLoginAuthentication.Model;

namespace WebLoginAuthentication.Controllers
{
    public class LoginController : Controller
    {
        public static string WebURl = "http://localhost:44352/";
        string TokenBased = string.Empty;

        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel objloginmodel)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(WebURl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            var responsemessage = await client.GetAsync(requestUri: "api/Autheticate/login?Username=" + objloginmodel.Username + "&Password=" + objloginmodel.Password);
            if (responsemessage.IsSuccessStatusCode)
            {
                var resultmessage = responsemessage.Content.ReadAsStringAsync().Result;
                TokenBased = JsonConvert.DeserializeObject<string>(resultmessage);

            }
            return View();
        }
    }
}