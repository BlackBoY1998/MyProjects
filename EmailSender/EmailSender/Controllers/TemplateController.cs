using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailSender.Models;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EmailSender.Controllers
{
    public class TemplateController : Controller
    {
        private SqlConnection con;
        string constr = ConfigurationManager.ConnectionStrings["usercon"].ToString();
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TemplateMaster tobj)
        {
            AddTemplate(tobj);
            return View();
        }
        public ActionResult Mail()
        {
            var result = Session["data"];
            if (result != null)
            {
                ViewBag.setData = result;
            }
            int id =Convert.ToInt32(Session["id"]);
            var Template = ListAllTemplate().Where(x => x.Id == id).FirstOrDefault();
            return View(Template);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Mail(TemplateMaster t,FormCollection frm, HttpPostedFileBase fileUploader)
        {
            try
            {
                string To = frm["InputName"];
                string Subject = frm["InputSubject"];
                //string Template = t.Template;
                string cc = frm["Inputcc"];

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
                mm.Body = t.Template;
                if (fileUploader != null)
                {
                    string fileName = Path.GetFileName(fileUploader.FileName);
                    mm.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                }
                if (cc != "" )
                {
                    if (cc != null)
                    {
                        string[] CCId = cc.Split(',');
                        foreach (string CCEmail in CCId)
                        {
                            mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                        }
                    }
                }

                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.logix.in";
                smtp.Port = 587;
                smtp.EnableSsl = true;


                NetworkCredential nc = new NetworkCredential("akash.gawde@nexsussolutions.com", "");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
                ViewBag.Message = "Sent";
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Error="Error";
                return View();
            }

        }
        public ActionResult GetTemplate()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GetTemplate(string InputTemplate)
        {
            Session["data"] = InputTemplate;
            return RedirectToAction("Mail", "Template");
        }


        public JsonResult show()
        {

            return Json(ListAllTemplate(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Templatedetails(int Id)
        {

            Session["id"] = Id;
            var Template = ListAllTemplate().Find(x => x.Id.Equals(Id));
            return Json(Template, JsonRequestBehavior.AllowGet);
        }

        private void conn()
        {
            con = new SqlConnection(constr);
        }
        private void AddTemplate(TemplateMaster tobj)
        {
            conn();
            SqlCommand cmd = new SqlCommand("AddTemplate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", tobj.Name);
            cmd.Parameters.AddWithValue("@Template", tobj.Template);
            cmd.Parameters.AddWithValue("@DOC", tobj.DOC);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public List<TemplateMaster> ListAllTemplate()
        {
            List<TemplateMaster> lstTemplate = new List<TemplateMaster>();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectTemplate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstTemplate.Add(new TemplateMaster
                {
                    Id = Convert.ToInt32(sdr["Id"]),
                    Name = sdr["Name"].ToString(),
                    DOC = Convert.ToDateTime(sdr["DOC"]),
                    Template = sdr["Template"].ToString()
                });

            }
            return lstTemplate;
        }
    }
}