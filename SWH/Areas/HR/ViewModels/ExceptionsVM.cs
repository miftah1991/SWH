using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.ViewModels
{
    public class ExceptionsVM
    {
        public int ExceptionID { get; set; }
        public int USERID { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string ExceptionDateDr { get; set; }
        public DateTime? ETimeIN { get; set; }
        public DateTime? ETimeOut { get; set; }
        public int? AbsentMin { get; set; }
        public string Justification { get; set; }
        public Int64? CheckinoutID { get; set; }
    }
    public class AttendanceVm
    {
        public int WorkHourID { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
}