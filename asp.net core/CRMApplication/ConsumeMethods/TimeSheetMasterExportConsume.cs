using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class TimeSheetMasterExportConsume
    {
        HttpClient proxy;
        public List<GetProjectNames> GetProjectNamesbyTimeSheetMasterID(string Method,int timesheetID)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetMasterExport/");
            var Insertparameter = proxy.GetAsync(Method + "?timesheetID=" + timesheetID);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<List<GetProjectNames>>();
                result.Wait();
                var data = result.Result;
                return data;
            }
            return new List<GetProjectNames>();
        }
        public dynamic GetPeriodsbyTimeSheetMasterID(string Method,int timesheetID)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetMasterExport/");
            var Insertparameter = proxy.GetAsync(Method + "?timesheetID=" + timesheetID);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<DataSet>();
                result.Wait();
                var data = result.Result;
                return data;
            }
            return null;
        }

        public DataSet GetTimeSheetMasterIDTimeSheet(string Method, DateTime? FromDate, DateTime? ToDate)
        {
            proxy = new HttpClient();
            proxy.BaseAddress = new Uri("https://localhost:44304/api/TimeSheetMasterExport/");
            var Insertparameter = proxy.GetAsync(Method + "?FromDate=" + FromDate+"&"+ "ToDate="+ToDate);
            Insertparameter.Wait();

            var save = Insertparameter.Result;
            if (save.IsSuccessStatusCode)
            {
                var result = save.Content.ReadAsAsync<DataSet>();
                result.Wait();
                var data = result.Result;
                return data;
            }
            return null;
        }
    }
}