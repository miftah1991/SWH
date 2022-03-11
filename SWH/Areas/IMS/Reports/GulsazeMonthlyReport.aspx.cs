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
    public partial class GulsazeMonthlyReport : System.Web.UI.Page
    {
        private IMSDbContext db = new IMSDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string  MonthYear = Request.QueryString["MonthYear"];
                string[] month = MonthYear.Split('-');
                string m = month[0];
                string y = month[1];
                var data = db.Database.SqlQuery<MonthlyGulsazeReportVM>(@"exec spGetGulsazeMonthlyReport {0},{1}",m,y).ToList();
                GMRRV.SizeToReportContent = true;
                GMRRV.LocalReport.ReportPath = Server.MapPath("GulsazeMonthlyReport.rdlc");



                //int GID = db.Database.SqlQuery<int>("SELECT GID FROM Gulsaze WHERE BOOKINGID ={0}", BOOKINGID).Single();
                //var dataClearance = db.Database.SqlQuery<GulsazeDetailVM>("exec spGetGulsazeClearance {0}", GID);
                //string ServiceChargeAmount = dataClearance.Select(a => a.ServiceChargeAmount).Single().ToString();
                //string GulsazeTotal = dataClearance.Select(a => a.GulsazeTotal).Single().ToString();
                //string CRName = dataClearance.Select(a => a.CRName).Single().ToString();
                //string GPrePaidAmount = dataClearance.Select(a => a.GPrePaidAmount).Single().ToString();
                //string GPrePaidExchangeRate = dataClearance.Select(a => a.GPrePaidExchangeRate).Single().ToString();
                //string PrePaid = dataClearance.Select(a => a.PrePaid).Single().ToString();
                //string Balance = dataClearance.Select(a => a.Balance).Single().ToString();

                //ReportParameter[] paras = new ReportParameter[7];
                //paras[0] = new ReportParameter("ServiceChargeAmount", ServiceChargeAmount);
                //paras[1] = new ReportParameter("GulsazeTotal", GulsazeTotal);

                //paras[2] = new ReportParameter("CRName", CRName);
                //paras[3] = new ReportParameter("GPrePaidAmount", GPrePaidAmount);
                //paras[4] = new ReportParameter("GPrePaidExchangeRate", GPrePaidExchangeRate);

                //paras[5] = new ReportParameter("PrePaid", PrePaid);
                //paras[6] = new ReportParameter("Balance", Balance);

               // GMRRV.LocalReport.SetParameters(paras);
                ReportDataSource ds = new ReportDataSource("GulsazeDS", data);
                GMRRV.LocalReport.DataSources.Add(ds);
                GMRRV.LocalReport.Refresh();

            }
        }
    }
}