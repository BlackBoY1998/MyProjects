using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMWebAPI.Repository;
using CRMModelClassLiberary;

namespace CRMWebAPI.Controllers
{
    public class AllExpenseController : ApiController
    {
        IExpense _IExpense;
        IDocument _IDocument;
        public AllExpenseController()
        {
            _IExpense = new ExpenseData();
            _IDocument = new DocumentData();
        }

        [HttpGet]
        public IHttpActionResult IsExpenseSubmitted(int ExpenseID, int UserID)
        {
            bool dataSubmitted = _IExpense.IsExpenseSubmitted(ExpenseID, UserID);
            return Ok(dataSubmitted);
        }
        [HttpGet]
        public IHttpActionResult DeleteExpensetByExpenseID(int ExpenseID, int UserID)
        {
            int result = _IExpense.DeleteExpensetByExpenseID(ExpenseID, UserID);
            return Ok(result);
        }
        [HttpGet]
        public IHttpActionResult ExpenseDetailsbyExpenseID(int ExpenseID)
        {
            ExpenseModelView expenseModelView = null;
            expenseModelView = _IExpense.ExpenseDetailsbyExpenseID(ExpenseID);
            return Ok(expenseModelView);
        }
        [HttpGet]
        public IHttpActionResult GetListofDocumentByExpenseID(int ExpenseID)
        {
            var documents =_IDocument.GetListofDocumentByExpenseID(ExpenseID);
            return Ok(documents);
        }
        [HttpGet]
        public IHttpActionResult GetDocumentByExpenseID(int ExpenseID, int DocumentID)
        {
            var document = _IDocument.GetDocumentByExpenseID(ExpenseID, DocumentID);
            return Ok(document);
        }
    }
}
