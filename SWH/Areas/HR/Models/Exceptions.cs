using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("Exceptions")]
    
    public class Exceptions
    {
        [Key]
        public int ExceptionID { get; set; }
        public int USERID { get; set; }
        public DateTime? ExceptionDate { get; set; }
        public DateTime? ETimeIN { get; set; }
        public DateTime ETimeOut { get; set; }
        public int? AbsentMin { get; set; }
        public string Justification { get; set; }
        public Int64? CheckinoutID { get; set; }

    }
}