using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("zShift")]
    
    public class Shift
    {
        
        [Key]
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string Remarks { get; set; }
    }
}