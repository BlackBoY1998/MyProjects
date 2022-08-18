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
    public class AllTimeSheetController : ApiController
    {
        IProject _IProject;
        ITimeSheet _ITimeSheet;

        public AllTimeSheetController()
        {
            _IProject = new ProjectData();
            _ITimeSheet = new TimeSheetData();
        }
        [HttpGet]
        public IHttpActionResult TimesheetDetailsbyTimeSheetMasterID(int UserID, int TimeSheetMasterID)
        {
            var data = _ITimeSheet.TimesheetDetailsbyTimeSheetMasterID(UserID, TimeSheetMasterID);
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetProjectNamesbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            var data= _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(TimeSheetMasterID);
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPeriodsbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            var data = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(TimeSheetMasterID);
            return Ok(data);
        }
    }
}
