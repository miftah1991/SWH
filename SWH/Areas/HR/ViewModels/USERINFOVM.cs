using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class USERINFOVM
    {
        public int USERID { get; set; }
        public string Name { get; set; }
    }
    public class USERINFODVM
    {
        public int USERID {get;set;}
        public int? PositionID {get;set;}
        public string Name {get;set;}
        public string FName {get;set;}
        public string Mobile {get;set;}
        public string Email {get;set;}
        public decimal? GorssSalary {get;set;}
        public DateTime? DOB { get; set; }
        public string DOBDr { get; set; }
        public DateTime? JoinDate { get; set; }
        public string JoinDateDr { get; set; }
        public DateTime? ResignDate { get; set; }
        public string ResignDateDr { get; set; }
        public string Gender {get;set;}
        public string CAddress {get;set;}
        public string PAddress {get;set;}
        public int? Status {get;set;}
        public int? QualificationID {get;set;}
        public string Specilization {get;set;}
        public string University {get;set;}
        public int? Experiance {get;set;}
        public string PositionName {get;set;}
        public string QualificationName {get;set;}
        public string Remarks { get; set; }
        public string ResignReason { get; set; }
    }
}