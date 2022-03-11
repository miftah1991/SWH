using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using System.Data.Entity;

namespace SWH.Areas.IMS.Controllers
{
    public class MenuItemsSettingController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            var data = db.Database.SqlQuery<MenuItemsSettingVM>(@"
                          SELECT MIS.*,M.Name MenuName,CI.CIName FROM MenuItemsSetting MIS
                          LEFT JOIN Menu M ON M.MID = MIS.MID
                          LEFT JOIN CategoryItems CI ON CI.CIID = MIS.CIID");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        [HttpPost]
        public ActionResult CreateSetting(MenuItemsSetting MIS)
        {
            var datacount = db.Database.SqlQuery<MenuItemsSetting>(@"SELECT *  FROM MenuItemsSetting WHERE MID={0} AND CIID={1}",MIS.MID,MIS.CIID).ToList();
            if(datacount.Count() >0)
            {
                return Json("AlreadyExist", JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                db.MenuItemsSetting.Add(MIS);
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
        public ActionResult EditSetting(MenuItemsSetting MIS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(MIS).State = EntityState.Modified;
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
            var data = db.MenuItemsSetting.Find(Id);
            db.MenuItemsSetting.Remove(data);
            db.SaveChanges();
            return Json("true");
        }
	}
}