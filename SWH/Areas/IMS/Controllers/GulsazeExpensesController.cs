using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.IMS.Controllers
{
   [Authorize(Roles = "GULSAZE")]
    public class GulsazeExpensesController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            var data = db.Database.SqlQuery<GulsazeExpensesVM>(@"
SELECT *,dbo.ToPersianDate(GE.GEDate) GEDateDr,ISNULL(GE.GEQuantity,0)*ISNULL(GE.GEPricePerUnit,0) Total
FROM GulsazeExpenses GE
LEFT JOIN GulsazeExpensesMenu GEM ON GEM.GEMID = GE.GEMID");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;

        }
        public ActionResult GetGEM()
        {
            var data = db.GulsazeExpensesMenu.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(GulsazeExpenses GE)
        {
            if (ModelState.IsValid)
            {
                db.GulsazeExpenses.Add(GE);
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string validationErrors = string.Join("<br /> ",
                                ModelState.Values.Where(E => E.Errors.Count > 0)
                                                    .SelectMany(E => E.Errors)
                                                    .Select(E => E.ErrorMessage)
                                                    .ToArray());

                return Content(validationErrors, "text/html");
            }
        }
        [HttpPost]
        public ActionResult Edit(GulsazeExpenses GE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(GE).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Updated");
            }
            else
            {
                string validationErrors = string.Join("<br /> ",
                ModelState.Values.Where(E => E.Errors.Count > 0)
                                 .SelectMany(E => E.Errors)
                                 .Select(E => E.ErrorMessage)
                                 .ToArray());

                return Content(validationErrors, "text/html");
            }
        }
	}
}