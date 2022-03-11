using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("MenuItemsSetting")]
    public class MenuItemsSetting
    {
        [Key]
        public int MISID { get; set; }
        public int MID { get; set; }
        public int CIID { get; set; }
        public int Person { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
    }
}