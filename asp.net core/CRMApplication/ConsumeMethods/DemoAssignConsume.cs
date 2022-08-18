using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class DemoAssignConsume
    {

        HttpClient proxy;
        public DemoAssignConsume()
        {
            proxy = new HttpClient();
        }

        public List<AdminModel> ListofAdmins(string Method)
        {
            List<AdminModel> lstadmin = new List<AdminModel>();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/DemoAssign/");

            var Parameters = proxy.GetAsync(Method);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<AdminModel>>();
                Data.Wait();
                lstadmin = Data.Result;
                return lstadmin;
            }
            return null;
        }

        public List<UserModel> GetListofUnAssignedUsers(string Method)
        {
            List<UserModel> lstuser = new List<UserModel>();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/DemoAssign/");

            var Parameters = proxy.GetAsync(Method);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<UserModel>>();
                Data.Wait();
                lstuser = Data.Result;
                return lstuser;
            }
            return null;
        }

        public void  SaveAssignedRoles(string Method,AssignRolesModel AssignRolesModel)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/DemoAssign/");
            var parameters = proxy.PostAsJsonAsync(Method, AssignRolesModel);
            parameters.Wait();
            var save = parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<bool>();
                Data.Wait();
                bool result = Data.Result;
            }
        }
    }
}