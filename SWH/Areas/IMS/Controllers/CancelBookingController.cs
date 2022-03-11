using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Areas.IMS.Controllers
{
    public class CancelBookingController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCancelData()
        {
            var data = db.Database.SqlQuery<CancelBookingVM>(@"
            SELECT BC.*,B.*,BH.Name BHName,BH.ShiftName,M.Name MenuName,M.MenuUnitPrice,C.CRName 
            ,dbo.ToPersianDate(bc.CancelDate) CancelDateDr,dbo.ToPersianDate(B.Date) DateDr
            FROM BookingCancel BC
            LEFT JOIN BOOKING B ON B.BOOKINGID = BC.BOOKINGID
            LEFT JOIN BookingHall BH ON BH.BHID= B.BHID
            LEFT JOIN Menu M ON M.MID= B.MID
            LEFT JOIN Currency C ON C.CRID= BC.CancelAmountCID");
            var JSONData = Json(data, JsonRequestBehavior.AllowGet);
            JSONData.MaxJsonLength = int.MaxValue;
            return JSONData;
        }
	}
}