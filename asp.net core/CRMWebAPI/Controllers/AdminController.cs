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
    public class AdminController : ApiController
    {
        private ITimeSheet _ITimeSheet;
        private IExpense _IExpense;
        public AdminController()
        {
            _ITimeSheet = new TimeSheetData();
            _IExpense = new ExpenseData();
        }
        [HttpGet]
        public IHttpActionResult GetTimeSheetsCountByAdminID(string AdminID)
        {
            DisplayViewModel displayViewModel;
            displayViewModel = _ITimeSheet.GetTimeSheetsCountByAdminID(AdminID);
            return Ok(displayViewModel);
        }
        [HttpGet]
        public IHttpActionResult GetExpenseAuditCountByAdminID(string AdminID)
        {
            DisplayViewModel displayViewModel;
            displayViewModel = _IExpense.GetExpenseAuditCountByAdminID(AdminID);
            return Ok(displayViewModel);

        }
        
    }
}
