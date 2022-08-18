using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Interfaces
{
    public interface IDocument
    {
        int AddDocument(Documents Documents);
        Documents GetDocumentByExpenseID(int? ExpenseID, int? DocumentID);
        List<DocumentsVM> GetListofDocumentByExpenseID(int? ExpenseID);
    }
}