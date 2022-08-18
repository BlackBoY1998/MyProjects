using CRMApplication.Helpers;
using CRMModelClassLiberary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class SuperAdminConsume
    {
        private ICacheManager _ICacheManager;
        HttpClient proxy;
        public SuperAdminConsume()
        {
            _ICacheManager= new CacheManager();
   
        }
        public async Task<int> GetCount(string Method,string Key)
        {
            proxy = new HttpClient();
            var value = _ICacheManager.Get<object>(Key);
            int TotalCount = Convert.ToInt32(value);
            if (value == null)
            {
                int Totalcount = 0;
                proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");
                var details = proxy.GetAsync(Method);
                details.Wait();

                var read = details.Result;
                if (read.IsSuccessStatusCode)
                {
                    var displaydetails = read.Content.ReadAsAsync<int>();
                    displaydetails.Wait();
                    Totalcount = displaydetails.Result;
                    _ICacheManager.Add(Key, Totalcount);
         
                }
                return Totalcount;
            }
            else
            {
                return TotalCount;
            }
        }
        public async Task<bool> CheckUserNameExists(string Method,string username)
        {
            proxy = new HttpClient();
            //"GetTotalProjectsCounts" + registration.Username
            string path = Method + "?" +"username="+username;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");
            var stringContent = new StringContent(username);
            var UserNameExists = proxy.PostAsync(path,stringContent);
            UserNameExists.Wait();
            var read = UserNameExists.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<bool>();
                displaydetails.Wait();
                bool Isvalid = displaydetails.Result;
                return Isvalid;
            }
            return false;
        }
        public async Task<int> AddUser(string Method,Registration registration)
        {
            proxy = new HttpClient();
            int Count = 0;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");

            var Insert = proxy.PostAsJsonAsync<Registration>("AddUser", registration);
            Insert.Wait();

            var save = Insert.Result;
            if (save.IsSuccessStatusCode)
            {
                var displaydetails = save.Content.ReadAsAsync<int>();
                displaydetails.Wait();
                int count = displaydetails.Result;
                return count;
            }
            return Count;
        }
        public async Task<int> GetRole(string Method,string Role)
        {
            proxy = new HttpClient();
            int RoleId = 0;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");
            //var stringContent = new StringContent(Role);
            string path= Method + "?" + "Role=" + Role;
            var UserNameExists = proxy.GetAsync(path);
            UserNameExists.Wait();
            var read = UserNameExists.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<int>();
                displaydetails.Wait();
                int roleid = displaydetails.Result;
                return roleid;
            }
            return RoleId;

        }
        public AssignRolesModel AssignRoles(string Method)
        {
            AssignRolesModel assignRolesModel = null;
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");
            var GetData = proxy.GetAsync(Method);
            GetData.Wait();
            var read = GetData.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<AssignRolesModel>();
                displaydetails.Wait();
                assignRolesModel = displaydetails.Result;
                return assignRolesModel;
            }
            return assignRolesModel;
        }
        public AssignRolesModel AssignRole(string Method,AssignRolesModel assignRolesModel)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");
            string JsonTest = JsonConvert.SerializeObject(assignRolesModel);
            var GetData = proxy.PostAsJsonAsync<AssignRolesModel>(Method,assignRolesModel);
            GetData.Wait();
            var read = GetData.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<AssignRolesModel>();
                displaydetails.Wait();
                assignRolesModel = displaydetails.Result;
                return assignRolesModel;
            }
            return assignRolesModel;
        }
        public async Task<bool> SaveAssignedRoles(string method, AssignRolesModel AssignRolesModel)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/SuperAdmin/");

            var InsertStd = proxy.PostAsJsonAsync<AssignRolesModel>(method, AssignRolesModel);
            InsertStd.Wait();

            var save = InsertStd.Result;
            if (save.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}