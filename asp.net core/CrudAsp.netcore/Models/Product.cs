using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAsp.netcore.Models
{
    public class Product:BaseEntity
    {
        [Key]
        public int pid { get; set; }

        [Required(ErrorMessage ="Product Name required")]
        public string pname { get; set; }
    }
}
