using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailSender.Models
{
    public class TemplateMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Template { get; set; }
        public DateTime DOC { get; set; }
    }
}