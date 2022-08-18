using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace ProductApi.Controllers
{
   [EnableCors(origins: "http://localhost:55152/api/ProductInfo", headers: "*", methods: "*")]  
    public class ProductInfoController : ApiController
    {
        Entities db = new Entities();
        public string Post( ProductInfo productinfo)
        {
            db.ProductInfoes.Add(productinfo);
            db.SaveChanges();
            return "Product Added";
        }
        public IEnumerable<ProductInfo> Get()
        {
            return db.ProductInfoes.ToList();
        }
        public ProductInfo Get(int id)
        {
            ProductInfo emp = db.ProductInfoes.Find(id);
            return emp;
        }
        public string Put(int id, ProductInfo productinfo)
        {
            var pro = db.ProductInfoes.Find(id);
            pro.Name = productinfo.Name;
            pro.Price = productinfo.Price;
            pro.Quantity = productinfo.Quantity;
            pro.Active = productinfo.Active;

            db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "Product Updated";

        }
        public string Delete(int id)
        {
            ProductInfo productinfo = db.ProductInfoes.Find(id);
            db.ProductInfoes.Remove(productinfo);
            db.SaveChanges();
            return "Product Deleted";
        }
    }
}
