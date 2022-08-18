using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TestThirdpartywebApi.Models;
using System.Text.Json;

namespace TestThirdpartywebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly string Url = ConfigurationManager.AppSettings["APIURL"];
        // GET: Home
        private readonly HttpClient hc;
        public HomeController()
        {
            hc = new HttpClient();
        }
        public async Task<ActionResult> Index()
        {
            var result = new List<Publicholidays>();
            var response = await hc.GetAsync(Url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<Publicholidays>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return View(result);
        }
    }
}