using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("BookingPayment")]
    
    public class BookingPayment
    {
        [Key]
        public int BPID { get; set; }
        public int CRID { get; set; }
        public int BOOKINGID { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRate { get; set; }
        public int PaymentType { get; set; }
        public DateTime? BPDate { get; set; }
        public string Remarks { get; set; }
        public Booking Booking { get; set; }
        public Currency Currency { get; set; }
    }
}