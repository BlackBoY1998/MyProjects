using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GetReport.Models;
using System.Web.Helpers;

namespace GetReport.Controllers
{
    public class ReportController : Controller
    {

        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=Northwind;User ID=sa;Password=ccntspl@123";
        private void conn()
        {
            con = new SqlConnection(constr);
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            string value1 = frm["cbospan"].ToString();
            //string value = Request.Form["cbospan"].ToString();
            if (value1 == "Hourly")
            {
                var data = ListAllHourlyData();
            }
            return View("Index");

        }
        public JsonResult HourlyReport()
        {
            var data = ListAllHourlyData();
            return Json(data);
        }
        public JsonResult WeeklyReport()
        {
            var data = ListAllWeeklyData();
            return Json(data);
        }
        public JsonResult MonthlyReport()
        {
            var data = ListAllMonthlyData();
            return Json(data);
        }
        public JsonResult YearlyReport()
        {
            var data = ListAllYearlyData();
            return Json(data);
        }
        public List<ReportModel> ListAllHourlyData()
        {
            int i=1;
            List<ReportModel> lstTemplate = new List<ReportModel>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetReport", con);
            cmd.Parameters.AddWithValue("@Option", i);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new ReportModel
                {
                    Date= Convert.ToDateTime(sdr["DATE"]),
                    Hours=Convert.ToInt32(sdr["Hours"]),
                    OrderCount=Convert.ToInt32(sdr["Order Count"])
                });

            }
            return lstTemplate;
        }

        public List<ReportModel> ListAllWeeklyData()
        {
            int i = 2;
            List<ReportModel> lstTemplate = new List<ReportModel>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetReport", con);
            cmd.Parameters.AddWithValue("@Option", i);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new ReportModel
                {
                    Week=Convert.ToInt32(sdr["Week"]),
                    OrderCount = Convert.ToInt32(sdr["Orders COUNT"])
                });

            }
            return lstTemplate;
        }

        public List<ReportModel> ListAllMonthlyData()
        {
            int i = 3;
            List<ReportModel> lstTemplate = new List<ReportModel>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetReport", con);
            cmd.Parameters.AddWithValue("@Option", i);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new ReportModel
                {
                    year = sdr["Year"].ToString(),
                    Month = Convert.ToInt32(sdr["Month"]),
                    MonthName = sdr["MONTH NAME"].ToString(),
                    OrderCount = Convert.ToInt32(sdr["Orders COUNT"])

                });

            }
            return lstTemplate;
        }
        public List<ReportModel> ListAllYearlyData()
        {
            int i = 4;
            List<ReportModel> lstTemplate = new List<ReportModel>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetReport", con);
            cmd.Parameters.AddWithValue("@Option", i);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new ReportModel
                {
                    year = sdr["YEAR"].ToString(),
                    OrderCount = Convert.ToInt32(sdr["Orders COUNT"])

                });

            }
            return lstTemplate;
        }
    }
}