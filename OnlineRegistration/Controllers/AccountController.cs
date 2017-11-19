using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserInfo usrInfo)
        {
            /*UserInfo_DBAccess usr_dbAccess = new UserInfo_DBAccess();
            List<UserInfo> list = usr_dbAccess.SelectUserInfo(usrInfo);
            LogInData LID = new LogInData();
            if (list != null && list.Count > 0)
            {
                
                LID.EmpOrStdId = list[0].EmpOrStdId;
                LID.UserId = list[0].UserId;
                LID.UserName = list[0].UserName;
                LID.UserTypeCode = list[0].UserTypeCode;
                LID.Password = list[0].Password;
            }*/
            List<LogInData> list = new List<LogInData>();
            if (usrInfo.UserTypeCode == 1)
            {
                LogInStudent_DBAccess std_dbAccess = new LogInStudent_DBAccess();
                list = std_dbAccess.GetStudent(usrInfo);
            }
            else
            {
                LogInEmployee_DBAccess emp_dbAccess = new LogInEmployee_DBAccess();
                list = emp_dbAccess.GetEmployee(usrInfo);
            }
            if (list[0].EmpOrStdId != 0)
            {
                if (list != null && list.Count > 0)
                {
                    LogInData LID = new LogInData();
                    LID.EmpOrStdId = list[0].EmpOrStdId;
                    LID.UserId = list[0].UserId;
                    LID.UserName = list[0].UserName;
                    LID.UserTypeCode = list[0].UserTypeCode;
                    LID.Password = list[0].Password;
                    LID.Address = list[0].Address;
                    LID.ContactNo = list[0].ContactNo;
                    LID.Email = list[0].Email;
                    LID.BloodGroup = list[0].BloodGroup;
                    LID.Sex = list[0].Sex;
                    LID.DepartmentName = list[0].DepartmentName;
                    LID.BioPhoto = list[0].BioPhoto;
                    LID.PhotoByteString = list[0].PhotoByteString;

                    if (LID.UserTypeCode == 1)
                    {
                        LID.StudentCode = list[0].StudentCode;
                        LID.StudentName = list[0].StudentName;
                        LID.FatherName = list[0].FatherName;
                        LID.MotherName = list[0].MotherName;
                        LID.Hall = list[0].Hall;
                        LID.AdmissionYear = list[0].AdmissionYear;
                        LID.DateOfBirth = list[0].DateOfBirth;

                        Session["LogInData"] = LID;

                        return RedirectToAction("Index", "StdHome");
                    }
                    else
                    {
                        LID.EmployeeCode = list[0].EmployeeCode;
                        LID.EmployeeName = list[0].EmployeeName;
                        LID.JoiningDate = list[0].JoiningDate;

                        Session["LogInData"] = LID;

                        return RedirectToAction("Index", "EmpHome");
                    }
                }
                
            }
            return View("Index");
        }
        public ActionResult LogOut()
        {
            Session["LogInData"] = null;
            return View("Index");
        }
	}
}