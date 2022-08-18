using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FileUpload_And_Download.Models
{
    public class Repository
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sync"].ConnectionString);
        public List<EmployeeModel> GetAllMessages()
        {
            var messages = new List<EmployeeModel>();
            using (var cmd = new SqlCommand(@"SELECT [id],  
[Name],[Salary] FROM [dbo].[EMP]", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    messages.Add(item: new EmployeeModel
                    {
                        Id= int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        Name = ds.Tables[0].Rows[i][1].ToString(),
                        Salary = Convert.ToInt32(ds.Tables[0].Rows[i][2]),
                      
                    });
                }
            }
            return messages;
        }
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table.  
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MyHub1.SendMessages();
            }
        }  
    }
}