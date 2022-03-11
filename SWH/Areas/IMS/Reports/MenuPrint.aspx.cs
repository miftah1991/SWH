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
    public partial class MenuPrint : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                int MID = Convert.ToInt32(Request.QueryString["MID"]);

                var data = db.Database.SqlQuery<MenuItemsSettingVM>(@"
SELECT MIS.*,M.Name MenuName,CI.CIName FROM MenuItemsSetting MIS
LEFT JOIN Menu M ON M.MID = MIS.MID
LEFT JOIN CategoryItems CI ON CI.CIID = MIS.CIID WHERE M.MID = {0}
",MID).ToList();
                //string FromDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS FromDateDr", FromDate).Single();
                //string ToDateDr = db.Database.SqlQuery<string>("SELECT dbo.ToPersianDate({0}) AS ToDateDr", ToDate).Single();
                MPRV.SizeToReportContent = true;
                MPRV.LocalReport.ReportPath = Server.MapPath("MenuPrint.rdlc");
                //ReportParameter[] paras = new ReportParameter[2];
                //paras[0] = new ReportParameter("FromDateDr", FromDateDr);
                //paras[1] = new ReportParameter("ToDateDr", ToDateDr);
                //IORRV.LocalReport.SetParameters(paras);

                ReportDataSource ds = new ReportDataSource("MenuPrintDS", data);
                MPRV.LocalReport.DataSources.Add(ds);
                MPRV.LocalReport.Refresh();

            }
        }
    }
}