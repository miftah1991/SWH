using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Booking")]
    
    public class Booking
    {
        [Key]
        public int BOOKINGID { get; set; }
        public int MID { get; set; }
        public int BHID { get; set; }
        public int? BSID { get; set; }
        public int BTID { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public decimal MenuPrice { get; set; }
        public decimal PrePaidAmount { get; set; }
        public decimal PrePaidExchangeRate { get; set; }
        public int PrePaidCurrencyID { get; set; }
        public int? BookedGuest { get; set; }
        public int? ActualGuest { get; set; }
        public int BookingNumber { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? FinalExchangeRate { get; set; }
        public int? FinalCurrencyID { get; set; }
        public int? IsCanceled { get; set; }
        public string BookingRemarks { get; set; }
        public string CustSign { get; set; }
        public int? IsCleared { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
        public decimal? GuestPrice { get; set; }
        public int? IsClearedG { get; set; }
        public decimal? Tax { get; set; }
        public int? IsCaredPrt { get; set; }
        public string AddBHID { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingHall BookingHall { get; set; }
        public Menu Menu { get; set; }
        public BookingType BookingType { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<Gulsaze> Gulsaze { get; set; }
        public ICollection<Production> Production { get; set; }
        public ICollection<BookingItems> BookingItems { get; set; }
        public ICollection<BookingPayment> BookingPayment { get; set; }
    }
}