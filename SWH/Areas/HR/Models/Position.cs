using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string PositionCode { get; set; }
        public string Remarks { get; set; }
    }
}