using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.ReportSource;
//using CrystalDecisions.Reporting;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;  
  

namespace DemoCrystalReports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Fetch_Click(object sender, EventArgs e)
        {
            CrystalReport1 cr = new CrystalReport1();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["DemoCrystalReports.Properties.Settings.CrystalReportConnectionString"].ToString();
            //int ProductId = int.Parse(txtProductId.Text);
            //string Query = "select * from [Order Details]";
            //DataSet ds = new DataSet();
            //SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            //cr.ParameterFields()


            //cr.Load(Server.MapPath("~/CrystalReport.rpt"));
            //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;User Instance=True");
            //SqlCommand cmd = new SqlCommand("sp_select", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //cr.SetDataSource(dt);
            //cr.SetParameterValue("age1", txtProductId.Text);
            //crystalReportViewer1.ReportSource = cr;
            //crystalReportViewer1.BindingContext();
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load("D:\\akash\\study\\jquery ajax practicse\\DemoCrystalReports\\DemoCrystalReports\\CrystalReport1.rpt");

            ParameterFieldDefinitions crParameterFieldDefinitions ;
            ParameterFieldDefinition crParameterFieldDefinition ;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            crParameterDiscreteValue.Value = txtProductId.Text;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["ProductId"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh(); 
    
        }
    }
}
