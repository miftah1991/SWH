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
    public partial class DailyDetail : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string BDate = Request.QueryString["BDate"];
                var data = db.Database.SqlQuery<BookingDetailVM>(@"exec spGetDailyBookingDetal {0}", BDate).ToList();
                
                DDRV.SizeToReportContent = true;
                DDRV.LocalReport.ReportPath = Server.MapPath("DailyDetail.rdlc");
                ReportDataSource ds = new ReportDataSource("BookingDS", data);
                DDRV.LocalReport.DataSources.Add(ds);
                DDRV.LocalReport.Refresh();

            }
        }
    }
}