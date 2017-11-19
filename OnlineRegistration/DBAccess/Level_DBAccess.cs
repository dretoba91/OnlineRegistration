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
    public class Level_DBAccess
    {
        SqlConnection con;
        public Level_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }
        public List<Level> SelectLevel(Level obj=null)
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT * FROM Level WHERE 1=1";
            if (obj != null)
            {
                if (obj.LevelId > 0)
                {
                    query = query + " AND LEVELID=" + obj.LevelId;
                }
                if (obj.LevelCode != null && obj.LevelCode != "")
                {
                    query = query + " AND LEVELCODE=" + obj.LevelCode;
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();

            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<Level> lvl_list = new List<Level>();
            foreach (DataRow dr in dt.Rows)
            {
                Level lvl = new Level();
                lvl.LevelCode = dr["LevelCode"].ToString();
                lvl_list.Add(lvl);
            }
            return lvl_list;
        }

        public bool InsertLevel(Level lvl)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "INSERT INTO Level(LevelCode) VALUES ('" + lvl.LevelCode + "')";
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
        public bool UpdateLevel(Level lvl)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "UPDATE LEVEL SET LevelCode=" + lvl.LevelCode;
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
        public bool DeleteLevel(long LevelId)
        {
            try
            {
                con.Open();
                SqlCommand _command = new SqlCommand();
                string query = "DELETE FROM LEVEL WHERE LevelId=" + LevelId;
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