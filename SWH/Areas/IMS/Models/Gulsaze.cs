using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Gulsaze")]
    
    public class Gulsaze
    {
        [Key]
        public int GID { get; set; }
        public int BOOKINGID { get; set; }
        public decimal? GPrePaidAmount { get; set; }
        public decimal? GPrePaidExchangeRate { get; set; }
        public int? GPrePaidCurrencyID { get; set; }
        public decimal? GFinalAmount { get; set; }
        public decimal? GFinalExchangeRate { get; set; }
        public int? GFinalCurrencyID { get; set; }

        public string Remarks { get; set; }
        public int ServiceCharge { get; set; }
        public int? IsCleared { get; set; }
        public decimal? GDiscount { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
        public ICollection<GulsazeItem> GulsazeItem { get; set; }
    }
}