using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class OrdersVM
    {
        public int OID { get; set; }
        public int OCode { get; set; }
        public DateTime? ODate { get; set; }
        public string ORemarks { get; set; }
        public decimal? OTotal { get; set; }
        public int? OrderItems { get; set; }
        public string ODateDr { get; set; }
    }
    public class OrderItemsVM
    {
        public int OID { get; set; }
        public int OCode { get; set; }
        public DateTime? ODate { get; set; }
        public string ORemarks { get; set; }
        public string CIName { get; set; }
        public string CIUnit { get; set; }
        public int? OIID { get; set; }
        public int? CIID { get; set; }
        public decimal? OIQuantity { get; set; }
        public decimal? OIPricePerUnit { get; set; }
        public decimal? OIDiscount { get; set; }
        public int? OIStatus { get; set; }
        public string OIRemarks { get; set; }
        public decimal? OITotal { get; set; }
    }
}