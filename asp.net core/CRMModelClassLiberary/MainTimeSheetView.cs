using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
    public class MainTimeSheetView
    {
        public List<TimeSheetDetailsView> ListTimeSheetDetails { get; set; }
        public List<GetPeriods> ListofPeriods { get; set; }
        public List<GetProjectNames> ListofProjectNames { get; set; }
        public List<string> ListoDayofWeek { get; set; }
        public int TimeSheetMasterID { get; set; }
    }
}
