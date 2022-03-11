using SWH.Areas.HR.Models;
using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.HR.Controllers
{
    public class PositionController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Position P)
        {
            if (ModelState.IsValid)
            {
                db.Position.Add(P);
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
        public ActionResult GetPosition()
        {
            var data = db.Position.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(Position P)
        {
            if (ModelState.IsValid)
            {
                db.Entry(P).State = EntityState.Modified;
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
        public ActionResult Delete(int Id)
        {
            var data = db.Position.Find(Id);
            db.Position.Remove(data);
            db.SaveChanges();
            return Json("true");
        }
	}
}