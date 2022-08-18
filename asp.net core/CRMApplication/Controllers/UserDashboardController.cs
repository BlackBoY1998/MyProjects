using CRMApplication.ConsumeMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMApplication.Controllers
{
    public class UserDashboardController : Controller
    {
       private UserConsume objUser;
        public UserDashboardController()
        {
            objUser = new UserConsume();
        }
        public ActionResult Dashboard()
        {
            var timesheetResult = objUser.GetData("GetTimeSheetsCountByUserID", Convert.ToString(Session["UserID"]));

            if (timesheetResult != null)
            {
                ViewBag.SubmittedTimesheetCount = timesheetResult.SubmittedCount;
                ViewBag.ApprovedTimesheetCount = timesheetResult.ApprovedCount;
                ViewBag.RejectedTimesheetCount = timesheetResult.RejectedCount;
            }
            else
            {
                ViewBag.SubmittedTimesheetCount = 0;
                ViewBag.ApprovedTimesheetCount = 0;
                ViewBag.RejectedTimesheetCount = 0;
            }


            var expenseResult = objUser.GetData("GetExpenseAuditCountByUserID", Convert.ToString(Session["UserID"]));

            if (expenseResult != null)
            {
                ViewBag.SubmittedExpenseCount = expenseResult.SubmittedCount;
                ViewBag.ApprovedExpenseCount = expenseResult.ApprovedCount;
                ViewBag.RejectedExpenseCount = expenseResult.RejectedCount;
            }
            else
            {
                ViewBag.SubmittedExpenseCount = 0;
                ViewBag.ApprovedExpenseCount = 0;
                ViewBag.RejectedExpenseCount = 0;
            }

            return View();
        }
    }
}