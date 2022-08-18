using System;
using System.Collections.Generic;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class CustomersN
    {
        public string Id { get; set; }
        public string Uccno { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string DepositoryType { get; set; }
        public string PanCardNo { get; set; }
        public string Details { get; set; }
    }
}
