using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class BookingPaymentVM
    {
        public int BPID { get; set; }
        public int CRID { get; set; }
        public int BOOKINGID { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? PaymentType { get; set; }
        public DateTime? BPDate { get; set; }
        public string BPDateDr { get; set; }
        public string Remarks { get; set; }
        public string CRName { get; set; }
        public decimal? AmountAf { get; set; }
    }
}