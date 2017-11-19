using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Models
{
    public class DropDownData
    {
        SqlConnection con;

        public DropDownData()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<SelectListItem> GetDepartmentList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();

            if(con.State!=ConnectionState.Open)            
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Department";
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();

            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Item_List.Add(new SelectListItem { Text = dr["DepartmentName"].ToString(), Value = dr["DepartmentId"].ToString() });
            }
            if (con.State != ConnectionState.Closed)
                con.Close();
            return Item_List;
        }

        public List<SelectListItem> GetSubjectList()
        {
            List<SelectListItem> Item_list = new List<SelectListItem>();

            if (con.State != ConnectionState.Open)
                con.Open();

            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Subject";

            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();

            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();

            _adapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                Item_list.Add(new SelectListItem { Text = dr["SubjectCode"].ToString(), Value = dr["SubjectId"].ToString() });
            }

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Item_list;
        }

        public List<SelectListItem> GetLevelList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();

            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Level";

            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                Item_List.Add(new SelectListItem { Text = dr["LevelCode"].ToString(), Value = dr["LevelId"].ToString() });
            }
            if (con.State != ConnectionState.Closed)
                con.Close();

            return Item_List;
        }

        public List<SelectListItem> GetTermList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();

            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Term";

            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Item_List.Add(new SelectListItem { Text = dr["TermCode"].ToString(), Value = dr["TermId"].ToString() });
            }
            if (con.State != ConnectionState.Closed)
                con.Close();

            return Item_List;
        }
        public List<SelectListItem> GetBloodGroupList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();
            Item_List.Add(new SelectListItem { Text = "A+", Value = "A+" });
            Item_List.Add(new SelectListItem { Text = "A-", Value = "A-" });
            Item_List.Add(new SelectListItem { Text = "B+", Value = "B+" });
            Item_List.Add(new SelectListItem { Text = "B-", Value = "B-" });
            Item_List.Add(new SelectListItem { Text = "O+", Value = "O+" });
            Item_List.Add(new SelectListItem { Text = "O-", Value = "O-" });
            Item_List.Add(new SelectListItem { Text = "AB+", Value = "AB+" });
            Item_List.Add(new SelectListItem { Text = "AB-", Value = "AB-" });

            return Item_List;
        }
        public List<SelectListItem> GetHallList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();
            Item_List.Add(new SelectListItem { Text="Dr.Q K Hall"});
            Item_List.Add(new SelectListItem { Text = "Tareq Huda Hall" });
            Item_List.Add(new SelectListItem { Text = "Shahid Muhammad Shah Hall" });
            Item_List.Add(new SelectListItem { Text = "Bangabondhu Hall" });
            Item_List.Add(new SelectListItem { Text = "Sufia Kamal Hall" });

            return Item_List;
        }

        public List<SelectListItem> GetYearList()
        {
            List<SelectListItem> Item_List = new List<SelectListItem>();
            Item_List.Add(new SelectListItem { Text = "2010", Value = "2010" });
            Item_List.Add(new SelectListItem { Text = "2011", Value = "2011" });
            Item_List.Add(new SelectListItem { Text = "2012", Value = "2012" });
            Item_List.Add(new SelectListItem { Text = "2013", Value = "2013" });
            Item_List.Add(new SelectListItem { Text = "2014", Value = "2014" });
            Item_List.Add(new SelectListItem { Text = "2015", Value = "2015" });
            Item_List.Add(new SelectListItem { Text = "2016", Value = "2016" });
            Item_List.Add(new SelectListItem { Text = "2017", Value = "2017" });
            Item_List.Add(new SelectListItem { Text = "2018", Value = "2018" });
            Item_List.Add(new SelectListItem { Text = "2019", Value = "2019" });
            Item_List.Add(new SelectListItem { Text = "2020", Value = "2020" });

            return Item_List;
        }


        public List<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> list_item = new List<SelectListItem>();

            if (con.State != ConnectionState.Open)
                con.Open();

            string query = "SELECT EmployeeName,EmployeeId FROM Employee";

            SqlCommand _command = new SqlCommand();

            _command.Connection = con;
            _command.CommandText = query;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            DataTable dt=new DataTable();

            _adapter.SelectCommand=_command;
            _adapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                list_item.Add(new SelectListItem { Text = dr["EmployeeName"].ToString(), Value = dr["EmployeeId"].ToString() });
            }

            return list_item;
        }
    }
}