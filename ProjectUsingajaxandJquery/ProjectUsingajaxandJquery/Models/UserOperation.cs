using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProjectUsingajaxandJquery.Models
{
    public class UserOperation
    {
        string cs = ConfigurationManager.ConnectionStrings["usercon"].ConnectionString;
        SqlConnection con;
        public List<User> ListAllusers()
        {
            List<User> lstuser = new List<User>();
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("selectUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lstuser.Add(new User
                {
                    Id = Convert.ToInt32(sdr["Id"]),
                    Name = sdr["Name"].ToString(),
                    UserName = sdr["UserName"].ToString(),
                    Password = sdr["Password"].ToString(),
                    Gender = sdr["Gender"].ToString(),
                    DOB = Convert.ToDateTime(sdr["DOB"]),
                    EmailId = sdr["EmailID"].ToString(),
                    Mobile = sdr["Mobile"].ToString(),
                    Landline = sdr["Landline"].ToString(),
                    Address = sdr["Address"].ToString(),
                    Pincode = sdr["Pincode"].ToString(),
                });

            }
            return lstuser;
        }
        public int AddUser(User user)
        {
            int i;
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("InsertupdateUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@DOB", user.DOB);
            cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Landline", user.Landline);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@PinCode", user.Pincode);
            cmd.Parameters.AddWithValue("@Operation", "Insert");
            i = cmd.ExecuteNonQuery();
            return i;
        }
        public int updateuser(User user)
        {
            int i;
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("InsertupdateUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@DOB", user.DOB);
            cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Landline", user.Landline);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@PinCode", user.Pincode);
            cmd.Parameters.AddWithValue("@Operation", "Update");
            i = cmd.ExecuteNonQuery();
            return i;

        }

        public int deleteuser(int Id)
        {
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            int i = cmd.ExecuteNonQuery();
            return i;
        }

    }
}