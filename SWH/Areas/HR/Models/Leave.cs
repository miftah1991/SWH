using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("Leave")]
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }
        public int USERID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Days { get; set; }
        public int BackupUser { get; set; }
        public string Justification { get; set; }
        public int LeaveTypeID { get; set; }
        public string Remarks { get; set; }
    }
}