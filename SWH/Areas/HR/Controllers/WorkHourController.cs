using SWH.Areas.HR.Models;
using SWH.Areas.HR.ViewModels;
using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.HR.Controllers
{
    public class WorkHourController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetWorkHour()
        {
            var data = db.Database.SqlQuery<WorkHourVM>(@"
            SELECT WH.WorkHourID,WH.ShiftID,WH.StartDate,WH.EndDate,
            CONVERT(Varchar(10),WH.TIMEIN) TIMEIN,CONVERT(Varchar(10),WH.TIMOUT) TIMOUT,WH.LateIn,WH.EarlyOut,WH.Remarks,S.ShiftName,
            dbo.ToPersianDate(WH.StartDate) StartDateDr,dbo.ToPersianDate(WH.EndDate) EndDateDr FROM WorkHour WH 
            LEFT JOIN zShift S ON WH.ShiftID = S.ShiftID");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetShift()
        {
            var data = db.Shift.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(WorkHour Wh)
        {
            if (ModelState.IsValid)
            {
                db.WorkHour.Add(Wh);
                db.SaveChanges();
                return Json("true");
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

        public ActionResult Edit(WorkHour WH)
        {
            if (ModelState.IsValid)
            {

                db.Entry(WH).State = EntityState.Modified;
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
        public ActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                var WH = db.WorkHour.Find(Id);
                db.WorkHour.Remove(WH);
                db.SaveChanges();
                return Json("true");
            }
            else
            {
                return Json("Please select a record then delete?");
            }
        }
	}
}