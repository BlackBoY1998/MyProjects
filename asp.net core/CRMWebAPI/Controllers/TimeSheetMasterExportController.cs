using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMWebAPI.Repository;

namespace CRMWebAPI.Controllers
{
    public class TimeSheetMasterExportController : ApiController
    {
        ITimeSheetExport _ITimeSheetExport;
        ITimeSheet _ITimeSheet;
        public TimeSheetMasterExportController()
        {
            _ITimeSheetExport = new TimeSheetExportData();
            _ITimeSheet = new TimeSheetData();
        }
        [HttpGet]
        public IHttpActionResult GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate)
        {
            var timesheet = _ITimeSheetExport.GetTimeSheetMasterIDTimeSheet(FromDate, ToDate);
            return Ok(timesheet);
        }
        [HttpGet]
        public IHttpActionResult GetPeriodsbyTimeSheetMasterID(int timesheetID)
        {
            var data= _ITimeSheet.GetPeriodsbyTimeSheetMasterID(timesheetID);
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetProjectNamesbyTimeSheetMasterID(int timesheetID)
        {
          var ListofProjects= _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(timesheetID);
          return Ok(ListofProjects);
        }
    }
}
