using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCodeFirst.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string server { get; set; }
        public int PortNo { get; set; }
        public string Username { get; set; }
        public string Passphrase { get; set; }
        public int RequireAuthentication { get; set; }
        public int UseSSL { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public int Active { get; set; }
        

    }
}


