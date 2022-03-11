using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWH.Controllers
{
    public class DashboardController : Controller
    {
        //
        private IMSDbContext db = new IMSDbContext();

        public ActionResult Index()
        {
            int Counts = db.Database.SqlQuery<int>("GetWarningBookingCount").Single();
            int Counts1 = db.Database.SqlQuery<int>("GetWarningBookingCount1").Single();
            ViewBag.Counts = Counts;
            ViewBag.Counts1 = Counts1;
            return View();
        }
        
	}
}