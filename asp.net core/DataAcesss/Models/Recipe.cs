using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcesss.Models
{
    public class Recepie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
