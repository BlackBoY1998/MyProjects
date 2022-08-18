using CRMModelClassLiberary;
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
    public class UserDashboardController : ApiController
    {
        private ITimeSheet _ITimeSheet;
        private IExpense _IExpense;

        public UserDashboardController()
        {
            _ITimeSheet = new TimeSheetData();
            _IExpense = new ExpenseData();
        }
        [HttpGet]
        public IHttpActionResult GetTimeSheetsCountByUserID(string UserID)
        {
            DisplayViewModel displayViewModel;
            displayViewModel = _ITimeSheet.GetTimeSheetsCountByUserID(UserID);
            return Ok(displayViewModel);
        }
        [HttpGet]
        public IHttpActionResult GetExpenseAuditCountByUserID(string UserID)
        {
            DisplayViewModel displayViewModel;
            displayViewModel = _IExpense.GetExpenseAuditCountByUserID(UserID);
            return Ok(displayViewModel);
        }
    }
}
