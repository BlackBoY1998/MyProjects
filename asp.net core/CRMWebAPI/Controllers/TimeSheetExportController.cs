using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRMWebAPI.Controllers
{
    public class TimeSheetExportController : ApiController
    {
        ITimeSheetExport _ITimeSheetExport;
        ITimeSheet _ITimeSheet;
        public TimeSheetExportController()
        {
            _ITimeSheetExport = new TimeSheetExportData();
            _ITimeSheet = new TimeSheetData();
        }

        [HttpGet]
        public IHttpActionResult GetUsernamebyRegistrationID(int RegistrationID)
        {
           var filename=_ITimeSheetExport.GetUsernamebyRegistrationID(RegistrationID);
            return Ok(filename);
        }
        [HttpGet]
        public IHttpActionResult GetPeriodsbyTimeSheetMasterID(int TimeSheetMasterID)
        {
          var data =  _ITimeSheet.GetPeriodsbyTimeSheetMasterID(TimeSheetMasterID);
           return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetProjectNamesbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            var Project=_ITimeSheet.GetProjectNamesbyTimeSheetMasterID(TimeSheetMasterID);
            return Ok(Project);
        }
        [HttpGet]
        public IHttpActionResult ListofEmployees(int UserID)
        {
            var Emp=_ITimeSheetExport.ListofEmployees(UserID);
            return Ok(Emp);
        }
    }
}
