using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class EmpAttVM
    {
        public DateTime? SignDate {get;set;}
        public string Timein {get;set;}
        public string TimeOut { get; set; }
        public string SignDateDr {get;set;}
        public int USERID {get;set;}
        public string AttStatus {get;set;}
        public Int64? CheckinoutID { get; set; }
        public int? AbsentMin {get;set;}
    }
}