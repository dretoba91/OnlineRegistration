﻿using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineRegistration.DBAccess
{
    public class LogInStudent_DBAccess
    {
        SqlConnection con;

        public LogInStudent_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<LogInData> GetStudent(UserInfo obj=null)
        {
            con.Open();
            SqlCommand _command = new SqlCommand();

            string query = "SELECT A.*,B.*,C.* "+    
                            "FROM UserInfo A "+
                            "LEFT JOIN Student B "+
	                            "ON A.EmpOrStdId=B.StudentId "+
                            "LEFT JOIN Department C "+
	                            "ON B.DepartmentId=C.DepartmentId "+
                            "WHERE A.UserName='"+obj.UserName+ "' AND A.Password='"+obj.Password+
                                                "' AND A.UserTypeCode='"+obj.UserTypeCode+"';";

            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt=new DataTable();
            _adapter.Fill(dt);

            List<LogInData> LogInStu_list = new List<LogInData>();

            foreach(DataRow dr in dt.Rows)
            {
                LogInData logdata = new LogInData();
                logdata.UserName = dr["UserName"].ToString();
                logdata.UserId = Convert.ToInt64(dr["UserId"].ToString());
                logdata.Password = dr["Password"].ToString();
                logdata.UserTypeCode = Convert.ToInt16(dr["UserTypeCode"].ToString());
                logdata.EmpOrStdId = Convert.ToInt64(dr["EmpOrstdId"].ToString());

                logdata.StudentCode = dr["StudentCode"].ToString();
                logdata.StudentName = dr["StudentName"].ToString();
                logdata.FatherName = dr["FatherName"].ToString();
                logdata.MotherName = dr["MotherName"].ToString();
                logdata.Hall = dr["Hall"].ToString();
                logdata.Address = dr["Address"].ToString();
                logdata.ContactNo = dr["ContactNo"].ToString();
                logdata.Email = dr["Email"].ToString();
                logdata.BloodGroup = dr["BloodGroup"].ToString();
                logdata.Sex = dr["Sex"].ToString();
                logdata.AdmissionYear = dr["AdmissionYear"].ToString();
                logdata.DepartmentName = dr["DepartmentName"].ToString();
                logdata.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString());

                logdata.BioPhoto = (byte[])dr["BioPhoto"];

                string imageBase64 = Convert.ToBase64String((byte[])dr["BioPhoto"]);
                string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);

                logdata.PhotoByteString = imageSrc;

                LogInStu_list.Add(logdata);
            }
            return LogInStu_list;
        }
    }
}