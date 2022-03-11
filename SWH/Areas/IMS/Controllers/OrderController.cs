using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;

namespace SWH.Areas.IMS.Controllers
{
    public class OrderController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUnit(int Id)
        {
            string Unit = db.Database.SqlQuery<string>("SELECT CIUnit FROM CategoryItems WHERE CIID={0}", Id).Single();
            return Json(Unit,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryItem(int? Id)
        {
            if(Id== null)
            {
                var data = db.Database.SqlQuery<CategoryItemsVM>(@"
                        SELECT * FROM CategoryItems CI 
                        LEFT JOIN Category C ON C.CID = CI.CID
                        ORDER BY CI.CIName");
                var JSONdata = Json(data, JsonRequestBehavior.AllowGet);
                JSONdata.MaxJsonLength = int.MaxValue;
                return JSONdata;
            }
            else
            {
                var data = db.Database.SqlQuery<CategoryItemsVM>(@"
                        SELECT * FROM CategoryItems CI 
                        LEFT JOIN Category C ON C.CID = CI.CID
                        WHERE CI.CID = {0}
                        ORDER BY CI.CIName",Id);
                var JSONdata = Json(data, JsonRequestBehavior.AllowGet);
                JSONdata.MaxJsonLength = int.MaxValue;
                return JSONdata;
            }
        }
        [HttpPost]
        public ActionResult SaveOrder(Orders OR)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Orders.Add(OR);
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
        public ActionResult UpdateOrder(Orders OR)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ORRecord = db.Orders.Find(OR.OID);
                    db.Entry(ORRecord).CurrentValues.SetValues(OR);

                    foreach (var item in OR.OrderItems)
                    {
                        if (item.OIID == 0)
                        {
                            item.OID = OR.OID;
                            db.OrderItems.Add(item);
                        }
                        else
                        {
                            var OrderItemRecord = db.OrderItems.Find(item.OIID);

                            if (OrderItemRecord != null)
                            {
                                item.OID = OrderItemRecord.OID;
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
        public ActionResult DeleteOrderItem(int Id)
        {
           
            try
            {
                var OrderItem = db.OrderItems.Find(Id);
                db.OrderItems.Remove(OrderItem);
                db.SaveChanges();
                return Json("deleted");
            }
            catch (Exception exp)
            {
                return Content(exp.InnerException.Message.ToString(), "text/html");
            }
            
        }
        public ActionResult GetOrders()
        {
            var data = db.Database.SqlQuery<OrdersVM>(@"
                            SELECT O.*,OT.OTotal,OIC.OrderItems,dbo.ToPersianDate(ODate) ODateDr FROM Orders O 
                            LEFT JOIN OrderTotal OT ON OT.OID = O.OID
                            LEFT JOIN OrderItemsCount OIC ON OIC.OID = O.OID WHERE BOOKINGID=0");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetOrderItems(int Id)
        {
            var data = db.Database.SqlQuery<OrderItemsVM>(@"
                    SELECT OI.*,CI.CIName,CI.CIUnit,
                    (ISNULL(OI.OIQuantity,0)*ISNULL(OI.OIPricePerUnit,0))-ISNULL(OI.OIDiscount,0) OITotal
                    FROM OrderItems OI
                    LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID
                    WHERE OI.OID={0}", Id);
            var JSONdata = Json(data, JsonRequestBehavior.AllowGet);
            JSONdata.MaxJsonLength = int.MaxValue;
            return JSONdata;
        }
        public ActionResult ExistStock()
        {
            return View();
        }
        public ActionResult StockReport()
        {
            return View();
        }
	}
}