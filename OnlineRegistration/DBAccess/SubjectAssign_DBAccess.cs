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
    public class SubjectAssign_DBAccess
    {
        SqlConnection con;

        public SubjectAssign_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }

        public List<SubjectAssign> SelectSubjectAssign(SubjectAssign obj=null)
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM SubjectAssign WHERE 1=1 ";

            if(obj!=null)
            {
                if(obj.SubjectAssignId>0)
                {
                    query = query + "AND SubjectAssignId=" + obj.SubjectAssignId;
                }
            }

            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            List<SubjectAssign> sub_list = new List<SubjectAssign>();

            foreach(DataRow dr in dt.Rows)
            {
                SubjectAssign subas = new SubjectAssign();

                subas.DepartmentId = Convert.ToInt64(dr["DepartmentId"].ToString());

                subas.LevelId = Convert.ToInt64(dr["LevelId"].ToString());

                subas.TermId = Convert.ToInt64(dr["TermId"].ToString());

                subas.SubjectId = Convert.ToInt64(dr["SubjectId"].ToString());

                subas.SubjectAssignId = Convert.ToInt64(dr["SubjectAssignId"].ToString());

                sub_list.Add(subas);
            }

            return sub_list;
        }
        public bool InsertSubjectAssign(SubjectAssign obj=null)
        {
            try 
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO SubjectAssign (DepartmentId,LevelId,TermId,SubjectId) VALUES('" +
                    obj.DepartmentId + "','" + obj.LevelId + "','" + obj.TermId + "','" + obj.SubjectId + "' ) ";
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

        public bool UpdateSubjectAssign(SubjectAssign obj)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "UPDATE SubjectAssign SET DepartmentId = ' " + obj.DepartmentId + "',LevelId='" + obj.LevelId +
                    "',TermId='" +obj.TermId+"',SubjectId='" +obj.SubjectId + "'";
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
        public bool DeleteSubjectAssign(long SubjectAssignId)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM SubjectAssign WHERE 1=1 ";
                if(SubjectAssignId>0)
                {
                    query = query + " AND SubjectAssignId='" + SubjectAssignId + "'";
                }
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