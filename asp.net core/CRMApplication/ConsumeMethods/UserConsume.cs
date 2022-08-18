using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class UserConsume
    {
        HttpClient proxy;

        public DisplayViewModel GetData(string Method, string UserId)
        {
            proxy = new HttpClient();
            DisplayViewModel displayViewModel = null;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/UserDashboard/");
            var Insertparameter = proxy.GetAsync(Method + "?UserID=" + UserId);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<DisplayViewModel>();
                result.Wait();
                displayViewModel = result.Result;
                return displayViewModel;
            }
            return displayViewModel;
        }
    }
}