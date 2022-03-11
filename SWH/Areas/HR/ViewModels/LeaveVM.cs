using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class LeaveVM
    {
        public int LeaveID {get;set;}
        public int USERID {get;set;}
        public DateTime FromDate { get; set; }
        public string FromDateDr { get; set; }
        public DateTime ToDate { get; set; }
        public string ToDateDr { get; set; }
        public int? Days {get;set;}
        public int? BackupUser {get;set;}
        public string Justification {get;set;}
        public int? LeaveTypeID {get;set;}
        public string Remarks {get;set;}
        public string LeaveTypeName {get;set;}
        public string Abbreviation {get;set;}
        public string PositionName {get;set;}
        public string Name {get;set;}
        public string FName {get;set;}
        public string BackupEmployee {get;set;}


    }
    public class YearVM
    {
        public int YearID { get; set; }
        public int YearVal { get; set; }
    }
}