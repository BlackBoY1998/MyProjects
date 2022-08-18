using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileUpload_And_Download.Models;
using FileUpload_And_Download.DataAccessLayer;
using System.Data;
using System.IO;

namespace FileUpload_And_Download.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index(FileUpload model)
        {
            DatabaseLogic d=new DatabaseLogic();
            List<FileUpload> list = new List<FileUpload>();
            DataTable dtFiles = d.GetFileDetails();
            foreach (DataRow dr in dtFiles.Rows)
            {
                list.Add(new FileUpload
                {
                    FileId = @dr["Id"].ToString(),
                    FileName = @dr["Filename"].ToString(),
                    FileUrl = @dr["FilePath"].ToString()
                });
            }
            model.FileList = list;
            return View(model);  
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            DatabaseLogic d = new DatabaseLogic();
            FileUpload model = new FileUpload();
            List<FileUpload> list = new List<FileUpload>();
            DataTable dtFiles = d.GetFileDetails();
            foreach (DataRow dr in dtFiles.Rows)
            {
                list.Add(new FileUpload
                {
                    FileId = @dr["Id"].ToString(),
                    FileName = @dr["Filename"].ToString(),
                    FileUrl = @dr["FilePath"].ToString()
                });
            }
            model.FileList = list;

            if (files != null)
            {
                var Extension = Path.GetExtension(files.FileName);
                var fileName = "my-file-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                model.FileUrl = Url.Content(Path.Combine("~/UploadedFiles/", fileName));
                model.FileName = fileName;

                if (d.SaveFile(model))
                {
                    files.SaveAs(path);
                    TempData["AlertMessage"] = "Uploaded Successfully !!";
                    return RedirectToAction("Index", "File");
                }
                else
                {
                    ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                return View(model);
            }
            return RedirectToAction("Index", "File");
        }
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        public ActionResult home()
        {
            return View();
        }
        public JsonResult GetMessages()
        {
            List<EmployeeModel> messages = new List<EmployeeModel>();
            Repository r = new Repository();
            messages = r.GetAllMessages();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Tempdata()
        {
            TempData["Temp"] = "Tempdata";
            return RedirectToAction("Tempdata2");
        }
        public ActionResult Tempdata2()
        {
            string a = TempData["Temp"].ToString();
            return View();
        }
    }
}