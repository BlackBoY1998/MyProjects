using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DempProject.Models;

namespace DempProject.DataAccessLayer
{
  
    public class ProductOperations
    {
        string cs = ConfigurationManager.ConnectionStrings["Productcon"].ConnectionString;
        SqlConnection con;
        public List<Product> ListAllProduct()
        {
            List<Product> lstuser = new List<Product>();
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(" GetProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstuser.Add(new Product
                {
                    ProductId = Convert.ToInt32(sdr["ProductId"]),
                    ProductName = sdr["ProductName"].ToString(),
                    SupplierID =  Convert.ToInt32(sdr["SupplierID"]),
                    CategoryID = Convert.ToInt32(sdr["CategoryID"]),
                    QuantityPerUnit = sdr["SupplierID"].ToString(),
                    UnitPrice = Convert.ToDouble(sdr["UnitPrice"]),
                    UnitsInStock = Convert.ToInt32(sdr["UnitsInStock"]),
                    UnitsOnOrder = Convert.ToInt32(sdr["UnitsOnOrder"]),
                    ReorderLevel = Convert.ToInt32(sdr["ReorderLevel"]),
                    Discontinued = Convert.ToInt32(sdr["Discontinued"])
                });

            }
            return lstuser;
        }
    }
}