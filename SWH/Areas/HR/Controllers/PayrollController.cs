using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using SWH.Areas.HR.Models;
using SWH.Areas.HR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.HR.Controllers
{
    public class PayrollController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Generate()
        {
            return View();
        }
        public ActionResult GetMonth()
        {
            var data = db.Database.SqlQuery<TimeTableVM>(@"SELECT Distinct  SubString(T.PersianDate,0,5) Year ,SUBSTRING(T.PersianDate,6,2) Month,    
(Concat(dbo.GetPersianMonthName(SUBSTRING(T.PersianDate,6,2)),'-',SubString(T.PersianDate,0,5))) AS MonthYearName,    
Concat(SUBSTRING(T.PersianDate,6,2),'-',SubString(T.PersianDate,0,5)) AS MonthYearVal    
FROM TimeTable T    
--WHERE T.Date<=GETDATE()    
ORDER BY SubString(T.PersianDate,0,5) asc,SUBSTRING(T.PersianDate,6,2) asc");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPayrollDetail(string MonthYear)
        {
            string[] condition = MonthYear.Split('-');
            int Month = Convert.ToInt32(condition[0]);
            int Year = Convert.ToInt32(condition[1]);
            var data = db.Database.SqlQuery<PayrollDetailVM>(@"exec spGeneratePayrolll {0},{1}", Month, Year);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}