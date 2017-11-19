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
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        Department_DBAccess db_Dpt = new Department_DBAccess();
        public ActionResult Index(Department dpt)
        {
            List<Department> dpt_list = db_Dpt.SelectDepartment(dpt);
            return View(dpt_list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department dpt)
        {

            bool IsSaved = db_Dpt.InsertDepartment(dpt);

            return RedirectToAction("Index",dpt);
        }
        public ActionResult Edit(long DepartmentId)
        {

            List<Department> dpt_list = db_Dpt.SelectDepartment(new Department { DepartmentId = DepartmentId });
            return View(dpt_list[0]);

        }

        [HttpPost]
        public ActionResult Edit(Department dpt)
        {
            bool IsSaved = db_Dpt.UpdateDepartment(dpt);

            List<Department> dpt_list = db_Dpt.SelectDepartment();
            return View("Index", dpt_list);//////called index view
        }
        [HttpPost]
        public ActionResult Delete(long DepartmentId)
        {
            bool IsSaved = db_Dpt.DeleteDepartment(DepartmentId);
            return RedirectToAction("Index");///////called index Action
        }

	}
}