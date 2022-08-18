using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.Net.Mail;
namespace LogService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SendMail("Windows Service is started at " + DateTime.Now);
            WriteToFile("Windows Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; 
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            SendMail("Windows Service is stopped at " + DateTime.Now);
            WriteToFile("Windows Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            SendMail("Windows Service Recalling Date And Time " + DateTime.Now);
            WriteToFile("Logwritten Date And Time " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        public void SendMail(string MailMessage)
        {
            string To = "kamlesh.deshmukh@nexsussolutions.com";
            string Subject = "Service Status";
            //string Template = t.Template;
            //string cc = frm["Inputcc"];

            //MailMessage mm = new MailMessage("gawdeakash17@gmail.com", mb.To);
            //mm.Subject = mb.Subject;
            //mm.Body = mb.Template;
            //mm.IsBodyHtml = false;

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.EnableSsl = true;


            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = nc;
            //smtp.Send(mm);
            //ViewBag.Msg = "Mail sent Succesfully";
            //return View();
            MailMessage mm = new MailMessage("akash.gawde@nexsussolutions.com", To);
            mm.Subject = Subject;
            mm.Body = MailMessage;
    
            //if (cc != "")
            //{
            //    if (cc != null)
            //    {
            //        string[] CCId = cc.Split(',');
            //        foreach (string CCEmail in CCId)
            //        {
            //            mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
            //        }
            //    }
            //}

            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.logix.in";
            smtp.Port = 587;
            smtp.EnableSsl = true;


            NetworkCredential nc = new NetworkCredential("akash.gawde@nexsussolutions.com", "Akash@171998");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
        }
    }
}
