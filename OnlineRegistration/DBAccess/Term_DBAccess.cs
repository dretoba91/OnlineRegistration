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
    public class Term_DBAccess
    {
        SqlConnection con;
        public Term_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<Term> SelectTerm(Term obj=null)
        {
            if(con.State!=ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Term WHERE 1=1";
            if (obj != null)
            {
                if (obj.TermId > 0)
                {
                    query = query + " AND TermId=" + obj.TermId;
                }
                if (obj.TermCode != null && obj.TermCode != "")
                {
                    query = query + " AND TermCode=" + obj.TermCode;
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<Term> trm_list = new List<Term>();
            foreach (DataRow dr in dt.Rows)
            {
                Term trm = new Term();
                trm.TermId = Convert.ToInt64(dr["TermId"].ToString());
                trm.TermCode = dr["TermCode"].ToString();
                
                trm_list.Add(trm);
            }
            return trm_list;
        }

        public bool InsertTerm(Term trm)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO Term (TermCode) VALUES ('"+ trm.TermCode + "')";
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
        public bool UpdateTerm(Term trm)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "Update Term SET TermCode='" + trm.TermCode + "' WHERE TermId='" + trm.TermId+"'";
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
        public bool DeleteTerm(long TermId)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM Term WHERE 1=1";
            
                if (TermId > 0)
                {
                    query = query + " AND TermId=" + TermId;
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