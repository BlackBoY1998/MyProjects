using Implementaion_of_charts_by_Time.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Implementaion_of_charts_by_Time.Controllers
{
    public class DefaultController : Controller
    {
        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123";
        // GET: Default
        public ActionResult Index()
        {
            var query = ListCountData();
            return Json(query,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Line()
        {
            return View();
        }
        public List<Data> ListCountData()
        {
            int i = 1;
            List<Data> lstData = new List<Data>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("dbo.Get5MinDailyReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstData.Add(new Data
                {
                    WithinTime = sdr["WithinTime"].ToString(),
                    CountofEntries = Convert.ToInt32(sdr["CountofEntries"])
                });

            }
            return lstData;
        }
        public ActionResult chart()
        {
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
    }
}