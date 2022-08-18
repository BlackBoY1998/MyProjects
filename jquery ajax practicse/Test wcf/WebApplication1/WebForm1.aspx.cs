using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Calc p1 = new ServiceReference1.Calc();
                p1.First = Convert.ToInt32(TextBox1.Text);

                ServiceReference1.Calc p2 = new ServiceReference1.Calc();
                p2.First = Convert.ToInt32(TextBox2.Text);

                ServiceReference1.CalcArithmeticserviceClient obj = new ServiceReference1.CalcArithmeticserviceClient();
                ServiceReference1.Calc Result = obj.Add(p1, p2);
                ServiceReference1.Calc Result2 = obj.Sub(p1, p2);
                ServiceReference1.Calc Result3 = obj.Mul(p1, p2);
                Label3.Text = Result.First.ToString();
                Label4.Text = Result2.First.ToString();
                Label5.Text = Result3.First.ToString();

            }
            catch(Exception ex)
            {
                Response.Write("Exception occurred" + ex);
            }

        }
    }
}