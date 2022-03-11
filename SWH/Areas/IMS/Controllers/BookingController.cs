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
   [Authorize(Roles = "Booking,IMS,GULSAZE,Production,Purchase,Admin")]
    public class BookingController : Controller
    {
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            int PCurrentYear = db.Database.SqlQuery<int>(@"SELECT dbo.PYear(dbo.ToPersianDate(GETDATE())) PCurrentYear").Single();
            int PCurrentMonth = db.Database.SqlQuery<int>(@"SELECT dbo.PMonth(dbo.ToPersianDate(GETDATE())) PCurrentMonth").Single();
            string CurrentName = db.Database.SqlQuery<string>("SELECT CONCAT(N' برج',' ',dbo.GetPersianMonthName(dbo.PMonth(dbo.ToPersianDate(GETDATE()))),N' سال ',' ',dbo.PYear(dbo.ToPersianDate(GETDATE()))) PCurrentYear").Single();
            ViewBag.PCurrentYear = PCurrentYear;
            ViewBag.PCurrentMonth = PCurrentMonth;
            ViewBag.CurrentName = CurrentName;
            return View();
        }
        public ActionResult IMSIndex()
        {
            int PCurrentYear = db.Database.SqlQuery<int>(@"SELECT dbo.PYear(dbo.ToPersianDate(GETDATE())) PCurrentYear").Single();
            int PCurrentMonth = db.Database.SqlQuery<int>(@"SELECT dbo.PMonth(dbo.ToPersianDate(GETDATE())) PCurrentMonth").Single();
            string CurrentName = db.Database.SqlQuery<string>("SELECT CONCAT(N' برج',' ',dbo.GetPersianMonthName(dbo.PMonth(dbo.ToPersianDate(GETDATE()))),N' سال ',' ',dbo.PYear(dbo.ToPersianDate(GETDATE()))) PCurrentYear").Single();
            ViewBag.PCurrentYear = PCurrentYear;
            ViewBag.PCurrentMonth = PCurrentMonth;
            ViewBag.CurrentName = CurrentName;
            return View();
        }
        public ActionResult GetBookingData(int Year, int Month)
        {
            var data = db.Database.SqlQuery<BookingVM>(@"exec GetCurrentMonthBooking {0},{1}",Year,Month);
            var JSONdata = Json(data, JsonRequestBehavior.AllowGet);
            JSONdata.MaxJsonLength = int.MaxValue;
            return JSONdata;

        }
        public ActionResult GetBookingType()
        {
            var data = db.BookingType.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenu()
        {
            var data = db.Menu.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCurrency()
        {
            var data = db.Currency.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenuPrice(int Id)
        {
            var data = db.Database.SqlQuery<decimal>("SELECT MenuUnitPrice FROM Menu WHERE MID={0}",Id).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       public int GETInsertBookingID()
        {
            int BOOKINGID = db.Database.SqlQuery<int>("SELECT ISNULL(MAX(BOOKINGID),0)  BOOKINGID FROM Booking").Single();
            if (BOOKINGID == 0)
            {
                BOOKINGID = 100;
            }
            else
            {
                BOOKINGID = BOOKINGID + 1;
            }
            return BOOKINGID;
        }
        [HttpPost]
        public ActionResult CreateBooking(Booking BK)
        {
            
            if (ModelState.IsValid)
            {
               // BK.BOOKINGID = BOOKINGID;
                BK.BookingDate = DateTime.Now;
                
                string International = Request.Form["International"];
                string Almas = Request.Form["Almas"];
                string Capital = Request.Form["Capital"];
                string AddBHID ="";
                if(International != null) { AddBHID = International;};
                if(Almas != null) { AddBHID += " ,"+Almas;};
                if(Capital != null) { AddBHID += " ,"+Capital; };
                BK.AddBHID = AddBHID;
                BK.BSID = 0;
                db.Booking.Add(BK);
                db.SaveChanges();
                if (Almas != null) {
                    int BHID;
                    if (BK.BHID >= 4) { BHID = 5; } else { BHID = 2; }
                    db.Database.ExecuteSqlCommand(@"INSERT INTO Booking(MID,BHID,BTID,Date,CustomerName,CustomerMobile,CustomerEmail,MenuPrice,PrePaidAmount,PrePaidExchangeRate,PrePaidCurrencyID,
BookedGuest,BookingDate,IsCleared) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", BK.MID, BHID, BK.BTID, BK.Date, BK.CustomerName, BK.CustomerMobile, BK.CustomerEmail, BK.MenuPrice, 1, 1, 1, 0, DateTime.Now,1);
                }
                if (Capital != null) {
                    int BHID;
                    if (BK.BHID >= 4) { BHID = 6; } else { BHID = 3; }
                    db.Database.ExecuteSqlCommand(@"INSERT INTO Booking(MID,BHID,BTID,Date,CustomerName,CustomerMobile,CustomerEmail,MenuPrice,PrePaidAmount,PrePaidExchangeRate,PrePaidCurrencyID,
BookedGuest,BookingDate,IsCleared) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", BK.MID, BHID, BK.BTID, BK.Date, BK.CustomerName, BK.CustomerMobile, BK.CustomerEmail, BK.MenuPrice, 1, 1, 1, 0, DateTime.Now,1);
                }
                if (International != null)
                {
                    int BHID;
                    if (BK.BHID >= 4) { BHID = 4; } else { BHID = 1; }
                    db.Database.ExecuteSqlCommand(@"INSERT INTO Booking(MID,BHID,BTID,Date,CustomerName,CustomerMobile,CustomerEmail,MenuPrice,PrePaidAmount,PrePaidExchangeRate,PrePaidCurrencyID,
BookedGuest,BookingDate,IsCleared) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", BK.MID, BHID, BK.BTID, BK.Date, BK.CustomerName, BK.CustomerMobile, BK.CustomerEmail, BK.MenuPrice, 1, 1, 1, 0, DateTime.Now,1);
                
                }
                BookingPayment BP = new BookingPayment();
                BP.BOOKINGID = BK.BOOKINGID;
                BP.CRID = BK.PrePaidCurrencyID;
                BP.Amount = BK.PrePaidAmount;
                BP.ExchangeRate = BK.PrePaidExchangeRate;
                BP.PaymentType = 1;
                BP.BPDate = DateTime.Now;
                BP.Remarks = "Pre Payment";
                db.BookingPayment.Add(BP);
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
        public ActionResult EditBooking(Booking BK)
        {
            if (ModelState.IsValid)
            {
                var data = db.Booking.Find(BK.BOOKINGID);
                data.MID = BK.MID;
                data.BTID = BK.BTID;
                data.Date = BK.Date;
                data.CustomerName = BK.CustomerName;
                data.CustomerMobile = BK.CustomerMobile;
                data.CustomerEmail = BK.CustomerEmail;
                data.MenuPrice = BK.MenuPrice;
                data.PrePaidAmount = BK.PrePaidAmount;
                data.PrePaidExchangeRate = BK.PrePaidExchangeRate;
                data.PrePaidCurrencyID = BK.PrePaidCurrencyID;
                data.BookedGuest = BK.BookedGuest;
                data.BookingNumber = BK.BookingNumber;
                data.BookingRemarks = BK.BookingRemarks;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("DELETE FROM BookingPayment  WHERE BOOKINGID ={0} AND PaymentType=1", BK.BOOKINGID);
                BookingPayment BP = new BookingPayment();
                BP.BOOKINGID = BK.BOOKINGID;
                BP.CRID = BK.PrePaidCurrencyID;
                BP.Amount = BK.PrePaidAmount;
                BP.ExchangeRate = BK.PrePaidExchangeRate;
                BP.PaymentType = 1;
                BP.BPDate = DateTime.Now;
                BP.Remarks = "Pre Payment";
                db.BookingPayment.Add(BP);
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
        public ActionResult GetBookingExpandSettingItems(int Id)
        {
            var data = db.Database.SqlQuery<MenuItemsSettingVM>(@"
                        SELECT * FROM MenuItemsSetting MIS
                        LEFT JOIN Menu M ON M.MID = MIS.MID
                        LEFT JOIN CategoryItems CI ON CI.CIID = MIS.CIID
                        WHERE MIS.MID = {0}", Id);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetBOOKINGID(int BHID, string Date)
        {
            int data = db.Database.SqlQuery<int>(@"SELECT ISNULL(SUM(BOOKINGID),0)  BOOKINGID FROM Booking WHERE BHID ={0} AND Date = {1} AND ISNULL(IsCanceled,0)!=1", BHID, Date).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBOOKINGDetail(int Id)
        {
            var data = db.Database.SqlQuery<BookingDetailVM>(@"
                        SELECT B.*,M.Name MenuName,C.CRName,BT.BTName FROM Booking B
                        LEFT JOIN Menu M ON M.MID = B.MID
                        LEFT JOIN Currency C ON C.CRID= B.PrePaidCurrencyID
                        LEFT JOIN BookingType BT ON BT.BTID = B.BTID
                        WHERE B.BOOKINGID ={0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDateBookingDetail(string BDate)
        {
            var data = db.Database.SqlQuery<Menu>(@"
                       SELECT * FROM Menu M WHERE M.MID IN (
                            SELECT MID FROM Booking B 
                            WHERE B.Date = {0})",BDate).ToList();
            string Menus ="";
            foreach(var i in data)
            {
                Menus += ',' + i.Name;
            }
            int Counts = db.Database.SqlQuery<int>(@"
                    SELECT COUNT(*) Counts FROM Booking B 
                    WHERE B.Date = {0}",BDate).Single();
            var dataHall = db.Database.SqlQuery<BookingHall>(@"
                           SELECT CONCAT(BH.ShiftName, ' ',BH.Name) Name FROM BookingHall BH WHERE BH.BHID IN (
                           SELECT BHID FROM Booking B 
                           WHERE B.Date ={0}
                            )", BDate);
            string BookedHalls="";
            foreach(var i in dataHall)
            {
                BookedHalls += "," + i.Name;
            }
            int GuestCounts = db.Database.SqlQuery<int>(@"
                    SELECT SUM(BookedGuest) Counts FROM Booking B 
                    WHERE B.Date = {0}", BDate).Single();
            return Json(new { BookingMenus = Menus,BookedCounts= Counts,BookedHalls = BookedHalls,BookedGuest =GuestCounts });
        }
        public ActionResult GetCalculatedMenuPrice(int MID,int BookedGuest)
        {
            var data = db.Database.SqlQuery<CalculatedMenuItesmVM>("exec CalculateMenuPrice {0},{1}", MID, BookedGuest);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetCalculatedMenuPriceEdit(int MID,int BookedGuest, int BOOKINGID)
        {
            var data = db.Database.SqlQuery<CalculatedMenuItesmVM>("exec CalculateMenuPriceEdit {0},{1},{2}", BookedGuest,BOOKINGID,MID);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetCalculatedMenuPriceNonEx(int MID, int BookedGuest)
        {
            var data = db.Database.SqlQuery<CalculateNonExMenuItemVM>("exec CalculateMenuPriceNonEx {0},{1}", MID, BookedGuest);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetCalculatedMenuPriceNonExEdit(int MID, int BookedGuest,int BOOKINGID)
        {
            //var data = db.Database.SqlQuery<CalculateNonExMenuItemVM>("exec CalculateMenuPriceNonExEdit {0},{1},{2}", BookedGuest, BOOKINGID, MID);
            var data = db.Database.SqlQuery<CalculateNonExMenuItemVM>("exec CalculateMenuPriceNonEx {0},{1}", MID, BookedGuest);
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        [HttpPost]
        public ActionResult SaveExpandable(Orders OR)
        {
            
            OR.ODate = DateTime.Now;
            using (var transaction = db.Database.BeginTransaction())
            {
                int data = CheckExpandableForInsert(OR.BOOKINGID);
                if(data >0)
                {
                   int OID= db.Database.SqlQuery<int>("SELECT OID FROM Orders WHERE BOOKINGID = {0} and ORemarks = 'Order By System For Expandable Items'", OR.BOOKINGID).Single();
                   db.Database.ExecuteSqlCommand("DELETE FROM OrderItems WHERE OID = {0}", OID);
                   db.Database.ExecuteSqlCommand("DELETE FROM Orders WHERE OID = {0}", OID);
                   db.Database.ExecuteSqlCommand("DELETE FROM BookingItems WHERE BOOKINGID = {0} AND IsExpandable=1", OR.BOOKINGID);
                }
                try
                {
                    db.Orders.Add(OR);
                    db.SaveChanges();


                    foreach(var item in OR.OrderItems)
                    {
                        BookingItems BI = new BookingItems();
                        BI.BIQuantity = item.OIQuantity;
                        BI.BIPricePerUnit = item.OIPricePerUnit;
                        BI.CIID = item.CIID;
                        BI.BOOKINGID = OR.BOOKINGID;
                        BI.IsExpandable = 1;
                        db.BookingItems.Add(BI);
//                        db.Database.ExecuteSqlCommand(@"
//                                INSERT INTO BookingItems(BOOKINGID,OIID,BIQuantity)
//                                VALUES({0},{1},{2})", OR.BOOKINGID, item.OIID, item.OIQuantity);
                    }
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
                            return Json("<br />AN Order with the same Order and ORDER Date exists.", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(exp.InnerException.InnerException.Message.ToString(), JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
        }
        public int CheckExpandableForInsert(int Id)
        {
            var data = db.Database.SqlQuery<int>(@"
                        SELECT COUNT(*) Counts FROM BookingItems
                        WHERE BOOKINGID ={0}  AND IsExpandable =1",Id).Single();
            return data;
        }
        public int CheckNonExpandableForInsert(int Id)
        {
            var data = db.Database.SqlQuery<int>(@"
                        SELECT COUNT(*) Counts FROM BookingItems
                        WHERE BOOKINGID ={0}  AND IsExpandable !=1", Id).Single();
            return data;
        }
        public ActionResult SaveNonExpandable(Orders OR)
        {
            if(ModelState.IsValid)
            {
                int data = CheckNonExpandableForInsert(OR.BOOKINGID);
                if (data > 0)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM BookingItems WHERE BOOKINGID = {0} AND IsExpandable=0", OR.BOOKINGID);
                }
                foreach (var item in OR.OrderItems)
                {
                    BookingItems BI = new BookingItems();
                    BI.BIQuantity = item.OIQuantity;
                    BI.CIID = item.CIID;
                    BI.BIPricePerUnit = item.OIPricePerUnit;
                    BI.BOOKINGID = OR.BOOKINGID;
                    BI.IsExpandable = 0;
                    db.BookingItems.Add(BI);
                }
                db.SaveChanges();
                return Json("saved");

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
        public ActionResult GetBookingPayment(int Id)
        {
            var data = db.Database.SqlQuery<BookingPaymentVM>(@"
                        SELECT BP.*,CR.CRID,CR.CRName,Cast(ISNULL(Amount,0)*ISNULL(ExchangeRate,0) as decimal(18,2)) AmountAf,
                        dbo.ToPersianDate(BPDate) BPDateDr 
                        FROM BookingPayment BP
                        LEFT JOIN Currency CR ON CR.CRID = BP.CRID
                        WHERE BP.BOOKINGID={0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateBookingPayment(BookingPayment BP)
        {
            BP.BPDate = DateTime.Now;
            if(ModelState.IsValid)
            {
                db.BookingPayment.Add(BP);
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
        public ActionResult EditBookingPayment(BookingPayment BP)
        {
            BP.BPDate = DateTime.Now;
            if(ModelState.IsValid)
            {
                db.Entry(BP).State = EntityState.Modified;
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
        public ActionResult UpdateBooking1(Booking Bs)
        {
            var data = db.Booking.Find(Bs.BOOKINGID);
            data.Tax = Bs.Tax;
            data.ActualGuest = Bs.ActualGuest;
            data.ServiceCharge = Bs.ServiceCharge;
            data.ServiceChargeAmount = Bs.ServiceChargeAmount;
            data.GuestPrice = Bs.GuestPrice;
            if (ModelState.IsValid)
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("UPDATE Booking SET ActualGuest={0},ServiceCharge={1},ServiceChargeAmount={2},GuestPrice={3},Tax = {4} WHERE BOOKINGID={5}", Bs.ActualGuest, Bs.ServiceCharge, Bs.ServiceChargeAmount, Bs.GuestPrice,Bs.Tax, Bs.BOOKINGID);
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
        public ActionResult UpdateBooking(Booking Bs)
        {
            var data = db.Booking.Find(Bs.BOOKINGID);
            data.ActualGuest = Bs.ActualGuest;
            data.ServiceCharge = Bs.ServiceCharge;
            data.FinalAmount = Bs.FinalAmount;
            data.FinalCurrencyID = Bs.FinalCurrencyID;
            data.FinalExchangeRate = Bs.FinalExchangeRate;
            data.IsCleared = 1;
            data.Discount = Bs.Discount;
            data.ServiceChargeAmount = Bs.ServiceChargeAmount;
            data.GuestPrice = Bs.GuestPrice;
            if (ModelState.IsValid)
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                BookingPayment BP = new BookingPayment();
                BP.BOOKINGID = Bs.BOOKINGID;
                BP.CRID = Convert.ToInt32(Bs.FinalCurrencyID);
                BP.Amount = Convert.ToDecimal(Bs.FinalAmount);
                BP.ExchangeRate = Convert.ToDecimal(Bs.FinalExchangeRate);
                BP.PaymentType = 2;
                BP.BPDate = DateTime.Now;
                BP.Remarks = "Final Payment";
                db.BookingPayment.Add(BP);
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
        public int GetBookingCode()
        {
            int BookingCode = db.Database.SqlQuery<int>("SELECT MAX(BOOKINGID)+1 BOOKINGID FROM Booking").Single();
            return BookingCode;
        }
        [HttpPost]
        public ActionResult CancelBooking(BookingCancel BC)
        {
            BC.CancelDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BookingCancel.Add(BC);
                db.SaveChanges();
                var data = db.Booking.Find(BC.BOOKINGID);
                data.IsCanceled = 1;
                db.Entry(data).State = EntityState.Modified;
                db.Database.ExecuteSqlCommand("UPDATE Booking SET IsCanceled = 1 WHERE BOOKINGID = {0}", BC.BOOKINGID);
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

        public ActionResult OtherExpenses()
        {
            return View();
        }
        public ActionResult GetExistStock()
        {
            var data = db.Database.SqlQuery<ExistStockVM>(@"
SELECT CI.CIID,CIName,CIUnit,SUM(OIQuantity) OIQuantity,SUM(OIQuantity*OIPricePerUnit) TotalPrice FROM VWOrderItems OI
LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID  
LEFT JOIN CategoryClass CC ON CC.CCID = CI.CCID 
WHERE CC.CCID=1
GROUP BY CI.CIID,CIName,CIUnit
ORDER BY CIName 
");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetStockForOtherExpenses()
        {
            var data = db.Database.SqlQuery<CalculateNonExMenuItemVM>(@"
SELECT * FROM VWOrderItems OI
LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID  
LEFT JOIN CategoryClass CC ON CC.CCID = CI.CCID 
WHERE CC.CCID=1 AND OI.OIQuantity > 0
ORDER BY CIName
");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult SaveNonExpandableOthers(Orders OR)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in OR.OrderItems)
                {
                    BookingItems BI = new BookingItems();
                    BI.BIQuantity = item.OIQuantity;
                    BI.CIID = item.CIID;
                    BI.BIPricePerUnit = item.OIPricePerUnit;
                    BI.BOOKINGID = 0;
                    BI.IsExpandable = 0;
                    BI.BIDate = OR.ODate;
                    BI.OtherExpenseName = OR.ORemarks;
                    db.BookingItems.Add(BI);
                }
                db.SaveChanges();
                return Json("saved");

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
        public ActionResult GetOtherExpenses()
        {
            var data = db.Database.SqlQuery<BookingItemsVM>(@"
SELECT BI.*,CI.CIName,CI.CIUnit,BI.BIPricePerUnit OIPricePerUnit,dbo.ToPersianDate(BI.BIDate) BIDateDr 
,ISNULL(BI.BIQuantity,0)*ISNULL(BI.BIPricePerUnit,0) Total
FROM BookingItems BI
LEFT JOIN CategoryItems CI ON CI.CIID = BI.CIID
WHERE BOOKINGID =0");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
        public ActionResult GetClearanceDetail(int Id)
        {
            var data = db.Database.SqlQuery<BookingDetailVM>(@"
SELECT B.BOOKINGID,BookedGuest,M.Name MenuName,B.MenuPrice,BP.PaymentAmount,B.CustomerName,B.IsCleared,B.ServiceCharge,
ISNULL(B.Discount,0) Discount,ISNULL(B.ServiceChargeAmount,0) ServiceChargeAmount,ISNULL(B.GuestPrice,0) GuestPrice,
ISNULL(B.ActualGuest,0) ActualGuest,(ISNULL(B.FinalAmount,0)*ISNULL(B.FinalExchangeRate,0)) FinalPayment,
ISNULL(B.Tax,0) Tax
FROM BOOKING B
LEFT JOIN Menu M ON M.MID = B.MID
LEFT JOIN vwBOOKINGPayments BP ON BP.BOOKINGID=B.BOOKINGID
WHERE B.BOOKINGID={0}", Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       [HttpPost]
       public ActionResult CardPrinted(int Id)
        {
            int IsCleared = db.Database.SqlQuery<int>("SELECT ISNULL(IsCleared,0) IsCleared FROM Booking where BOOKINGID = {0} ", Id).Single();
            if (IsCleared == 1)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Database.ExecuteSqlCommand("UPDATE Booking SET IsCaredPrt = 1 WHERE BOOKINGID = {0}", Id);
                var data1 = db.Database.ExecuteSqlCommand("UPDATE Booking SET IsCleared = 2 WHERE BOOKINGID = {0}", Id);
                return Json("true", JsonRequestBehavior.AllowGet);

            }
        }
       public ActionResult GetWarning()
       {
           var data = db.Database.SqlQuery<BookingWarningVM>("exec GetWarningBooking");
           return Json(data,JsonRequestBehavior.AllowGet);
       }
       public ActionResult GetWarning1()
       {
           var data = db.Database.SqlQuery<BookingWarningVM>("exec GetWarningBooking1");
           return Json(data, JsonRequestBehavior.AllowGet);
       }
	}
}