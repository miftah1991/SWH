using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class PayrollDetailVM
    {
        public string Name {get;set;}
        public string FName {get;set;}
        public string PositionName {get;set;}
        public int USERID {get;set;}
        public int AttAbsent {get;set;}
        public decimal? Amount {get;set;}
        public decimal? DeductAmount {get;set;}
        public decimal? PrePaidDeductd {get;set;}
        public int? Days {get;set;}
        public int? Present {get;set;}
        public int? Absent {get;set;}
        public int? Leave {get;set;}
        public decimal? Salary {get;set;}
        public decimal? SalaryPerDay {get;set;}
        public decimal? AbsentDeduction {get;set;}
        public decimal? LateDeduction {get;set;}
        public decimal? Per75 {get;set;}
        public decimal? Per25 {get;set;}
        public decimal? Per2Tax {get;set;}
        public decimal? PreBalance {get;set;}
        public decimal? Balance {get;set;}

    }
}