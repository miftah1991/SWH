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
    public partial class ProductionRecipt : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int BOOKINGID = Convert.ToInt32(Request.QueryString["BOOKINGID"]);
                var data = db.Database.SqlQuery<ProductionReportVM>(@"exec getProductionDetail {0}", BOOKINGID).ToList();
                PRRV.SizeToReportContent = true;
                PRRV.LocalReport.ReportPath = Server.MapPath("ProductionRecipt.rdlc");



                int PID = db.Database.SqlQuery<int>("SELECT PID FROM Production WHERE BOOKINGID ={0}", BOOKINGID).Single();
                var dataClearance = db.Database.SqlQuery<ProductionDetailVM>("exec spGetProductionClearance {0}", PID);
                //string ServiceChargeAmount = dataClearance.Select(a => a.ServiceChargeAmount).Single().ToString();
                string ProductionTotal = dataClearance.Select(a => a.TotalGI).Single().ToString();
                string CRName = dataClearance.Select(a => a.CRName).Single().ToString();
                string PPrePaidAmount = dataClearance.Select(a => a.PPrePaidAmount).Single().ToString();
                string PPrePaidExchangeRate = dataClearance.Select(a => a.PPrePaidExchangeRate).Single().ToString();
                string PrePaid = dataClearance.Select(a => a.PrePaid).Single().ToString();
                string Balance = dataClearance.Select(a => a.Balance).Single().ToString();
                string PostPaid = dataClearance.Select(a => a.PostPaid).Single().ToString();
                if (PostPaid == "") { PostPaid = "0"; }
                ReportParameter[] paras = new ReportParameter[7];
                //paras[0] = new ReportParameter("ServiceChargeAmount", ServiceChargeAmount);
                paras[0] = new ReportParameter("PostPaid", PostPaid);
                paras[1] = new ReportParameter("ProductionTotal", ProductionTotal);
                paras[2] = new ReportParameter("CRName", CRName);
                paras[3] = new ReportParameter("PPrePaidAmount", PPrePaidAmount);
                paras[4] = new ReportParameter("PPrePaidExchangeRate", PPrePaidExchangeRate);
                paras[5] = new ReportParameter("PrePaid", PrePaid);
                paras[6] = new ReportParameter("Balance", Balance);

                PRRV.LocalReport.SetParameters(paras);
                ReportDataSource ds = new ReportDataSource("ProductionDS", data);
                PRRV.LocalReport.DataSources.Add(ds);
                PRRV.LocalReport.Refresh();

            }
        }
    }
}