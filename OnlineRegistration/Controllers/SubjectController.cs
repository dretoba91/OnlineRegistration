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
    public class SubjectController : Controller
    {
        //
        // GET: /Subject/
        Subject_DBAccess db_sub = new Subject_DBAccess();
        public ActionResult Index()
        {
            List<Subject> sub_list = db_sub.SelectSubject();
            return View(sub_list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Subject sub)
        {

            bool IsSaved = db_sub.InsertSubject(sub);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long SubjectId)
        {
            List<Subject> sub_list = db_sub.SelectSubject(new Subject { SubjectId=SubjectId });
            return View(sub_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(Subject sub)
        {
            bool IsSaved = db_sub.UpdateSubject(sub);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(long SubjectId)
        {
            bool IsSaved = db_sub.DeleteSubject(SubjectId);
            return RedirectToAction("Index");
        }
	}
}