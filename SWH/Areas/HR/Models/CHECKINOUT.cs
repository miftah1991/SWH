using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("CHECKINOUT")]
    public class CHECKINOUT
    {
        [Key]
        public Int64 CheckinoutID { get; set; }
        public int USERID { get; set; }
        public DateTime CHECKTIME { get; set; }
        public DateTime CHECKOUTTIME { get; set; }
        public int WorkHourID { get; set; }
    }
}