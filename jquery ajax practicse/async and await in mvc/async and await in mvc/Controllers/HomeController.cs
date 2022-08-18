using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace async_and_await_in_mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            var watch = new Stopwatch();
            watch.Start();
            var country = GetCountry();
            var state = GetState();
            var city = GetCity();
            watch.Stop();
            ViewBag.WatchMilliseconds = watch.ElapsedMilliseconds;
            return View();  
        }
        public async Task<ActionResult> GetListAsync()
        {
            //Create a stopwatch for getting excution time  
            var watch = new Stopwatch();
            watch.Start();
            var country = GetCountryAsync();
            var state = GetStateAsync();
            var city = GetCityAsync();
            var content = await country;
            var count = await state;
            var name = await city;
            watch.Stop();
            ViewBag.WatchMilliseconds = watch.ElapsedMilliseconds;
            return View();
        } 
        #region-- > GetCountry Methods  for GetList && GetListAsync  
        public string GetCountry() {  
            Thread.Sleep(3000); //Use - when you want to block the current thread.  
            return "India";  
        }  
        public async Task < string > GetCountryAsync() {  
            await Task.Delay(3000); //Use - when you want a logical delay without blocking the current thread.  
             return "India";  
        }
        #endregion  
# region-- > GetState Methods  for GetList && GetListAsync  
        public string GetState() {  
            Thread.Sleep(5000); //Use - when you want to block the current thread.  
        return "Gujarat";  
        }  
        public async Task < string > GetStateAsync() {  
            await Task.Delay(5000); //Use - when you want a logical delay without blocking the current thread.  
            return "Gujarat";  
        }
    #endregion  
# region-- > GetCity Methods  for GetList && GetListAsync  
    public string GetCity() {  
        Thread.Sleep(6000); //Use - when you want to block the current thread.  
        return "Junagadh";  
    }  
    public async Task < string > GetCityAsync() {  
        await Task.Delay(6000); //Use - when you want a logical delay without blocking the current thread.  
     return "Junagadh";  
    }
#endregion 
    }
}