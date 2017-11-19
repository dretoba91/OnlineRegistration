using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using OnlineRegistration.DBAccess;

namespace OnlineRegistration.Controllers
{
    public class LevelController : Controller
    {
        //
        Level_DBAccess db_level = new Level_DBAccess();
        // GET: /Level/
        public ActionResult Index()
        {
            List<Level> lvl_list = db_level.SelectLevel();
            return View(lvl_list);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Level lvl)
        {
            bool IsSaved = db_level.InsertLevel(lvl);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long LevelId)
        {
            List<Level> lvl_list=db_level.SelectLevel(new Level{LevelId=LevelId});
            return View(lvl_list[0]);
        }
        [HttpPost]
        public ActionResult Edit(Level lvl)
        {
            bool IsSaved = db_level.InsertLevel(lvl);
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(long LevelId)
        {
            bool IsSaved = db_level.DeleteLevel(LevelId);
            return RedirectToAction("Index");
        }
	}
}