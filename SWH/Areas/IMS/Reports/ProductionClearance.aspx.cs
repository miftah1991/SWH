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
    public partial class ProductionClearance : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string BDate = Request.QueryString["BDate"];
                var data = db.Database.SqlQuery<GulsazeClearanceVM>(@"
SELECT B.BOOKINGID,B.Date,dbo.ToPersianDate(Date) DateDr,
(ISNULL(PPrePaidAmount,0)*ISNULL(PPrePaidExchangeRate,0)) PrePaid,
(ISNULL(PFinalAmount,0)*ISNULL(PFinalExchangeRate,0)) FinalPayment,
(ISNULL(PPrePaidAmount,0)*ISNULL(PPrePaidExchangeRate,0))+(ISNULL(PFinalAmount,0)*ISNULL(PFinalExchangeRate,0)) Total
FROM Production P
LEFT JOIN Booking B ON P.BOOKINGID = B.BOOKINGID
WHERE B.Date={0}", BDate).ToList();
 
                PCRV.SizeToReportContent = true;
                PCRV.LocalReport.ReportPath = Server.MapPath("ProductionClearance.rdlc");

                ReportDataSource ds = new ReportDataSource("ProductionDS", data);
                PCRV.LocalReport.DataSources.Add(ds);
                PCRV.LocalReport.Refresh();

            }
        }
    }
}