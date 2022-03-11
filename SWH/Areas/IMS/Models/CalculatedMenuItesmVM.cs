using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    public class CalculatedMenuItesmVM
    {

        public int CIID { get; set; }
        public string CIName { get; set; }
        public string CIUnit { get; set; }
        public decimal? SettingCount { get; set; }
        public string Remarks { get; set; }
        public string CICode { get; set; }
        public decimal? CostPerUnit { get; set; }
        public int? BIID { get; set; }
    }
    public class CalculateNonExMenuItemVM
    {
        public int CIID { get; set; }
        public int OIID { get; set; }
        public int? BIID { get; set; }
        public decimal? OIQuantity { get; set; }
        public decimal? OIPricePerUnit { get; set; }
        public string CIName { get; set; }
        public string CIUnit { get; set; }
        public decimal? SettingCount { get; set; }
    }
    public class ExistStockVM
    {
        public int CIID {get; set;}
        public string CIName {get; set;}
        public string CIUnit {get; set;}
        public decimal? OIQuantity {get; set;}
        public decimal? TotalPrice {get; set;}
    }
}