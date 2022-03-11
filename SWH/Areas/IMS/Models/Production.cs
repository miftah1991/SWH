using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Production")]
    public class Production
    {
        [Key]
        public int PID { get; set; }
        public int? BOOKINGID { get; set; }
        public decimal PPrePaidAmount { get; set; }
        public decimal PPrePaidExchangeRate { get; set; }
        public int PPrePaidCurrencyID { get; set; }
        public decimal? PFinalAmount { get; set; }
        public decimal? PFinalExchangeRate { get; set; }
        public int? PFinalCurrencyID { get; set; }
        public string Remarks { get; set; }
        public int? IsCleared { get; set; }
        //public Booking Booking { get; set; }
        public ICollection<ProductionItem> ProductionItem { get; set; }
    }
}