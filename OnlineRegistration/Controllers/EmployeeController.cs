//////AShim dey
using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        Employee_DBAccess db_emp = new Employee_DBAccess();
        DropDownData Drop_Data = new DropDownData();
        public ActionResult Index()
        {
            //if(Session["LogInData"]==null)
               // return RedirectToAction("Index", "Account");

            List<Employee> emp_list = db_emp.SelectEmployee();
            return View(emp_list);
        }
        public ActionResult Create()
        {
            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["BloodGroupList"] = Drop_Data.GetBloodGroupList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp,HttpPostedFileBase upload)
        {
            byte[] imageBytes=null;
            BinaryReader reader=new BinaryReader(upload.InputStream);
            imageBytes = reader.ReadBytes((int)upload.ContentLength);

            emp.BioPhoto = imageBytes;

            bool IsSaved = db_emp.InsertEmployee(emp);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long EmployeeId)
        {
            List<Employee> emp_list = db_emp.SelectEmployee(new Employee { EmployeeId=EmployeeId});
            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["BloodGroupList"] = Drop_Data.GetBloodGroupList();
            return View(emp_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp,HttpPostedFileBase upload)
        {
            byte[] imageBytes=null;
            BinaryReader reader = new BinaryReader(upload.InputStream);

            imageBytes = reader.ReadBytes((int)upload.ContentLength);

            emp.BioPhoto = imageBytes;

            bool IsSaved = db_emp.UpdateEmployee(emp);

            return RedirectToAction("Index");
        }
        public ActionResult Delete(long EmployeeId)
        {
            bool IsSaved = db_emp.DeleteEmployee(EmployeeId);
            return RedirectToAction("Index");
        }
	}
}