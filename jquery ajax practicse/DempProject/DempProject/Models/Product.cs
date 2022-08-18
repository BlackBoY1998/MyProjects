using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DempProject.Models
{
    public class Product
    {
      public int ProductId{get;set;}
      public string ProductName{get;set;}
      public int SupplierID{get;set;}  
      public int CategoryID{get;set;}
      public string QuantityPerUnit{get;set;}
      public double UnitPrice{get;set;}
      public int UnitsInStock{get;set;}
      public int UnitsOnOrder{get;set;}
      public int ReorderLevel{get;set;}
      public int Discontinued { get; set; }
    }
}