using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class AllUsersConsume
    {
        HttpClient proxy;
        public AllUsersConsume()
        {
            proxy = new HttpClient();   
        }
        public RegistrationViewDetailsModel GetUserDetailsByRegistrationID(string Method, int? RegistrationID)
        {
            RegistrationViewDetailsModel registrationViewDetailsModel;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllUsers/");
            var Insertparameter = proxy.GetAsync(Method + "?RegistrationID=" + RegistrationID);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<RegistrationViewDetailsModel>();
                result.Wait();
                registrationViewDetailsModel = result.Result;
                return registrationViewDetailsModel;
            }
            return null;
        }

        public RegistrationViewDetailsModel GetAdminDetailsByRegistrationID(string Method, int? RegistrationID)
        {
            RegistrationViewDetailsModel registrationViewDetailsModel;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllUsers/");
            var Insertparameter = proxy.GetAsync(Method + "?RegistrationID=" + RegistrationID);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<RegistrationViewDetailsModel>();
                result.Wait();
                registrationViewDetailsModel = result.Result;
                return registrationViewDetailsModel;
            }
            return null;
        }
    }
}