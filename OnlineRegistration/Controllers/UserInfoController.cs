using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        DropDownData Drop_Data = new DropDownData();
        UserInfo_DBAccess db_usrinfo = new UserInfo_DBAccess();
        public ActionResult Index()
        {
            List<UserInfo> usrinf_list = db_usrinfo.SelectUserInfo();
            return View(usrinf_list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo usr)
        {
            bool IsSaved = db_usrinfo.InsertUserInfo(usr);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long EmpOrStdId=0)
        {
            LogInData LID = (LogInData)Session["LogInData"];
            List<UserInfo> usrinf_list = new List<UserInfo>();
            if(LID.AdminEqual1!=1)
            {
                UserInfo usr = new UserInfo();
                usr.UserName = LID.UserName;
                usr.UserTypeCode = LID.UserTypeCode;
                usr.UserId = LID.UserId;
                usr.EmpOrStdId = LID.EmpOrStdId;
                usr.Password = LID.Password;
                usrinf_list.Add(usr);
            }
            else
                usrinf_list = db_usrinfo.SelectUserInfo(new UserInfo { EmpOrStdId=EmpOrStdId});

            return View(usrinf_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(UserInfo usr)
        {
            bool IsSaved = db_usrinfo.UpdateUserInfo(usr);

            LogInData LID = (LogInData)Session["LogInData"];
            LID.UserName = usr.UserName;
            LID.Password = usr.Password;

            Session["LogInData"] = LID;

            if (LID.AdminEqual1 != 1)
            {
                if(LID.UserTypeCode==1)
                    return RedirectToAction("Index", "StdHome");
                else
                    return RedirectToAction("Index", "EmpHome");
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long UserId)
        {
            
            bool IsSaved = db_usrinfo.DeleteUserInfo(UserId);
            return RedirectToAction("Index");
        }
	}
}