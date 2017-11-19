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
    public class TermController : Controller
    {
        //
        // GET: /Term/
        Term_DBAccess db_trm = new Term_DBAccess();
        public ActionResult Index()
        {
            List<Term> trm_list = db_trm.SelectTerm();
            return View(trm_list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Term trm)
        {
            bool IsSaved = db_trm.InsertTerm(trm);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long TermId)
        {
            List<Term> trm_list = db_trm.SelectTerm(new Term { TermId=TermId});
            return View(trm_list[0]);
        }
        [HttpPost]

        public ActionResult Edit(Term trm)
        {
            bool IsSaved = db_trm.UpdateTerm(trm);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long TermId)
        {
            bool IsSaved = db_trm.DeleteTerm(TermId);
            return RedirectToAction("Index");
        }
    }
}