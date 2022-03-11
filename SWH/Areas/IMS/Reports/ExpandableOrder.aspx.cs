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
    public partial class ExpandableOrder : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string BDate = Request.QueryString["BDate"];
                var data = db.Database.SqlQuery<ExpandableOrderCustomerVM>(@"
SELECT CI.CIName,SUM(OI.OIQuantity) OIQuantity,CI.CIUnit,dbo.ToPersianDate(B.Date) DateDr 
,OI.OIPricePerUnit,C.CName,C.SellerName
FROM BookingItems BI
LEFT JOIN Booking B ON B.BOOKINGID = BI.BOOKINGID
LEFT JOIN BookingHall BH ON BH.BHID = B.BHID
LEFT JOIN ORDERS O ON O.BOOKINGID = BI.BOOKINGID
LEFT JOIN OrderItems OI ON OI.CIID = BI.CIID AND O.OID = OI.OID
LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID
LEFT JOIN Category C ON C.CID = CI.CID
WHERE Date = {0} AND --C.CID =8 AND
 IsExpandable =1
 GROUP BY CI.CIName,CI.CIUnit,B.Date,OI.OIPricePerUnit,C.CName,C.SellerName
", BDate).ToList();
//                string BookedGuest = db.Database.SqlQuery<string>(@"SELECT CAST(SUM(BookedGuest) as nvarchar(MAX)) FROM Booking WHERE Date = {0}", BDate).Single();
//                string BHID = db.Database.SqlQuery<string>(@"SELECT CAST(COUNT(BHID) as nvarchar(MAX))BookedGuest FROM Booking WHERE Date = {0}", BDate).Single();
//                string BookingHalls = "";
//                var halldata = db.Database.SqlQuery<BookingHallNameVM>(@"
//                        SELECT CONCAT(BH.Name,' ',N'تعداد ',B.BookedGuest,' ',M.Name) Halls FROM Booking B
//                        LEFT JOIN BookingHall BH ON BH.BHID = B.BHID
//                        LEFT JOIN Menu M ON M.MID = B.MID
//                        WHERE Date = {0}", BDate).ToList();
//                foreach (var dss in halldata)
//                {
//                    BookingHalls += "," + dss.Halls;
//                }
                EORV.SizeToReportContent = true;
                EORV.LocalReport.ReportPath = Server.MapPath("ExpandableOrder.rdlc");
                //ReportParameter[] paras = new ReportParameter[3];
                //paras[0] = new ReportParameter("BookedGuest", BookedGuest);
                //paras[1] = new ReportParameter("BHID", BHID);
                //paras[2] = new ReportParameter("BookingHalls", BookingHalls);
                //EORV.LocalReport.SetParameters(paras);


                ReportDataSource ds = new ReportDataSource("Expandable", data);
                EORV.LocalReport.DataSources.Add(ds);
                EORV.LocalReport.Refresh();

            }
        }
    }
}