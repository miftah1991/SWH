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
    public class CategoryItemsController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCategory()
        {
            var data = db.Database.SqlQuery<Category>("SELECT * FROM Category ORDER BY CName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryClass()
        {
            var data = db.CategoryClass.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryItems()
        {
            var data = db.Database.SqlQuery<CategoryItemsVM>(@"
                        SELECT * FROM CategoryItems CI 
                        LEFT JOIN Category C ON C.CID = CI.CID
                        LEFT JOIN CategoryClass CC ON CC.CCID = CI.CCID
                        ORDER BY CI.CIName");
            var JSONdata = Json(data, JsonRequestBehavior.AllowGet);
            JSONdata.MaxJsonLength = int.MaxValue;
            return JSONdata;
        }
        [HttpPost]
        public ActionResult Create(CategoryItems CI)
        {
            if (ModelState.IsValid)
            {
                db.CategoryItems.Add(CI);
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
        public ActionResult Edit(CategoryItems CI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(CI).State = EntityState.Modified;
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
            var data = db.CategoryItems.Find(Id);
            db.CategoryItems.Remove(data);
            db.SaveChanges();
            return Json("true");
        }
	}
}