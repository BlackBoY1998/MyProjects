using System;
using System.Collections.Generic;

#nullable disable

namespace TestCodeFirst.Entities
{
    public partial class UsState
    {
        public short StateId { get; set; }
        public string StateName { get; set; }
        public string StateAbbr { get; set; }
        public string StateRegion { get; set; }
    }
}
