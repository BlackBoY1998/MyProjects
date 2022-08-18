using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core__WebAPI_Client.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Core__WebAPI_Client.Controllers
{
    public class CustomerUIcs : Controller
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        List<Customer> listCust = new List<Customer>();
        IConfiguration configuration;
        Customer c = new Customer();

     
        public CustomerUIcs(IConfiguration icon)
        {
            configuration = icon;
            httpClientHandler.ServerCertificateCustomValidationCallback =(sender,cert,chain,sslPolicyErrors )=>{ return true; };
        }
        public IActionResult Index()
        {
         
            return View();
        }
  

        public async Task<List<Customer>> GetAllCustomers()
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;

            var con2 = configuration.GetValue<string>("WebUrl:WebApiUrl");
            listCust = new List<Customer>();
            using (var HttpClient = new HttpClient(httpClientHandler))
            {
                using (var response = await HttpClient.GetAsync(con))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    listCust = JsonConvert.DeserializeObject<List<Customer>>(ApiResponse);
                }
            }
            return listCust;
        }


        [HttpGet]
        public async Task<Customer> GetByCustomerId(int CustomerID)
        {
            c = new Customer();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
                 
            using (var HttpClient = new HttpClient(httpClientHandler))
            {
                using (var response = await HttpClient.GetAsync(con+CustomerID))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<Customer>(ApiResponse);
                }
            }
            return c;
        }

        [HttpPost]
        public async Task<Customer> AddUpdateCustomer(Customer customer)
        {
            c = new Customer();
               
                var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
                using (var HttpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                    using (var response = await HttpClient.PostAsync(con, content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        c = JsonConvert.DeserializeObject<Customer>(ApiResponse);
                    }
                }
                return c;
            
        }

        public async Task<IActionResult> UpdateReservation(int CustomerID)
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            Customer cust = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync( con + CustomerID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cust = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(cust);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Customer customer)
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            Customer cust = new Customer();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(customer.CustomerID.ToString()), "Id");
                content.Add(new StringContent(customer.CustomerName), "Name");
                content.Add(new StringContent(customer.CustomerLocation), "StartLocation");
    

                using (var response = await httpClient.PutAsync(con, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    cust = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(cust);
        }
    [HttpDelete]
        public async Task<Customer> DeleteCustomer(int CustomerID)
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            string messgae = "";
            using (var HttpClient = new HttpClient(httpClientHandler))
            { 
                using (var response = await HttpClient.DeleteAsync(con+CustomerID))
                {
                    messgae = await response.Content.ReadAsStringAsync();
                }
            }
            return c;
        }
    }
}
