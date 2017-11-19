using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class HomeController : Controller
    {
        //
        LogInStudent_DBAccess db_logstu = new LogInStudent_DBAccess();
        // GET: /Home/
        /*public ActionResult Index(LogInData LID)
        {
            if (LID.UserTypeCode == 1)///student
            {
               // List<LogInData> logstu_list = db_logstu.GetStudent(LID);
                //return View("StudentIndex",logstu_list);
            }
            else ////employee
            {
                LogInEmployee_DBAccess db_logemp = new LogInEmployee_DBAccess();
                List<LogInData> logemp_list = db_logemp.GetEmployee(LID);
                return View("EmployeeIndex",logemp_list);
            }
        }*/
	}
}