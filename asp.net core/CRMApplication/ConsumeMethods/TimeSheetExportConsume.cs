using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class TimeSheetExportConsume
    {
        HttpClient proxy;
        public TimeSheetExportConsume()
        {
            proxy = new HttpClient();
        }

        public async Task<string> GetUsernamebyRegistrationID(string Method, int RegistrationID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetExport/");

            var Parameters = proxy.GetAsync(Method + "?RegistrationID=" + RegistrationID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<string>();
                Data.Wait();
                string username = Data.Result;
                return username;
            }
            return null;
        }


        public List<GetPeriods> GetPeriodsbyTimeSheetMasterID(string Method,int TimeSheetMasterID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetExport/");

            var Parameters = proxy.GetAsync(Method + "?TimeSheetMasterID=" + TimeSheetMasterID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<GetPeriods>>();
                Data.Wait();
                var periods = Data.Result;
                return periods;
            }
            return new List<GetPeriods>();

        }
        public List<GetProjectNames> GetProjectNamesbyTimeSheetMasterID(string Method,int TimeSheetMasterID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetExport/");

            var Parameters = proxy.GetAsync(Method + "?TimeSheetMasterID=" + TimeSheetMasterID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<GetProjectNames>>();
                Data.Wait();
                var Projectname = Data.Result;
                return Projectname;
            }
            return new List<GetProjectNames>();

        }

        public List<Registration> ListofEmployees(string Method,int UserID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetExport/");

            var Parameters = proxy.GetAsync(Method + "?UserID=" + UserID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<Registration>>();
                Data.Wait();
                var Employee = Data.Result;
                return Employee;
            }
            return null;
        }
    }
}