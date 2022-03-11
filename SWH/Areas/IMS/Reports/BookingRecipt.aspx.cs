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
    public partial class BookingRecipt : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int BOOKINGID = Convert.ToInt32(Request.QueryString["BOOKINGID"]);
                var data = db.Database.SqlQuery<BookingReciptVM>(@"
SELECT B.BOOKINGID,CustomerName,BookedGuest,ActualGuest,MenuPrice,ServiceCharge,ServiceChargeAmount,dbo.ToPersianDate(b.Date) DateDr,
isnull(GuestPrice,0)+isnull(ServiceChargeAmount,0) TotalWithServiceCharge,BP.PaymentAmount,isnull(B.Tax,0) Tax,
(isnull(GuestPrice,0)+isnull(ServiceChargeAmount,0)+isnull(b.tax,0))-isnull(BP.PaymentAmount,0) Balance
FROM Booking B
LEFT JOIN vwBOOKINGPayments BP ON BP.BOOKINGID = B.BOOKINGID
WHERE B.BOOKINGID ={0}", BOOKINGID).ToList();
                BRRV.SizeToReportContent = true;
                BRRV.LocalReport.ReportPath = Server.MapPath("BookingRecipt.rdlc");
                ReportDataSource ds = new ReportDataSource("BookingDS", data);
                BRRV.LocalReport.DataSources.Add(ds);
                BRRV.LocalReport.Refresh();

            }
        }
    }
}