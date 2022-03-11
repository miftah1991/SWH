using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class BookingItemsVM
    {

       public int? BIID {get;set;}
       public int? BOOKINGID {get;set;}
       public int? OIID {get;set;}
       public decimal? BIQuantity {get;set;}
       public int? IsExpandable {get;set;}
       public DateTime? BIDate {get;set;}
       public string CIName {get;set;}
       public string CIUnit {get;set;}
       public decimal? OIPricePerUnit {get;set;}
       public string BIDateDr { get; set; }
       public decimal Total { get; set; }
       public string OtherExpenseName { get; set; }

    }
}