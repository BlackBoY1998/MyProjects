using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class NotificationConsume
    {
        private HttpClient proxy;
        public NotificationConsume()
        {
            proxy = new HttpClient();
        }
        public async Task<int> AddNotification(string Method, NotificationsTB NotificationsTB)
        {
            int Count = 0;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AddNotification/");
            var Insertnotifcation = proxy.PostAsJsonAsync<NotificationsTB>(Method, NotificationsTB);
            Insertnotifcation.Wait();
            var save = Insertnotifcation.Result;
            if (save.IsSuccessStatusCode)
            {
                var displayNotifcation = save.Content.ReadAsAsync<int>();
                displayNotifcation.Wait();
                int count = displayNotifcation.Result;
                return count;
            }
            return Count;
        }

    }
}