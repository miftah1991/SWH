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
    public partial class ExpandableDaily : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string BDate = Request.QueryString["BDate"];
                var data = db.Database.SqlQuery<ExpandableDailyVM>(@"exec spGetExpandableDaily {0}", BDate).ToList();
                string BookedGuest = db.Database.SqlQuery<string>(@"SELECT CAST(SUM(BookedGuest) as nvarchar(MAX)) FROM Booking WHERE Date = {0} AND IsCanceled IS NULL", BDate).Single();
                string BHID = db.Database.SqlQuery<string>(@"SELECT CAST(COUNT(BHID) as nvarchar(MAX))BookedGuest FROM Booking WHERE Date = {0} AND IsCanceled IS NULL", BDate).Single();
                string BookingHalls = "";
                var halldata = db.Database.SqlQuery<BookingHallNameVM>(@"
                        SELECT CONCAT(BH.Name,' ',N'تعداد ',B.BookedGuest,' ',M.Name) Halls FROM Booking B
                        LEFT JOIN BookingHall BH ON BH.BHID = B.BHID
                        LEFT JOIN Menu M ON M.MID = B.MID
                        WHERE Date = {0} AND B.IsCanceled IS NULL", BDate).ToList();
                foreach (var dss in halldata)
                {
                    BookingHalls += "," + dss.Halls;
                }
                EDRV.SizeToReportContent = true;
                EDRV.LocalReport.ReportPath = Server.MapPath("ExpandableDaily.rdlc");
                ReportParameter[] paras = new ReportParameter[3];
                paras[0] = new ReportParameter("BookedGuest", BookedGuest);
                paras[1] = new ReportParameter("BHID", BHID);
                paras[2] = new ReportParameter("BookingHalls", BookingHalls);
                EDRV.LocalReport.SetParameters(paras);

                
                ReportDataSource ds = new ReportDataSource("Expandable", data);
                EDRV.LocalReport.DataSources.Add(ds);
                EDRV.LocalReport.Refresh();

            }
        }
    }
}