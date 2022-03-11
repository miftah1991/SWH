using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("GulsaziMenu")]
    public class GulsaziMenu
    {
        [Key]
        public int GMID { get; set; }
        public string GMName { get; set; }
        public string GMUnit { get; set; }
        public decimal? GMUnitPrice { get; set; }
    }
}