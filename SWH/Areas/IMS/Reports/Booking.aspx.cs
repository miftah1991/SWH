using Microsoft.Reporting.WebForms;
using SWH.Areas.IMS.Models;
using SWH.Areas.IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SWH.Areas.IMS.Reports
{
    public partial class Booking : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    int BOOKINGID =Convert.ToInt32(Request.QueryString["BOOKINGID"]);
                    var data = db.Database.SqlQuery<BookingDetailVM>(@"exec GetBookingDetail {0}", BOOKINGID).ToList();
                    BookingRV.SizeToReportContent = true;
                    BookingRV.LocalReport.ReportPath = Server.MapPath("Booking.rdlc");
                    ReportDataSource ds = new ReportDataSource("BookingDS", data);
                    BookingRV.LocalReport.DataSources.Add(ds);
                    BookingRV.LocalReport.Refresh();

            }
        }
    }
}