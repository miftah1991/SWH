using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class CancelBookingVM
    {
        public int BCID {get;set;}
        public int BOOKINGID {get;set;}
        public DateTime? CancelDate {get;set;}
        public string CancelReason {get;set;}
        public decimal? CancelAmount {get;set;}
        public int? CancelAmountCID {get;set;}
        public decimal? CancelAmountExchange {get;set;}
        public string Remarks {get;set;}
        public int? MID {get;set;}
        public DateTime Date {get;set;}
        public string CustomerName {get;set;}
        public string CustomerMobile {get;set;}
        public string CustomerEmail {get;set;}
        public decimal? MenuPrice {get;set;}
        public decimal? PrePaidAmount {get;set;}
        public decimal? PrePaidExchangeRate {get;set;}
        public int? PrePaidCurrencyID {get;set;}
        public int? BookedGuest {get;set;}
        public int? ActualGuest {get;set;}
        public int? BookingNumber {get;set;}
        public int? IsCanceled {get;set;}
        public string BookingRemarks {get;set;}
        public string CustSign {get;set;}
        public DateTime BookingDate {get;set;}
        public int? IsCleared {get;set;}
        public int? BHID {get;set;}
        public string BHName {get;set;}
        public string ShiftName {get;set;}
        public string MenuName {get;set;}
        public decimal? MenuUnitPrice {get;set;}
        public int? CRID {get;set;}
        public string CRName {get;set;}
        public string DateDr { get; set; }
        public string CancelDateDr { get; set; }

    }
}