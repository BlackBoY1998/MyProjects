using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapiusingsp.Models;
using System.Net.Http;
using Microsoft.Reporting.WebForms;//Exporting data from SQL to PDF 
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using LinqToExcel; //for importing data from Excel to Database
using System.Data.Entity.Validation;
using PagedList;
using PagedList.Mvc;

namespace Webapiusingsp.Controllers
{
    public class StudentUIController : Controller
    {
        Entities db;
        public StudentUIController()
        {
            db=new Entities();
        }
        // GET: StudentUI
        public ActionResult Index(string search, string Order, string current, int?i)
        {
            
            var result = Session["Count"];
            if(result!=null)
            {
                ViewBag.count=result;
            }
            IEnumerable<Student> stdobj = null;
            HttpClient hc = new HttpClient(); //class for sending HTTP request and Receving Http Request
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");
            var Consume = hc.GetAsync("Student"); //it will call method from api from URL
            Consume.Wait(); //wait for task for exexcution

            var read = Consume.Result;
            if(read.IsSuccessStatusCode) //success status code is 200
            {
                var display = read.Content.ReadAsAsync<IList<Student>>();
                display.Wait();
                stdobj = display.Result;
            }

            ViewBag.currentsortorder = Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Order) ? "Name" : "";
            if (search != null)
            {
                i = 1;
            }
            else
            {
                search = current;
            }
            ViewBag.current = search;
            var student = from s in db.Students
                          select s;
            if (!string.IsNullOrEmpty(search)) //it will show all data or searched name in return
            {
                student = student.Where(s => s.StdName.Contains(search));
            }
            switch (Order)
            {
                case "Name":
                    stdobj = student.OrderByDescending(s => s.StdName);
                    break;
                default:
                    stdobj = student.OrderBy(s => s.StdName);
                    break;

            }

            
         
            return View(stdobj.ToList().ToPagedList(i??1,10));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var InsertStd = hc.PostAsJsonAsync<Student>("Student", student);
            InsertStd.Wait();

            var save = InsertStd.Result;
            if(save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Details(int id)
        {
            StudentClass obj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var details = hc.GetAsync("Student?id=" + id.ToString());
            details.Wait();

            var read = details.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<StudentClass>();
                displaydetails.Wait();
                obj = displaydetails.Result;
            }
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            StudentClass obj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/");

            var details = hc.GetAsync("Student?id=" + id.ToString());
            details.Wait();

            var read = details.Result;
            if (read.IsSuccessStatusCode)
            {
                var displaydetails = read.Content.ReadAsAsync<StudentClass>();
                displaydetails.Wait();
                obj = displaydetails.Result;
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(StudentClass std)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");

            var updateStudent = hc.PutAsJsonAsync<StudentClass>("Student", std);
            updateStudent.Wait();

            var save = updateStudent.Result;
            if (save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(std);

        }
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:51333/api/Student");

            var deletestudent = hc.DeleteAsync("Student/" + id.ToString());
            deletestudent.Wait();

            var save = deletestudent.Result;
            if (save.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    
        public ActionResult CheckRadio(String Type)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/StudentReport/Student.rdlc");

            ReportDataSource reportdatasource = new ReportDataSource();
            reportdatasource.Name = "StudentDataset";
            reportdatasource.Value=db.Students.ToList();
            localreport.DataSources.Add(reportdatasource);
            string type=Type;
            string mimeType;
            string encoding;
            string Extension;
            if(Type=="Excel")
            {
                Extension="xlsx";
            }
            else if(Type=="PDF")
            {
                Extension="pdf";
            }
            else
            {
                Extension="jpg";
            }
            string[] streams;
            Warning[] warning;
            byte[] renderedbyte;

            renderedbyte=localreport.Render(type,"",out mimeType,out encoding,out Extension,out streams,out warning);
            Response.AddHeader("content-disposition","attachment;filename=student_report."+Extension);
            return File(renderedbyte,Extension);


        }

        [HttpPost]
        public ActionResult Import(ImportExcel importExcel, HttpPostedFileBase FileUpload)//in fileupload it will take filename from viewpage
        {
            
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                //In contenttype there is multimedia type
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Content/Upload/"); //stored file in server for temp
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))//two type of Excel latest and Old one
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }
                    //query for Retriving data from sheet1
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    string sheetName = "Sheet1";

                    //for storing file name and Extension of file
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);

                    //putting data in database from Excelsheet 
                    var artistAlbums = from a in excelFile.Worksheet<Student>(sheetName) select a; //LINQ query for Retriving data

                    var counter = 0;
                    //checking data whether it is null or not
                    //Note:your Excelsheet coloumn name should match with database coloumn name and Excel sheet should be closed after Editing
                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.StdName != "" && a.StdEmail != "")
                            {
                                Student TU = new Student();
                                TU.StdName = a.StdName;
                                TU.StdEmail = a.StdEmail;
                                db.Students.Add(TU);
                                db.SaveChanges();
                                counter++;
                                Session["Count"] = counter;
                               
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.StdName == "" || a.StdName == null) data.Add("<li> name is required</li>");
                                if (a.StdEmail == "" || a.StdEmail == null) data.Add("<li> Email is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                            
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        //deleting Existing Excel File(If Exists)
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return RedirectToAction("Index");
                    //return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    //return RedirectToAction("Index");
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //if user not selected any file then it will show error
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                //return RedirectToAction("Index");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

      


        //public ActionResult Search(string search) //serach value from textbox will come here
        //{
        //    //rertiving all data
        //    var student = from s in db.Students
        //                  select s;
        //    if(!string.IsNullOrEmpty(search)) //it will show all data or searched name in return
        //    {
        //        student = student.Where(s => s.StdName.Contains(search));
        //    }
        //    return View("Index",student.ToList());

        //}
        //public ActionResult sort(string Order)
        //{
        //    ViewBag.SortingName = String.IsNullOrEmpty(Order) ? "Name" : "";
        //    var student = from s in db.Students select s;
        //    switch(Order)
        //    {
        //        case "Name":
        //            student = student.OrderByDescending(s => s.StdName);
        //            break;
        //        default:
        //            student = student.OrderBy(s => s.StdName);
        //            break;

        //    }
        //    return View("Index",student.ToList());

        //}
        //public ViewResult AllActions(string search, string Order, string current, int? page)
        //{
        //    ViewBag.currentsortorder = Order;
        //    ViewBag.SortingName = String.IsNullOrEmpty(Order) ? "Name" : "";
        //    var modelList = new List<Student>();
          
        //    if (search != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        search = current;
        //    }
        //    ViewBag.current = search;
        //    var student = from s in db.Students
        //                  select s;
        //    if (!string.IsNullOrEmpty(search)) //it will show all data or searched name in return
        //    {
        //        student = student.Where(s => s.StdName.Contains(search));
        //    }
        //    switch (Order)
        //    {
        //        case "Name":
        //            modelList = student.OrderByDescending(s => s.StdName).ToList();
        //            break;
        //        default:
        //            modelList = student.OrderBy(s => s.StdName).ToList();
        //            break;

        //    }
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(modelList.ToPagedList(pageNumber, pageSize));


        //}
      

       
    }
}