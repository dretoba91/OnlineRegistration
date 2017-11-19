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
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        Student_DBAccess db_stu = new Student_DBAccess();
        DropDownData Drop_Data = new DropDownData();
        public ActionResult Index()
        {
            List<Student> stu_list = db_stu.SelectStudent();
            return View(stu_list);
        }
        public ActionResult Create()
        {

            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["HallList"] = Drop_Data.GetHallList();
            ViewData["BloodGroupList"] = Drop_Data.GetBloodGroupList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student stu,HttpPostedFileBase upload)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(upload.InputStream);
            imageBytes = reader.ReadBytes((int)upload.ContentLength);

            stu.BioPhoto = imageBytes;

            bool IsSaved = db_stu.InsertStudent(stu);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long StudentId)
        {
            List<Student> stu_list = db_stu.SelectStudent(new Student { StudentId = StudentId });
            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["HallList"] = Drop_Data.GetHallList();
            ViewData["BloodGroupList"] = Drop_Data.GetBloodGroupList();
            return View(stu_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(Student stu,HttpPostedFileBase upload)
        {
            byte[] imageBytes = null;

            BinaryReader reader = new BinaryReader(upload.InputStream);
            imageBytes = reader.ReadBytes((int)upload.ContentLength);

            stu.BioPhoto = imageBytes;

            bool IsSaved = db_stu.UpdateStudent(stu);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(long StudentId)
        {
            bool IsSaved = db_stu.DeleteStudent(StudentId);

            return RedirectToAction("Index");
        }
	}
}