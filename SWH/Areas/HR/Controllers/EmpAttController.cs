using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using SWH.Areas.HR.ViewModels;
using SWH.Areas.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.HR.Controllers
{
    public class EmpAttController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAttData(int USERID,string MonthYear)
        {
            string[] condition = MonthYear.Split('-');
            int Month = Convert.ToInt32(condition[0]);
            int Year = Convert.ToInt32(condition[1]);
            var data = db.Database.SqlQuery<EmpAttVM>("exec spAttInOut {0},{1},{2}", USERID, Year, Month);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExceptions(int USERID,string MonthYear )
        {
            string[] condition = MonthYear.Split('-');
            int Month = Convert.ToInt32(condition[0]);
            int Year = Convert.ToInt32(condition[1]);
            var data = db.Database.SqlQuery<ExceptionsVM>(@"
            select *,dbo.ToPersianDate(ExceptionDate) ExceptionDateDr from Exceptions E
            WHERE USERID= {0} AND  dbo.PYear(dbo.ToPersianDate(ExceptionDate))={1}
            AND dbo.PMonth(dbo.ToPersianDate(ExceptionDate))={2}", USERID, Year, Month);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RequestException(Exceptions ex)
        {
            if (ModelState.IsValid)
            {
                if (ex.CheckinoutID == null || ex.CheckinoutID == 0)
                {
                    db.Exception.Add(ex);
                    db.SaveChanges();
                    AddAtt(ex.USERID,ex.ExceptionDate);
                }
                else
                {
                    db.Exception.Add(ex);
                    db.SaveChanges();
                    int NewExceptionID = ex.ExceptionID;
                    AutoApprove(NewExceptionID, ex.CheckinoutID);
                }
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
        public void AutoApprove(int ExceptionID, Int64? CheckinoutID)
        {
            //db.Database.ExecuteSqlCommand(@"UPDATE Exception SET LeaveDeductedInMin =0, ApproveStatusID=4, ApproveDate={0},ApproveBy={1} where ExceptionID={2}", DateTime.Now.ToString("yyyy-MM-dd"), Session["EmployeeID"].ToString(), ExceptionID);

            var data = db.Database.SqlQuery<AttendanceVm>(@"SELECT W.WorkHourID,Convert(varchar,CONCAT(CAST(C.CHECKTIME AS DATE),' ',DATEADD(MINUTE,-1,W.TimeIn))) AS TimeIn,
                                                                Convert(varchar,CONCAT(CAST(C.CHECKTIME AS DATE),' ',DATEADD(MINUTE,1,W.TimOut))) AS TimeOut FROM WorkHour W
                                                                LEFT JOIN CHECKINOUT C ON C.WorkHourID = W.WorkHourID
                                                                WHERE  C.CheckinoutID={0}", CheckinoutID).ToList();
            DateTime CHECKTIME = Convert.ToDateTime(data[0].TimeIn);
            DateTime CHECKOUTTIME = Convert.ToDateTime(data[0].TimeOut);
            db.Database.ExecuteSqlCommand(@"UPDATE CHECKINOUT SET CHECKTIME = {0}, CHECKOUTTIME ={1} WHERE CheckinoutID = {2}", CHECKTIME, CHECKOUTTIME, CheckinoutID);
        }
        public void AddAtt(int USERID,DateTime? dat)
        {
            var data = db.Database.SqlQuery<AttendanceVm>(@"
                    SELECT W.WorkHourID,Convert(varchar,CONCAT(CAST({0} AS DATE),' ',DATEADD(MINUTE,-10,W.TimeIn))) AS TimeIn,
                    Convert(varchar,CONCAT(CAST({1} AS DATE),' ',DATEADD(MINUTE,15,W.TimOut))) AS TimeOut FROM WorkHour W
                    WHERE  {2} BETWEEN StartDate AND EndDate AND ShiftID = 591", dat,dat,dat).ToList();
            DateTime CHECKTIME = Convert.ToDateTime(data[0].TimeIn);
            DateTime CHECKOUTTIME = Convert.ToDateTime(data[0].TimeOut);
            int WorkHourID =Convert.ToInt16(data[0].WorkHourID);

            db.Database.ExecuteSqlCommand("INSERT INTO CHECKINOUT(USERID,CHECKTIME,WorkHourID) VALUES({0},{1},{2})", USERID, CHECKTIME, WorkHourID);
            db.Database.ExecuteSqlCommand("INSERT INTO CHECKINOUT(USERID,CHECKTIME,WorkHourID) VALUES({0},{1},{2})", USERID,CHECKOUTTIME, WorkHourID);
        }
	}
}