using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRMModelClassLiberary
{
    [Table("NotificationsTB")]
    public class NotificationsTB
    {
        [Key]
        public int NotificationsID { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
        public DateTime? CreatedOn { get; set; }
        [Required(ErrorMessage = "FromDate Required")]
        public DateTime? FromDate { get; set; }
        [Required(ErrorMessage = "ToDate Required")]
        public DateTime? ToDate { get; set; }
    }
}