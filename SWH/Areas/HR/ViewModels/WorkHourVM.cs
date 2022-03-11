using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class WorkHourVM
    {
      public int WorkHourID {get;set;}
      public int? ShiftID {get;set;}
      public DateTime? StartDate {get;set;}
      public DateTime? EndDate {get;set;}
      public string TIMEIN {get;set;}
      public string TIMOUT { get; set; }
      public int? LateIn {get;set;}
      public int? EarlyOut {get;set;}
      public string Remarks {get;set;}
      public string ShiftName { get; set; }
      public string StartDateDr { get; set; }
      public string EndDateDr { get; set; }


    }
}