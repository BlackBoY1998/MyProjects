using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CRMApplication.ConsumeMethods
{
    public class AllExpenseConsume
    {
        HttpClient proxy;
        public AllExpenseConsume()
        {
            proxy = new HttpClient();
        }
        public async Task<bool> IsExpenseSubmitted(string Method, int ExpenseID, int UserID)
        {

           
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllExpense/");

            var Parameters = proxy.GetAsync(Method + "?ExpenseID=" + ExpenseID +"&"+ "UserID=" + UserID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<bool>();
                Data.Wait();
                bool result = Data.Result;
                return result;
            }
            return false;
        }

        public async Task<int> DeleteExpensetByExpenseID(string Method,int ExpenseID, int UserID)
        {
            int Result = 0;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllExpense/");

            var Parameters = proxy.GetAsync(Method + "?ExpenseID=" + ExpenseID + "&" + "UserID=" + UserID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<int>();
                Data.Wait();
                int result = Data.Result;
                return result;
            }
            return Result;
        }

        public ExpenseModelView ExpenseDetailsbyExpenseID(string Method,int ExpenseID)
        {
            ExpenseModelView expenseModelView;
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllExpense/");

            var Parameters = proxy.GetAsync(Method + "?ExpenseID=" + ExpenseID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<ExpenseModelView>();
                Data.Wait();
                expenseModelView = Data.Result;
                return expenseModelView;
            }
            return null;
        }

        public List<DocumentsVM> GetListofDocumentByExpenseID(string Method,int? ExpenseID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllExpense/");

            var Parameters = proxy.GetAsync(Method + "?ExpenseID=" + ExpenseID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<List<DocumentsVM>>();
                Data.Wait();
                var document = Data.Result;
                return document;
            }
            return null;
        }

        public Documents GetDocumentByExpenseID(string Method,int? ExpenseID, int? DocumentID)
        {
            proxy.BaseAddress = new Uri("https://localhost:44304/api/AllExpense/");

            var Parameters = proxy.GetAsync(Method + "?ExpenseID=" + ExpenseID + "&" + "DocumentID="+ DocumentID);
            Parameters.Wait();

            var save = Parameters.Result;
            if (save.IsSuccessStatusCode)
            {
                var Data = save.Content.ReadAsAsync<Documents>();
                Data.Wait();
                var document = Data.Result;
                return document;
            }
            return null;
        }
    }
}