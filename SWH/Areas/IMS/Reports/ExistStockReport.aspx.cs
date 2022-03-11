using Microsoft.Reporting.WebForms;
using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SWH.Areas.IMS.Reports
{
    public partial class ExistStockReport : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string Query =
@"SELECT CI.CIID,CIName,CIUnit,SUM(OIQuantity) OIQuantity,SUM(OIQuantity*OIPricePerUnit) TotalPrice FROM VWOrderItems OI
LEFT JOIN CategoryItems CI ON CI.CIID = OI.CIID  
LEFT JOIN CategoryClass CC ON CC.CCID = CI.CCID 
WHERE CC.CCID=1
GROUP BY CI.CIID,CIName,CIUnit
ORDER BY CIName ";

                var data = db.Database.SqlQuery<ExistStockVM>(Query).ToList();

                ESRV.SizeToReportContent = true;
                ESRV.LocalReport.ReportPath = Server.MapPath("ExistStockReport.rdlc");
                //ReportParameter[] paras = new ReportParameter[2];
                //paras[0] = new ReportParameter("FromDateDr", FromDateDr);
                //paras[1] = new ReportParameter("ToDateDr", ToDateDr);
                //ESRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("ExistStockDS", data);
                ESRV.LocalReport.DataSources.Add(ds);
                ESRV.LocalReport.Refresh();

            }
        }
    }
}