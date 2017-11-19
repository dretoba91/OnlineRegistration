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
    public class ControlNotice_DBAccess
    {
        SqlConnection con;
        public ControlNotice_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<ControlNotice> SelectControlNotice(ControlNotice obj = null)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT A.*,B.DepartmentName,C.LevelCode,D.TermCode " +
                "FROM ControlNotice A " +
                "LEFT JOIN Department B " +
                    "ON A.DepartmentId=B.DepartmentId " +
                "LEFT JOIN Level C " +
                    "ON A.LevelId=C.LevelId " +
                "LEFT JOIN Term D " +
                    "ON A.TermId=D.TermId ";
            if (obj != null)
            {
                if (obj.ControlNoticeId > 0)
                {
                    query = query + " AND ControlNoticeId=" + obj.ControlNoticeId;
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<ControlNotice> connot_list = new List<ControlNotice>();
            foreach (DataRow dr in dt.Rows)
            {
                ControlNotice connot = new ControlNotice();
                connot.ControlNoticeId = Convert.ToInt64(dr["ControlNoticeId"].ToString());
                connot.LevelId = Convert.ToInt64(dr["LevelId"].ToString());
                connot.TermId = Convert.ToInt64(dr["TermId"].ToString());
                connot.DepartmentId = Convert.ToInt64(dr["DepartmentId"].ToString());
                connot.StartDate = Convert.ToDateTime(dr["StartDate"]).Date;
                connot.EndDate = Convert.ToDateTime(dr["EndDate"]).Date;

                //connot.StartTime = Convert.ToDateTime(dr["StartTime"]);
               // connot.EndTime = Convert.ToDateTime(dr["EndTime"]);

                connot.OnOff = Convert.ToBoolean(dr["OnOff"]);

                connot.LevelCode = dr["LevelCode"].ToString();
                connot.TermCode = dr["TermCode"].ToString();
                connot.DepartmentName = dr["DepartmentName"].ToString();
                connot.Year = dr["Year"].ToString();
                connot_list.Add(connot);
            }
            return connot_list;
        }

        public bool InsertControlNotice(ControlNotice connot)
        {
            try
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO ControlNotice (Year,LevelId,TermId,StartDate,EndDate,DepartmentId,OnOff) VALUES ('"+connot.Year+"','" + connot.LevelId + "','" + 
                    connot.TermId + "','" + connot.StartDate + "','" +
                    connot.EndDate + "','" +connot.DepartmentId + "','" + connot.OnOff + "')";
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
        public bool UpdateControlNotice(ControlNotice connot)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "Update ControlNotice SET Year='"+connot.Year+"',LevelId= '" + connot.LevelId + ",TermId="+connot.TermId +",StartDate="+
                    connot.StartDate + ",EndDate=" + connot.EndDate + ",DepartmentId=" + connot.DepartmentId + ",OnOff=" + connot.OnOff + "' WHERE ControlNoticeId='" + connot.ControlNoticeId + "'";
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
        public bool DeleteControlNotice(long ControlNoticeId)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM ControlNotice WHERE ControlNoticeId= " + ControlNoticeId;
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