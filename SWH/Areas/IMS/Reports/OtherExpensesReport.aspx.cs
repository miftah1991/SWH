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
    public partial class OtherExpensesReport : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                var data = db.Database.SqlQuery<BookingItemsVM>(@"
SELECT BI.*,CI.CIName,CI.CIUnit,BI.BIPricePerUnit OIPricePerUnit,dbo.ToPersianDate(BI.BIDate) BIDateDr 
,ISNULL(BI.BIQuantity,0)*ISNULL(BI.BIPricePerUnit,0) Total
FROM BookingItems BI
LEFT JOIN CategoryItems CI ON CI.CIID = BI.CIID
WHERE BOOKINGID =0
").ToList();
                //string FromDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS FromDateDr", FromDate).Single();
               // string ToDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS ToDateDr", ToDate).Single();
                OERV.SizeToReportContent = true;
                OERV.LocalReport.ReportPath = Server.MapPath("OtherExpensesReport.rdlc");
                //ReportParameter[] paras = new ReportParameter[2];
                //paras[0] = new ReportParameter("FromDateDr", FromDateDr);
                //paras[1] = new ReportParameter("ToDateDr", ToDateDr);
                //IRRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("OtherExpensesDS", data);
                OERV.LocalReport.DataSources.Add(ds);
                OERV.LocalReport.Refresh();

            }
        }
    }
}