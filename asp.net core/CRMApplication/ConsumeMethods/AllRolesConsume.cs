using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class AllRolesConsume
    {
        HttpClient proxy;
        public AllRolesConsume()
        {
            proxy = new HttpClient();
        }
        public async Task<bool> RemovefromUserRole(string Method, string RegistrationID)
        {
            proxy = new HttpClient();
            //"GetTotalProjectsCounts" + registration.Username
            string path = Method + "?" + "RegistrationID=" + RegistrationID;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllRoles/");
            var data = proxy.GetAsync(path);
            data.Wait();
            var read = data.Result;
            if (read.IsSuccessStatusCode)
            {
                var Role = read.Content.ReadAsAsync<bool>();
                Role.Wait();
                bool role = Role.Result;
                return role;
            }
            return false;
        }
    }
}