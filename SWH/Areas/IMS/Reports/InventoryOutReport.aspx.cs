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
    public partial class InventoryOutReport : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FromDate = Request.QueryString["FromDate"];
                string ToDate = Request.QueryString["ToDate"];
                int CIID = Convert.ToInt32(Request.QueryString["CIID"]);
                string Query =
@"SELECT A.*,dbo.ToPersianDate(A.BIDate) BIDateDr FROM (
SELECT CASE WHEN BI.BIDate IS NULL THEN B.Date ELSE BI.BIDate END BIDate,CI.CIName,CI.CIUnit,
BI.BIQuantity,BI.BIPricePerUnit OIPricePerUnit,
(ISNULL(BI.BIQuantity,0)*ISNULL(BI.BIPricePerUnit,0)) Total,ci.CIID
FROM BookingItems BI
LEFT JOIN Booking B ON B.BOOKINGID = BI.BOOKINGID
LEFT JOIN CategoryItems CI ON CI.CIID = BI.CIID
WHERE IsExpandable=0
) AS A
WHERE A.BIDate Between '" + FromDate + "' AND '" + ToDate + "' ";
                if (CIID != 0)
                {
                    Query += " AND A.CIID = " + CIID;
                }
                var data = db.Database.SqlQuery<StockOutReportVM>(Query).ToList();
                string FromDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS FromDateDr", FromDate).Single();
                string ToDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS ToDateDr", ToDate).Single();
                IORRV.SizeToReportContent = true;
                IORRV.LocalReport.ReportPath = Server.MapPath("InventoryOutReport.rdlc");
                ReportParameter[] paras = new ReportParameter[2];
                paras[0] = new ReportParameter("FromDateDr", FromDateDr);
                paras[1] = new ReportParameter("ToDateDr", ToDateDr);
                IORRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("InventoryDS", data);
                IORRV.LocalReport.DataSources.Add(ds);
                IORRV.LocalReport.Refresh();

            }
        }
    }
}