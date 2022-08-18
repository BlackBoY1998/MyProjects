using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;  
using System.Web.UI.WebControls;
using FileUpload_And_Download.Models;

namespace FileUpload_And_Download.DataAccessLayer
{
    public class DatabaseLogic
    {
        string connectionString = @"Data Source=NEXSUS-DV67\CCNEXSUS;Initial Catalog=Akash;User ID=sa;Password=ccntspl@123";
        public DataTable GetFileDetails()
        {
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand command = new SqlCommand("Select * From  dbo.TableDetails", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }

        public bool SaveFile(FileUpload_And_Download.Models.FileUpload model)
        {
            string strQry = "INSERT INTO  dbo.TableDetails (FileName,FilePath) VALUES('" +
                model.FileName + "','" + model.FileUrl + "')";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand command = new SqlCommand(strQry, con);
            int numResult = command.ExecuteNonQuery();
            con.Close();
            if (numResult > 0)
                return true;
            else
                return false;
        }  
    }
}