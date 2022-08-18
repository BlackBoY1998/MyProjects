using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebAPI.Models
{
    public class Customer
    {   [Key]
        public int CustomerID { get; set; }

        [Column(TypeName ="nvarchar(20)")]
        public string CustomerName { get; set; }

        [Column(TypeName="varchar(10)")]
        public string CustomerLocation { get; set; }
    }
}
