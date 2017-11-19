using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class EmpHomeController : Controller
    {
         LogInEmployee_DBAccess db_logemp = new LogInEmployee_DBAccess();
         
        // GET: /Home/
        public ActionResult Index()
        {
            LogInData LID = (LogInData)Session["LogInData"];

            List<LogInData> logemp_list = new List<LogInData>();
            logemp_list.Add(LID);
            return View("Index", logemp_list);
        }

        public ActionResult About()
        {
            return View("About");
        }
	}
}