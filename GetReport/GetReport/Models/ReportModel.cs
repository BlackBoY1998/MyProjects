using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetReport.Models
{
    public class ReportModel
    {
        public string year { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
        public string MonthName { get; set; }
        public int OrderCount { get; set; }

    }
}