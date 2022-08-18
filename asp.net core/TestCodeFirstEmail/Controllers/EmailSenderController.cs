using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCodeFirstEmail.Data;
using TestCodeFirstEmail.Models;
using TestCodeFirstEmail.Helper;
using System.Net.Mail;
using System.Net;

namespace TestCodeFirstEmail.Controllers
{
    public class EmailSenderController : Controller
    {

        private readonly ApplicationDbContext _db;
        public EmailSenderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult EmailList()
        {
            IEnumerable<Email> EmailLst = _db.Emails;
            return View(EmailLst);
        }
        public IActionResult AddEmail()
        {
            return View();
        }
        [HttpPost]
       public IActionResult AddEmail(Email ObjEmail)
       {
            if (ModelState.IsValid)
            {
                ObjEmail.Passphrase = EncryptionLibrary.EncryptText(ObjEmail.Passphrase);
                _db.Emails.Add(ObjEmail);
                _db.SaveChanges();
                return RedirectToAction("EmailList");
            }
            return View("AddEmail");
       }
        public IActionResult MailContent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MailContent(EmailContent ObjContent)
        {
            string server = _db.Emails.Select(n => n.server).First();
            int PortNo = _db.Emails.Select(x => x.PortNo).First();
            string password = EncryptionLibrary.DecryptText (_db.Emails.Select(z => z.Passphrase).First());
            string FromAddress = _db.Emails.Select(t => t.FromAddress).First();
            int SSL = _db.Emails.Select(k => k.UseSSL).First();
            MailMessage mm = new MailMessage(FromAddress, ObjContent.To);
            mm.Subject = ObjContent.Subject;
            mm.Body = ObjContent.Content;
            if (ObjContent.CC != "")
            {
                if (ObjContent.CC != null)
                {
                    string[] CCId = ObjContent.CC.Split(',');
                    foreach (string CCEmail in CCId)
                    {
                        mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                    }
                }
            }

            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = server;
            smtp.Port = PortNo;
            smtp.EnableSsl =Convert.ToBoolean(SSL);


            NetworkCredential nc = new NetworkCredential(FromAddress, password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Sent";
            return View();
        }
    }
}
