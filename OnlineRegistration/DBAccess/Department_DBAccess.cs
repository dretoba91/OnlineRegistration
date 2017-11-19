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
    public class Department_DBAccess
    {
        SqlConnection con;
        public Department_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<Department> SelectDepartment(Department obj=null)
        {
            
            con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Department WHERE 1=1";
            if (obj != null)
            {
                if (obj.DepartmentId > 0)
                {
                    query = query + " AND DepartmentId=" + obj.DepartmentId;
                }
                if (obj.DepartmentName != null && obj.DepartmentName != "")
                {
                    query = query + " AND DepartmentName='" + obj.DepartmentName+"';";
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<Department> dpt_list = new List<Department>();
            foreach (DataRow dr in dt.Rows)
            {
                Department dpt = new Department();
                dpt.DepartmentId = Convert.ToInt64(dr["DepartmentId"].ToString());
                dpt.DepartmentName = dr["DepartmentName"].ToString();
                dpt.DepartmentCode = dr["DepartmentCode"].ToString();

                dpt_list.Add(dpt);
            }
            return dpt_list;
        }

        public bool InsertDepartment(Department dpt)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO Department (DepartmentName,DepartmentCode) VALUES ('" + dpt.DepartmentName + "','" + dpt.DepartmentCode + "')";
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
                con.Close();
            }
            return true;
        }
        public bool UpdateDepartment(Department dpt)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "Update Department SET DepartmentName='" + dpt.DepartmentName + "',DepartmentCode= '" + dpt.DepartmentCode + "' WHERE DepartmentId='" + dpt.DepartmentId+"'";
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
                con.Close();
            }
            return true;
        }
        public bool DeleteDepartment(long DepartmentId)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM Department WHERE DepartmentId= " + DepartmentId;
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
                con.Close();
            }
            return true;
        }
    }
}