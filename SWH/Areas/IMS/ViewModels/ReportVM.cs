using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class ReportVM
    {
        public string Date { get; set; }
    }
    public class ExpandableDailyVM
    {
        public string CIName { get; set; }
        public decimal? OIQuantity { get; set; }
        public string CIUnit { get; set; }
        public string DateDr { get; set; }
        public decimal? OIPricePerUnit { get; set; }
    }
    public class BookingHallNameVM
    {
        public string Halls { get; set; }
        public string Remarks { get; set; }
    }
    public class ExpandableOrderCustomerVM
    {
        public string CIName { get; set; }
        public decimal? OIQuantity { get; set; }
        public string CIUnit { get; set; }
        public string DateDr { get; set; }
        public decimal OIPricePerUnit { get; set; }
        public string CName { get; set; }
        public string SellerName { get; set; }
    }
}