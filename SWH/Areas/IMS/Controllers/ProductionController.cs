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
   [Authorize(Roles = "Production")]
    
    public class ProductionController : Controller
    {
        //
        // GET: /IMS/Production/
        private IMSDbContext db = new IMSDbContext();

        public ActionResult Index()
        {
            int PCurrentYear = db.Database.SqlQuery<int>(@"SELECT dbo.PYear(dbo.ToPersianDate(GETDATE())) PCurrentYear").Single();
            int PCurrentMonth = db.Database.SqlQuery<int>(@"SELECT dbo.PMonth(dbo.ToPersianDate(GETDATE())) PCurrentMonth").Single();
            ViewBag.PCurrentYear = PCurrentYear;
            ViewBag.PCurrentMonth = PCurrentMonth;
            return View();
        }
        public int GetPID(int Id)
        {
            int PID = db.Database.SqlQuery<int>(@"SELECT ISNULL(SUM(PID),0) PID FROM Production WHERE BOOKINGID={0}", Id).Single();
            return PID;
        }
        public ActionResult GetProductionMenu()
        {
            var data = db.ProductionMenu.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(Production P)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Production.Add(P);
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
        [HttpPost]
        public ActionResult Update(Production PL)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ORRecord = db.Production.Find(PL.PID);
                    db.Entry(ORRecord).CurrentValues.SetValues(PL);

                    foreach (var item in PL.ProductionItem)
                    {
                        if (item.PIID == 0)
                        {
                            item.PIID = PL.PID;
                            db.ProductionItem.Add(item);
                        }
                        else
                        {
                            var OrderItemRecord = db.ProductionItem.Find(item.PIID);

                            if (OrderItemRecord != null)
                            {
                                item.PID = OrderItemRecord.PID;
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
        public ActionResult GetProductionDetail(int Id)
        {
            var data = db.Database.SqlQuery<ProductionVM>(@"
SELECT G.*,C.CRName FROM Production G
LEFT JOIN Currency C ON C.CRID = G.PPrePaidCurrencyID
where BOOKINGID ={0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductionItems(int Id)
        {
            var data = db.Database.SqlQuery<ProductionItemsVM>(@"
SELECT P.*,PM.PMName,PMUnit,P.Quantity*P.PricePerUnit AS Total FROM ProductionItem P 
LEFT JOIN ProductionMenu PM ON PM.PMID= P.PMID
WHERE P.PID={0}", Id);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult DeleteProductionItem(int Id)
        {
            var data = db.ProductionItem.Find(Id);
            db.ProductionItem.Remove(data);
            db.SaveChanges();
            return Json("deleted");
        }
        public ActionResult GetProductionClearanceDetail(int Id)
        {
            var data = db.Database.SqlQuery<ProductionDetailVM>("exec spGetProductionClearance {0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateProduction(Production pl)
        {
            var data = db.Production.Find(pl.PID);
            data.PFinalAmount = pl.PFinalAmount;
            data.PFinalCurrencyID = pl.PFinalCurrencyID;
            data.PFinalExchangeRate = pl.PFinalExchangeRate;
            data.IsCleared = 1;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }
	}
}