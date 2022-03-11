using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.IMS.Controllers
{
    public class CategoryController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category C)
        {
            if(ModelState.IsValid)
            {
                db.Category.Add(C);
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
        public ActionResult GetCategory()
        {
            var data = db.Category.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(Category C)
        {
            if(ModelState.IsValid)
            {
                db.Entry(C).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Updated");
            }
            else {
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
            var data = db.Category.Find(Id);
            db.Category.Remove(data);
            db.SaveChanges();
            return Json("true");
        }
	}
}