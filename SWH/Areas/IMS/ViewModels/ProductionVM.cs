using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class ProductionVM
    {
        public int PID { get; set; }
        public int? BOOKINGID { get; set; }
        public decimal PPrePaidAmount { get; set; }
        public decimal PPrePaidExchangeRate { get; set; }
        public int PPrePaidCurrencyID { get; set; }
        public decimal? PFinalAmount { get; set; }
        public decimal? PFinalExchangeRate { get; set; }
        public int? PFinalCurrencyID { get; set; }
        public string Remarks { get; set; }
        public string CRName { get; set; }
    }
    public class ProductionItemsVM
    {
        public int PIID { get; set; }
        public int PID { get; set; }
        public int PMID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? Total { get; set; }
        public string Remarks { get; set; }
        public string PMName { get; set; }
        public string PMUnit { get; set; }
    }
    public class ProductionDetailVM
    {
        public decimal? TotalGI { get; set; }
        public decimal? PrePaid { get; set; }
        public decimal? Balance { get; set; }
        public string CRName { get; set; }
        public decimal? PPrePaidAmount { get; set; }
        public decimal? PPrePaidExchangeRate { get; set; }
        public decimal? PFinalAmount { get; set; }
        public decimal? PFinalExchangeRate { get; set; }
        public int? PFinalCurrencyID { get; set; }
        public string FCRName { get; set; }
        public int? IsCleared { get; set; }
        public decimal? ProductionTotal { get; set; }
        public int PID { get; set; }
        public decimal? PostPaid { get; set; }

    }
    public class ProductionReportVM
    {
    public int? PID {get; set;}
    public int? BOOKINGID {get; set;}
    public decimal? PPrePaidAmount {get; set;}
    public decimal? PPrePaidExchangeRate {get; set;}
    public int? PPrePaidCurrencyID {get; set;}
    public decimal? PFinalAmount {get; set;}
    public decimal? PFinalExchangeRate {get; set;}
    public int? PFinalCurrencyID {get; set;}
    public int? IsCleared {get; set;}
    public int? PMID {get; set;}
    public string PMName {get; set;}
    public string PMUnit {get; set;}
    public decimal? PMUnitPrice {get; set;}
    public DateTime Date {get; set;}
    public string CustomerName {get; set;}
    public string CustomerMobile {get; set;}
    public string CustomerEmail {get; set;}
    public string ShiftName {get; set;}
    public string CRName {get; set;}
    public decimal? Quantity {get; set;}
    public decimal? PricePerUnit {get; set;}
    public string BHName {get; set;}
    public string DateDr {get; set;}
    public decimal? Total {get; set;}
    public string PRemarks {get; set;}

    }
}