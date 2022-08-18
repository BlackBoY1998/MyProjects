using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crudusingEntity.Models
{
    public class Leadtype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public DateTime DOC { get; set; }
        public DateTime DOM { get; set; }
        public int TenantId { get; set; }
        public string Description { get; set; }
    }
}