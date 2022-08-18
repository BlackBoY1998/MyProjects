using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using CRMModelClassLiberary;


namespace CRMWebAPI.Repository
{
    public class LoginData:ILogin
    {
        public Registration ValidateUser(string userName, string password)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    var validate = (from user in _context.Registration
                                    where user.Username == userName && user.Password == password
                                    select user).SingleOrDefault();

                    return validate;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdatePassword(string NewPassword, int UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@NewPassword", NewPassword);
                    param.Add("@UserID", UserID);
                    var result = con.Execute("Usp_Updatepassword", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public string GetPasswordbyUserID(int UserID)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    var password = (from temppassword in _context.Registration
                                    where temppassword.RegistrationID == UserID
                                    select temppassword.Password).FirstOrDefault();

                    return password;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Registration ILogin.ValidateUser(string userName, string passWord)
        //{
        //    throw new NotImplementedException();
        //}
    }
}