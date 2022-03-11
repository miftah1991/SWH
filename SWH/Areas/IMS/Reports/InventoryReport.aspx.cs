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
    public partial class InventoryReport : System.Web.UI.Page
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
@"SELECT O.ODate,dbo.ToPersianDate(O.ODate) ODateDr,CI.CIName,CI.CIUnit,
OI.OIQuantity,OI.OIPricePerUnit,OI.OIDiscount,((ISNULL(OI.OIQuantity,0)*ISNULL(OI.OIPricePerUnit,0))-ISNULL(OI.OIDiscount,0)) Total
FROM Orders O
LEFT JOIN OrderItems OI ON OI.OID = O.OID
LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID
WHERE O.BOOKINGID=0 AND  O.ODate BETWEEN '"+FromDate+"' AND '"+ToDate+"' ";
                if(CIID!=0)
                {
                    Query += " AND OI.CIID = "+CIID;
                }
                var data = db.Database.SqlQuery<StockReportVM>(Query).ToList();
                string FromDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS FromDateDr", FromDate).Single();
                string ToDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS ToDateDr", ToDate).Single();
                IRRV.SizeToReportContent = true;
                IRRV.LocalReport.ReportPath = Server.MapPath("InventoryReport.rdlc");
                ReportParameter[] paras = new ReportParameter[2];
                paras[0] = new ReportParameter("FromDateDr", FromDateDr);
                paras[1] = new ReportParameter("ToDateDr", ToDateDr);
                IRRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("InventoryDS", data);
                IRRV.LocalReport.DataSources.Add(ds);
                IRRV.LocalReport.Refresh();

            }
        }
    }
}