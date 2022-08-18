using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Reportshower.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            string url = ConfigurationManager.AppSettings["SSRSURL"].ToString();
            ReportViewer view = new ReportViewer();
            view.ProcessingMode = ProcessingMode.Remote;
            view.ZoomMode = ZoomMode.FullPage;
            view.Width = Unit.Percentage(1000);
            view.Height = Unit.Pixel(1000);
            view.SizeToReportContent = true;
            view.AsyncRendering = true;
            view.ServerReport.ReportServerUrl = new Uri(url);
            view.ServerReport.ReportPath = "folder name/ReportName";


            ViewBag.Report = view;
            return View();
        }
    }
}