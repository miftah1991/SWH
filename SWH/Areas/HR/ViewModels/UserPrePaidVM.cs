using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class UserPrePaidVM
    {

       public int USERID	{get;set;}
       public decimal Amount{get;set;}
       public decimal? PrePaidDeductd{get;set;}
       public string Name{get;set;}
       public string PositionName{get;set;}
       public string FName{get;set;}
       public decimal? Balance { get; set; }
       public decimal? DeductedAmount { get; set; }
       public string DateDr { get; set; }
       public DateTime? Date { get; set; }
       public int? PrepaidID { get; set; }
    }
}