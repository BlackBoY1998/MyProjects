using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailSender.Models
{
    public class MailBody
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string Template { get; set; }
    }
}