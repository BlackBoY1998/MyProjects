using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCodeFirstEmail.Models
{
    public class EmailContent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }


    }
}
