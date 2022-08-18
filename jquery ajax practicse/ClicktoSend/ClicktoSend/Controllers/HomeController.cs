using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClicktoSend.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection conn= null;
        string con = ConfigurationManager.ConnectionStrings["Add"].ConnectionString.ToString();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser()
        {
            conn=new SqlConnection(con);

           // int Id = 1228;
            int PrefferedGatewayID=1095;
            string Sender="QMF";
            string Mobile = "09867899650";
            string TextMessage = "Dear Aijaz Ansari, Your dedicated Relationship Manager Swapnil Shirsat at Quantum Mutual Fund tried contacting you to update you on your investment with us. Please feel free to contact your RM on 9137256622 or email on ansari.aijaz123@gmail.com. You can also visit www.QuantumAMC.com for more information about our Goal based solutions. - QMF";
            DateTime CreatedOn = DateTime.Now;
            int IsProcessed=0;
            //DateTime? LastProcessedOn = DBNull.Value;
            int Attempts=0;
            //DateTime? SentOn = DBNull.Value;
            //DateTime? DeliveredOn = DBNull.Value;
           string AcknowledgementID="9";
            int Status=0;
            int IsDeleted=0;
            string query = "Insert into Communication_SMS_SMS values(@PrefferedGatewayID,@Sender,@Mobile,@TextMessage,@CreatedOn,@IsProcessed,@LastProcessedOn,@Attempts,@SentOn,@DeliveredOn,@AcknowledgementID,@Status,@IsDeleted)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
           // cmd.Parameters.AddWithValue("@ID", Id);
            cmd.Parameters.AddWithValue("@PrefferedGatewayID", PrefferedGatewayID);
            cmd.Parameters.AddWithValue("@Sender", Sender);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@TextMessage", TextMessage);
            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("@IsProcessed", IsProcessed);
            cmd.Parameters.AddWithValue("@LastProcessedOn", DBNull.Value);
            cmd.Parameters.AddWithValue("@Attempts", Attempts);
            cmd.Parameters.AddWithValue("@SentOn", DBNull.Value);
            cmd.Parameters.AddWithValue("@DeliveredOn", DBNull.Value);
            cmd.Parameters.AddWithValue("@AcknowledgementID", AcknowledgementID);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult UpdateUser()
        {
            conn = new SqlConnection(con);
            string query = " Update Communication_SMS_SMS  set  AcknowledgementID=@AcknowledgementID,CreatedOn=@CreatedOn,IsProcessed=@IsProcessed,LastProcessedOn=@LastProcessedOn,Attempts=@Attempts,SentOn=@SentOn,DeliveredOn=@DeliveredOn ,Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                // cmd.Parameters.AddWithValue("@ID", Id);
                int IsProcessed = 0;
                int Attempts = 0;
                int Status = 0;
                cmd.Parameters.AddWithValue("@AcknowledgementID", "8");
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsProcessed", IsProcessed);
                cmd.Parameters.AddWithValue("@LastProcessedOn", DBNull.Value);
                cmd.Parameters.AddWithValue("@Attempts", Attempts);
                cmd.Parameters.AddWithValue("@SentOn", DBNull.Value);
                cmd.Parameters.AddWithValue("@DeliveredOn", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return View("Index");
        }
    }
}