﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModelClassLiberary
{
    [NotMapped]
    public class NotificationsTB_ViewModel
    {
        [Key]
        public int NotificationsID { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
        public string CreatedOn { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? Min { get; set; }
    }
}
