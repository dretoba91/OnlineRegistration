using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class CourseRegController : Controller
    {
        //
        // GET: /CourseReg/

        DropDownData Drop_Data = new DropDownData();

        CourseReg_DBAccess db_access = new CourseReg_DBAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewData["EmployeeList"] = Drop_Data.GetEmployeeList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseReg cor_reg)
        {
            bool IsSaved = db_access.InsertCourseReg(cor_reg);
            return View("Index");
        }
	}
}