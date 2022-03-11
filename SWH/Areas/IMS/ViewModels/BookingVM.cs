using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class BookingVM
    {
        public DateTime Date { get; set; }
        public int PDay { get; set; }
        public string DayName { get; set; }
        public string T1International { get; set; }
        public string T1Almas { get; set; }
        public string T1Capital { get; set; }
        public string T2International { get; set; }
        public string T2Almas { get; set; }
        public string T2Capital { get; set; }
    }
    public class BookingDetailVM
    {
        public int? BOOKINGID { get; set; }
        public int? MID { get; set; }
        public int? BHID { get; set; }
        public int? BSID { get; set; }
        public int? BTID { get; set; }
        public DateTime? Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string MenuName { get; set; }
        public decimal? MenuPrice { get; set; }
        public int? BookedGuest { get; set; }
        public string CustomerEmail { get; set; }
        public int BookingNumber { get; set; }
        public string BookingRemarks { get; set; }
        public decimal? PrePaidAmount { get; set; }
        public decimal PrePaidExchangeRate { get; set; }
        public int PrePaidCurrencyID { get; set; }
        public string CRName { get; set; }
        public string DateDr { get; set; }
        public string BookingDateDr { get; set; }
        public string Timing { get; set; }
        public string BHName { get; set; }
        public string ShiftName { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int? IsCleared { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
        public decimal? GuestPrice { get; set; }
        public int? ActualGuest { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? FinalPayment { get; set; }
        public string BTName { get; set; }
        public decimal? Tax { get; set; }

    }
    public class BookingReciptVM
    {
        public int BOOKINGID { get; set;}
        public string CustomerName { get; set;}
        public int? BookedGuest { get; set;}
        public int? ActualGuest { get; set; }
        public decimal? MenuPrice { get; set;}
        public decimal? ServiceCharge { get; set;}
        public decimal? ServiceChargeAmount { get; set;}
        public decimal? TotalWithServiceCharge { get; set;}
        public decimal? PaymentAmount { get; set;}
        public decimal? Balance { get; set;}
        public string DateDr { get; set; }
        public decimal? Tax { get; set; }

    }
    public class BookingWarningVM
    {
        public string BookingDateDr	{get;set;}
        public string MenuName{get;set;}
        public string DateDr{get;set;}
        public string ShiftName{get;set;}
        public string BTName{get;set;}
        public string BHName{get;set;}
        public string CustomerName{get;set;}
        public string CustomerMobile{get;set;}
        public int BookedGuest{get;set;}
        public string Prepaid{ get; set; }

    }
}