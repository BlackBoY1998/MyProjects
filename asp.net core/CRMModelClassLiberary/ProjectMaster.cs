using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRMModelClassLiberary
{
    [Table("ProjectMaster")]
    public class ProjectMaster
    {
            [Key]
            public int ProjectID { get; set; }

            [Required(ErrorMessage = "Enter Project Code")]
            public string ProjectCode { get; set; }

            [Required(ErrorMessage = "Enter Nature of Industry")]
            public string NatureofIndustry { get; set; }

            [Required(ErrorMessage = "Enter ProjectName")]
            public string ProjectName { get; set; }

    }
}