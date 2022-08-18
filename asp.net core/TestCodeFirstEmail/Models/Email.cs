using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCodeFirstEmail.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string server { get; set; }
        public int PortNo { get; set; } = 587;
        public string Username { get; set; }
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Passphrase { get; set; }
        public int RequireAuthentication { get; set; } = 1;
        public int UseSSL { get; set; } = 1;
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public int Active { get; set; } = 1;

    }
}
