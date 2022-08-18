using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CRMApplication.ConsumeMethods;
using CRMApplication.Filters;

namespace CRMApplication.Controllers
{
    [ValidateUserSession]
    public class AllExpenseController : Controller
    {
        private IExpense _IExpense;
        private AllExpenseConsume objAllExpense;
        public AllExpenseController()
        {
            _IExpense = new ExpenseData();
            objAllExpense = new AllExpenseConsume();
        }
        public ActionResult Expense()
        {
            return View();
        }
        public ActionResult LoadExpenseData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var v = _IExpense.ShowExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult Delete(int ExpenseID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ExpenseID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                Task<bool> dataSubmitted = objAllExpense.IsExpenseSubmitted("IsExpenseSubmitted", ExpenseID, Convert.ToInt32(Session["UserID"]));

                if (dataSubmitted.Result == true)
                {
                    Task<int> data = objAllExpense.DeleteExpensetByExpenseID("DeleteExpensetByExpenseID", ExpenseID, Convert.ToInt32(Session["UserID"]));

                    if (data.Result > 0)
                    {
                        return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(data: "Cannot", behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Details(int ExpenseID)
        {
            try
            {
                //var ExpenseDetails = _IExpense.ExpenseDetailsbyExpenseID(ExpenseID);
                var ExpenseDetails = objAllExpense.ExpenseDetailsbyExpenseID("ExpenseDetailsbyExpenseID", ExpenseID);

                //ViewBag.documents = _IDocument.GetListofDocumentByExpenseID(ExpenseID);
                ViewBag.documents = objAllExpense.GetListofDocumentByExpenseID("GetListofDocumentByExpenseID", ExpenseID);
                return PartialView("_Details", ExpenseDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Download(string ExpenseID, int DocumentID)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ExpenseID)) && !string.IsNullOrEmpty(Convert.ToString(DocumentID)))
                {
                    //var document = _IDocument.GetDocumentByExpenseID(Convert.ToInt32(ExpenseID), Convert.ToInt32(DocumentID));
                    var document = objAllExpense.GetDocumentByExpenseID("GetDocumentByExpenseID", Convert.ToInt32(ExpenseID), Convert.ToInt32(DocumentID));
                    return File(document.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
                }
                else
                {
                    return RedirectToAction("Expense", "ShowAllExpense");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SubmittedExpense()
        {
            return View();
        }

        public ActionResult ApprovedExpense()
        {
            return View();
        }

        public ActionResult RejectedExpense()
        {
            return View();
        }

        public ActionResult LoadSubmittedExpenseData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult LoadApprovedExpenseData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult LoadRejectedExpenseData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}