using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class MonthlyGulsazeReportVM
    {
        public string Date { get; set; }
        public int? Counts { get; set; }
        public decimal? Kek { get; set; }
        public decimal? Gul { get; set; }
        public decimal? Motar { get; set; }
        public decimal? Light { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Dool { get; set; }
        public decimal? Other { get; set; }
        public decimal? Total { get; set; }
        public decimal? Income { get; set; }
        public decimal? NetIcome { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
    }
}