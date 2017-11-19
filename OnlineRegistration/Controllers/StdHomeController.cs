using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class StdHomeController : Controller
    {
        LogInStudent_DBAccess db_logstu = new LogInStudent_DBAccess();
        
        // GET: /Home/
        public ActionResult Index()
        {
         
            LogInData LID=(LogInData)Session["LogInData"];

            List<LogInData> logstd_list = new List<LogInData>();
            logstd_list.Add(LID);

            return View("Index", logstd_list);
        }
        public ActionResult About()
        {
            return View("About");
        }
        
    }
}