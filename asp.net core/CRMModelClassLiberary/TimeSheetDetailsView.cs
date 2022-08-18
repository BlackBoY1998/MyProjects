using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
    public class TimeSheetDetailsView
    {
        public int TimeSheetID { get; set; }
        public string DaysofWeek { get; set; }
        public int? Hours { get; set; }
        public string Period { get; set; }
        public int? ProjectID { get; set; }
        public int UserID { get; set; }
        public string CreatedOn { get; set; }
        public int TimeSheetMasterID { get; set; }
        public string ProjectName { get; set; }

    }
}
