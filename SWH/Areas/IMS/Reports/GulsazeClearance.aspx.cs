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
    public partial class GulsazeClearance : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string BDate = Request.QueryString["BDate"];
                var data = db.Database.SqlQuery<GulsazeClearanceVM>(@"
SELECT B.BOOKINGID,B.Date,dbo.ToPersianDate(Date) DateDr,
(GPrePaidAmount*GPrePaidExchangeRate) PrePaid,
(GFinalAmount*GFinalExchangeRate) FinalPayment,
(GPrePaidAmount*GPrePaidExchangeRate)+(GFinalAmount*GFinalExchangeRate) Total,
CASE WHEN B.AddBHID IS NULL THEN BH.Name ELSE 
CONCAT(BH.Name, ' ',B.AddBHID) END BHName,BH.ShiftName
FROM Gulsaze G
LEFT JOIN Booking B ON G.BOOKINGID = B.BOOKINGID
LEFT JOIN BookingHall BH ON BH.BHID = B.BHID
WHERE B.Date ={0}", BDate).ToList();
                var data1 = db.Database.SqlQuery<GulsazeClearance1VM>(@"
SELECT CAST(GEDate AS date) Date,CONCAT(GEM.GEMName,'  ',GE.GERemarks) GEMName,ISNULL(GE.GEPricePerUnit,0)*ISNULL(GE.GEQuantity,0) Total FROM GulsazeExpenses GE
LEFT JOIN GulsazeExpensesMenu GEM ON GEM.GEMID=GE.GEMID
WHERE CAST(GE.GEDate AS date) = {0}", BDate).ToList();
                GCRV.SizeToReportContent = true;
                GCRV.LocalReport.ReportPath = Server.MapPath("GulsazeClearance.rdlc");

                string INComeTotal = db.Database.SqlQuery<decimal>(@"
SELECT isnull(sum((GPrePaidAmount*GPrePaidExchangeRate)+(GFinalAmount*GFinalExchangeRate)),0) INComeTotal
FROM Gulsaze G
LEFT JOIN Booking B ON G.BOOKINGID = B.BOOKINGID
WHERE B.Date ={0}
",BDate).Single().ToString();
                string GulsazeExpenses = db.Database.SqlQuery<decimal>(@"
SELECT ISNULL(SUM(ISNULL(GE.GEPricePerUnit,0)*ISNULL(GE.GEQuantity,0)),0) ExpnesesTotal FROM GulsazeExpenses GE
LEFT JOIN GulsazeExpensesMenu GEM ON GEM.GEMID=GE.GEMID
WHERE CAST(GE.GEDate AS date)= {0}
", BDate).Single().ToString();
                ReportParameter[] paras = new ReportParameter[2];
                paras[0] = new ReportParameter("INComeTotal", INComeTotal);
                paras[1] = new ReportParameter("GulsazeExpenses", GulsazeExpenses);
                GCRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("GulSazeDS", data);
                ReportDataSource ds1 = new ReportDataSource("GulSaze1DS", data1);
                GCRV.LocalReport.DataSources.Add(ds);
                GCRV.LocalReport.DataSources.Add(ds1);
                GCRV.LocalReport.Refresh();

            }
        }
    }
}