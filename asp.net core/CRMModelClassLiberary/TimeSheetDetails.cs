﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRMModelClassLiberary
{
    [Table("TimeSheetDetails")]
    public class TimeSheetDetails
    {
        [Key]
        public int TimeSheetID { get; set; }
        public string DaysofWeek { get; set; }
        public int? Hours { get; set; }
        public DateTime? Period { get; set; }
        public int? ProjectID { get; set; }
        public int UserID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int TimeSheetMasterID { get; set; }

    }
}