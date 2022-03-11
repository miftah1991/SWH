using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("WorkHour")]
    public class WorkHour
    {
        [Key]
        public int WorkHourID { get; set; }
        public int ShiftID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan TIMEIN { get; set; }
        public TimeSpan TIMOUT { get; set; }
        public int LateIn { get; set; }
        public int EarlyOut { get; set; }
        public string Remarks { get; set; }
    }
}