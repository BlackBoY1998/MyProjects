using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crudusingEntity.Models
{
    public class LeadManagements
    {
        public int Id { get; set; }
        public string LeadId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string LeadType { get; set; }
        public string Address { get; set; }
        public string LeadSource { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DOC { get; set; }
        public Nullable<System.DateTime> DOM { get; set; }
        public string AssignedToUser { get; set; }
        public string Remarks { get; set; }
        public int TenantId { get; set; }
        public string TelphoneNo1 { get; set; }
        public string TelePhoneNo2 { get; set; }
    }
    public class LeadManagementViewModel
    {
        public List<LeadManagements> Leads { get; set; }
       
    }
}