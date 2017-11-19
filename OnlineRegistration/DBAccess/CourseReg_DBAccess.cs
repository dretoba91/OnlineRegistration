using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineRegistration.DBAccess
{
    public class CourseReg_DBAccess
    {
        SqlConnection con;

        public CourseReg_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }

        public bool InsertCourseReg(CourseReg obj)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    for (long i = Convert.ToInt64(obj.From); i <= Convert.ToInt64(obj.To);i++ )
                    {
                        SqlCommand _command = new SqlCommand();
                        obj.StudentCode = i.ToString();
                        string query = "INSERT INTO CourseReg(EmployeeId,StudentCode,ControlNoticeId,CourseRegDate,IsDeposite,Approval) VALUES('" +
                            obj.EmployeeId + "','" + obj.StudentCode + "','" + obj.ControlNoticeId + "','" + obj.CourseRegDate + "','" + obj.IsDeposite + "','" +
                            obj.Approval + "')";

                        _command.Connection = con;
                        _command.CommandText = query;
                        _command.Transaction = tran;
                        _command.ExecuteNonQuery();
                    }
                    tran.Commit();
                    
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                        con.Close();
                }
                return true;
            }
        }
    }
}