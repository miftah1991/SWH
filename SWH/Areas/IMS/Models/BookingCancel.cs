using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("BookingCancel")]
    
    public class BookingCancel
    {
        [Key]
        public int BCID { get; set; }
        public int BOOKINGID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public decimal CancelAmount { get; set; }
        public int CancelAmountCID { get; set; }
        public decimal CancelAmountExchange { get; set; }
        public string Remarks { get; set; }
    }
}