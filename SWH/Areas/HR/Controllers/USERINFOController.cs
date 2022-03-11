using SWH.Areas.IMS.Models;
using SWH.Areas.HR.Models;
using SWH.Areas.HR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SWH.Areas.HR.Controllers
{
    public class USERINFOController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUSERData(int Id)
        {
            var data = db.Database.SqlQuery<USERINFODVM>(@"exec spGetUSERS {0}",Id);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetQualification()
        {
            var data = db.Database.SqlQuery<QualificationVM>(@"SELECT * FROM zQualification");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(USERINFO UI)
        {
            UI.Status = 1;
            if (ModelState.IsValid)
            {
                db.USERINFO.Add(UI);
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
        public ActionResult Edit(USERINFO UI)
        {
            if (ModelState.IsValid)
            {
                var data = db.USERINFO.Find(UI.USERID);
                data.PositionID = UI.PositionID;
                data.Name = UI.Name;
                data.FName = UI.FName;
                data.Mobile = UI.Mobile;
                data.Email = UI.Email;
                data.GorssSalary = UI.GorssSalary;
                data.DOB = UI.DOB;
                data.JoinDate = UI.JoinDate;
                data.CAddress = UI.CAddress;
                data.PAddress = UI.PAddress;
                data.Gender = UI.Gender;
                data.Experiance = UI.Experiance;
                data.QualificationID = UI.QualificationID;
                data.Specilization = UI.Specilization;
                data.University = UI.University;
                data.Remarks = UI.Remarks;
                db.Entry(data).State = EntityState.Modified;
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
        [HttpPost]
        public ActionResult UserResign(USERINFO UI)
        {
            var data = db.USERINFO.Find(UI.USERID);
            data.ResignDate = UI.ResignDate;
            data.ResignReason = UI.ResignReason;
            data.Status = 0;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return Json("true");
        }
	}
}