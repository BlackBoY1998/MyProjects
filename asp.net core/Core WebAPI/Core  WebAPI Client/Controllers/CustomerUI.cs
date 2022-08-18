using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core__WebAPI_Client.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Core__WebAPI_Client.Controllers
{
    public class CustomerUI : Controller
    {
        IConfiguration configuration;
        public CustomerUI(IConfiguration icon)
        {
            configuration= icon;
        }
        public async Task<IActionResult> Index()
        {
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            List<Customer> customersList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(con))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(customersList);
        }
        public IActionResult GetCustomer()
        {
            return View();
        }
        //public ViewResult GetCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer customer = new Customer();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(con + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(customer);
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer cust)
        {
            Customer customer = new Customer();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cust), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(con, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCustomer(int id)
        {
            Customer customer = new Customer();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(con + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                }
            }
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer,int id)
        {
            Customer cust = new Customer();
            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(con + id,content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    cust = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {

            var con = configuration.GetSection("WebUrl").GetSection("WebApiUrl").Value;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(con + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
