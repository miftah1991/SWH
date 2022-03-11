using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class StockReportVM
    {
       public DateTime? ODate { get; set;}
       public string ODateDr { get; set;}
       public string CIName { get; set; }
       public string CIUnit { get; set; }
       public decimal? OIQuantity { get; set;}
       public decimal? OIPricePerUnit { get; set;}
       public decimal? OIDiscount { get; set;}
       public decimal? Total { get; set;}

    }
    public class StockOutReportVM
    {
        public DateTime? BIDate { get; set; }
        public string BIDateDr { get; set; }
        public string CIName { get; set; }
        public string CIUnit { get; set; }
        public decimal? BIQuantity { get; set; }
        public decimal? OIPricePerUnit { get; set; }
        public decimal? OIDiscount { get; set; }
        public decimal? Total { get; set; }

    }
}