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
    public class LeaveController : Controller
    {
        private IMSDbContext db = new IMSDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLeaveData()
        {
            var data = db.Database.SqlQuery<LeaveVM>(@"
SELECT L.*,LT.LeaveTypeName,LT.Abbreviation,U.Name,P.PositionName,U.Name,U.FName,UB.Name BackupEmployee
,dbo.ToPersianDate(FromDate) FromDateDr,dbo.ToPersianDate(ToDate) ToDateDr
FROM Leave L
LEFT JOIN zLeaveType LT ON LT.LeaveTypeID = L.LeaveTypeID
LEFT JOIN USERINFO U ON U.USERID = L.USERID
LEFT JOIN Position P ON P.PositionID = U.PositionID
LEFT JOIN USERINFO UB ON UB.USERID = L.BackupUser");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetEmpLeaveData(int USERID, int YearVal)
        {
            var data = db.Database.SqlQuery<LeaveVM>(@"
SELECT L.*,LT.LeaveTypeName,LT.Abbreviation,U.Name,P.PositionName,U.Name,U.FName,UB.Name BackupEmployee
,dbo.ToPersianDate(FromDate) FromDateDr,dbo.ToPersianDate(ToDate) ToDateDr
FROM Leave L
LEFT JOIN zLeaveType LT ON LT.LeaveTypeID = L.LeaveTypeID
LEFT JOIN USERINFO U ON U.USERID = L.USERID
LEFT JOIN Position P ON P.PositionID = U.PositionID
LEFT JOIN USERINFO UB ON UB.USERID = L.BackupUser
WHERE L.USERID ={0} AND dbo.PYear(dbo.ToPersianDate(FromDate))={1} ", USERID, YearVal);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetLeaveType()
        {
            var data = db.Database.SqlQuery<LeaveTypeVM>(@"SELECT * FROM zLeaveType");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(Leave L)
        {
            int Days = db.Database.SqlQuery<int>(@"SELECT DATEDIFF(DAY,{0},{1})+1 Days", L.FromDate, L.ToDate).Single();
            L.Days = Days;
            if (ModelState.IsValid)
            {
                db.Leave.Add(L);
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
        public ActionResult Edit(Leave L)
        {
            int Days = db.Database.SqlQuery<int>(@"SELECT DATEDIFF(DAY,{0},{1})+1 Days", L.FromDate, L.ToDate).Single();
            L.Days = Days;
            if (ModelState.IsValid)
            {

                db.Entry(L).State = EntityState.Modified;
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
       
        public ActionResult GetYears()
        {
            var data = db.Database.SqlQuery<YearVM>(@"SELECT DISTINCT dbo.PYear(PersianDate) YearID,dbo.PYear(PersianDate) YearVal FROM TimeTable");
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetLeaveYearSummary(int USERID,int YearVal)
        {
            var data = db.Database.SqlQuery<LeaveVM>(@"
                    SELECT  SUM(Days) Days, LeaveTypeName FROM vwLeave
                    WHERE USERID ={0} AND dbo.PYear(FromDateDr)={1}
                    GROUP BY LeaveTypeName",USERID,YearVal);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

	}
}