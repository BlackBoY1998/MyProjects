using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Implementation_of_charts.Models;
using System.Data.SqlClient;
using System.Data;

namespace Implementation_of_charts.Controllers
{
    public class DisplayController : Controller
    {
        private SqlConnection con;
        string constr = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=Northwind;User ID=sa;Password=ccntspl@123";
        ProductEntities pe;
        EmployeeEntities e;
        public DisplayController()
        {
          
            pe = new ProductEntities();
            e = new EmployeeEntities();
        
        }
        // GET: Display
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Line()
        {
            //data of Product Catogrey wise
            var query = pe.Order_Details.Include("Product").GroupBy(p => p.Product.ProductName).Select(q => new { Product = q.Key, Quantity = q.Sum(r => r.Quantity) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Pie()
        {
            return View();

        }
        public ActionResult PieData()
        {
            //Data of Employees Citywise
            int London = e.Employees.Where(a => a.City == "London").Count();
            int Seattle = e.Employees.Where(a => a.City == "Seattle").Count();
            int Tacoma = e.Employees.Where(a => a.City == "Tacoma").Count();
            int Kirkland= e.Employees.Where(a => a.City == "Kirkland").Count();
            int Redmond = e.Employees.Where(a => a.City == "Redmond").Count();
            Country c = new Country();
            c.London = London;
            c.Seattle = Seattle;
            c.Kirkland = Kirkland;
            c.Tacoma = Tacoma;
            c.Redmond = Redmond;
            return Json(c, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Bar()
        {
            return View();
        }
        public ActionResult BarData()
        {
            //var query = c.Products.Include("Product").GroupBy(p => p.Category.CategoryName).Select(q => new { Categorey = q.Key, Quantity = q.Sum(r => r.ProductID) }).ToList();
            var query1 = ListCountData();
           // return View();
            return Json(query1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Coloumn()
        {
            return View();
        }
        public ActionResult DisplayAllGraphs()
        {
            return View();
        }

        public List<ProductAndCategorey> ListCountData()
        {
            int i = 1;
            List<ProductAndCategorey> lstTemplate = new List<ProductAndCategorey>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetProductcount", con);
            cmd.Parameters.AddWithValue("@Option", i);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new ProductAndCategorey
                {
                    CategoreyName = sdr["CategoryName"].ToString(),
                    ProductCount = Convert.ToInt32(sdr["ProductCount"])
                });

            }
            return lstTemplate;
        }
       
    }
}