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
    public class UserInfo_DBAccess
    {
        SqlConnection con;
        SqlCommand _command;
        bool calledByOtherFuction = false;

        public UserInfo_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
            _command = new SqlCommand();
        }

        public UserInfo_DBAccess(SqlConnection PrevCon)
        {
            con = PrevCon;
            _command = new SqlCommand();
            calledByOtherFuction = true;
        }

        public UserInfo_DBAccess(SqlConnection PrevCon,SqlCommand cmd)
        {
            con = PrevCon;
            _command = cmd;
            calledByOtherFuction = true;
        }
        public List<UserInfo> SelectUserInfo(UserInfo obj=null)
        {
            if(con.State!=ConnectionState.Open)
                con.Open();
            
            string query = "SELECT * FROM UserInfo WHERE 1=1";
            if (obj != null)
            {
                
                if (obj.UserName!= null && obj.UserName != "")
                {
                    query = query + " AND UserName='" + obj.UserName + "'";
                }
                if (obj.Password != null && obj.Password != "")
                {
                    query = query + " AND Password='" + obj.Password + "'";
                }
                if (obj.UserTypeCode > 0)
                {
                    query = query + " AND UserTypeCode='" + obj.UserTypeCode + "'";
                }
                if (obj.UserId > 0)
                {
                    query = query + " AND UserId='" + obj.UserId + "'";
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            List<UserInfo> usr_list = new List<UserInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                UserInfo usr = new UserInfo();
                usr.EmpOrStdId = Convert.ToInt64(dr["EmpOrStdId"].ToString());
                usr.UserId = Convert.ToInt64(dr["UserId"].ToString());
                usr.UserName=dr["UserName"].ToString();
                usr.UserTypeCode=(int)dr["UserTypeCode"];
                usr.Password=dr["Password"].ToString();
                
                usr_list.Add(usr);
            }
            return usr_list;
        }

        public bool InsertUserInfo(UserInfo usr)
        {
            try
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();
                
                string query = "INSERT INTO UserInfo (UserName,UserTypeCode,Password,EmpOrStdId) VALUES ('"+usr.UserName+"','"+usr.UserTypeCode+"','"
                    +usr.Password+"','"+usr.EmpOrStdId+"')";
                
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
                if(!calledByOtherFuction)
                    con.Close();
            }
            return true;
        }
        public bool UpdateUserInfo(UserInfo usr)
        {
            try
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();
                ///SqlCommand _command = new SqlCommand();
                string query = "UPDATE UserInfo SET UserName='" + usr.UserName +"',UserTypeCode='"+
                    usr.UserTypeCode+"',Password='"+usr.Password+"' WHERE EmpOrStdId=" +usr.EmpOrStdId;
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
                if(!calledByOtherFuction)//////In the case of change password by USER
                    con.Close();
            }
            return true;
        }
        public bool DeleteUserInfo(long EmpOrStdId)
        {
            try
            {
                if(con.State!=ConnectionState.Open)   
                    con.Open();
                
                string query = "DELETE FROM UserInfo WHERE EmpOrStdId= " + EmpOrStdId;

                ///SqlCommand _command = new SqlCommand();
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
                if(!calledByOtherFuction)
                    con.Close();
            }
            return true;
        }
    }
    
}