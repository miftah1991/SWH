using SWH.Areas.HR.ViewModels;
using SWH.Areas.IMS.Models;
using SWH.Areas.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SWH.Areas.HR.Controllers
{
    public class UserPrepaidController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUsers()
        {
            var data = db.Database.SqlQuery<USERINFOVM>("SELECT USERID,Name FROM USERINFO ORDER BY NAME");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPrepaidSum()
        {
            var data = db.Database.SqlQuery<UserPrePaidVM>(@"
            SELECT A.USERID,A.Amount,ISNULL(UPD.PrePaidDeductd,0) PrePaidDeductd,U.Name,P.PositionName,U.FName,ISNULL(A.Amount,0)-ISNULL(UPD.PrePaidDeductd,0) Balance FROM (
	SELECT UP.USERID,SUM(UP.Amount) Amount FROM UserPrepaid UP
	GROUP BY UP.USERID
	) AS A 
LEFT JOIN VWUSERPrePaidDeductd UPD ON A.USERID = UPD.USERID 
LEFT JOIN USERINFO U ON U.USERID = A.USERID
LEFT JOIN Position P ON P.PositionID = U.PositionID");
            var JSONData = Json(data,JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetUserPrepaid(int Id)
        {
            var data = db.Database.SqlQuery<UserPrePaidVM>(@"
SELECT UP.*,U.Name,dbo.ToPersianDate(Date) DateDr 
FROM UserPrepaid UP
LEFT JOIN USERINFO U ON U.USERID = UP.USERID
WHERE UP.USERID ={0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(UserPrepaid UP)
        {
            if (ModelState.IsValid)
            {
                db.UserPrepaid.Add(UP);
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
        public ActionResult Edit(UserPrepaid UP)
        {
            if (ModelState.IsValid)
            {

                db.Entry(UP).State = EntityState.Modified;
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
                var UP = db.UserPrepaid.Find(Id);
                db.UserPrepaid.Remove(UP);
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