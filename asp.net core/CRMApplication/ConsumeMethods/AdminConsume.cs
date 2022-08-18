using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class AdminConsume
    {
        HttpClient proxy;
        
        public DisplayViewModel GetData(string Method, string AdminID)
        {
            proxy = new HttpClient();
            DisplayViewModel displayViewModel = null;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/Admin/");
            var Insertparameter = proxy.GetAsync(Method+ "?AdminID="+AdminID);
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