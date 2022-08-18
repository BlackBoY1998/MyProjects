using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class UserProfileConsume
    {
        HttpClient proxy;
       
        public async Task<string> GetPasswordbyUserID(string Method, int UserID)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/UserProfile/");
            string path = Method + "?" + "UserID=" + UserID;
            var UserNameExists = proxy.GetAsync(path);
            UserNameExists.Wait();
            var read = UserNameExists.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<string>();
                displaydetails.Wait();
                string  Password = displaydetails.Result;
                return Password;
            }
            return null;

        }
        public async Task<bool> UpdatePassword(string Method, string NewPassword, int UserID)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/UserProfile/");
            string path = Method + "?" + "NewPassword=" + NewPassword +"&"+ "UserID="+ UserID;
            var updatepwd = proxy.GetAsync(path);
            updatepwd.Wait();
            var read = updatepwd.Result;
            if (read.IsSuccessStatusCode)
            {
                var pwd = read.Content.ReadAsAsync<bool>();
                pwd.Wait();
                bool Password = pwd.Result;
                return Password;
            }
            return false;
        }
    }
}