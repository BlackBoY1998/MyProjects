using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using context = System.Web.HttpContext;  
  

namespace crudusingEntity.Models
{
    public static class ExceptionLogging
    {

        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

        public static void SendErrorToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();

            try
            {
                string filepath = context.Current.Server.MapPath("~/ExceptionDetailsFile/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {


                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
        public static void Logger(string Message)
        {
            StreamWriter sw = null;
            string logfolder = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string path = context.Current.Server.MapPath("~/ExceptionDetailsFile/"); ;
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                string LogPath = path + "/" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
                if (!System.IO.File.Exists(LogPath))
                {
                    FileStream fs = System.IO.File.Create(LogPath);
                    fs.Close();
                }
                else
                {
                    //sw = new StreamWriter(LogPath, true);
                    //// string data = "Date: " + DateTime.Now + ", user ID:" + AgentID + ", currentcampaign :" + currentcampaign.ID + ":" + currentcampaign.Name + ", Host Name:" + strHostName + ", extension :" + extension + "ip1:" + strHostName + "hostname" + Hostname[0];
                    //string data = "Date: " + DateTime.Now + ", Message:" + Message;
                    //sw.WriteLine(data);
                    sw = new StreamWriter(LogPath, true);
                    string data = "Log Written Date:" + " " + DateTime.Now.ToString() +" "+ "Message" +" "+Message;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(data);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();

            }
        }


    } 
}