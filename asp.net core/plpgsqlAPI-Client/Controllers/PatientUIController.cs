using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using plpgsqlAPI_Client.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace plpgsqlAPI_Client.Controllers
{
    public class PatientUIController : Controller
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        List<PatientsModel> listpatients = new List<PatientsModel>();
        IConfiguration configuration;
        PatientsModel c = new PatientsModel();


        public PatientUIController(IConfiguration icon)
        {
            configuration = icon;
            httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {

            return View();
        }


        public async Task<List<PatientsModel>> GetAllAPtients()
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;

            var con2 = configuration.GetValue<string>("WebUrl:WebApiUrl");
            listpatients = new List<PatientsModel>();
            using (var HttpClient = new HttpClient(httpClientHandler))
            {
                using (var response = await HttpClient.GetAsync(con))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    listpatients = JsonConvert.DeserializeObject<List<PatientsModel>>(ApiResponse);
                }
            }
            return listpatients;
        }


        [HttpGet]
        public async Task<PatientsModel> GetPatientById(int id)
        {
            c = new PatientsModel();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;

            using (var HttpClient = new HttpClient(httpClientHandler))
            {
                using (var response = await HttpClient.GetAsync(con + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<PatientsModel>(ApiResponse);
                }
            }
            return c;
        }

        [HttpPost]
        public async Task<PatientsModel> AddUpdatePatient(PatientsModel patients, int id = 0)
        {
            c = new PatientsModel();
            if (id == 0)
            {

                var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
                using (var HttpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(patients), Encoding.UTF8, "application/json");
                    using (var response = await HttpClient.PostAsync(con, content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        c = JsonConvert.DeserializeObject<PatientsModel>(ApiResponse);
                    }
                }
                return c;
            }
            else
            {
                var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
                using (var HttpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(patients), Encoding.UTF8, "application/json");
                    using (var response = await HttpClient.PutAsync(con, content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        c = JsonConvert.DeserializeObject<PatientsModel>(ApiResponse);
                    }
                }
                return c;
            }
        }
        [HttpDelete]
        public async Task<PatientsModel> DeleteCustomer(int id)
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            string messgae = "";
            using (var HttpClient = new HttpClient(httpClientHandler))
            {
                using (var response = await HttpClient.DeleteAsync(con + id))
                {
                    messgae = await response.Content.ReadAsStringAsync();
                }
            }
            return c;
        }
    }
}
