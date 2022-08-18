using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudusingEntity.Models
{
    public class StateAndZonecs
    {
        public IList<SelectListItem> StateNames { get; set; }
        public IList<SelectListItem> ZoneName { get; set; }  
    }
}