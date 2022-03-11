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
    public class GulsazeController : Controller
    {
        //
        // GET: /IMS/Gulsaze/
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            int PCurrentYear = db.Database.SqlQuery<int>(@"SELECT dbo.PYear(dbo.ToPersianDate(GETDATE())) PCurrentYear").Single();
            int PCurrentMonth = db.Database.SqlQuery<int>(@"SELECT dbo.PMonth(dbo.ToPersianDate(GETDATE())) PCurrentMonth").Single();
            ViewBag.PCurrentYear = PCurrentYear;
            ViewBag.PCurrentMonth = PCurrentMonth;
            return View();
        }
        public ActionResult GetGulsazeMenu()
        {
            var data = db.GulsaziMenu.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGulsazeItems(int Id)
        {
            var data = db.Database.SqlQuery<GulsazeItemsVM>(@"
SELECT GI.*,GM.GMName,GMUnit,GI.Quantity*GI.PricePerUnit AS Total FROM GulsazeItem GI 
LEFT JOIN GulsaziMenu GM ON GM.GMID = GI.GMID
WHERE GI.GID={0}", Id);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult DeleteGulsazeItem(int Id)
        {
            var data = db.GulsazeItem.Find(Id);
            db.GulsazeItem.Remove(data);
            db.SaveChanges();
            return Json("deleted");
        }
        [HttpPost]
        public ActionResult Save(Gulsaze G)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Gulsaze.Add(G);
                    db.SaveChanges();
                    transaction.Commit();

                    return Json("saved");
                }
                catch (Exception exp)
                {
                    transaction.Rollback();
                    if (exp.InnerException.InnerException == null)
                    {
                        return Json(exp.InnerException.Message.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (exp.InnerException.InnerException.Message.ToString().Contains("UniqueOrder")) // UniqueM7 (M7No, M7Date)
                        {
                            return Json("<br />A M7 with the same M7No and M7 Date exists.", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(exp.InnerException.InnerException.Message.ToString(), JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

           
        }
        public ActionResult GetGulSazeDetail(int Id)
        {
            var data = db.Database.SqlQuery<GulsazeVM>(@"
SELECT G.*,C.CRName FROM Gulsaze G
LEFT JOIN Currency C ON C.CRID = G.GPrePaidCurrencyID
where BOOKINGID ={0}",Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       public int GetGID(int Id)
        {
            int GID = db.Database.SqlQuery<int>(@"SELECT ISNULL(SUM(GID),0) GID FROM Gulsaze WHERE BOOKINGID={0}", Id).Single();
            return GID;
        }
        [HttpPost]
        public ActionResult Update(Gulsaze GL)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ORRecord = db.Gulsaze.Find(GL.GID);
                    db.Entry(ORRecord).CurrentValues.SetValues(GL);

                    foreach (var item in GL.GulsazeItem)
                    {
                        if (item.GIID == 0)
                        {
                            item.GIID = GL.GID;
                            db.GulsazeItem.Add(item);
                        }
                        else
                        {
                            var OrderItemRecord = db.GulsazeItem.Find(item.GIID);

                            if (OrderItemRecord != null)
                            {
                                item.GID = OrderItemRecord.GID;
                                db.Entry(OrderItemRecord).CurrentValues.SetValues(item);
                            }
                        }

                    }
                    db.SaveChanges();

                    transaction.Commit();

                    return Json("updated");
                }
                catch (Exception exp)
                {
                    if (exp.Message.ToString().Contains("An object with the same key already exists in the ObjectStateManager"))
                    {
                        db.SaveChanges();

                        transaction.Commit();

                        return Json("updated");
                    }


                    transaction.Rollback();

                    if (exp.InnerException.InnerException == null)
                    {
                        return Json(exp.InnerException.Message.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (exp.InnerException.InnerException.Message.ToString().Contains("UniqueOrder")) // UniqueM7 (M7No, M7Date)
                        {
                            return Json("<br />A M7 with the same M7No and M7 Date exists.", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(exp.InnerException.InnerException.Message.ToString(), JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
        }
        public ActionResult GetGulsazeClearanceDetail(int Id)
        {
            var data = db.Database.SqlQuery<GulsazeDetailVM>("exec spGetGulsazeClearance {0}",Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateGulsaze(Gulsaze gl)
        {
            var data = db.Gulsaze.Find(gl.GID);
            data.GFinalAmount = gl.GFinalAmount;
            data.GFinalCurrencyID = gl.GFinalCurrencyID;
            data.GFinalExchangeRate = gl.GFinalExchangeRate;
            data.ServiceChargeAmount = gl.ServiceChargeAmount;
            data.GDiscount = gl.GDiscount;
            data.IsCleared = 1;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            var data1 = db.Booking.Find(data.BOOKINGID);
            data1.IsClearedG = 1;
            db.Entry(data1).State = EntityState.Modified;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }
	}
}