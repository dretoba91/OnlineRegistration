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
    public class Subject_DBAccess
    {
        SqlConnection con;
        public Subject_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<Subject> SelectSubject(Subject obj=null)
        {
            if(con.State!=ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Subject WHERE 1=1";
            if(obj!=null)
            {
                if(obj.SubjectId>0)
                {
                    query = query + " AND SubjectId=" + obj.SubjectId;
                }
                if(obj.SubjectCode!=null && obj.SubjectCode!="")
                {
                    query = query + " AND SubjectCode=" + obj.SubjectCode;
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();

            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<Subject> sub_list = new List<Subject>();
            foreach (DataRow dr in dt.Rows)
            {
                Subject sub = new Subject();
                sub.SubjectName = dr["SubjectName"].ToString();
                sub.SubjectCode = dr["SubjectCode"].ToString();
                sub.SubjectId = Convert.ToInt64( dr["SubjectId"].ToString() );
                sub.Credit = Convert.ToDouble( dr["Credit"].ToString() );
                sub.SubjectType = dr["SubjectType"].ToString();
                sub_list.Add(sub);
            }
            return sub_list;
        }
        public bool InsertSubject(Subject obj)
        {
            try
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO Subject(SubjectCode,SubjectName,SubjectType,Credit) Values('" + obj.SubjectCode + "','" +
                    obj.SubjectName + "','" + obj.SubjectType +"','"+ obj.Credit+ "')";
                _command.CommandText = query;
                _command.Connection = con;
                _command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                if(con.State!=ConnectionState.Closed)
                    con.Close();
            }
            return true;
        }
        public bool DeleteSubject(long SubjectId)
        {
            try
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM Subject WHERE SubjectId=" + SubjectId; 
                _command.CommandText = query;
                _command.Connection = con;
                _command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                if(con.State!=ConnectionState.Closed)
                    con.Close();
            }
            return true;
        }
        public bool UpdateSubject(Subject obj)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "UPDATE Subject SET SubjectName='"+ obj.SubjectName+"',SubjectCode='"+obj.SubjectCode+"',SubjectType='"+
                    obj.SubjectType+"',Credit='"+obj.Credit+"' WHERE SubjectId='"+obj.SubjectId+"'";
                _command.CommandText = query;
                _command.Connection = con;
                _command.ExecuteNonQuery();
            }
            catch
            {
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