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
    public class Employee_DBAccess
    {
        SqlConnection con;

        //UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess();
        public Employee_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }

        public Employee_DBAccess(SqlConnection PrevCon)
        {
            con = PrevCon;
        }
        public List<Employee> SelectEmployee(Employee obj=null)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=THE-OPPORTUNIST\SQLEXPRESS; Initial Catalog=OR_DB; Integrated Security=True";
            if(con.State!=ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            string query = "SELECT A.*,B.*,C.DepartmentName " +
               "FROM Employee A " +
               "LEFT JOIN UserInfo B " +
                   "ON A.EmployeeId=B.EmpOrStdId " +
               "LEFT JOIN Department C " +
                   "ON A.DepartmentId=C.DepartmentId " +
               "WHERE 1=1 ";
            if (obj != null)
            {
                if (obj.EmployeeId > 0)
                {
                    query = query + "AND EmployeeId=" + obj.EmployeeId;
                }
                if (obj.EmployeeCode != null && obj.EmployeeCode != "")
                {
                    query = query + " AND EmployeeCode='" + obj.EmployeeCode+"'";
                }
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();

            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            List<Employee> emp_list = new List<Employee>();
            foreach (DataRow dr in dt.Rows)
            {
                Employee emp = new Employee();

                emp.UserName = dr["UserName"].ToString();
                emp.Password = dr["Password"].ToString();

                emp.EmployeeId = (long)dr["EmployeeId"];
                emp.DepartmentName = dr["DepartmentName"].ToString();
                emp.DepartmentId = (long)dr["DepartmentId"];
                emp.EmployeeName = dr["EmployeeName"].ToString();
                emp.EmployeeCode = dr["EmployeeCode"].ToString();
                //emp.DepartmentId = (long)dr["DepartmentId"];
                emp.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]).Date;
                emp.Sex = dr["Sex"].ToString();
                emp.Address = dr["Address"].ToString();
                emp.BloodGroup = dr["BloodGroup"].ToString();
                emp.ContactNo = dr["ContactNo"].ToString();
                emp.Email = dr["Email"].ToString();

                emp.BioPhoto = (byte[])dr["BioPhoto"];

                string imageBase64 = Convert.ToBase64String((byte[])dr["BioPhoto"]);
                string imageSrc = string.Format("data:image/jpeg;base64{0}", imageBase64);

                emp.PhotoByteString = imageSrc;

                emp_list.Add(emp);
            }
            if (con.State != ConnectionState.Closed)
                con.Close();
            return emp_list;
        }
        public bool InsertEmployee(Employee emp)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=THE-OPPORTUNIST\SQLEXPRESS; Initial Catalog=OR_DB; Integrated Security=True";
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    //con.Open();
                    SqlCommand _command = new SqlCommand();
                    string query = "INSERT INTO Employee(EmployeeName,EmployeeCode,Address,ContactNo,Email,BloodGroup,Sex,JoiningDate,DepartmentId,BioPhoto) VALUES ('" + emp.EmployeeName + "','" + emp.EmployeeCode + "','"
                        + emp.Address + "','" + emp.ContactNo + "','" + emp.Email + "','"
                        + emp.BloodGroup + "','" + emp.Sex + "','" + emp.JoiningDate + "','"
                        + emp.DepartmentId +"',@BioPhoto);" + " SELECT SCOPE_IDENTITY();";

                    _command.Parameters.Add("@BioPhoto", SqlDbType.VarBinary).Value = emp.BioPhoto;
                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.Transaction = tran;

                    UserInfo usrobj = new UserInfo();


                    usrobj.UserName = emp.UserName;
                    usrobj.Password = emp.Password;
                    usrobj.UserTypeCode = 2;///////////type=empoyee

                    Int64 ID = Convert.ToInt64(_command.ExecuteScalar());

                    usrobj.EmpOrStdId = ID;

                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con, _command);
                    db_usrinfo.InsertUserInfo(usrobj);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    if(con.State!=ConnectionState.Closed)
                        con.Close();
                }
            }
            return true;
        }

        public bool UpdateEmployee(Employee emp)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                   // con.Open();
                    SqlCommand _command = new SqlCommand();
                    string query = "UPDATE Employee SET EmployeeName='" + emp.EmployeeName + "',EmployeeCode='" + emp.EmployeeCode +
                         "',Address='" + emp.Address + "',ContactNo='" + emp.ContactNo + "',Email='" + emp.Email + "',BloodGroup='"
                        + emp.BloodGroup + "',Sex='" + emp.Sex + "',JoiningDate='" + emp.JoiningDate + "',DepartmentId='"
                        + emp.DepartmentId + "',BioPhoto=@BioPhoto" + " WHERE EmployeeId='" + emp.EmployeeId + "'";


                    _command.Parameters.Add("@BioPhoto", SqlDbType.VarBinary).Value = emp.BioPhoto;
                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.Transaction = tran;
                    _command.ExecuteNonQuery();

                    UserInfo usrobj = new UserInfo();

                    usrobj.EmpOrStdId = emp.EmployeeId;

                    usrobj.UserName = emp.UserName;
                    usrobj.Password = emp.Password;
                    usrobj.UserTypeCode = 2;

                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con,_command);
                    db_usrinfo.UpdateUserInfo(usrobj);

                    tran.Commit();///my
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    if(con.State!=ConnectionState.Closed)
                        con.Close();
                }
            }
            return true;
        }
        public bool DeleteEmployee(long EmployeeId)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    //con.Open();
                    string query = "DELETE FROM Employee WHERE EmployeeId=" + EmployeeId;

                    SqlCommand _command = new SqlCommand();
                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.Transaction = tran;
                    _command.ExecuteNonQuery();

                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con, _command) ;
                    db_usrinfo.DeleteUserInfo(EmployeeId);

                    tran.Commit();////my
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    if(con.State!=ConnectionState.Closed)
                        con.Close();
                }
            }
            return true;
        }
    }
}