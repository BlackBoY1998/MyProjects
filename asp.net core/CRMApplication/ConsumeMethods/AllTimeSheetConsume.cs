using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class AllTimeSheetConsume
    {
        HttpClient proxy;
        public AllTimeSheetConsume()
        {
            proxy = new HttpClient();
        }
        public List<TimeSheetDetailsView> TimesheetDetailsbyTimeSheetMasterID(string Method, int UserID, int TimeSheetMasterID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllTimeSheet/");

            var Parameters = proxy.GetAsync(Method + "?UserID=" + UserID+"&"+ "TimeSheetMasterID="+ TimeSheetMasterID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<TimeSheetDetailsView>>();
                Data.Wait();
                var timeSheetDetails = Data.Result;
                return timeSheetDetails;
            }
            return null;
        }
        public List<GetProjectNames> GetProjectNamesbyTimeSheetMasterID(string Method, int TimeSheetMasterID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllTimeSheet/");

            var Parameters = proxy.GetAsync(Method + "?TimeSheetMasterID=" + TimeSheetMasterID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<GetProjectNames>>();
                Data.Wait();
                var Projecttimesheet = Data.Result;
                return Projecttimesheet;
            }
            return null;
        }
        public List<GetPeriods> GetPeriodsbyTimeSheetMasterID(string Method,int TimeSheetMasterID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllTimeSheet/");

            var Parameters = proxy.GetAsync(Method + "?TimeSheetMasterID=" + TimeSheetMasterID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<GetPeriods>>();
                Data.Wait();
                var Projecttimesheet = Data.Result;
                return Projecttimesheet;
            }
            return null;
        }
    }
}