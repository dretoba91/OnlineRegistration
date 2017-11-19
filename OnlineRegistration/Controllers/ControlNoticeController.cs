using OnlineRegistration.DBAccess;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class ControlNoticeController : Controller
    {
        //
        // GET: /ControlNotice/
        ControlNotice_DBAccess db_connot = new ControlNotice_DBAccess();
        DropDownData Drop_Data = new DropDownData();
        public ActionResult Index()
        {

            List<ControlNotice> connot_list = db_connot.SelectControlNotice();
            return View(connot_list);
        }
        public ActionResult Create()
        {
            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["TermList"] = Drop_Data.GetTermList();
            ViewData["LevelList"] = Drop_Data.GetLevelList();
            ViewData["YearList"] = Drop_Data.GetYearList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ControlNotice connot)
        {
            bool IsSaved = db_connot.InsertControlNotice(connot);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long ControlNoticeId)
        {
            ViewData["DepartmentList"] = Drop_Data.GetDepartmentList();
            ViewData["TermList"] = Drop_Data.GetTermList();
            ViewData["LevelList"] = Drop_Data.GetLevelList();
            ViewData["YearList"] = Drop_Data.GetYearList();
            List<ControlNotice> connot_list = db_connot.SelectControlNotice(new ControlNotice { ControlNoticeId = ControlNoticeId });
            return View(connot_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(ControlNotice connot)
        {
            bool IsSaved = db_connot.UpdateControlNotice(connot);
            List<ControlNotice> connot_list = db_connot.SelectControlNotice();
            return View("Index", connot_list);
        }
        public ActionResult Delete(long ControlNoticeId)
        {
            bool IsSaved = db_connot.DeleteControlNotice(ControlNoticeId);
            return RedirectToAction("Index");
        }
    }
}