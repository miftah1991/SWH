using SWH.Areas.IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class GulsazeItemsVM
    {
        public int GIID { get; set; }
        public int GID { get; set; }
        public int GMID { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
        public string Remarks { get; set; }
        public string GMName { get; set; }
        public string GMUnit { get; set; }
        public decimal? GMUnitPrice { get; set; }
    }
    public class GulsazeVM
    {
        public int GID { get; set; }
        public int BOOKINGID { get; set; }
        public decimal? GPrePaidAmount { get; set; }
        public decimal? GPrePaidExchangeRate { get; set; }
        public int? GPrePaidCurrencyID { get; set; }
        public decimal? GFinalAmount { get; set; }
        public decimal? GFinalExchangeRate { get; set; }
        public int? GFinalCurrencyID { get; set; }
        public string Remarks { get; set; }
        public int? ServiceCharge { get; set; }
        public string CRName { get; set; }
        public decimal? GDiscount { get; set; }
    }
    public class GulsazeDetailVM
    {
        public decimal? TotalGI { get; set; }
        public int? ServiceCharge { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
        public decimal? GulsazeTotal { get; set; }
        public decimal? PrePaid { get; set; }
        public decimal? Balance { get; set; }
        public string CRName { get; set; }
        public decimal? GPrePaidAmount { get; set; }
        public decimal? GPrePaidExchangeRate { get; set; }
        public decimal? GFinalAmount { get; set; }
        public decimal? GFinalExchangeRate { get; set; }
        public int? GFinalCurrencyID { get; set; }
        public string FCRName { get; set; }
        public int? IsCleared { get; set; }
        public decimal? GDiscount { get; set; }
    }
    public class GulsazeReportVM
    {
       public int? GID {get;set;}
       public int? BOOKINGID {get;set;}
       public decimal? GPrePaidAmount {get;set;}
       public decimal? GPrePaidExchangeRate {get;set;}
       public int? GPrePaidCurrencyID {get;set;}
       public decimal? GFinalAmount {get;set;}
       public decimal? GFinalExchangeRate {get;set;}
       public int? GFinalCurrencyID {get;set;}
       public string GRemarks {get;set;}
       public int? ServiceCharge {get;set;}
       public int? GIID {get;set;}
       public decimal? PricePerUnit {get;set;}
       public decimal? Quantity {get;set;}
       public decimal? Total { get; set; }
       public int? GMID {get;set;}
       public string GMName {get;set;}
       public string GMUnit {get;set;}
       public decimal? GMUnitPrice {get;set;}
       public string CRName {get;set;}
       public int? BHID {get;set;}
       public int? BSID {get;set;}
       public DateTime? Date {get;set;}
       public string CustomerName {get;set;}
       public string CustomerMobile {get;set;}
       public string CustomerEmail {get;set;}
       public int? BookingNumber {get;set;}
       public string BHName {get;set;}
       public string ShiftName {get;set;}
       public string DateDr { get; set; }
       public decimal? GDiscount { get; set; }
       public string GIRemarks { get; set; }

    }
    public class GulsazeExpensesVM
    {
        public int GEID { get; set; }
        public int GEMID { get; set; }
        public decimal? GEPricePerUnit { get; set; }
        public decimal GEQuantity { get; set; }
        public DateTime GEDate { get; set; }
        public string GERemarks { get; set; }
        public string GEMName { get; set; }
        public string Unit { get; set; }
        public string Remarks { get; set; }
        public string GEDateDr { get; set; }
        public decimal? Total { get; set; }
    }
    public class GulsazeClearanceVM
    {
        public int BOOKINGID { get; set; }
        public DateTime? Date { get; set; }
        public decimal? PrePaid { get; set; }
        public decimal? FinalPayment { get; set; }
        public decimal? Total { get; set; }
        public string DateDr { get; set; }
        public string BHName { get; set; }
        public string ShiftName { get; set; }
    }
    public class GulsazeClearance1VM
    {
        public DateTime? Date { get; set; }
        public string GEMName { get; set; }
        public decimal? Total { get; set; }
    }
}