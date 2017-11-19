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
    public class Student_DBAccess
    {
        SqlConnection con;

        //UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess();
        public Student_DBAccess()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["My_DB"].ConnectionString;
        }

        public List<Student> SelectStudent(Student obj=null)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=THE-OPPORTUNIST\SQLEXPRESS; Initial Catalog=OR_DB; Integrated Security=True";
            if(con.State!=ConnectionState.Open)
                con.Open();
            SqlCommand _command = new SqlCommand();
            ////join//////
            string query = "SELECT A.*,B.*,C.DepartmentName "+
                "FROM Student A "+
                "LEFT JOIN UserInfo B "+
                    "ON A.StudentId=B.EmpOrStdId "+
                "LEFT JOIN Department C " +
                    "ON A.DepartmentId=C.DepartmentId " +
                "WHERE 1=1 ";
            if(obj!=null)
            {
                if(obj.StudentId>0)
                {
                    query = query + "AND A.StudentId= " + obj.StudentId;
                }
                /*if(obj.StudentCode!=null && obj.StudentCode!="")
                {
                    query = query + " AND A.StudentCode='" + obj.StudentCode+"';";
                }*/
            }
            _command.CommandText = query;
            _command.Connection = con;

            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);

            List<Student> stu_list = new List<Student>();

            foreach (DataRow dr in dt.Rows)
            {
                Student stu = new Student();
                stu.UserName = dr["UserName"].ToString();
                stu.Password = dr["Password"].ToString();

                stu.DepartmentId = (long)dr["DepartmentId"];
                stu.DepartName = dr["DepartmentName"].ToString();
                stu.StudentId = (long)dr["StudentId"];
                stu.StudentName = dr["StudentName"].ToString();
                stu.StudentCode = dr["StudentCode"].ToString();
                stu.FatherName = dr["FatherName"].ToString();
                stu.MotherName = dr["MotherName"].ToString();
                stu.Address = dr["Address"].ToString();
                stu.AdmissionYear = dr["AdmissionYear"].ToString();
                stu.BloodGroup = dr["BloodGroup"].ToString();
                stu.ContactNo = dr["ContactNo"].ToString();
                stu.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                stu.Email = dr["Email"].ToString();
                stu.Hall = dr["Hall"].ToString();
                stu.Sex = dr["Sex"].ToString();
                //stu.UserTypeCode = (int)dr["UserTypeCode"];
                stu.BioPhoto = (byte[])dr["BioPhoto"];

                string imageBase64=Convert.ToBase64String((byte[])dr["BioPhoto"]);
                string imageSrc = string.Format("data:image.jpeg;base64{0}", imageBase64);

                stu.PhotoByteString = imageSrc;

                stu_list.Add(stu);
            }

            return stu_list;
        }

        public bool InsertStudent(Student stu)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {

                    //con.Open();
                    SqlCommand _command = new SqlCommand();
                    string query = "INSERT INTO Student(StudentName,DepartmentId,FatherName,MotherName,DateOfBirth,AdmissionYear,StudentCode,Hall,Address,ContactNo,Email,BloodGroup,Sex,BioPhoto) VALUES ('"
                        + stu.StudentName + "','" + stu.DepartmentId + "','" + stu.FatherName + "','" + stu.MotherName + "','" + stu.DateOfBirth + "','" + stu.AdmissionYear + "','" + stu.StudentCode +
                        "','" + stu.Hall + "','" + stu.Address + "','" + stu.ContactNo + "','" + stu.Email + "','" + stu.BloodGroup +
                        "','" + stu.Sex + "',@BioPhoto);" + " SELECT SCOPE_IDENTITY();";


                    _command.Parameters.Add("@BioPhoto", SqlDbType.VarBinary).Value = stu.BioPhoto;

                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.Transaction = tran;


                    UserInfo usrobj = new UserInfo();



                    usrobj.UserName = stu.UserName;
                    usrobj.Password = stu.Password;
                    usrobj.UserTypeCode = 1;////student

                    Int64 ID = Convert.ToInt64(_command.ExecuteScalar());////Get id Of StudentTable

                    usrobj.EmpOrStdId = ID;

                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con, _command); 
                    db_usrinfo.InsertUserInfo(usrobj);
                    tran.Commit();
                    ///_command.ExecuteNonQuery();
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

        public bool UpdateStudent(Student stu)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    //con.Open();

                    string query = "UPDATE Student SET StudentName='" + stu.StudentName + "',FatherName='"
                        + stu.FatherName + "',MotherName='" + stu.MotherName + "',DateOfBirth='" + stu.DateOfBirth + "',AdmissionYear='"
                        + stu.AdmissionYear + "',StudentCode='" + stu.StudentCode + "',DepartmentId='" + stu.DepartmentId + "',Hall='" + stu.Hall + "',Address='" + stu.Address +
                        "',ContactNo='" + stu.ContactNo + "',Email='" + stu.Email + "',BloodGroup='" + stu.BloodGroup + "',Sex='" + stu.Sex + "',BioPhoto=@BioPhoto" + " WHERE StudentId='" + stu.StudentId + "'";

                    SqlCommand _command = new SqlCommand();
                    _command.Parameters.Add("@BioPhoto", SqlDbType.VarBinary).Value = stu.BioPhoto;

                    _command.Transaction = tran;
                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.ExecuteNonQuery();



                    UserInfo usrobj = new UserInfo();

                    usrobj.EmpOrStdId = stu.StudentId;

                    usrobj.UserName = stu.UserName;
                    usrobj.Password = stu.Password;
                    usrobj.UserTypeCode = 1;

                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con,_command);
                    db_usrinfo.UpdateUserInfo(usrobj);

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

        public bool DeleteStudent(long StudentId)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                   // con.Open();

                    string query = "DELETE FROM Student WHERE StudentId=" + StudentId;

                    SqlCommand _command = new SqlCommand();
                    _command.Transaction = tran;
                    _command.CommandText = query;
                    _command.Connection = con;
                    _command.ExecuteNonQuery();



                    UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess(con,_command);
                    db_usrinfo.DeleteUserInfo(StudentId);
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
    }
}