using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace FileUpload_And_Download.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sync"].ConnectionString);

        OleDbConnection Econ;

        private void ExcelConn(string filepath)
        {

            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);

            Econ = new OleDbConnection(constr);

        }
        private void InsertExceldata(string fileepath, string filename)
        {

            string fullpath = Server.MapPath("~/UploadedFiles/") + filename;

            ExcelConn(fullpath);

            string query = string.Format("Select * from [{0}]", "Sheet1$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();

            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);

            DataTable dt = ds.Tables[0];

            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "EMP";

            objbulk.ColumnMappings.Add("id", "id");

            objbulk.ColumnMappings.Add("Name", "Name");

            objbulk.ColumnMappings.Add("Salary", "Salary");

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(HttpPostedFileBase file)
        {

            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string filepath = "~/UploadedFiles/" + filename;

            file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), filename));

            InsertExceldata(filepath, filename);



            return View();

        }
    }
}