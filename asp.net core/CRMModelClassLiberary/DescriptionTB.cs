using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRMModelClassLiberary
{
    [Table("DescriptionTB")]
    public class DescriptionTB
    {
        [Key]
        public int DescriptionID { get; set; }
        public string Description { get; set; }
        public int? ProjectID { get; set; }
        public int? TimeSheetMasterID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UserID { get; set; }

    }
}