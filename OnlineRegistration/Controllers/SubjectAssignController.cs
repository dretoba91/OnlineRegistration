using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class SubjectAssignController : Controller
    {

        DropDownData drop_data = new DropDownData();
        SubjectAssign_DBAccess sub_dbaccess = new SubjectAssign_DBAccess();
        //
        // GET: /SubjectAssign/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewData["LevelList"] = drop_data.GetLevelList();
            ViewData["TermList"] = drop_data.GetTermList();
            ViewData["DepartmentList"] = drop_data.GetDepartmentList();
            ViewData["SubjectList"] = drop_data.GetSubjectList();

            return View();
        }
        [HttpPost]
        public ActionResult Create(SubjectAssign subAss)
        {
            bool IsSaved = sub_dbaccess.InsertSubjectAssign(subAss);
            return View("Index");
        }
        public ActionResult Edit(SubjectAssign subAss)
        {
            return View();
        }
	}
}